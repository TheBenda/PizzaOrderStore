using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Infrastructure;

internal class PizzaOrderDbInitializer(IServiceProvider serviceProvider, ILogger<PizzaOrderDbInitializer> logger) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";

    private readonly ActivitySource _activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PizzaOrderDbContext>();

        await InitializeDatabaseAsync(dbContext, stoppingToken);
    }

    private async Task InitializeDatabaseAsync(PizzaOrderDbContext dbContext, CancellationToken cancellationToken)
    {
        using var activity = _activitySource.StartActivity("Initializing pizza order database", ActivityKind.Client);

        var sw = Stopwatch.StartNew();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(dbContext.Database.MigrateAsync, cancellationToken);


        logger.LogInformation("Database initialization completed after {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
    }
}