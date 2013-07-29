using System;
using Machine.Specifications;

namespace PubSub.Tests
{
    [Subject(typeof (PubSub))]
    public class When_I_unsubscribe_an_action_that_is_not_subscribed
    {
        private static PubSub _sut;
        private static Action<string> _action;
        private static Exception _error;
        private static string _receivedMessage;

        private Establish context = () =>
        {
            _sut = new PubSub();

            _action = s => _receivedMessage = s;
        };

        private Because of = () => _error = Catch.Exception(() => _sut.Unsubscribe(_action));

        private It should_not_throw_an_exception = () => _error.ShouldBeNull();
    }
}