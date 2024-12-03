namespace Website.Core.Abstractions.Events
{
    public interface IEventFactory
    {
        IIntegrationEvent? Create(string json);
    }
}