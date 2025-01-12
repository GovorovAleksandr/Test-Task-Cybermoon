using System;
using UnityEngine.SceneManagement;
using ZenjectInstallation.Public;

namespace ZenjectInstallation.Core
{
    internal sealed class LocalBindingsInstaller : BindingsInstaller<ILocalContextInstaller>
    {
        protected override bool CanBind(ILocalContextInstaller installer)
        {
            var bindingScenes = installer.BindingScenes;
            
            return
                bindingScenes.HasFlag(BindingScene.Any) ||
                bindingScenes.HasFlag(GetActiveSceneNameAsBindingScene());
        }
        
        private static BindingScene GetActiveSceneNameAsBindingScene() =>
            (BindingScene)Enum.Parse(typeof(BindingScene), GetActiveSceneName());
        
        private static string GetActiveSceneName() => SceneManager.GetActiveScene().name;
    }
}