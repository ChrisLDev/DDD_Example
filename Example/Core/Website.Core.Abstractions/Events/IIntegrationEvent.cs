namespace Website.Core.Abstractions.Events
{
    public interface IIntegrationEvent : IEvent
    {
        string Type { get; }
    }
}