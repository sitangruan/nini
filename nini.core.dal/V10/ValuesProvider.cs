using System;

namespace nini.core.dal.V10
{
    public class ValuesProvider: IValuesProvider
    {
        public string[] ReadValues()
        {
            string[] values =  new string[] { "Values 1 from DAL", "Value 2 from DAL"};
            return values;
        }

        public string ReadValue(int id)
        {
            var value = $"Value {id} from DAL";
            return value;
        }
    }
}
