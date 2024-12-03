namespace Website.Core.Pipeline
{
    using Abstractions.Messages;
    using Abstractions.Pipeline;
    using Dispatchers;

    public class Pipeline(IEnumerable<IMessageMiddleware> middleware, MessageObserver observer) : IPipeline
    {
        private readonly IEnumerable<IMessageMiddleware> _middleware = middleware;

        private readonly MessageObserver _observer = observer;

        public Task<T?> Invoke<T>(IMessage<T> message, IDispatcher inner)
        {
            IDispatcher current = new ObservableDispatcher(_observer, inner);

            foreach (var middleware in _middleware)
            {
                current = new MiddlewareDispatcher(middleware, current);
            }

            return current.Dispatch(message);
        }
    }
}