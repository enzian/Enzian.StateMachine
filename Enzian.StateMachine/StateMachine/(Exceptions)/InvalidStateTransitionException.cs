namespace StateMachine
{
    public class InvalidStateTransitionException : StateMachineException
    {
        public InvalidStateTransitionException(string message) : base(message)
        {
        }
    }
}