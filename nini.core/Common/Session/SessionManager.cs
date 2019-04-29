using System;
using System.Collections.Generic;
using System.Text;

namespace nini.core.Common.Session
{
    public class SessionManager: ISessionManager
    {
        private ISessionFactory _sessionFactory;
        private Dictionary<Guid, ISession> _sessionDictionary;
        private const int MAX_SESSION_NUMBER = 300;

        public SessionManager(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _sessionDictionary = new Dictionary<Guid, ISession>(MAX_SESSION_NUMBER);
        }

        public ISession CreateSession(Guid Id, string userName)
        {
            var session = _sessionFactory.CreateSession(Id, userName);
            _sessionDictionary[session.Id] = session;

            return session;
        }

        public bool RemoveSession(Guid sessionId)
        {
            if (_sessionDictionary.ContainsKey(sessionId))
            {
                _sessionDictionary.Remove(sessionId);

                return true;
            }

            return false;
        }

        public ISession GetSession(Guid sessionId)
        {
            if (_sessionDictionary.ContainsKey(sessionId))
            {
                return _sessionDictionary[sessionId];
            }

            return null;
        }
    }
}
