using Input.Public;

namespace Input.Core
{
    internal sealed class GameModeInputBinder : IHandlerBinder<IGameModeInput, GameplayInput>
    {
        public void Bind(IGameModeInput handler, GameplayInput input)
        {
            input.GameMode.CharacterSelection.performed += context => handler.HandleCharacterSelectionMode();
            input.GameMode.Gameplay.performed += context => handler.HandleGameplayMode();
        }

        public void Unbind(IGameModeInput handler, GameplayInput input)
        {
            input.GameMode.CharacterSelection.performed -= context => handler.HandleCharacterSelectionMode();
            input.GameMode.Gameplay.performed -= context => handler.HandleGameplayMode();
        }
    }
}