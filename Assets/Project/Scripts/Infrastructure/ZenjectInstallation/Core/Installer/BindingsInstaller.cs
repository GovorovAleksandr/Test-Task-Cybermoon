using System;
using System.Linq;
using TypeFinding.Public;
using Zenject;
using ZenjectInstallation.Public;

namespace ZenjectInstallation.Core
{
    internal abstract class BindingsInstaller<T> : MonoInstaller where T : class, IContextInstaller
    {
        protected virtual bool CanBind(T installer) => true;
        
        public override void InstallBindings()
        {
            if (!ChildTypeFinder.TryGetChildTypes(typeof(T), out var childTypes)) return;

            var installers = childTypes
                .Select(type => Activator.CreateInstance(type) as T).ToList();

            var sortedInstallers = installers.OrderBy(installer =>
            {
                var type = installer.GetType();
                if(type.GetInterface(nameof(IPrioritizedContextInstaller)) == null) return 0;
                
                var prioritizedInstaller = (IPrioritizedContextInstaller)installer;
                
                return prioritizedInstaller.Priority;
            }).ToList();
            
            foreach (var installer in sortedInstallers)
            {
                if (!CanBind(installer)) continue;
                installer.InstallBindings(Container);
            }
        }
    }
}