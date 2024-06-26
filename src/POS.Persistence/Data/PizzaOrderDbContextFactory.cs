using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Data;

namespace Persistence;

public class PizzaOrderDbContextFactory: IDesignTimeDbContextFactory<PizzaOrderDbContext>
{
    public PizzaOrderDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PizzaOrderDbContext>();
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=dotnet-grpc-example;User Id=dotnetuser;Password=supersecretpw;");
    
        return new PizzaOrderDbContext(optionsBuilder.Options);
    }
}
