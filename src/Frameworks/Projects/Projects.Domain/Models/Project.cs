using Projects.Domain.Exceptions;
using Website.Core.Abstractions.Models;

namespace Projects.Domain.Models
{
	public class Project : Entity<int>
	{
        public Project(int id, string name, string description, string link)
        {
			if (string.IsNullOrEmpty(name))
			{
                throw new ProjectDomainException("Invalid project name");
            }

			if (string.IsNullOrEmpty(link))
			{
				throw new ProjectDomainException("Invalid project link");
			}

			Id = id;
			Name = name;
			Description = description;
			Link = link;
        }

        public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public string Link { get; set; } = string.Empty;
	}
}
