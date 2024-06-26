var builder = DistributedApplication.CreateBuilder(args);

var pizzaOrderDb = builder.AddPostgres("pizzaorder", password: builder.CreateStablePassword("pizzaorder-password"))
    .WithDataVolume()
    .AddDatabase("pizzaorderdb");

builder.AddProject<Projects.POS_DatabaseManager>("infrastructure").WithReference(pizzaOrderDb);

builder.Build().Run();
