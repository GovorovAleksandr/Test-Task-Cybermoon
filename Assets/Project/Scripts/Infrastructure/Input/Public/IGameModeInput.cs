namespace Input.Public
{
    public interface IGameModeInput : IInputHandler
    {
        void HandleGameplayMode();
        void HandleCharacterSelectionMode();
    }
}