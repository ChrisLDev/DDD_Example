
namespace Website.Core.Dispatchers
{
    using Abstractions.Messages;
    using Abstractions.Pipeline;

    public class MiddlewareDispatcher(IMessageMiddleware messageMiddleware, IDispatcher inner) : IDispatcher
    {
        private readonly IDispatcher _inner = inner;
        private readonly IMessageMiddleware _messageMiddleware = messageMiddleware;

        /// <inheritdoc />
        public bool CanHandle<T>(IMessage<T> message)
        {
            return _inner.CanHandle(message);
        }

        /// <inheritdoc />
        public Task<T?> Dispatch<T>(IMessage<T> message)
        {
            return _messageMiddleware.Invoke(message, _inner);
        }
    }
}