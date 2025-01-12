using System.Collections.Generic;
using System.Linq;
using CharacterSelection.Data;
using DataPersistence.Events;
using EventBus.Events;
using EventBus.Public;
using UnityEngine;
using Zenject;

namespace CharacterSelection.Core.Persistence
{
    internal sealed class CharacterPositionSaver :
        ITickable, IAutoRegistrableEventHandler,
        IEventHandler<MonoReferenceLoaded<CharacterReferencesData>>
    {
        [Inject] private readonly IEventBusSender _eventBus;
        
        private IEnumerable<Transform> _characters;
        
        public void Handle(MonoReferenceLoaded<CharacterReferencesData> eventData) =>
            _characters = eventData.Data.CharacterTransforms;

        public void Tick() => Save();
        
        private void Save()
        {
            if (_characters == null) return;
            
            var positions = _characters.Select(character =>
            {
                var position = character.position;

                var x = position.x;
                var y = position.y;
                var z = position.z;
                return (x, y, z);
            }).ToList();

            _eventBus.Send(new SaveGameplayData(new CharacterPositionsData(positions)));
        }
    }
}