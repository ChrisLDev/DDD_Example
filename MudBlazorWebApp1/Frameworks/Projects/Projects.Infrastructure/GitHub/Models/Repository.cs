using System.Text.Json.Serialization;

namespace Projects.Infrastructure.GitHub.Models
{
	public class Repository
	{
		[JsonPropertyName("id")]
		public int Id { get; set; } = -1;

		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		[JsonPropertyName("description")]
		public string Description { get; set; } = string.Empty;

		[JsonPropertyName("html_url")]
		public string Link { get; set; } = string.Empty;
	}
}
