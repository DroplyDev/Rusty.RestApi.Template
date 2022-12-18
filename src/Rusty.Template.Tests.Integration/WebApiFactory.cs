using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Presentation;

namespace Rusty.Template.Tests.Integration;

public class WebApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly MsSqlTestcontainer _dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
        .WithDatabase(new MsSqlTestcontainerConfiguration("mcr.microsoft.com/mssql/server:2022-latest")
        {
            Password = "Test_Pass123"
        }).Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connectionString = _dbContainer.ConnectionString + "TrustServerCertificate=True";
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(ConnectionStringFactory));
            services.AddSingleton(new ConnectionStringFactory(connectionString));
        });
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