namespace Website.Core.Abstractions.Messages
{
    using System.Reactive;

    public interface ICommand : IMessage<Unit> { }
}