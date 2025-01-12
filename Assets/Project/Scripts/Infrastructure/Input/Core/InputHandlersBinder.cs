using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Input.Public;
using TypeFinding.Public;
using UnityEngine.InputSystem;
using Zenject;

namespace Input.Core
{
    internal sealed class InputHandlersBinder : IInitializable, IDisposable
    {
        [Inject] private readonly List<IInputHandler> _handlers;
        [Inject] private readonly IReadOnlyInputRepository _repository;
        
        private static Assembly Assembly => Assembly.GetAssembly(typeof(InputHandlersBinder));

        public void Initialize() => ProcessHandlers(BindHandler);
        public void Dispose() => ProcessHandlers(UnbindHandler);

        private void ProcessHandlers(Action<IInputHandler, IEnumerable<Type>> handlerAction)
        {
            if (!ChildTypeFinder.TryGetChildTypes(typeof(IHandlerBinder<,>), out var binderTypes, assembly: Assembly))
                return;

            foreach (var handler in _handlers)
            {
                handlerAction(handler, binderTypes);
            }
        }

        private void BindHandler(IInputHandler handler, IEnumerable<Type> binderTypes) =>
            ProcessAtAllBinders(handler, binderTypes, HandlerBinderHelper.Bind);
        private void UnbindHandler(IInputHandler handler, IEnumerable<Type> binderTypes) =>
            ProcessAtAllBinders(handler, binderTypes, HandlerBinderHelper.Unbind);

        private void ProcessAtAllBinders(IInputHandler inputHandler, IEnumerable<Type> binderTypes, 
            Action<IInputHandler, Type, IInputActionCollection2> binderAction)
        {
            var inputHandlerType = inputHandler.GetType();

            foreach (var binderType in binderTypes)
            {
                if (!HandlerBinderHelper.IsValidBinder(binderType, inputHandlerType)) continue;

                ProcessWithActions(inputHandler, binderType, binderAction);
            }
        }

        private void ProcessWithActions(IInputHandler inputHandler, Type binderType, 
            Action<IInputHandler, Type, IInputActionCollection2> binderAction)
        {
            var actionType = GetActionType(binderType);
            var actions = GetActions(actionType);

            foreach (var action in actions)
            {
                binderAction(inputHandler, binderType, action);
            }
        }

        private IEnumerable<IInputActionCollection2> GetActions(Type actionType) =>
            _repository.GetInputs().Where(action => actionType.IsAssignableFrom(action.GetType()));

        private static Type GetActionType(Type binderType)
        {
            return binderType.GetInterfaces()
                .FirstOrDefault(@interface => 
                    @interface.IsGenericType && 
                    @interface.GetGenericTypeDefinition() == typeof(IHandlerBinder<,>))
                ?.GenericTypeArguments[1];
        }
    }
}