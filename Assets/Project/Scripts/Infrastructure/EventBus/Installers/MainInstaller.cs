using EventBus.Core;
using Zenject;
using ZenjectInstallation.Public;

namespace EventBus.Installers
{
    internal sealed class MainInstaller : IGlobalContextInstaller, IPrioritizedContextInstaller
    {
        public int Priority => -100;

        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesTo<Core.EventBus>().FromNew().AsSingle().NonLazy();
            container.BindInterfacesTo<GenericEventSender>().FromNew().AsSingle().NonLazy();
        }
    }
}