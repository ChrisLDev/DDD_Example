using Website.Core.Abstractions.Messages;

namespace Projects.Shared.Events
{

	public class ProjectDeletedEvent : IntegrationEvent
	{
		public int Id { get; set; }
	}

	public class ProjectUpdatedEvent : IntegrationEvent
	{
		public int Id { get; set; }
	}

	public class ProjectCreatedEvent : IntegrationEvent
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }
	}
}
