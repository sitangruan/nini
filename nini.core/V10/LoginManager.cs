using System;
using System.Collections.Generic;
using System.Text;
using nini.core.Common;
using nini.core.Common.Session;
using nini.core.dal.V10;

namespace nini.core.V10
{
    public class LoginManager: BaseManager, ILoginManager
    {
        private readonly ILoginProvider _loginProvider;

        public LoginManager(ILoginProvider loginProvider, ISessionManager sessionManager) : base(sessionManager)
        {
            _loginProvider = loginProvider;
        }

        public Guid DoLogin(string userName, string password)
        {
            var isValid = _loginProvider.ValidateUserCredential(userName, password);

            if (isValid)
            {
                var sessionId = Guid.NewGuid();
                var session = SessionManager.CreateSession(sessionId, userName);

                return session?.Id ?? Guid.Empty;
            }

            return Guid.Empty;
        }

        public bool Logout(Guid sessionId)
        {
            var success = SessionManager.RemoveSession(sessionId);

            return success;
        }

        public void KeepLive(Guid sessionId)
        {
            SessionManager.RemoveSession(sessionId);
        }
    }
}
