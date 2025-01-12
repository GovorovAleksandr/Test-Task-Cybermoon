using PlayerMovement.Core;
using Zenject;
using ZenjectInstallation.Public;

namespace PlayerMovement.Installer
{
    internal sealed class Installer : ILocalContextInstaller
    {
        public BindingScene BindingScenes => BindingScene.Gameplay;

        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<PlayerInput>().FromNew().AsSingle().NonLazy();
            container.BindInterfacesTo<Core.PlayerMovement>().FromNew().AsSingle().NonLazy();
        }
    }
}