using UnityEngine;

namespace Input.Public
{
    public interface IPlayerInputHandler : IInputHandler
    {
        void HandleInput(Vector2 inputVector);
        void HandleCancel();
    }
}