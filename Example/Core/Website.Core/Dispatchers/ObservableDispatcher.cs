
namespace Website.Core.Dispatchers
{
    using Abstractions.Messages;
    using Abstractions.Pipeline;
    using Pipeline;

    public class ObservableDispatcher(MessageObserver observer, IDispatcher dispatcher) : IDispatcher
    {
        private readonly IDispatcher _dispatcher = dispatcher;
        private readonly MessageObserver _observer = observer;

        /// <inheritdoc />
        public bool CanHandle<T>(IMessage<T> message)
        {
            return true;
        }

        /// <inheritdoc />
        public async Task<T?> Dispatch<T>(IMessage<T> message)
        {
            var result = await _dispatcher.Dispatch(message);

            _observer.Publish(message);

            return result;
        }
    }
}