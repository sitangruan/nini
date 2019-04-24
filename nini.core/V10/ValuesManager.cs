using System;
using System.Collections.Generic;
using System.Text;
using nini.core.dal.V10;

namespace nini.core.V10
{
    public class ValuesManager: IValuesManager
    {
        public string mConstructionIfno { get; set; }

        private IValuesProvider _valuesProvider;

        public ValuesManager(IValuesProvider valuesProvider)
        {
            mConstructionIfno = "This manager is instantiated by system, probably by Dependency Injection framework.";
            _valuesProvider = valuesProvider;
        }

        public string[] GetValues()
        {
            string[] values = _valuesProvider.ReadValues();
            return values;
        }

        public string GetValue(int id)
        {
            string value = _valuesProvider.ReadValue(id);
            return value;
        }
    }
}
