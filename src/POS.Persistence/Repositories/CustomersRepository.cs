using CSharpFunctionalExtensions;

using Microsoft.EntityFrameworkCore;

using POS.AppHost.ServiceDefaults;
using POS.Persistence.Data;
using POS.Domain.Entities;
using POS.Core.Repositories;

namespace POS.Persistence.Repositories;

public class CustomersRepository(PizzaOrderDbContext _pizzaOrderDbContext) : ICustomersRepository
{
    public async Task<List<Customer>> GetCustomersAsync(CancellationToken ct = default) => await _pizzaOrderDbContext.Customers.ToListAsync(ct);

    public async Task<IResult<Customer, DomainError>> GetCustomerAsync(Guid id, CancellationToken ct = default) => 
                await _pizzaOrderDbContext.Customers.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id, ct) 
                is Customer model ? 
                Result.Success<Customer, DomainError>(model) : 
                Result.Failure<Customer, DomainError>(DomainError.NotFound($"Customer with id: {id} not found."));

    public async Task<IResult<DomainStatus, DomainError>> UpdateCustomerAsync(Guid id, Customer customer) 
    {
        var affected = await _pizzaOrderDbContext.Customers
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, customer.Id)
                    .SetProperty(m => m.FirstName, customer.FirstName)
                    .SetProperty(m => m.LastName, customer.LastName)
                    .SetProperty(m => m.Address, customer.Address)
                    .SetProperty(m => m.Phone, customer.Phone)
                    .SetProperty(m => m.Email, customer.Email)
                    );

        return affected == 1 ? Result.Success<DomainStatus, DomainError>(DomainStatus.OK($"Updated Custumer with id: {id}")) : 
            Result.Failure<DomainStatus, DomainError>(DomainError.NotFound($"Customer with id: {id} not found."));
    }

    public async Task<IResult<Customer, DomainError>> CreateCustomer(Customer customer, CancellationToken ct = default)
    {
        try {
            _pizzaOrderDbContext.Customers.Add(customer);
            await _pizzaOrderDbContext.SaveChangesAsync(ct);
        
            return Result.Success<Customer, DomainError>(customer);
        } catch(Exception e) {
            // TODO add proper TraceEvent
            return Result.Failure<Customer, DomainError>(DomainError.Conflict("Error occured when saving Customer", e));
        }
    }

    public async Task<IResult<DomainStatus, DomainError>> DeleteCustomer(Guid id)
    {
        var affected = await _pizzaOrderDbContext.Customers
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
        
        return affected == 1 ? Result.Success<DomainStatus, DomainError>(DomainStatus.OK($"Deleted Custumer with id: {id}")) : 
            Result.Failure<DomainStatus, DomainError>(DomainError.NotFound($"Customer with id: {id} not found."));
    }
}
