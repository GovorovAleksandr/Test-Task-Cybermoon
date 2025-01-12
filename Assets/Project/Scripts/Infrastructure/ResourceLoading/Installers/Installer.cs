using Zenject;
using ZenjectInstallation.Public;

namespace ResourceLoading.Installers
{
    internal sealed class Installer : ILocalContextInstaller, IPrioritizedContextInstaller
    {
        public BindingScene BindingScenes => BindingScene.Any;

        public int Priority => 300;

        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesTo<ResourceLoader>().FromNew().AsSingle().NonLazy();
        }
    }
}