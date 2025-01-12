using System;
using EventBus.Events;
using EventBus.Public;
using UnityEngine;
using Zenject;

namespace ResourceLoading
{
    internal sealed class ResourceLoader : IInitializable, IDisposable
    {
        private const string Path = "Configs/";
        
        [Inject] private readonly IEventBus _eventBus;
        
        public void Initialize()
        {
            var resources = Resources.LoadAll(Path);

            foreach (var resource in resources)
            {
                var resourceType = resource.GetType();
                _eventBus.Send(new SendGenericEvent(typeof(ResourceLoaded<>), resourceType, resource));
            }
            
            Resources.UnloadUnusedAssets();
        }

        public void Dispose() => Resources.UnloadUnusedAssets();
    }
}