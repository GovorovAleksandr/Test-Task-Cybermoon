using System.Collections.Generic;
using DataPersistence.Events;
using EventBus.Events;
using EventBus.Public;
using Zenject;

namespace DataPersistence.Core
{
    internal sealed class DataPersistenceHandler :
        IInitializable, IAutoRegistrableEventHandler,
        IEventHandler<SaveData>
    {
        [Inject] private readonly IEventBus _eventBus;
        
        private readonly Dictionary<string, Data.SaveData> _data = new();
        
        private readonly IDataSerializer _dataSerializer;
        private readonly IFilePersistence _filePersistence;
        private readonly List<string> _fileNames;

        public DataPersistenceHandler(IDataSerializer dataSerializer, IFilePersistence filePersistence, List<string> fileNames)
        {
            _dataSerializer = dataSerializer;
            _filePersistence = filePersistence;
            _fileNames = fileNames;
        }

        public void Handle(SaveData eventData)
        {
            var fileName = eventData.FileNameBuilder.FileName;

            var saveFileData = GetFileData(fileName);
            
            var type = eventData.Data.GetType();
            var data = eventData.Data;
            
            var typeFullName = type.FullName;
            saveFileData.Values[typeFullName] = data;
            Save(fileName);
        }

        public void Initialize() => LoadAll();
        
        private Data.SaveData GetFileData(string fileName)
        {
            if (_data.TryGetValue(fileName, out var saveFileData)) return saveFileData;
            
            var data = Data.SaveData.GetDefault();
            _data[fileName] = data;
            return data;
        }

        private void Save(string fileName)
        {
            var data = GetFileData(fileName);
            var serializedData = _dataSerializer.Serialize(data);
            _filePersistence.Save(fileName, serializedData);
        }

        private void LoadAll()
        {
            foreach (var fileName in _fileNames)
            {
                var serializedData = _filePersistence.Load(fileName);
                var loadedData = _dataSerializer.Deserialize(serializedData);
                
                _data.Add(fileName, loadedData);

                foreach (var data in loadedData.Values.Values)
                {
                    var dataType = data.GetType();
                    _eventBus.Send(new SendGenericEvent(typeof(DataLoaded<>), dataType, data));
                }
            }
        }
    }
}