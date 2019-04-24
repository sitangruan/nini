using System;
using System.Collections.Generic;
using System.Text;

namespace nini.core.dal.V10
{
    public interface IValuesProvider
    {
        string[] ReadValues();
        string ReadValue(int id);
    }
}
