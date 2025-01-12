using ProjectState.Core;
using Zenject;
using ZenjectInstallation.Public;

namespace ProjectState.Installers
{
    internal sealed class Installer : IGlobalContextInstaller
    {
        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesTo<StateMachine>().FromNew().AsSingle().NonLazy();
        }
    }
}