namespace Website.Core.Pipeline
{
    using System.Reactive.Linq;
    using System.Reactive.Subjects;

    public sealed class MessageObserver : IDisposable
    {
        private readonly Subject<object> _sink = new();

        /// <inheritdoc />
        public void Dispose()
        {
            _sink.Dispose();
        }

        public IObservable<T> OfType<T>()
        {
            return _sink.OfType<T>().AsObservable();
        }

        public void Publish<T>(T message)
        {
            if (message != null)
            {
                _sink.OnNext(message);
            }
        }
    }
}