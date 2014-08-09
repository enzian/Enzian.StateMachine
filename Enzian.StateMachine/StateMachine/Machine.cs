using System.Collections.Generic;
using System.Linq;
using StateMachine;

namespace Enzian.StateMachine
{
    public class Machine<TEventType>
    {
        public Machine()
        {
            StartState = new State<TEventType>(StateType.Start);
            CurrentState = StartState;
            EndState = new State<TEventType>(StateType.End);
            States = new List<State<TEventType>>(){ StartState, EndState};
        }

        public List<State<TEventType>> States { get; private set; }

        public State<TEventType> StartState { get; private set; }

        public State<TEventType> EndState { get; private set; }

        public State<TEventType> CurrentState { get; private set; }

        public State<TEventType> Step(TEventType argument)
        {
            var nextStateCandidates = CurrentState.Transitions.Where(s => s.If(argument));
            if (nextStateCandidates.Count() != 1)
            {
                if (!nextStateCandidates.Any())
                {
                    throw new InvalidStateTransitionException(
                        "There are no transition from this state that lead to a next state using the given the current transition argument");
                }

                if (nextStateCandidates.Count() > 1)
                {
                    throw new InvalidStateTransitionException(
                        "There are multiple transitions that match the given transition argument.");
                }
            }

            var transition = nextStateCandidates.First();
            var nextState = transition.GoTo;
            
            if (transition.Then != null)
            {
                transition.Then(argument);
            }

            CurrentState = nextState;
            return CurrentState;
        }
    }
}