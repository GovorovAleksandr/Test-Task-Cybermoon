using System;
using RequestBus.Core;
using Zenject;

namespace RequestBus
{
    public interface IRequestHandler<TRequest> : IBaseRequestHandler, IInitializable, IDisposable
        where TRequest : IRequest
    {
        void Handle(ref TRequest request);
    }
}