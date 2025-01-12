using System.Collections.Generic;
using System.Linq;
using CharacterSelection.Data;
using DataPersistence.Events;
using EventBus.Events;
using EventBus.Public;
using Input.Public;
using ProjectState.Events;
using ProjectState.Public;
using UnityEngine;
using Zenject;

namespace CharacterSelection.Core
{
    internal sealed class CharacterSelector :
        ICharacterSelectionInputHandler, IAutoRegistrableEventHandler,
        IEventHandler<MonoReferenceLoaded<CharacterReferencesData>>,
        IEventHandler<StateChanged<Gameplay>>,
        IEventHandler<StateChanged<ProjectState.Public.CharacterSelection>>,
        IEventHandler<DataLoaded<CharacterIdSaveData>>
    {
        [Inject] private readonly IEventBus _eventBus;
        
        private IList<Transform> _characters;

        private int _characterId;
        
        private bool _isActive;
        
        public void MoveRight()
        {
            if (!_isActive) return;
            
            _characterId++;
            if (_characterId >= _characters.Count) _characterId = 0;
            
            NotifyNewCharacter(_characterId);
            Save();
        }

        public void MoveLeft()
        {
            if (!_isActive) return;
            
            _characterId--;
            if (_characterId < 0) _characterId = _characters.Count - 1;
            
            NotifyNewCharacter(_characterId);
            Save();
        }

        public void Handle(DataLoaded<CharacterIdSaveData> eventData) =>
            NotifyNewCharacter(_characterId = eventData.Data.CharacterId);
        
        public void Handle(StateChanged<Gameplay> eventData) => _isActive = false;
        
        public void Handle(StateChanged<ProjectState.Public.CharacterSelection> eventData) =>
            _isActive = true;
        
        public void Handle(MonoReferenceLoaded<CharacterReferencesData> eventData) =>
            _characters = eventData.Data.CharacterTransforms;

        private void NotifyNewCharacter(int index) =>
            _eventBus.Send(new PlayerTransformChanged(_characters[index]));

        private void Save() => _eventBus.Send(new SaveGameplayData(new CharacterIdSaveData(_characterId)));
    }
}