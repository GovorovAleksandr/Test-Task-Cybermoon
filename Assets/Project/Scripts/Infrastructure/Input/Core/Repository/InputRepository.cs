using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Input.Core
{
    internal sealed class InputRepository : IInputRepository
    {
        private readonly List<IInputActionCollection2> _inputs = new();
        
        public void Add(IInputActionCollection2 input) => _inputs.Add(input);
        public void Remove(IInputActionCollection2 input) => _inputs.Remove(input);
        public void Clear() => _inputs.Clear();

        public IEnumerable<IInputActionCollection2> GetInputs() => _inputs;
    }
}