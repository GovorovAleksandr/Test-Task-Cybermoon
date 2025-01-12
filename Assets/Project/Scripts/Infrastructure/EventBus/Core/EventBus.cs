using System;
using System.Collections.Generic;
using System.Linq;
using EventBus.Public;

namespace EventBus.Core
{
    internal sealed class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<(int Priority, IBaseEventHandler Receiver)>> _handlers = new();
        private readonly Dictionary<int, IBaseEventHandler> _handlerHashToReference = new();

        public void Register<T>(IEventHandler<T> handler, int priority = 0) where T : struct, IEvent
        {
            var eventType = typeof(T);

            if (!_handlers.ContainsKey(eventType))
                _handlers[eventType] = new List<(int, IBaseEventHandler)>();

            var receiverHashCode = handler.GetHashCode();
        
            _handlerHashToReference[receiverHashCode] = handler;

            _handlers[eventType].Add((priority, handler));

            var handlers = _handlers[eventType];
            
            _handlers[eventType] = handlers.OrderBy(receiver => receiver.Priority).ToList();
        }

        public void Unregister<T>(IEventHandler<T> handler) where T : struct, IEvent
        {
            var eventType = typeof(T);
            var receiverHashCode = handler.GetHashCode();

            if (!_handlers.ContainsKey(eventType)) return;
        
            if (!_handlerHashToReference.TryGetValue(receiverHashCode, out var reference)) return;
        
            _handlers[eventType].RemoveAll(pair => pair.Receiver == reference);
            _handlerHashToReference.Remove(receiverHashCode);
        }

        public void Send<T>(T eventData) where T : struct, IEvent
        {
            var eventType = typeof(T);

            if (!_handlers.TryGetValue(eventType, out var handlers)) return;

            foreach (var (_, handler) in handlers.ToList())
            {
                CastEventHandler<T>(handler).Handle(eventData);
            }
        }
        
        private static IEventHandler<T> CastEventHandler<T>(IBaseEventHandler handler) where T : struct, IEvent =>
            (IEventHandler<T>)handler;
    }
}