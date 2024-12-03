namespace Website.Core.Abstractions.Events
{
    public interface IEventService : IAsyncDisposable
    {
        IObservable<T> OfType<T>() where T : IEvent;

        Task StartAsync();
    }
}