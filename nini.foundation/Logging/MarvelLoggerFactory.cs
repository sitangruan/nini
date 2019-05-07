using System;
using System.Collections.Generic;
using System.Text;
using NLog.Config;

namespace nini.foundation.Logging
{
    public static class MarvelLoggerFactory
    {
        public static IMarvelLogger Generate(Type type)
        {
            var nLogger = NLog.LogManager.GetLogger(type.FullName);
            var marvel = new MarvelLogger(nLogger);
            return marvel;
        }

        public static void AssignConfiguration(string path)
        {
            NLog.LogManager.Configuration = new XmlLoggingConfiguration(path);
        }
    }
}
