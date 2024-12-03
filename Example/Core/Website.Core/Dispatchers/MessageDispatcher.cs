
namespace Website.Core.Dispatchers
{
    using System.Reflection;
    using Abstractions.Handlers;
    using Abstractions.Messages;
    using Abstractions.Pipeline;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class MessageDispatcher(IServiceProvider services) : IDispatcher
    {
        /// <inheritdoc />
        public abstract bool CanHandle<T>(IMessage<T> message);

        /// <inheritdoc />
        public abstract Task<T?> Dispatch<T>(IMessage<T> message);

        protected static async Task<T?> Invoke<T>(object? handler, IMessage<T> message)
        {
            if (handler != null)
            {
                if (GetHandleMethod(handler, message)?.Invoke(handler, [message]) is Task<T> task)
                {
                    return await task;
                }
            }

            return default;
        }

        protected object? GetHandler<T>(Type message)
        {
            return services.GetService(typeof(IHandler<,>).MakeGenericType(message, typeof(T)));
        }

        protected IEnumerable<object?> GetHandlers<T>(Type message)
        {
            return services.GetServices(typeof(IHandler<,>).MakeGenericType(message, typeof(T)));
        }

        private static bool AcceptsType(MethodInfo method, Type message)
        {
            return method.GetParameters().Any(x => x.ParameterType == message);
        }

        private static MethodInfo? GetHandleMethod<T>(object? handler, IMessage<T> message)
        {
            return GetHandleMethods<T>(handler)?.FirstOrDefault(x => AcceptsType(x, message.GetType()));
        }

        private static IEnumerable<MethodInfo>? GetHandleMethods<T>(object? handler)
        {
            return handler?.GetType().GetMethods().Where(x => x.Name == nameof(IHandler<IMessage<T>, T>.Handle));
        }
    }
}