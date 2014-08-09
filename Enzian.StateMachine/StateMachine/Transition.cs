using System;

namespace Enzian.StateMachine
{
    public class Transition<TEventType>
    {
        public Func<TEventType, bool> If { get; set; }

        public Action<TEventType> Then { get; set; }

        public State<TEventType> GoTo { get; set; }
    }
}