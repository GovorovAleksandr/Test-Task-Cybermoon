using EventBusAutoRegistration.Core;
using Zenject;
using ZenjectInstallation.Public;

namespace EventBusAutoRegistration.Installer
{
    internal sealed class RegistrationInstaller : ILocalContextInstaller, IPrioritizedContextInstaller
    {
        public BindingScene BindingScenes => BindingScene.Any;

        public int Priority => 125;

        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesTo<Registrator>().FromNew().AsSingle().NonLazy();
        }
    }
}