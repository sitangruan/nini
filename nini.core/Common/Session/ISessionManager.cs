using System;
using System.Collections.Generic;
using System.Text;

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
