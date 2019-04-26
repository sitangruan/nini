using System;
using System.Collections.Generic;
using System.Text;

namespace nini.core.Common.Session
{
    public interface ISessionFactory
    {
        ISession CreateSession(Guid sessionId, string userName);
    }
}
