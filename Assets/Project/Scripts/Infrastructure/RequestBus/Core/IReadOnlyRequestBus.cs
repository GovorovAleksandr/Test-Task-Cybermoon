namespace RequestBus
{
    public interface IReadOnlyRequestBus
    {
        void Register<TRequest>(IRequestHandler<TRequest> handler) where TRequest : struct, IRequest;
        void Unregister<TRequest>(IRequestHandler<TRequest> handler) where TRequest : struct, IRequest;
    }
}