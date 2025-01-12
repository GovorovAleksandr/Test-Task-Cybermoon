using Zenject;

namespace ZenjectInstallation.Core
{
    public interface IContextInstaller
    {
        void InstallBindings(DiContainer container);
    }
}