
namespace Website.Core.Server
{
    using System.Reactive;
    using System.Reactive.Linq;
    using System.Reactive.Threading.Tasks;
    using System.Text.Json;
    using Abstractions.Events;
    using Abstractions.Pipeline;
    using Microsoft.AspNetCore.SignalR;

    public class EventWorker(IMediator mediator, IHubContext<EventHub> context) : BackgroundService
    {
        private readonly IHubContext<EventHub> _context = context;

        private readonly IMediator _mediator = mediator;

        /// <inheritdoc />
        protected override Task ExecuteAsync(CancellationToken token)
        {
            _mediator.OfType<IIntegrationEvent>().SelectMany(Handle).Subscribe(token);

            return Task.CompletedTask;
        }

        private IObservable<Unit> Handle<T>(T e) where T : IIntegrationEvent
        {
            return _context.Clients.All.SendAsync("Handle", Serialise(e)).ToObservable();
        }

        private string Serialise(object e)
        {
            return JsonSerializer.Serialize(e);
        }
    }
}