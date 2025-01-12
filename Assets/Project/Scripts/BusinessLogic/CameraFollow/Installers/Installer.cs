using CameraFollow.Core;
using Zenject;
using ZenjectInstallation.Public;

namespace CameraFollow.Installer
{
    internal sealed class Installer : ILocalContextInstaller
    {
        public BindingScene BindingScenes => BindingScene.Gameplay;

        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesTo<CameraFollowing>().FromNew().AsSingle().NonLazy();
        }
    }
}