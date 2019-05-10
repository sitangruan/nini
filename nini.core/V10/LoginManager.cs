using System;
using System.Collections.Generic;
using System.Text;
using nini.core.Common;
using nini.core.Common.ErrorHandling;
using nini.core.Common.Session;
using nini.core.dal.V10;

namespace nini.core.V10
{
    public class LoginManager: BaseManager<LoginManager>, ILoginManager
    {
        private readonly ILoginProvider _loginProvider;

        public LoginManager(ILoginProvider loginProvider, ISessionManager sessionManager) : base(sessionManager)
        {
            _loginProvider = loginProvider;
        }

        public Guid DoLogin(string userName, string password)
        {
            mLogger.LogDebug($"Username {userName} calls Login.");
            var isValid = _loginProvider.ValidateUserCredential(userName, password);

            if (isValid)
            {
                var sessionId = Guid.NewGuid();
                var session = SessionManager.CreateSession(sessionId, userName);

                return session?.Id ?? Guid.Empty;
            }
            else
            {
                throw new MarvelException(System.Net.HttpStatusCode.BadRequest, "The login user credential is invalid.", null);
            }
        }

        public bool Logout(Guid sessionId)
        {
            mLogger.LogDebug($"sessionId {sessionId} calls Logout.");
            var success = SessionManager.RemoveSession(sessionId);

            return success;
        }

        public void KeepLive(Guid sessionId)
        {
            mLogger.LogDebug($"sessionId {sessionId} calls KeepLive.");
            SessionManager.KeepSessionLive(sessionId);
        }
    }
}
