using System;
using System.Linq;
using Input.Public;

namespace Input.Core
{
    internal static class HandlerBinderHelper
    {
        private const string BindMethodName = "Bind";
        private const string UnbindMethodName = "Unbind";
        
        public static void Bind(IInputHandler inputHandler, Type binderType, object inputAction) =>
            InvokeBinderMethod(inputHandler, binderType, inputAction, BindMethodName);
        
        public static void Unbind(IInputHandler inputHandler, Type binderType, object inputAction) =>
            InvokeBinderMethod(inputHandler, binderType, inputAction, UnbindMethodName);
        
        public static bool IsValidBinder(Type type, Type handlerType) =>
            type.GetInterfaces().Any(@interface =>
            {
                if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == type) return false;
                
                var genericTypes = @interface.GenericTypeArguments;
                
                return genericTypes[0].IsAssignableFrom(handlerType);
            });
        
        private static void InvokeBinderMethod(IInputHandler inputHandler, Type binderType, object inputAction, string methodName)
        {
            var baseType = binderType.IsGenericType ? binderType.GetGenericTypeDefinition() : binderType;
            
            if (baseType.IsAssignableFrom(typeof(IHandlerBinder<,>))) return;
            
            var binderInstance = Activator.CreateInstance(binderType);
            var bindMethod = binderType.GetMethod(methodName);
            bindMethod.Invoke(binderInstance, new[] { inputHandler, inputAction });
        }
    }
}