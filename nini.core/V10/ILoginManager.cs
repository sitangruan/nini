using System;
using System.Collections.Generic;
using System.Text;

namespace nini.core.V10
{
    public interface ILoginManager
    {
        Guid DoLogin(string userName, string password);

        bool Logout(Guid sessionId);

        void KeepLive(Guid sessionId);
    }
}
