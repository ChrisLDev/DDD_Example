namespace Website.Core.Abstractions.Models
{
    using Events;

    public class Aggregate
    {
		public List<IEvent> Events { get; } = [];
    }
}