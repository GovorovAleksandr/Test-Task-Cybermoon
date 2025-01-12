using CharacterSelection.Core;
using CharacterSelection.Core.Persistence;
using Zenject;
using ZenjectInstallation.Public;

namespace CharacterSelection.Installer
{
    internal sealed class Installer : ILocalContextInstaller
    {
        public BindingScene BindingScenes => BindingScene.Gameplay;

        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesTo<CharacterSelector>().FromNew().AsSingle().NonLazy();
            container.BindInterfacesTo<CharacterPositionSaver>().FromNew().AsSingle().NonLazy();
            container.BindInterfacesTo<CharacterPositionLoader>().FromNew().AsSingle().NonLazy();
        }
    }
}