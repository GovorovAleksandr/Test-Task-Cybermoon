using System;
using MonoReferencing.Public;
using TMPro;
using UnityEngine;

namespace Hints.Data
{
    [Serializable]
    internal struct GameModeHintReferenceData : IMonoReferenceData
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public TextMeshProUGUI Text => _text;
    }
}