using Moq;
using Projects.Application.Queries;
using Website.Core.Abstractions.Pipeline;

namespace Projects.UnitTests.Application
{
    public class ProjectApiWebTests
    {
        private readonly IMediator mediator;


        public ProjectApiWebTests()
        {
            mediator = new Mock<IMediator>().Object;
        }

        [Fact]
        public async Task GetProjects_success()
        {
            mediator.Dispatch(new GetProjectsQuery());
        }
    }
}
