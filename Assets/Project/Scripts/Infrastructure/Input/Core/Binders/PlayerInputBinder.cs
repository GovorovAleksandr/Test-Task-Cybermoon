using Input.Public;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input.Core
{
    internal sealed class PlayerInputBinder : IHandlerBinder<IPlayerInputHandler, GameplayInput>
    {
        public void Bind(IPlayerInputHandler handler, GameplayInput input)
        {
            input.Player.Movement.performed += context => HandleInput(context, handler);
            input.Player.Movement.canceled += _ => handler.HandleCancel();
        }

        public void Unbind(IPlayerInputHandler handler, GameplayInput input)
        {
            input.Player.Movement.performed -= context => HandleInput(context, handler);
            input.Player.Movement.canceled -= _ => handler.HandleCancel();
        }

        private static void HandleInput(InputAction.CallbackContext context, IPlayerInputHandler handler) =>
            handler.HandleInput(context.ReadValue<Vector2>());
    }
}