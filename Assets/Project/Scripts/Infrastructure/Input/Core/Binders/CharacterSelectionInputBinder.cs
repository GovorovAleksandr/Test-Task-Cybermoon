using Input.Public;

namespace Input.Core
{
    internal sealed class CharacterSelectionInputBinder : IHandlerBinder<ICharacterSelectionInputHandler, GameplayInput>
    {
        public void Bind(ICharacterSelectionInputHandler handler, GameplayInput input)
        {
            input.CharacterSelection.Left.performed += context => handler.MoveLeft();
            input.CharacterSelection.Right.performed += context => handler.MoveRight();
        }

        public void Unbind(ICharacterSelectionInputHandler handler, GameplayInput input)
        {
            input.CharacterSelection.Left.performed -= context => handler.MoveLeft();
            input.CharacterSelection.Right.performed -= context => handler.MoveRight();
        }
    }
}