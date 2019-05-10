using System;

namespace nini.core.Common.Session
{
    public interface ISessionManager
    {
        ISession CreateSession(Guid sessionId, string userName);

        bool RemoveSession(Guid sessionId);

        ISession GetSession(Guid sessionId);

        bool KeepSessionLive(Guid sessionId);
    }
}
