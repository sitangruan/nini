using System;
using System.Collections.Generic;
using System.Text;

namespace nini.core.V10
{
    public class ValuesManager: IValuesManager
    {
        public string mConstructionIfno { get; set; }

        public ValuesManager()
        {
            mConstructionIfno = "This manager is instantiated by system, probably by Dependency Injection framework.";
        }

        public string[] GetValues()
        {
            return new string[] {"Value 1 from mgr", "Value 2 from mgr"};
        }

        public string GetValue(int id)
        {
            return $"Value {id} from mgr";
        }
    }
}
