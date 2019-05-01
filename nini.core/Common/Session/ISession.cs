using System;

namespace nini.core.Common.Session
{
    public interface ISession: IDisposable
    {
        Guid Id { get; }

        DateTime LastTouchTime { get; }

        string UserName { get; }

        void KeepMeLive();
    }
}
