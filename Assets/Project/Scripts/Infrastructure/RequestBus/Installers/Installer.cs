using Zenject;
using ZenjectInstallation.Public;

namespace RequestBus.Installers
{
    public class Installer : IGlobalContextInstaller, IPrioritizedContextInstaller
    {
        public int Priority => -100;

        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesTo<Core.RequestBus>().FromNew().AsSingle().NonLazy();
        }
    }
}