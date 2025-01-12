using System;
using System.Collections.Generic;
using DataPersistence.Core;
using TypeFinding.Public;
using UnityEngine;
using Zenject;
using ZenjectInstallation.Public;

namespace DataPersistence.Installers
{
    internal sealed class Installer : ILocalContextInstaller, IPrioritizedContextInstaller
    {
        private static string SavePath => Application.persistentDataPath;
        private static readonly IFilePersistence FilePersistence = new DefaultFilePersistence(SavePath);
        private static readonly IDataSerializer DataSerializer = new JsonDataSerializer();

        public BindingScene BindingScenes => BindingScene.Any;

        public int Priority => 150;

        public void InstallBindings(DiContainer container)
        {
            if (!ChildTypeFinder.TryGetChildTypes(typeof(BaseFileNameBuilder), out var fileNameBuilderTypes))
            {
                throw new Exception("Not found any file name builder types");
            }
            
            var fileNames = new List<string>();

            foreach (var fileNameBuilderType in fileNameBuilderTypes)
            {
                var instance = Activator.CreateInstance(fileNameBuilderType) as BaseFileNameBuilder;
                fileNames.Add(instance.FileName);
                
                container.BindInterfacesTo(fileNameBuilderType).FromNew().AsSingle().NonLazy();
            }
            
            container.BindInterfacesTo<DataPersistenceHandler>().FromNew().AsSingle()
                .WithArguments(DataSerializer, FilePersistence, fileNames).NonLazy();
        }
    }
}