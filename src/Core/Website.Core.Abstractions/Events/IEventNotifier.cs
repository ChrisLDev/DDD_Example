namespace Website.Core.Abstractions.Events
{
    using Models;

    public interface IEventNotifier
    {
        void Raise(Aggregate aggregate);
    }
}