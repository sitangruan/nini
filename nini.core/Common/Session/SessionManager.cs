using System;
using System.Collections.Generic;
using System.Text;

namespace nini.core.Common.Session
{
    public class SessionManager: ISessionManager
    {
        private ISessionFactory _sessionFactory;
        private Dictionary<Guid, ISession> _sessionDictionary;

        public SessionManager(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
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
