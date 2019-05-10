using System;

namespace nini.core.Common.Session
{
    public class SessionFactory: ISessionFactory
    {
        public ISession CreateSession(Guid Id, string userName)
        {
            return new Session(Id, userName);
        }
    }
}
