using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Input.Core
{
    internal interface IReadOnlyInputRepository
    {
        IEnumerable<IInputActionCollection2> GetInputs();
    }
}