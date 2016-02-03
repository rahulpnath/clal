using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineApplicationLauncherModel
{
    public static class DomainEvents
    {
        private static Object _lock = new Object();
        private static ConcurrentDictionary<Type, HashSet<WeakReference>> subscribers =
            new ConcurrentDictionary<Type, HashSet<WeakReference>>();

        public static void ClearAllSubscriptions()
        {
            lock(_lock)
            {
                subscribers = new ConcurrentDictionary<Type, HashSet<WeakReference>>();
            }
        }

        public static void Subscribe<T>(IEventHandler<T> subscriber) where T : IEvent
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            lock(_lock)
            {
                if (!subscribers.ContainsKey(typeof(T)))
                    subscribers[typeof(T)] = new HashSet<WeakReference>();

                subscribers[typeof(T)].Add(new WeakReference(subscriber));
            }
        }

        public static void Publish<T>(T eventMessage) where T : IEvent
        {
            if (eventMessage == null)
                throw new ArgumentNullException(nameof(eventMessage));

            var eventType = typeof(T);
            if (!subscribers.ContainsKey(eventType))
                return;

            var handlers = subscribers[typeof(T)];
            foreach (var handler in handlers)
                RaiseEvent(eventMessage, handler);
        }

        private static void RaiseEvent<T>(T eventMessage, WeakReference handler) where T : IEvent
        {
            var eventHandler = handler.Target as IEventHandler<T>;
            if (eventHandler != null)
                eventHandler.Handle(eventMessage);
        }
    }
}
