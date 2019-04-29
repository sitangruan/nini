using System;
using System.Collections.Generic;
using System.Text;

namespace nini.core.dal.V10
{
    public interface ILoginProvider
    {
        bool ValidateUserCredential(string userName, string password);
    }
}
