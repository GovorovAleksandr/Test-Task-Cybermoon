using UnityEngine;

namespace PlayerMovement.Configs
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Project/Gameplay/PlayerMovementConfig")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [SerializeField] [Min(0.1f)] private float _speed = 1f;
        
        public float Speed => _speed;
    }
}