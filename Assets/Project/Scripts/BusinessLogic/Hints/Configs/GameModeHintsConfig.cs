using UnityEngine;

namespace Hints.Configs
{
    [CreateAssetMenu(fileName = "GameModeHintsConfig", menuName = "Project/Hints/GameModeHintsConfig")]
    internal sealed class GameModeHintsConfig : ScriptableObject
    {
        [SerializeField] private string _gameplayText;
        [SerializeField] private string _characterSelectionText;
        
        public string GameplayText => _gameplayText;
        public string CharacterSelectionText => _characterSelectionText;
    }
}