using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using POS.Persistence.Data;
using POS.Domain.Entities;
namespace POS.Api.Apis;

public static class CustomerApi
{
    public static void MapCustomerEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Customer").WithTags(nameof(Customer));

        group.MapGet("/", async (PizzaOrderDbContext db) =>
        {
            return await db.Customers.ToListAsync();
        })
        .WithName("GetAllCustomers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Customer>, NotFound>> (Guid id, PizzaOrderDbContext db) =>
        {
            return await db.Customers.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Customer model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCustomerById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, Customer customer, PizzaOrderDbContext db) =>
        {
            var affected = await db.Customers
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, customer.Id)
                    .SetProperty(m => m.FirstName, customer.FirstName)
                    .SetProperty(m => m.LastName, customer.LastName)
                    .SetProperty(m => m.Address, customer.Address)
                    .SetProperty(m => m.Phone, customer.Phone)
                    .SetProperty(m => m.Email, customer.Email)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCustomer")
        .WithOpenApi();

        // group.MapPost("/", async (Customer customer, PizzaOrderDbContext db) =>
        // {
        //     db.Customers.Add(customer);
        //     await db.SaveChangesAsync();
        //     return TypedResults.Created($"/api/Customer/{customer.Id}",customer);
        // })
        // .WithName("CreateCustomer")
        // .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, PizzaOrderDbContext db) =>
        {
            var affected = await db.Customers
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCustomer")
        .WithOpenApi();
    }
}
