using Microsoft.Data.SqlClient;
using CumbrexSaaS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Testcontainers.MsSql;

namespace Integration.Tests.Fixtures;

/// <summary>
/// xUnit class fixture that spins up a SQL Server Testcontainer and
/// a <see cref="WebApplicationFactory{TProgram}"/> for integration testing.
/// Implements <see cref="IAsyncLifetime"/> to manage async setup and teardown.
/// </summary>
public sealed class IntegrationTestFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    private Respawner? _respawner;
    private string _connectionString = string.Empty;

    /// <summary>Gets the <see cref="WebApplicationFactory{TProgram}"/> for the test.</summary>
    public WebApplicationFactory<Program> Factory { get; private set; } = null!;

    /// <summary>Gets an <see cref="HttpClient"/> configured for the test application.</summary>
    public HttpClient Client { get; private set; } = null!;

    /// <inheritdoc />
    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();
        _connectionString = _sqlContainer.GetConnectionString();

        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    if (descriptor is not null)
                        services.Remove(descriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(_connectionString));
                });
            });

        Client = Factory.CreateClient();

        // Run EF Core migrations / ensure schema
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureCreatedAsync();

        // Configure Respawn v7 (requires an open DbConnection)
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer,
            TablesToIgnore = ["TBL_EF_MIGRATIONS_HISTORY"]
        });
    }

    /// <summary>Resets the database to a clean state between tests.</summary>
    public async Task ResetDatabaseAsync()
    {
        if (_respawner is not null)
        {
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            await _respawner.ResetAsync(connection);
        }
    }

    /// <inheritdoc />
    public async Task DisposeAsync()
    {
        Client.Dispose();
        await Factory.DisposeAsync();
        await _sqlContainer.DisposeAsync();
    }
}
