using System;
using System.Collections.Generic;
using System.Text;

namespace nini.foundation.Logging
{
    public class MarvelLogger: IMarvelLogger
    {
        private readonly NLog.Logger _logger;

        internal MarvelLogger(NLog.Logger logger)
        {
            _logger = logger;
        }

        public void LogTrace(string value)
        {
            if (_logger.IsTraceEnabled)
            {
                _logger.Trace(value);
            }
        }

        public void LogDebug(string value)
        {
            if (_logger.IsDebugEnabled)
            {
                _logger.Debug(value);
            }
        }

        public void LogInfo(string value)
        {
            if (_logger.IsInfoEnabled)
            {
                _logger.Info(value);
            }
        }

        public void LogWarning(string value)
        {
            if (_logger.IsWarnEnabled)
            {
                _logger.Warn(value);
            }
        }

        public void LogError(string value)
        {
            if (_logger.IsErrorEnabled)
            {
                _logger.Error(value);
            }
        }

        public void LogFatal(string value)
        {
            if (_logger.IsFatalEnabled)
            {
                _logger.Fatal(value);
            }
        }
    }
}
