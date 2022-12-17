using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Rusty.Template.Presentation;

namespace Rusty.Template.Tests.Integration;

public class WebApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly MySqlTestcontainer _dbContainer = new TestcontainersBuilder<MySqlTestcontainer>()
        .WithDatabase(new MySqlTestcontainerConfiguration
        {
            Database = "sqldb",
            Username = "sqlname",
            Password = "sqlpass"
        }).Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // builder.ConfigureTestServices(services =>
        // {
        //     services.RemoveAll(typeof(AppDbContext));
        //     services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_dbContainer.ConnectionString)
        //         .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        // });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}