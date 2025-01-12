using Input.Public;
using UnityEngine;

namespace PlayerMovement.Core
{
    internal sealed class PlayerInput : IPlayerInputHandler
    {
        public Vector2 InputVector { get; private set; }

        public void HandleInput(Vector2 inputVector) => InputVector = inputVector;
        public void HandleCancel() => InputVector = Vector2.zero;
    }
}