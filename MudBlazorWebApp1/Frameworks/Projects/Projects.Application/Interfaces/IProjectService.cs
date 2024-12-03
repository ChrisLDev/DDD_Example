using Projects.Domain.Models;

namespace Projects.Application.Interfaces
{
	public interface IProjectService
	{
		Task<Project[]> GetProjects(string username, CancellationToken token = default);
	}
}
