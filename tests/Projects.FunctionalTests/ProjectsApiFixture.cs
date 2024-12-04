using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Website.Server;

namespace Projects.FunctionalTests
{
    public sealed class ProjectsApiFixture : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly IHost _app;

        public ProjectsApiFixture()
        {
            var options = new DistributedApplicationOptions { AssemblyName = typeof(ProjectsApiFixture).Assembly.FullName, DisableDashboard = true };
            var appBuilder = DistributedApplication.CreateBuilder(options);
            _app = appBuilder.Build();
        }

        public async Task DisposeAsync()
        {
            await _app.StopAsync();
            if (_app is IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync().ConfigureAwait(false);
            }
            else
            {
                _app.Dispose();
            }
        }

        public async Task InitializeAsync()
        {
            await _app.StartAsync();
        }
    }
}
