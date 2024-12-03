using Microsoft.Extensions.Hosting;
using Projects.Application.Interfaces;
using Website.Core.Abstractions.Events;

namespace Projects.Infrastructure.Services
{
	public class ProjectWorker(IProjectService service, Domain.Models.Projects _projects, IEventNotifier notifier) : BackgroundService
	{
		private static readonly TimeSpan DefaultDelay = TimeSpan.FromMinutes(1);

		protected override async Task ExecuteAsync(CancellationToken token)
		{
			do
			{
				try
				{
					await Task.Delay(TimeSpan.FromSeconds(2), token);

					var projects = await service.GetProjects("ChrisLDev", token);

					_projects.Sync(projects);

					notifier.Raise(_projects);

					await Task.Delay(DefaultDelay, token);
				}
				catch(OperationCanceledException)
				{
					break;
				}
				catch(Exception)
				{
					await Task.Delay(DefaultDelay, token);
				}
			} 
			while (!token.IsCancellationRequested);
		}
	}
}
