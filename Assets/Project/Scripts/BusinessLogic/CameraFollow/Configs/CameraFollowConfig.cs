using UnityEngine;

namespace CameraFollow.Configs
{
    [CreateAssetMenu(fileName = "CameraFollowConfig", menuName = "Project/CameraFollow/Config")]
    internal sealed class CameraFollowConfig : ScriptableObject
    {
        [SerializeField] [Min(0.001f)] private float _smoothness = 0.5f;
        [SerializeField] private Vector3 _offset;
        
        public float Smoothness => _smoothness;
        public Vector3 Offset => _offset;
    }
}