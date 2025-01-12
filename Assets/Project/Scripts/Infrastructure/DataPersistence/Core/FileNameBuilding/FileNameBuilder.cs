using DataPersistence.Events;
using DataPersistence.Public;
using EventBus.Public;
using Zenject;

namespace DataPersistence.Core
{
    internal abstract class FileNameBuilder<T> :
        BaseFileNameBuilder, IAutoRegistrableEventHandler,
        IEventHandler<T> where T : struct, ISaveEvent
    {
        [Inject] private readonly IEventBusSender _eventBus;
        
        public void Handle(T data) => _eventBus.Send(new SaveData(data.Data, this));
    }
}