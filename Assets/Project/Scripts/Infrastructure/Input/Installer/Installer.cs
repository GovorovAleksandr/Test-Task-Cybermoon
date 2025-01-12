using Input.Core;
using Zenject;
using ZenjectInstallation.Public;

namespace Input.Installers
{
    internal sealed class Installer : ILocalContextInstaller, IPrioritizedContextInstaller
    {
        public int Priority => 100;

        public BindingScene BindingScenes => BindingScene.Any;

        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesTo<InputInitializer>().FromNew().AsSingle().NonLazy();
            container.BindInterfacesTo<InputRepository>().FromNew().AsSingle().NonLazy();
            container.BindInterfacesTo<InputHandlersBinder>().FromNew().AsSingle().NonLazy();
        }
    }
}