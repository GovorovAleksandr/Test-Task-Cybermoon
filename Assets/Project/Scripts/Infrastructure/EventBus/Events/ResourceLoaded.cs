using EventBus.Public;
using UnityEngine;

namespace EventBus.Events
{
    public struct ResourceLoaded<T> : IEvent where T : ScriptableObject
    {
        public readonly T Resource;

        public ResourceLoaded(T resource)
        {
            Resource = resource;
        }
    }
}