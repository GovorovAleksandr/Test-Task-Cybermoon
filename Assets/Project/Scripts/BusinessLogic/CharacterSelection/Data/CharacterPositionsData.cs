using System;
using System.Collections.Generic;
using DataPersistence.Public;

namespace CharacterSelection.Data
{
    [Serializable]
    public struct CharacterPositionsData : ISavableData
    {
        public List<(float x, float y, float z)> Positions;

        public CharacterPositionsData(List<(float x, float y, float z)> positions)
        {
            Positions = positions;
        }
    }
}