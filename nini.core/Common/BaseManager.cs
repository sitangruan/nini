using System;
using System.Collections.Generic;
using System.Text;
using nini.core.Common.Session;

namespace nini.core.Common
{
    public class BaseManager
    {
        protected ISessionManager SessionManager { get; }

        public BaseManager(ISessionManager sessionManager)
        {
            SessionManager = sessionManager;
        }
    }
}
