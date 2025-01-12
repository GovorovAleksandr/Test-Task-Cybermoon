using UnityEngine.InputSystem;

namespace Input.Core
{
    internal interface IInputRepository : IReadOnlyInputRepository
    {
        void Add(IInputActionCollection2 input);
        void Remove(IInputActionCollection2 input);
        void Clear();
    }
}