namespace Website.Core.Abstractions.Pipeline
{
    public interface IMediator : IDispatcher, IDisposable
    {
        IObservable<T> OfType<T>();
    }
}