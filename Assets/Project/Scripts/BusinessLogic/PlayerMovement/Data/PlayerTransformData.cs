using System;
using MonoReferencing.Public;
using PlayerMovement.Configs;
using UnityEngine;

namespace PlayerMovement.Data
{
    [Serializable]
    public struct PlayerTransformData : IMonoReferenceData
    {
        [SerializeField] private Transform _playerTransform;
        
        public Transform PlayerTransform => _playerTransform;
    }
}