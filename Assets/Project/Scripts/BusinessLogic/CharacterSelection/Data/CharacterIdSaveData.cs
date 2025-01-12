using System;
using DataPersistence.Public;

namespace CharacterSelection.Data
{
    [Serializable]
    public struct CharacterIdSaveData : ISavableData
    {
        public int CharacterId;

        public CharacterIdSaveData(int characterId)
        {
            CharacterId = characterId;
        }
    }
}