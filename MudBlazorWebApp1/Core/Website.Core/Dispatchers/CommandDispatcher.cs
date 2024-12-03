namespace Website.Core.Dispatchers
{
    using Abstractions.Messages;

    public class CommandDispatcher(IServiceProvider services) : MessageDispatcher(services)
    {
        /// <inheritdoc />
        public override bool CanHandle<T>(IMessage<T> message)
        {
            return message is ICommand;
        }

        /// <inheritdoc />
        public override async Task<T?> Dispatch<T>(IMessage<T> message) where T : default
        {
            foreach (var handler in GetHandlers<T>(message.GetType()))
            {
                await Invoke(handler, message);
            }

            return default;
        }
    }
}