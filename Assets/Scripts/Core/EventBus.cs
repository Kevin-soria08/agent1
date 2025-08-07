using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.Core
{
    /// <summary>
    /// A simple event bus to allow decoupled publishers and subscribers.
    /// </summary>
    public class EventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new Dictionary<Type, List<Delegate>>();

        /// <summary>
        /// Subscribes a callback to events of type T.
        /// </summary>
        /// <typeparam name="T">Type of event to subscribe to.</typeparam>
        /// <param name="callback">Callback invoked when an event of type T is published.</param>
        public void Subscribe<T>(Action<T> callback) where T : class
        {
            var type = typeof(T);
            if (!_subscribers.ContainsKey(type))
            {
                _subscribers[type] = new List<Delegate>();
            }

            _subscribers[type].Add(callback);
        }

        /// <summary>
        /// Unsubscribes a callback from events of type T.
        /// </summary>
        /// <typeparam name="T">Type of event to unsubscribe from.</typeparam>
        /// <param name="callback">Callback to remove.</param>
        public void Unsubscribe<T>(Action<T> callback) where T : class
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var list))
            {
                list.Remove(callback);
            }
        }

        /// <summary>
        /// Publishes an event to all subscribers of its type.
        /// </summary>
        /// <typeparam name="T">Type of the event being published.</typeparam>
        /// <param name="publishedEvent">The event instance.</param>
        public void Publish<T>(T publishedEvent) where T : class
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var list))
            {
                // Create a copy to avoid modification during iteration
                foreach (var del in list.ToList())
                {
                    if (del is Action<T> callback)
                    {
                        callback.Invoke(publishedEvent);
                    }
                }
            }
        }
    }
}
