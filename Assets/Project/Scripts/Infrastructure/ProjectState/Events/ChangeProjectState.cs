using EventBus.Public;
using ProjectState.Public;

namespace  ProjectState.Events
{
    public struct ChangeProjectState : IEvent
    {
        public readonly StateBase StateBase;

        public ChangeProjectState(StateBase stateBase)
        {
            StateBase = stateBase;
        }
    }
}