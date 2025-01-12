using EventBus.Events;
using EventBus.Public;
using PlayerMovement.Configs;
using PlayerMovement.Data;
using ProjectState.Events;
using ProjectState.Public;
using RequestBus;
using UnityEngine;
using Zenject;

namespace PlayerMovement.Core
{
    internal sealed class PlayerMovement :
        IFixedTickable, IAutoRegistrableEventHandler,
        IEventHandler<MonoReferenceLoaded<PlayerTransformData>>,
        IEventHandler<ResourceLoaded<PlayerMovementConfig>>,
        IEventHandler<PlayerTransformChanged>,
        IEventHandler<StateChanged<Gameplay>>,
        IEventHandler<StateChanged<CharacterSelection>>
    {
        [Inject] private readonly IEventBus _eventBus;
        [Inject] private readonly IRequestBusSender _requestBus;
        [Inject] private readonly PlayerInput _playerInput;
        
        private Transform _playerTransform;
        private PlayerMovementConfig _config;
        
        private bool _isActive;

        public void Handle(ResourceLoaded<PlayerMovementConfig> eventData) =>
            _config = eventData.Resource;
        
        public void Handle(MonoReferenceLoaded<PlayerTransformData> eventData) =>
            _playerTransform = eventData.Data.PlayerTransform;

        public void Handle(PlayerTransformChanged eventData) =>
            _playerTransform = eventData.Transform;

        public void Handle(StateChanged<Gameplay> eventData) => _isActive = true;
        public void Handle(StateChanged<CharacterSelection> eventData) => _isActive = false;

        public void FixedTick()
        {
            if (!_isActive) return;
            
            var inputVector = _playerInput.InputVector;
            
            var inputX = inputVector.x;
            var inputY = inputVector.y;

            Vector3 direction = new(inputX, 0f, inputY);
            
            _playerTransform.position += direction * _config.Speed * Time.fixedDeltaTime;
        }
    }
}