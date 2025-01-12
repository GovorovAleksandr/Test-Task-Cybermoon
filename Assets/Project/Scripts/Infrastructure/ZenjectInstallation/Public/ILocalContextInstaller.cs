using ZenjectInstallation.Core;

namespace ZenjectInstallation.Public
{
    public interface ILocalContextInstaller : IContextInstaller
    {
        BindingScene BindingScenes { get; }
    }
}