using DataPersistence.Events;

namespace DataPersistence.Core
{
    internal sealed class GameplayDataFileNameBuilder : FileNameBuilder<SaveGameplayData>
    {
        public override string FileName => "GameplayData";
    }
}