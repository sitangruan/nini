using System;

namespace nini.core.Common.Session
{
    public interface ISession
    {
        Guid Id { get; }

        DateTime LastTouchTime { get; }

        string UserName { get; }
    }
}
