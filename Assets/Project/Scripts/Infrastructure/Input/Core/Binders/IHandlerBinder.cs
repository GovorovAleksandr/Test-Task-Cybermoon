using Input.Public;
using UnityEngine.InputSystem;

namespace Input.Core
{
    internal interface IHandlerBinder<in THandler, TInputAction>
        where THandler : IInputHandler 
        where TInputAction : IInputActionCollection2
    {
        void Bind(THandler handler, TInputAction input);
        void Unbind(THandler handler, TInputAction input);
    }
}