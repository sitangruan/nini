using System;
using System.Collections.Generic;
using System.Text;
using nini.core.Common;
using nini.core.Common.Session;
using nini.core.dal.V10;

namespace nini.core.V10
{
    public class ValuesManager: BaseManager<ValuesManager>, IValuesManager
    {
        public string mConstructionIfno { get; set; }

        private IValuesProvider _valuesProvider;

        public ValuesManager(IValuesProvider valuesProvider, ISessionManager sessionManager) : base(sessionManager)
        {
            mConstructionIfno = "This manager is instantiated by system, by Dependency Injection!";
            _valuesProvider = valuesProvider;
        }

        public string[] GetValues()
        {
            mLogger.LogDebug($"sessionId calls GetValues.");
            string[] values = _valuesProvider.ReadValues();
            return values;
        }

        public string GetValue(int id)
        {
            mLogger.LogDebug($"sessionId calls GetValue.");
            string value = _valuesProvider.ReadValue(id);
            return value;
        }
    }
}
