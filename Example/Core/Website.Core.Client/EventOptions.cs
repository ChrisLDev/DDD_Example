namespace Website.Core.Client
{
    using System.Reflection;

    public class EventOptions
    {
        public Assembly Assembly { get; set; } = null!;

        public Uri Hub { get; set; } = null!;
    }
}