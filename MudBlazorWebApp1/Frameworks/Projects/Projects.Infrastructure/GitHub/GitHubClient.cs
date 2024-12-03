using Projects.Infrastructure.GitHub.Models;
using System.Net.Http.Json;

namespace Projects.Infrastructure.GitHub
{
	public class GitHubClient(HttpClient client)
	{
		public Task<Repository[]?> GetRepositories(string username, CancellationToken token = default)
		{
			return client.GetFromJsonAsync<Repository[]>($"users/{username}/repos", token);
		}
	}
}
