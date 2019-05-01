using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace nini.core.Common.Session
{
    public class SessionManager: ISessionManager
    {
        private ISessionFactory _sessionFactory;
        private Dictionary<Guid, ISession> _sessionDictionary;
        private readonly ReaderWriterLockSlim _sessionDictionaryLock;
        private readonly Timer _sessionTimeoutCheckTimer;
        private readonly TimeSpan _sessionCheckInterval;
        private readonly TimeSpan _sessionTimeOutThreshold;
        private readonly TimeSpan _lockAcquireTimout;
        private const int SESSION_CHECK_INTERVAL_MILLISECONDS = 30000;
        private const int SESSION_TIMEOUT_MILLISECONDS = 60000;
        private const int MAX_SESSION_NUMBER = 300;
        private const int LOCK_AQUIRE_TIMEOUNT_MILLISECONDS = 2000;

        public SessionManager(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _sessionDictionary = new Dictionary<Guid, ISession>(MAX_SESSION_NUMBER);
            _sessionDictionaryLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            _lockAcquireTimout = TimeSpan.FromMilliseconds(LOCK_AQUIRE_TIMEOUNT_MILLISECONDS);
            _sessionCheckInterval = TimeSpan.FromMilliseconds(SESSION_CHECK_INTERVAL_MILLISECONDS);
            _sessionTimeOutThreshold = TimeSpan.FromMilliseconds(SESSION_TIMEOUT_MILLISECONDS);
            _sessionTimeoutCheckTimer = new System.Threading.Timer(SessionTimeoutCheck, null, _sessionCheckInterval, TimeSpan.Zero); //Make the interval parameter zero so if the call back takes long time timer will not be triggered in the middle.
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

        public bool KeepSessionLive(Guid sessionId)
        {
            if (_sessionDictionary.ContainsKey(sessionId))
            {
                _sessionDictionary[sessionId].KeepMeLive();
                return true;
            }

            return false;
        }

        private void SessionTimeoutCheck(Object state)
        {
            bool readerGranted = false;
            bool writerGranted = false;
            _sessionTimeoutCheckTimer.Change(Timeout.Infinite, Timeout.Infinite); //Set the timer to be infinite so callback will not be interrupted buy timer event in the middle

            try
            {
                readerGranted = _sessionDictionaryLock.TryEnterUpgradeableReadLock(_lockAcquireTimout);
                if (!readerGranted)
                {
                    //To log the warning
                    return;
                }

                var sessionsToDispose = _sessionDictionary
                    .Where(kvp => DateTime.UtcNow.Subtract(kvp.Value.LastTouchTime) > _sessionTimeOutThreshold)
                    .Select(kvp => kvp.Value).ToList();

                writerGranted = _sessionDictionaryLock.TryEnterWriteLock(_lockAcquireTimout);

                if (!writerGranted)
                {
                    //to log the warning
                    return;
                }

                foreach (var session in sessionsToDispose)
                {
                    _sessionDictionary.Remove(session.Id);
                    session.Dispose();
                }
            }
            finally
            {
                if (writerGranted)
                {
                    _sessionDictionaryLock.ExitWriteLock();
                }

                if (readerGranted)
                {
                    _sessionDictionaryLock.ExitUpgradeableReadLock();
                }

                _sessionTimeoutCheckTimer.Change(_sessionCheckInterval, TimeSpan.Zero); //Restore timer to be running after the interval
            }
        }
    }
}
