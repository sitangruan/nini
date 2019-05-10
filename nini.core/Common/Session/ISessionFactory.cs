using System;

namespace nini.core.Common.Session
{
    public interface ISessionFactory
    {
        ISession CreateSession(Guid sessionId, string userName);
    }
}
