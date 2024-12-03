namespace Website.Core.Dispatchers
{
	using Abstractions.Messages;
	using Abstractions.Pipeline;

	public class CompositeDispatcher(IEnumerable<IDispatcher> dispatchers) : IDispatcher
    {
        private readonly IEnumerable<IDispatcher> _dispatchers = dispatchers;

        /// <inheritdoc />
        public bool CanHandle<T>(IMessage<T> message)
        {
            return _dispatchers.Any(x => x.CanHandle(message));
        }

        /// <inheritdoc />
        public async Task<T?> Dispatch<T>(IMessage<T> message)
        {
            foreach (var dispatcher in _dispatchers)
            {
                if (dispatcher.CanHandle(message))
                {
                    return await dispatcher.Dispatch(message);
                }
            }

            return default;
        }
    }
}