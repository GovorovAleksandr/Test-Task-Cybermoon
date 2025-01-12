using System;
using System.Collections.Generic;
using EventBus.Events;
using EventBus.Public;
using MonoReferencing.Public;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace MonoReferencing.Core
{
    internal sealed class ReferenceInstaller : IInitializable
    {
        private const string ErrorMessage =
            "The data from this reference has not been installed because data of this type has already been installed by another object";
        
        [Inject] private readonly IEventBusSender _eventBus;
        
        private readonly List<Type> _installedTypes = new();
        
        public void Initialize()
        {
            var references =
                Object.FindObjectsByType<MonoReference>
                    (FindObjectsInactive.Exclude,FindObjectsSortMode.None);

            foreach (var monoReference in references)
            {
                var monoReferenceType = monoReference.GetType();

                while ((monoReferenceType.IsGenericType
                           ? monoReferenceType.GetGenericTypeDefinition()
                           : monoReferenceType) != typeof(MonoReference<>))
                {
                    monoReferenceType = monoReferenceType.BaseType;
                }
                
                var dataType = monoReferenceType.GenericTypeArguments[0];
                
                if (_installedTypes.Contains(dataType))
                {
                    Debug.LogError(ErrorMessage);
                    return;
                }
                
                _eventBus.Send(new SendGenericEvent(typeof(MonoReferenceLoaded<>), dataType, monoReference.Data));
                _installedTypes.Add(dataType);
            }
        }
    }
}