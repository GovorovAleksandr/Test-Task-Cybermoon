using System.Collections.Generic;

namespace DataPersistence.Data
{
    public struct SaveData
    {
        public Dictionary<string, object> Values;

        private SaveData(IDictionary<string, object> values)
        {
            Values = new(values);
        }

        public static SaveData GetDefault() => new(new Dictionary<string, object>());
    }
}