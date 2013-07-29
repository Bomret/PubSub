using System;
using PubSub.Contracts;

namespace PubSub
{
    internal sealed class SubscriptionHandler<T> : IHandleSubscriptions
    {
        public readonly Action<T> Action;

        public SubscriptionHandler(Action<T> action)
        {
            Action = action;
        }

        public void Handle(object message)
        {
            Action((T) message);
        }

        #region Equality

        private bool Equals(SubscriptionHandler<T> other)
        {
            return Equals(Action, other.Action);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj is SubscriptionHandler<T> && Equals((SubscriptionHandler<T>) obj);
        }

        public override int GetHashCode()
        {
            return (Action != null ? Action.GetHashCode() : 0);
        }

        #endregion
    }
}