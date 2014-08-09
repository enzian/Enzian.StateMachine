using System;
using System.Linq;
using Enzian.StateMachine;
using FluentAssertions;
using NUnit.Framework;

namespace StateMachine.Test
{
    [TestFixture]
    public class SimpleStateMachineTest
    {
        public Machine<bool> Subject { get; set; }

        [SetUp]
        public void CreateSimpleStateMachine()
        {
            Subject = new Machine<bool>();
            Subject.StartState.Transitions.Add(new Transition<bool>()
            {
                If = arg => arg,
                Then = arg => { },
                GoTo = Subject.EndState
            });

            Subject.StartState.Transitions.Add(new Transition<bool>()
            {
                If = arg => !arg,
                Then = arg => { },
                GoTo = Subject.StartState
            });
        }

        [Test]
        public void TestInitialMachineState()
        {
            Subject.StartState.Should().Be(Subject.CurrentState);
            Subject.CurrentState.Should().NotBeNull();
        }

        [Test]
        public void TestStepToEnd()
        {
            Subject.Step(true);
            Subject.CurrentState.Should().Be(Subject.EndState);
        }

        [Test]
        public void TestStepWithEmptyThen()
        {
            Subject.StartState.Transitions.First(t => t.GoTo == Subject.EndState).Then = null;

            Action act = () => Subject.Step(true);
            act.ShouldNotThrow();
        }

        [Test]
        public void Test_OneWayStates()
        {
            Subject.StartState.Transitions.Clear();
            Action act = () => Subject.Step(true);
            act.ShouldThrow<InvalidStateTransitionException>();
        }
    }
}