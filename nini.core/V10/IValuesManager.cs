using System.Collections.Generic;

namespace nini.core.V10
{
    public interface IValuesManager
    {
        string[] GetValues();

        string GetValue(int id);
    }
}
