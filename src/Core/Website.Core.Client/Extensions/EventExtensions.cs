namespace Website.Core.Client.Extensions
{
	using Abstractions.Events;
	using System.Reflection;

	public static class EventExtensions
    {
        public static Dictionary<string, Type> GetEvents(this Assembly assembly)
        {
            var types = new Dictionary<string, Type>();

            foreach (var type in assembly.GetTypes().Where(IsEvent))
            {
                try
                {
                    var instance = (IIntegrationEvent)Activator.CreateInstance(type)!;

                    types[instance.Type] = type;
                }
                catch
                {
                    // ignored
                }
            }

            return types;
        }

        private static bool IsEvent(Type type)
        {
            return typeof(IIntegrationEvent).IsAssignableFrom(type) && type is
            {
                IsInterface: false,
                IsAbstract: false
            };
        }
    }
}