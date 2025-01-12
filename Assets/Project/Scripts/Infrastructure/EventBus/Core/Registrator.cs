using System;
using System.Collections.Generic;
using System.Linq;
using EventBus.Public;
using Zenject;

namespace EventBusAutoRegistration.Core
{
    internal sealed class Registrator : IInitializable, IDisposable
    {
        [Inject] private readonly List<IBaseEventHandler> _handlers;
        [Inject] private readonly IReadOnlyEventBus _eventBus;

        public void Initialize() => _handlers.ForEach(Register);
        public void Dispose() => _handlers.ForEach(Unregister);

        private void Register(IBaseEventHandler handler) =>
            ProcessHandler(handler, nameof(_eventBus.Register), handler, 0);
        
        private void Unregister(IBaseEventHandler handler) =>
            ProcessHandler(handler, nameof(_eventBus.Unregister), handler);

        private void ProcessHandler(IBaseEventHandler handler, string methodName, params object[] args)
        {
            var handlerType = handler.GetType();

            var interfaces = handlerType.GetInterfaces();

            if (!interfaces.Contains(typeof(IAutoRegistrableEventHandler))) return;

            var eventBusType = _eventBus.GetType();
            var method = eventBusType.GetMethod(methodName);

            foreach (var @interface in interfaces)
            {
                if (!@interface.IsGenericType ||
                    @interface.GetGenericTypeDefinition() != typeof(IEventHandler<>)) continue;
                
                var eventType = @interface.GenericTypeArguments[0];
                var genericMethod = method.MakeGenericMethod(eventType);
                
                genericMethod.Invoke(_eventBus, args);
            }
        }
    }
}