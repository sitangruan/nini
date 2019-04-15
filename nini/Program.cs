using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using nini.core.Helpers;

namespace nini
{
    public class Program
    {
        private const int DefaultPortNumber = 7500;
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args);

            var isService = !Debugger.IsAttached;

            if (isService)
            {
                host.RunAsService();
            }
            else
            {
                host.Run();
            }
        }

        public static IWebHost CreateWebHostBuilder(string[] args)
        {
            (int port, string url) cfg = GetNiniUrlFromConfig();
            var port = cfg.port;
            var niniUrls = Debugger.IsAttached ? string.Empty : cfg.url;

            CertificateHelper.CreateCertificate(out string certificateName, out string pwd);

            var host = new WebHostBuilder()
                .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, port, x =>
                    {
                        x.UseHttps(certificateName, pwd);
                    });
                })
                .UseUrls(niniUrls)
                .UseStartup<Startup>()
                .Build();

            return host;
        }

        private static (int port, string niniUrl) GetNiniUrlFromConfig()
        {
            if (Debugger.IsAttached)
            {
                return (44315, "http://0.0.0.0:44315"); ;
            }

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var portText = config.GetValue<string>("Port");
            bool isPortInt = int.TryParse(portText, out int port);
            port = isPortInt && (1000 <= port && port < 65536) ? port : DefaultPortNumber;

            string niniUrl = "http://0.0.0.0" + port;

            return (port, niniUrl);
        }
    }
}
