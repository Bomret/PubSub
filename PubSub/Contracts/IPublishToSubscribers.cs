using System;

namespace PubSub.Contracts
{
    public interface IPublishToSubscribers
    {
        void Subscribe<T>(Action<T> action);
        void Unsubscribe<T>(Action<T> action);
        void Publish<T>(T message);
    }
}