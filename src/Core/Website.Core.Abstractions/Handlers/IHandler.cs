namespace Website.Core.Abstractions.Handlers
{
    using Messages;

    public interface IHandler<in TMessage, TResult> where TMessage : IMessage<TResult>
    {
        Task<TResult> Handle(TMessage message);
    }
}