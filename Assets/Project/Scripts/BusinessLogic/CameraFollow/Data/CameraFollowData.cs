using System;
using MonoReferencing.Public;
using UnityEngine;

namespace CameraFollow.MonoReferences
{
    [Serializable]
    internal struct CameraFollowData : IMonoReferenceData
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _target;
        
        public Transform Camera  => _camera.transform;
        public Transform Target => _target;
    }
}