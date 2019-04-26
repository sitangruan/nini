using System;
using System.Collections.Generic;
using System.Text;

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
