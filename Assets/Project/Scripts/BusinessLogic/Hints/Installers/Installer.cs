using Hints.View;
using Zenject;
using ZenjectInstallation.Public;

namespace Hints.Installers
{
    internal class Installer : ILocalContextInstaller
    {
        public BindingScene BindingScenes => BindingScene.Gameplay;

        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesTo<GameModeHintsView>().FromNew().AsSingle().NonLazy();
        }
    }
}