using System;
using EventBus.Public;

namespace EventBus.Events
{
    public struct SendGenericEvent : IEvent
    {
        public readonly Type RawEventType;
        public readonly Type GenericArgType;
        public readonly object[] Args;

        public SendGenericEvent(Type rawEventType, Type genericArgType, params object[] args)
        {
            RawEventType = rawEventType;
            GenericArgType = genericArgType;
            Args = args;
        }
    }
}