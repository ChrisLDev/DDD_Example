using Projects.Domain.Events;
using Projects.Shared.Events;
using System.Reactive;
using Website.Core.Abstractions.Handlers;
using Website.Core.Abstractions.Pipeline;

namespace Projects.Application.Handlers
{
	public class ProjectEventHandlers(IMediator mediator) :
		IHandler<ProjectCreatedDomainEvent, Unit>,
		IHandler<ProjectUpdatedDomainEvent, Unit>,
		IHandler<ProjectDeletedDomainEvent, Unit>
	{
		public Task<Unit> Handle(ProjectUpdatedDomainEvent e)
		{
			return mediator.Dispatch(new ProjectUpdatedEvent
			{
				Id = e.Id
			});
		}

		public Task<Unit> Handle(ProjectCreatedDomainEvent e)
		{
			return mediator.Dispatch(new ProjectCreatedEvent
			{
				Id = e.Project.Id,
				Name = e.Project.Name,
				Description = e.Project.Description,
				Link = e.Project.Link,
			});
		}

		public Task<Unit> Handle(ProjectDeletedDomainEvent e)
		{
			return mediator.Dispatch(new ProjectDeletedEvent
			{
				Id = e.Id
			});
		}
	}
}
