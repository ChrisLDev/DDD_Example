using Projects.Application.Interfaces;
using Projects.Domain.Models;

namespace Projects.Infrastructure.GitHub
{
	public class GitHubService(GitHubClient client) : IProjectService
	{
		public async Task<Project[]> GetProjects(string username, CancellationToken token = default)
		{
			var repos = await client.GetRepositories(username, token);

			var projects = repos?
				.Select(x => new Project(
				
					x.Id,
					x.Name,
					x.Description,
					x.Link
				))
				.ToArray();

			return projects ?? [];
		}
	}
}
