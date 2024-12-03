using MudBlazor.Services;
using Projects.Application.Handlers;
using Projects.Application.Interfaces;
using Projects.Application.Queries;
using Projects.Domain.Events;
using Projects.Infrastructure.GitHub;
using Projects.Infrastructure.RateLimiting;
using Projects.Infrastructure.Services;
using Projects.Shared.DTOs;
using System.Reactive;
using System.Threading.RateLimiting;
using Website.Core.Abstractions.Handlers;
using Website.Core.Server;
using Website.Core.Server.Extensions;


public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddCore();

		ConfigureServices(builder.Services);

		builder.Services.AddControllersWithViews();
		builder.Services.AddRazorPages();

		builder.Services.AddSignalR();
		builder.Services.AddAuthorization();
		builder.Services.AddHostedService<EventWorker>();

		// Add MudBlazor services
		builder.Services.AddMudServices();

		// Add services to the container.
		builder.Services.AddRazorComponents()
			.AddInteractiveWebAssemblyComponents();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseWebAssemblyDebugging();
		}
		else
		{
			app.UseExceptionHandler("/Error", createScopeForErrors: true);
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseBlazorFrameworkFiles();
		app.UseStaticFiles();

		app.UseAuthorization();

		app.MapRazorPages();
		app.MapControllers();
		app.MapFallbackToFile("index.html");

		app.MapHub<EventHub>("/events");

		app.Run();
	}

	private static void ConfigureServices(IServiceCollection services)
	{
		services.AddTransient<IHandler<GetProjectsQuery, ProjectDto[]>, GetProjectsQueryHandler>();

		services.AddTransient<IHandler<ProjectCreatedDomainEvent, Unit>, ProjectEventHandlers>();
		services.AddTransient<IHandler<ProjectUpdatedDomainEvent, Unit>, ProjectEventHandlers>();
		services.AddTransient<IHandler<ProjectDeletedDomainEvent, Unit>, ProjectEventHandlers>();

		services.AddSingleton<Projects.Domain.Models.Projects>();
		services.AddTransient<IProjectService, GitHubService>();

		services.AddSingleton<RateLimiter>(_ => new FixedWindowRateLimiter(
				new FixedWindowRateLimiterOptions
				{
					PermitLimit = 1,
					Window = TimeSpan.FromSeconds(1),
					QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
					QueueLimit = 0,
					AutoReplenishment = true
				}));
		services.AddTransient<RateLimitingHandler>();

		services.AddHttpClient<GitHubClient>(client =>
		{
			client.BaseAddress = new Uri("https://api.github.com/");
			client.DefaultRequestHeaders.Add("User-Agent", "Lehrer");
		})
		.AddHttpMessageHandler<RateLimitingHandler>();

		services.AddHostedService<ProjectWorker>();
	}
}