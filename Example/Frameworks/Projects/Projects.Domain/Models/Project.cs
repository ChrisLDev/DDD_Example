using Website.Core.Abstractions.Models;

namespace Projects.Domain.Models
{
	public class Project : Entity<int>
	{
		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public string Link { get; set; } = string.Empty;


	}
}
