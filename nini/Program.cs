using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using nini.core.Helpers;
using NLog.Web;

namespace nini
{
    public class Program
    {
        private const int DefaultPortNumber = 7500;
        public static void Main(string[] args)
        {
            //Setup the logger first
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Info("Main function starts");
                var host = CreateWebHostBuilder(args);
                var isService = !Debugger.IsAttached;

                if (isService)
                {
                    logger.Info("Host running as a Windows service.");
                    host.RunAsService();
                }
                else
                {
                    logger.Info("Host running in normal mode.");
                    host.Run();
                }
            }
            catch (Exception e)
            {
                logger.Error(e, "Stopped program due to unknown exception");
            }
            finally
            {
                //Ensure to shut down the logger.
                NLog.LogManager.Shutdown();
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
                .ConfigureServices(services => services.AddAutofac())
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, port, x =>
                    {
                        x.UseHttps(certificateName, pwd);
                    });
                })
                .UseUrls(niniUrls)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog()  // NLog: setup NLog for Dependency injection
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
