using System;
using System.Reflection;
using TypeFinding.Public;
using UnityEngine.InputSystem;
using Zenject;

namespace Input.Core
{
    internal sealed class InputInitializer : IInitializable, IDisposable
    {
        private static Assembly Assembly => Assembly.GetAssembly(typeof(InputRepository));
        
        [Inject] private readonly IInputRepository _repository;

        public void Initialize()
        {
            if (!ChildTypeFinder.TryGetChildTypes(typeof(IInputActionCollection2),
                    out var types, assembly: Assembly)) return;

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type) as IInputActionCollection2;
                instance.Enable();
                _repository.Add(instance);
            }
        }

        public void Dispose()
        {
            foreach (var inputAction in _repository.GetInputs())
            {
                inputAction.Disable();
            }
            
            _repository.Clear();
        }
    }
}