
namespace Website.Core.Events
{
    using Abstractions.Events;
    using Abstractions.Models;
    using Abstractions.Pipeline;

    public class EventNotifier(IMediator mediator) : IEventNotifier
    {
        private readonly IMediator _mediator = mediator;

        /// <inheritdoc />
        public void Raise(Aggregate aggregate)
        {
            if (aggregate?.Events.Any() ?? false)
            {
                foreach (var e in aggregate.Events)
                {
                    _mediator.Dispatch(e);
                }

                aggregate.Events.Clear();
            }
        }
    }
}