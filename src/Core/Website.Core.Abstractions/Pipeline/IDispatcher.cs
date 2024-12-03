namespace Website.Core.Abstractions.Pipeline
{
    using Messages;

    public interface IDispatcher
    {
        bool CanHandle<T>(IMessage<T> message);

        Task<T?> Dispatch<T>(IMessage<T> message);
    }
}