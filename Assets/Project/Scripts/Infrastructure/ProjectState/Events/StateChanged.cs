using EventBus.Public;
using ProjectState.Public;

namespace ProjectState.Events
{
    public struct StateChanged<T> : IEvent where T : StateBase
    {
        public readonly T State;

        public StateChanged(T state)
        {
            State = state;
        }
    }
}