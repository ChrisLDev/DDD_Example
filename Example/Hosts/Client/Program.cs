using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Projects.Shared.Common;
using Website.Core.Client.Extensions;

namespace Website.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddMudServices();
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

			builder.Services.AddCore(opt =>
			{
				opt.Hub = new Uri(new Uri(builder.HostEnvironment.BaseAddress), "events");
				opt.Assembly = typeof(Constants).Assembly;
			});

			await builder.Build().RunAsync();
		}
	}
}