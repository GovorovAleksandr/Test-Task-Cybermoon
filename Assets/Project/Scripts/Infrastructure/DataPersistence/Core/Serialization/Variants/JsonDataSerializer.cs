using DataPersistence.Data;
using Unity.Plastic.Newtonsoft.Json;

namespace DataPersistence.Core
{
    internal sealed class JsonDataSerializer : IDataSerializer
    {
        private readonly JsonSerializerSettings _jsonSettings = new()
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All
        };

        public string Serialize(SaveData data) => JsonConvert.SerializeObject(data, _jsonSettings);

        public SaveData Deserialize(string data) =>
            string.IsNullOrEmpty(data) ?
            SaveData.GetDefault() :
            JsonConvert.DeserializeObject<SaveData>(data, _jsonSettings);
    }
}