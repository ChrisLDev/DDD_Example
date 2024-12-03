using Projects.Domain.Models;
using Projects.Shared.DTOs;
using System.Reactive.Linq;
using Website.Core.Abstractions.Handlers;
using Website.Core.Abstractions.Messages;

namespace Projects.Application.Queries
{
	public class GetProjectsQuery : IQuery<ProjectDto[]>;

	public class GetProjectsQueryHandler(Domain.Models.Projects projects) : IHandler<GetProjectsQuery, ProjectDto[]>
	{
		public async Task<ProjectDto[]> Handle(GetProjectsQuery message)
		{
			var project = await projects.Items
			  .ToObservable()
			  .Select(MapToDto)
			  .ToArray();

			return project ?? [];
		}

		private ProjectDto MapToDto(Project project)
		{
			return new ProjectDto
			{
				Id = project.Id,
				Name = project.Name,
				Description = project.Description,
				Link = project.Link,
			};
		}
	}
}
