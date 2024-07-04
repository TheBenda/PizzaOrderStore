using Testcontainers.PostgreSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using POS.Persistence.Data;

namespace POS.IntegrationTests;

public class PostgresTestcontainer : AppFixture<Program>
{
    private PostgreSqlContainer? _postgreSqlContainer;

    protected override async Task PreSetupAsync()
    {
        _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("pizzaorderdb")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

        await _postgreSqlContainer.StartAsync();
    }

    protected override void ConfigureApp(IWebHostBuilder b)
    {
        b.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<PizzaOrderDbContext>));

                if(descriptor is not null) services.Remove(descriptor);

                services.AddDbContext<PizzaOrderDbContext>(options =>
                {
                    options.UseNpgsql(_postgreSqlContainer?.GetConnectionString(), npgsqlBuilder =>
                        npgsqlBuilder.MigrationsAssembly(typeof(POS.Persistence.DependencyInjection).Assembly.GetName().Name));
                });
            }
        );
    }

    protected override async Task SetupAsync()
    {
        using var scope = Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<PizzaOrderDbContext>();
        var migrations = dbContext.Database.GetMigrations();
        await dbContext.Database.MigrateAsync();
    }
}
