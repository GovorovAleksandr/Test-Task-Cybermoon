using System;
using System.Collections.Generic;
using MonoReferencing.Public;
using UnityEngine;

namespace CharacterSelection.Data
{
    [Serializable]
    internal struct CharacterReferencesData : IMonoReferenceData
    {
        [SerializeField] private List<Transform> _characters;
        
        public IList<Transform> CharacterTransforms => _characters;
    }
}