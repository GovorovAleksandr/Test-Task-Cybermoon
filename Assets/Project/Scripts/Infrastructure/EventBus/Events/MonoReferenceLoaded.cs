using EventBus.Public;

namespace EventBus.Events
{
    public struct MonoReferenceLoaded<T> : IEvent where T : struct
    {
        public readonly T Data;

        public MonoReferenceLoaded(T data)
        {
            Data = data;
        }
    }
}