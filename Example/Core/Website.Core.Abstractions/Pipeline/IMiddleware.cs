namespace Website.Core.Abstractions.Pipeline
{
    using Messages;

    public interface IMessageMiddleware
    {
        Task<T?> Invoke<T>(IMessage<T> message, IDispatcher inner);
    }
}