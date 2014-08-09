using System.Collections.Generic;
using StateMachine;

namespace Enzian.StateMachine
{
    public class State<TEventType>
    {
        public State()
        {
            Type = StateType.Running;
            Transitions = new List<Transition<TEventType>>();
        }

        public State(StateType type)
        {
            Type = type;
            Transitions = new List<Transition<TEventType>>();
        }

        public StateType Type { get; set; }

        public List<Transition<TEventType>> Transitions { get; set; }
    }
}
