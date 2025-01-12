namespace EventBus.Public
{
    public interface IEventBusSender
    {
        void Send<T>(T eventData) where T : struct, IEvent;
    }
}