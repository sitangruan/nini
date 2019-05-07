using System;
using System.Collections.Generic;
using System.Text;

namespace nini.foundation.Logging
{
    public interface IMarvelLogger
    {
        void LogTrace(string value);
        void LogDebug(string value);
        void LogInfo(string value);
        void LogWarning(string value);
        void LogError(string value);
        void LogFatal(string value);
    }
}
