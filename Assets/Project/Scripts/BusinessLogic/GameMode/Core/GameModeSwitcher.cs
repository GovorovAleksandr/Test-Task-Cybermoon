using EventBus.Public;
using Input.Public;
using ProjectState.Events;
using ProjectState.Public;
using Zenject;

namespace GameMode.Core
{
    internal sealed class GameModeSwitcher : IInitializable, IGameModeInput
    {
        [Inject] private readonly IEventBusSender _eventBus;

        public void Initialize() => SetState<CharacterSelection>();
        
        public void HandleGameplayMode() => SetState<Gameplay>();
        public void HandleCharacterSelectionMode() => SetState<CharacterSelection>();

        private void SetState<T>() where T : StateBase, new()
        {
            _eventBus.Send(new ChangeProjectState(StateBase.Create<T>()));
        }
    }
}