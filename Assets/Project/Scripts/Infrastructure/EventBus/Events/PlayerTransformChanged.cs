using EventBus.Public;
using UnityEngine;

namespace EventBus.Events
{
    public struct PlayerTransformChanged : IEvent
    {
        public readonly Transform Transform;

        public PlayerTransformChanged(Transform transform)
        {
            Transform = transform;
        }
    }
}