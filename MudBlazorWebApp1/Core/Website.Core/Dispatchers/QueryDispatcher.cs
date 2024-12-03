
namespace Website.Core.Dispatchers
{
    using Abstractions.Messages;

    public class QueryDispatcher(IServiceProvider services) : MessageDispatcher(services)
    {
        /// <inheritdoc />
        public override bool CanHandle<T>(IMessage<T> message)
        {
            return message is IQuery<T>;
        }

        /// <inheritdoc />
        public override Task<T?> Dispatch<T>(IMessage<T> message) where T : default
        {
            return Invoke(GetHandler<T>(message.GetType()), message);
        }
    }
}