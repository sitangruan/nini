using System;
using System.Collections.Generic;
using System.Text;

namespace nini.core.Common.Session
{
    public class Session: ISession
    {
        public Guid Id { get; }

        public DateTime LastTouchTime { get; private set; }

        public string UserName { get; }

        public Session(Guid sessionId, string userName)
        {
            Id = sessionId;
            UserName = userName;
            LastTouchTime = DateTime.UtcNow;
        }

        public void KeepMeLive()
        {
            LastTouchTime = DateTime.UtcNow;
        }

        public void Dispose()
        {
            //To do: release the resource belongs to this session. Like DB client or sth.
        }
    }
}
