namespace ChatTest.Services.EventBus
{
    public class EventBus<T> : IEventBus<T>
    {
        private readonly List<Subscriber> _subscribers;

        public EventBus()
        {
            _subscribers = new List<Subscriber>();
        }

        public void Publish(T message)
        {
            lock (this)
            {
                foreach (var subscriber in _subscribers)
                {
                    subscriber.Action(message);
                }
            }
        }

        public IDisposable Subscribe(Action<T> action)
        {
            var subscriber = new Subscriber(this, action);
            lock (this)
            {
                _subscribers.Add(subscriber);
            }
            return subscriber;
        }

        private class Subscriber : IDisposable
        {
            private EventBus<T> _bus;

            public Action<T> Action { get; set; }

            public Subscriber(EventBus<T> bus, Action<T> action)
            {
                _bus = bus;
                Action = action;
            }

            public void Dispose()
            {
                lock (_bus)
                {
                    _bus._subscribers.Remove(this);
                }
            }
        }          
    }
}
