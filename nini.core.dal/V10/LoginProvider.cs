using System;
using System.Collections.Generic;
using System.Text;

namespace nini.core.dal.V10
{
    public class LoginProvider: ILoginProvider
    {
        public bool ValidateUserCredential(string userName, string password)
        {
            //Fake a simple credential verification logic, if userName == password, then the credential is valid
            var valid = userName.Equals(password);
            return valid;
        }
    }
}
