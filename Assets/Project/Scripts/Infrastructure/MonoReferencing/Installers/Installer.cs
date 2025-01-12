using MonoReferencing.Core;
using Zenject;
using ZenjectInstallation.Public;

namespace MonoReferencing.Installers
{
    public class Installer : ILocalContextInstaller, IPrioritizedContextInstaller
    {
        public BindingScene BindingScenes => BindingScene.Any;

        public int Priority => 135;

        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesTo<ReferenceInstaller>().FromNew().AsSingle().NonLazy();
        }
    }
}