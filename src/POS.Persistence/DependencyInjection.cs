using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using POS.Core.Repositories;
using POS.Persistence.Data;
using POS.Persistence.Repositories;

namespace POS.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomersRepository, CustomersRepository>();

        return services;
    }

    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder services)
    {
        services.AddNpgsqlDbContext<PizzaOrderDbContext>("pizzaorderdb");

        return services;
    }

}
