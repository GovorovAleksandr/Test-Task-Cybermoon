namespace EventBus.Public
{
    public interface IEventHandler<T> : IBaseEventHandler where T : struct, IEvent
    {
        void Handle(T eventData);
    }
}