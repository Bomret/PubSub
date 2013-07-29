using System;
using Machine.Specifications;

namespace PubSub.Tests
{
    [Subject(typeof (PubSub))]
    public class When_I_publish_a_message_to_two_subscribed_actions_that_either_handle_string_or_int_messages
    {
        private static PubSub _sut;
        private static int _message;
        private static Action<string> _actionOne;
        private static Action<int> _actionTwo;
        private static string _receivedMessageOne;
        private static int _receivedMessageTwo;

        private Establish context = () =>
        {
            _sut = new PubSub();

            _message = 123;

            _actionOne = s => _receivedMessageOne = s;
            _actionTwo = i => _receivedMessageTwo = i;

            _sut.Subscribe(_actionOne);
            _sut.Subscribe(_actionTwo);
        };

        private Because of = () => _sut.Publish(_message);

        private It should_not_receive_the_published_message_in_the_string_subscriber =
            () => _receivedMessageOne.ShouldBeNull();

        private It should_receive_the_published_message_in_the_int_subscriber =
            () => _receivedMessageTwo.ShouldEqual(_message);
    }
}