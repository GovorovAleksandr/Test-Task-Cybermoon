using CameraFollow.Configs;
using CameraFollow.MonoReferences;
using EventBus.Events;
using EventBus.Public;
using UnityEngine;
using Zenject;

namespace CameraFollow.Core
{
    internal sealed class CameraFollowing :
        IFixedTickable, IAutoRegistrableEventHandler,
        IEventHandler<MonoReferenceLoaded<CameraFollowData>>,
        IEventHandler<PlayerTransformChanged>,
        IEventHandler<ResourceLoaded<CameraFollowConfig>>
    {
        [Inject] private readonly IReadOnlyEventBus _eventBus;

        private CameraFollowConfig _config;
        
        private Transform _cameraTransform;
        private Transform _targetTransform;
        
        public void FixedTick()
        {
            if (_cameraTransform == null) return;
            if (_targetTransform == null) return;
            if (_config == null) return;
            
            var startPosition = _cameraTransform.position;
            var targetPosition = _targetTransform.position;
            
            var position = Vector3.Lerp(startPosition, targetPosition + _config.Offset, _config.Smoothness);
            
            _cameraTransform.position = position;
        }

        public void Handle(ResourceLoaded<CameraFollowConfig> eventData) => _config = eventData.Resource;

        public void Handle(MonoReferenceLoaded<CameraFollowData> eventData)
        {
            var data = eventData.Data;
            
            _cameraTransform = data.Camera;
            _targetTransform = data.Target;
        }
        
        public void Handle(PlayerTransformChanged eventData) => _targetTransform = eventData.Transform;
    }
}