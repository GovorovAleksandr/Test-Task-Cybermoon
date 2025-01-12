using EventBus.Events;
using EventBus.Public;
using ProjectState.Events;
using ProjectState.Public;
using Zenject;

namespace ProjectState.Core
{
    internal sealed class StateMachine :
        IAutoRegistrableEventHandler,
        IEventHandler<ChangeProjectState>
    {
        [Inject] private readonly IEventBus _eventBus;
        
        private StateBase _currentStateBase;
        
        public void Handle(ChangeProjectState eventData)
        {
            var state = eventData.StateBase;

            if (_currentStateBase != null && _currentStateBase.GetType() == state.GetType()) return;
            
            _currentStateBase = state;
            var stateType = state.GetType();
            _eventBus.Send(new SendGenericEvent(typeof(StateChanged<>), stateType, state));
        }
    }
}