using Asp.Versioning;
using Asp.Versioning.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Projects.Shared.DTOs;
using System.Text.Json;
using Website.Server;

namespace Projects.FunctionalTests
{
    public class ProjectsWebApiTest : IClassFixture<ProjectsApiFixture>
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new(JsonSerializerDefaults.Web);

        public ProjectsWebApiTest(ProjectsApiFixture fixture)
        {
            var handler = new ApiVersionHandler(new QueryStringApiVersionWriter(), new ApiVersion(1.0));
            _webApplicationFactory = fixture;
            _httpClient = _webApplicationFactory.CreateDefaultClient(handler);
        }

        [Fact]
        public async Task GetProjectsTest()
        {
            //act
            var reposnse = await _httpClient.GetAsync("api/projects");

            //assert
            reposnse.EnsureSuccessStatusCode();
            var body = await reposnse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ProjectDto[]>(body, _jsonSerializerOptions);

            Assert.NotNull(result);
        }
    }
}
