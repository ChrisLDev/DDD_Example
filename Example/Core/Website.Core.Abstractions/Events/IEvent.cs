namespace Website.Core.Abstractions.Events
{
    using System.Reactive;
    using Messages;

    public interface IEvent : IMessage<Unit>;
}