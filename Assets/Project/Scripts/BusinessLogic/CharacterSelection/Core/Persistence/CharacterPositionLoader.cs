using System.Collections.Generic;
using System.Linq;
using CharacterSelection.Data;
using DataPersistence.Events;
using EventBus.Events;
using EventBus.Public;
using UnityEngine;

namespace CharacterSelection.Core.Persistence
{
    internal sealed  class CharacterPositionLoader :
        IAutoRegistrableEventHandler,
        IEventHandler<MonoReferenceLoaded<CharacterReferencesData>>,
        IEventHandler<DataLoaded<CharacterPositionsData>>
    {
        private IEnumerable<Transform> _characters;

        public void Handle(MonoReferenceLoaded<CharacterReferencesData> eventData) =>
            _characters = eventData.Data.CharacterTransforms;

        public void Handle(DataLoaded<CharacterPositionsData> eventData)
        {
            var data = eventData.Data;
            var positions = data.Positions;
            
            for (var i = 0; i < positions.Count; i++)
            {
                var (x, y, z) = positions.ElementAt(i);
                
                _characters.ElementAt(i).transform.position = new (x, y, z);
            }
        }
    }
}