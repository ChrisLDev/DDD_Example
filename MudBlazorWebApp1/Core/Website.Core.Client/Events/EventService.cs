namespace Website.Core.Client.Events
{
	using Abstractions.Events;
	using Microsoft.AspNetCore.SignalR.Client;
	using System.Reactive.Linq;
	using System.Reactive.Subjects;

	public class EventService : IEventService
    {
        private readonly HubConnection _connection;

        private readonly EventFactory _factory;

        private readonly Subject<object> _sink = new();

        public EventService(EventFactory factory, Uri url)
        {
            _factory = factory;

            _connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string>("Handle", Handle);
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            await _connection.DisposeAsync();
        }

        /// <inheritdoc />
        public IObservable<T> OfType<T>() where T : IEvent
        {
            return _sink.OfType<T>().AsObservable();
        }

        public async Task StartAsync()
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                try
                {
                    await _connection.StartAsync();

                    Console.WriteLine("Connected");
                }
                catch
                {
                    // ignored
                }
            }
        }

        private Task Handle(string json)
        {
            try
            {
                Console.WriteLine(json);

                if (_factory.Create(json) is { } e)
                {
                    _sink.OnNext(e);
                }
            }
            catch
            {
                // ignored
            }

            return Task.CompletedTask;
        }
    }
}