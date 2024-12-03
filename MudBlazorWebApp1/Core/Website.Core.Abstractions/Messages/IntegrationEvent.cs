namespace Website.Core.Abstractions.Messages
{
    using Events;

    public abstract class IntegrationEvent : IIntegrationEvent
    {
        /// <inheritdoc />
        public string Type => GetType().Name;
    }
}