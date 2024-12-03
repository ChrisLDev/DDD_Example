using Projects.Domain.Models;
using Website.Core.Abstractions.Events;

namespace Projects.Domain.Events
{
	public class ProjectDeletedDomainEvent : IDomainEvent
	{
		public int Id { get; set; }
	}

	public class ProjectCreatedDomainEvent : IDomainEvent
	{
		public required Project Project { get; set; }
	}

	public class ProjectUpdatedDomainEvent : IDomainEvent
	{
		public int Id { get; set; }
	}
}
