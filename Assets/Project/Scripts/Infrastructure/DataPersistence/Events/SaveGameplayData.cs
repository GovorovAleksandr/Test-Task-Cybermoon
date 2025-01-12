using DataPersistence.Public;

namespace DataPersistence.Events
{
    public struct SaveGameplayData : ISaveEvent
    {
        public SaveGameplayData(ISavableData data)
        {
            Data = data;
        }

        public ISavableData Data { get; }
    }
}