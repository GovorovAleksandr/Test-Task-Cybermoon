using System;
using EventBus.Events;
using EventBus.Public;
using Hints.Configs;
using Hints.Data;
using ProjectState.Events;
using ProjectState.Public;
using TMPro;

namespace Hints.View
{
    internal sealed class GameModeHintsView :
        IAutoRegistrableEventHandler,
        IEventHandler<MonoReferenceLoaded<GameModeHintReferenceData>>,
        IEventHandler<ResourceLoaded<GameModeHintsConfig>>,
        IEventHandler<StateChanged<Gameplay>>,
        IEventHandler<StateChanged<CharacterSelection>>
    {
        private TextMeshProUGUI _text;
        private GameModeHintsConfig _config;

        public void Handle(MonoReferenceLoaded<GameModeHintReferenceData> eventData) =>
            _text = eventData.Data.Text;

        public void Handle(ResourceLoaded<GameModeHintsConfig> eventData) =>
            _config = eventData.Resource;

        public void Handle(StateChanged<Gameplay> eventData) =>
            _text.text = _config.GameplayText;

        public void Handle(StateChanged<CharacterSelection> eventData) =>
            _text.text = _config.CharacterSelectionText;
    }
}