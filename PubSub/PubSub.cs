using System;
using System.Collections.Generic;
using System.Linq;
using PubSub.Contracts;

namespace PubSub
{
    public sealed class PubSub : IPublishToSubscribers
    {
        private readonly Dictionary<Type, IList<IHandleSubscriptions>> _subscribers;

        public PubSub()
        {
            _subscribers = new Dictionary<Type, IList<IHandleSubscriptions>>();
        }

        public void Subscribe<TMessage>(Action<TMessage> action)
        {
            var messageType = typeof (TMessage);
            var handler = new SubscriptionHandler<TMessage>(action);

            if (!_subscribers.ContainsKey(messageType))
            {
                _subscribers.Add(messageType, new List<IHandleSubscriptions>());
            }

            if (_subscribers[messageType].Contains(handler))
            {
                return;
            }

            _subscribers[messageType].Add(handler);
        }

        public void Unsubscribe<TMessage>(Action<TMessage> action)
        {
            var messageType = typeof (TMessage);

            if (!_subscribers.ContainsKey(messageType))
            {
                return;
            }

            var comparator = new SubscriptionHandler<TMessage>(action);
            var handler = _subscribers[messageType].FirstOrDefault(h => h.Equals(comparator));

            if (handler != null)
            {
                _subscribers[messageType].Remove(handler);
            }
        }

        public void Publish<TMessage>(TMessage message)
        {
            var messageType = typeof (TMessage);

            if (!_subscribers.ContainsKey(messageType))
            {
                return;
            }

            _subscribers[messageType].ToList()
                                     .ForEach(s => s.Handle(message));
        }
    }
}