
namespace Website.Core.Server.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Website.Core.Abstractions.Events;
    using Website.Core.Abstractions.Pipeline;
    using Website.Core.Dispatchers;
    using Website.Core.Events;
    using Website.Core.Pipeline;
    using Website.Core.Server.Dispatchers;

    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IMediator, Mediator>();

            // CQRS
            services.AddTransient<IDispatcher, CommandDispatcher>();
            services.AddTransient<IDispatcher, QueryDispatcher>();

            // Events
            services.AddTransient<IDispatcher, DomainEventDispatcher>();
            services.AddTransient<IDispatcher, IntegrationEventDispatcher>();

            services.AddTransient<IEventNotifier, EventNotifier>();

            return services;
        }
    }
}