using System;
using System.Collections.Generic;
using System.Text;
using nini.core.Common.Session;
using nini.foundation.Logging;

namespace nini.core.Common
{
    public class BaseManager<T> where T : class
    {
        protected IMarvelLogger mLogger;
        protected ISessionManager SessionManager { get; }

        public BaseManager(ISessionManager sessionManager)
        {
            SessionManager = sessionManager;
            mLogger = MarvelLoggerFactory.Generate(typeof(T));
        }
    }
}
