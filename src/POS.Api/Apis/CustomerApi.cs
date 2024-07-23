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
    }
}
