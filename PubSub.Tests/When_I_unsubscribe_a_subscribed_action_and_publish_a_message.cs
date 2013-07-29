using System;
using Machine.Specifications;

namespace PubSub.Tests
{
    [Subject(typeof (PubSub))]
    public class When_I_unsubscribe_a_subscribed_action_and_publish_a_message
    {
        private static PubSub _sut;
        private static string _message;
        private static string _receivedMessage;
        private static Action<string> _action;

        private Establish context = () =>
        {
            _sut = new PubSub();

            _message = "hello";
            _action = s => _receivedMessage = s;

            _sut.Subscribe(_action);
            _sut.Unsubscribe(_action);
        };

        private Because of = () => _sut.Publish(_message);

        private It should_not_receive_the_published_message = () => _receivedMessage.ShouldBeNull();
    }
}