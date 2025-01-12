using System;
using EventBus.Public;

namespace DataPersistence.Events
{
    public struct DeleteSavedData : IEvent
    {
        public readonly Type DataType;

        public DeleteSavedData(Type dataType)
        {
            DataType = dataType;
        }
    }
}