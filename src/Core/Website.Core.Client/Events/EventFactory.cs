namespace Website.Core.Client.Events
{
	using Abstractions.Events;
	using Extensions;
	using System.Reflection;
	using System.Text.Json;

	public class EventFactory(Assembly assembly) : IEventFactory
    {
        private readonly Dictionary<string, Type> _types = assembly.GetEvents();

        public IIntegrationEvent? Create(string json)
        {
            var document = JsonDocument.Parse(json);

            if (!document.RootElement.TryGetProperty(nameof(IIntegrationEvent.Type), out var element))
            {
                return default(IIntegrationEvent);
            }

            if (!_types.TryGetValue(element.GetString() ?? string.Empty, out var type))
            {
                return default(IIntegrationEvent);
            }

            return (IIntegrationEvent)document.Deserialize(type)!;
        }
    }
}