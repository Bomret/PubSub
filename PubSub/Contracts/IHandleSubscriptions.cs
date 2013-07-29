namespace PubSub.Contracts
{
    internal interface IHandleSubscriptions
    {
        void Handle(object message);
    }
}