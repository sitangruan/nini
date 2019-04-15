using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;


namespace nini.core.Helpers
{
    public class CertificateHelper
    {
        //private static readonly ILogger Logger = LoggerFactory.Create(typeof(CertificateHelper));
        
        public static void CreateCertificate(out string certificateName, out string pwd)
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\nini";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            certificateName = directory + "\\cert.pfx";
            string passwordFile = directory + "\\cert.txt";

            if (File.Exists(passwordFile) && File.Exists(certificateName))
            {
                try
                {
                    // To test the certificate
                    pwd = File.ReadAllText(passwordFile);
                    var x5092 = new X509Certificate2(certificateName, pwd);
                    x5092.Dispose();
                    //Logger.LogInformation("Reusing current certificate");
                    return;
                }
                catch (Exception)
                {
                    //Logger.LogInformation("The current certificate is no longer resusable");
                }
            }

            //Create new certificate
            pwd = Guid.NewGuid().ToString();
            File.WriteAllText(passwordFile, pwd);
            var x509 = GenerateCertificate("nini", out var kp);
            SaveToFile(x509, kp, certificateName, "nini", pwd);
            //Logger.LogInformation("New certificate has been created");
        }

        public static X509Certificate GenerateCertificate(string subjectName, out AsymmetricCipherKeyPair subjectKeyPair)
        {
            // certificate strength 1024 bits
            int keyStrength = 1024;

            var randomGenerator = new CryptoApiRandomGenerator();
            var random = new SecureRandom(randomGenerator);
            subjectKeyPair = default(AsymmetricCipherKeyPair);
            var keyGenerationParameters = new KeyGenerationParameters(random, keyStrength);
            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            subjectKeyPair = keyPairGenerator.GenerateKeyPair();
            AsymmetricCipherKeyPair issuerKeyPair = subjectKeyPair;
            ISignatureFactory signatureFactory = new Asn1SignatureFactory("SHA512WITHRSA", issuerKeyPair.Private, random);

            var certificateGenerator = new X509V3CertificateGenerator();

            var certificateName = new X509Name("CN=" + subjectName);
            var serialNumver = BigInteger.ProbablePrime(120, new Random());

            certificateGenerator.SetSerialNumber(serialNumver);
            certificateGenerator.SetSubjectDN(certificateName);
            certificateGenerator.SetIssuerDN(certificateName);
            certificateGenerator.SetNotAfter(DateTime.Now.AddYears(100));
            certificateGenerator.SetNotBefore(DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0)));
            certificateGenerator.SetPublicKey(subjectKeyPair.Public);

            certificateGenerator.AddExtension(
                X509Extensions.AuthorityKeyIdentifier.Id,
                false,
                new AuthorityKeyIdentifier(
                    SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(subjectKeyPair.Public),
                    new GeneralNames(new GeneralName(certificateName)),
                    serialNumver));


            certificateGenerator.AddExtension(
                X509Extensions.ExtendedKeyUsage.Id,
                false,
                new ExtendedKeyUsage(new[] { KeyPurposeID.IdKPServerAuth }));

            var newCert = certificateGenerator.Generate(signatureFactory);
            return newCert;
        }

        public static void SaveToFile(
            X509Certificate newCert,
            AsymmetricCipherKeyPair kp,
            string filePath,
            string certAlias,
            string password)
        {
            var newStore = new Pkcs12Store();
            var certEntry = new X509CertificateEntry(newCert);
            newStore.SetCertificateEntry(certAlias, certEntry);
            newStore.SetKeyEntry(certAlias, new AsymmetricKeyEntry(kp.Private), new[] { certEntry });
            using (var certFile = File.Create(filePath))
            {
                newStore.Save(certFile, password.ToCharArray(), new SecureRandom(new VmpcRandomGenerator()));
            }
        }
    }
}