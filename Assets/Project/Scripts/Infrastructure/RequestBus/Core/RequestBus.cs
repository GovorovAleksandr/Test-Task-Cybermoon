using System;
using System.Collections.Generic;

namespace RequestBus.Core
{
    internal sealed class RequestBus : IRequestBus
    {
        private readonly Dictionary<Type, WeakReference<IBaseRequestHandler>> _handlers = new();

        public void Register<TRequest>(IRequestHandler<TRequest> handler) where TRequest : struct, IRequest
        {
            var requestType = typeof(TRequest);

            if (_handlers.ContainsKey(requestType))
                throw new InvalidOperationException($"Handler for request with \"{typeof(TRequest).Name}\" name already registered");
            
            _handlers[requestType] = new WeakReference<IBaseRequestHandler>(handler);
        }

        public void Unregister<TRequest>(IRequestHandler<TRequest> handler) where TRequest : struct, IRequest
        {
            var requestType = typeof(TRequest);

            if (!_handlers.ContainsKey(requestType)) return;
            
            _handlers.Remove(requestType);
        }

        public void Send<TRequest>(ref TRequest request) where TRequest : struct, IRequest
        {
            var requestType = typeof(TRequest);

            if (!_handlers.TryGetValue(requestType, out var handler))
                throw new InvalidOperationException($"No handler registered for request type {requestType.Name}.");

            if (!handler.TryGetTarget(out var receiver))
                throw new InvalidOperationException($"Handler for request type {requestType.Name} is no longer available.");
            
            ((IRequestHandler<TRequest>)receiver).Handle(ref request);
        }
    }
}