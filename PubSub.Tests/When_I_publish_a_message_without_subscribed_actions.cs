using System;
using Machine.Specifications;

namespace PubSub.Tests
{
    [Subject(typeof (PubSub))]
    public class When_I_publish_a_message_without_subscribed_actions
    {
        private static PubSub _sut;
        private static Exception _error;
        private static string _message;

        private Establish context = () =>
        {
            _sut = new PubSub();

            _message = "hello";
        };

        private Because of = () => _error = Catch.Exception(() => _sut.Publish(_message));

        private It should_not_throw_an_exception = () => _error.ShouldBeNull();
    }
}