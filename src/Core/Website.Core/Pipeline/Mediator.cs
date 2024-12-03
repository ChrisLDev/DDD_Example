namespace Website.Core.Pipeline
{
    using Abstractions.Messages;
    using Abstractions.Pipeline;
    using Dispatchers;

    public sealed class Mediator : IMediator
    {
        private readonly IDispatcher _dispatcher;

        private readonly MessageObserver _observer = new();

        private readonly IMessageMiddleware _pipeline;

        public Mediator(IEnumerable<IMessageMiddleware> middleware, IEnumerable<IDispatcher> dispatchers)
        {
            _pipeline = new Pipeline(middleware, _observer);
            _dispatcher = new CompositeDispatcher(dispatchers);
        }

        /// <inheritdoc />
        public bool CanHandle<T>(IMessage<T> message)
        {
            return _dispatcher.CanHandle(message);
        }

        public Task<T?> Dispatch<T>(IMessage<T> message)
        {
            if (!CanHandle(message))
            {
                throw new ArgumentException($"Unexpected message type: {message.GetType()}");
            }

            return _pipeline.Invoke(message, _dispatcher);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _observer.Dispose();
        }

        public IObservable<T> OfType<T>()
        {
            return _observer.OfType<T>();
        }
    }
}