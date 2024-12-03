
namespace Website.Core.Server.Dispatchers
{
    using System.Text.Json;
    using Abstractions.Events;
    using Abstractions.Messages;
    using Abstractions.Pipeline;
    using Microsoft.AspNetCore.SignalR;

    public class IntegrationEventDispatcher(IHubContext<EventHub> context) : IDispatcher
    {
        private readonly IHubContext<EventHub> _context = context;

        /// <inheritdoc />
        public bool CanHandle<T>(IMessage<T> message)
        {
            return message is IIntegrationEvent;
        }

        /// <inheritdoc />
        public async Task<T?> Dispatch<T>(IMessage<T> message)
        {
            await _context.Clients.All.SendAsync("Handle", JsonSerializer.Serialize(message));

            return default;
        }
    }
}