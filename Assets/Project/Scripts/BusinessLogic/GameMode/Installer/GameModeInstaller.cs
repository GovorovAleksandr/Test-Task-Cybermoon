using GameMode.Core;
using Zenject;
using ZenjectInstallation.Public;

namespace GameMode.Installer
{
    internal sealed class GameModeInstaller : ILocalContextInstaller, IPrioritizedContextInstaller
    {
        public BindingScene BindingScenes => BindingScene.Gameplay;

        public int Priority => 1000;

        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesTo<GameModeSwitcher>().FromNew().AsSingle().NonLazy();
        }
    }
}