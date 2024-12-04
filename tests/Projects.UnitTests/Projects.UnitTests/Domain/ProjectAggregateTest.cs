using Projects.Domain.Exceptions;
using Projects.Domain.Models;

namespace Projects.UnitTests.Domain
{
    public class ProjectAggregateTest
    {

        [Fact]
        public void Create_project_success()
        {
            int id = 1;
            var name = "test";
            var describtion = "...";
            var link = "http//:myrepolink.com";

            var fakeProject = new Project(id, name, describtion, link);

            //Assert
            Assert.NotNull(fakeProject);
        }

        [Fact]
        public void Invalid_name()
        {
            int id = 1;
            var name = string.Empty;
            var describtion = "...";
            var link = "http//:myrepolink.com";

            Assert.Throws<ProjectDomainException>(() => new Project(id, name, describtion, link));
        }

        [Fact]
        public void Invalid_link()
        {
            int id = 1;
            var name = "test";
            var describtion = "...";
            var link = string.Empty;

            Assert.Throws<ProjectDomainException>(() => new Project(id, name, describtion, link));
        }
    }
}
