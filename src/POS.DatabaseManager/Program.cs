using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<Persistence.Data.PizzaOrderDbContext>("pizzaorderdb", null,
    optionsBuilder => optionsBuilder.UseNpgsql(npgsqlBuilder =>
        npgsqlBuilder.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(PizzaOrderDbInitializer.ActivitySourceName));

builder.Services.AddSingleton<PizzaOrderDbInitializer>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<PizzaOrderDbInitializer>());
builder.Services.AddHealthChecks()
    .AddCheck<PizzaOrderDbInitializerHealthCheck>("DbInitializer", null);

var app = builder.Build();

app.MapDefaultEndpoints();

await app.RunAsync();