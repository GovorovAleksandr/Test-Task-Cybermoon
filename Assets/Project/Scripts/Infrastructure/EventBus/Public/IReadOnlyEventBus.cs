namespace EventBus.Public
{
    public interface IReadOnlyEventBus
    {
        void Register<T>(IEventHandler<T> handler, int priority = 0) where T : struct, IEvent;
        void Unregister<T>(IEventHandler<T> handler)  where T : struct, IEvent;
    }
}