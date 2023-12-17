namespace ChatTest.Services.EventBus
{
    public interface IEventBus<T>
    {
        void Publish(T message);

        IDisposable Subscribe(Action<T> action);
    }
}
