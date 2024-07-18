using CSharpFunctionalExtensions;

using POS.AppHost.ServiceDefaults;
using POS.Domain.Entities;

namespace POS.Core.Repositories;

public interface ICustomersRepository
{
    public Task<List<Customer>> GetCustomersAsync(CancellationToken ct = default);

    public Task<IResult<Customer, DomainError>> GetCustomerAsync(Guid id, CancellationToken ct = default);

    public Task<IResult<DomainStatus, DomainError>> UpdateCustomerAsync(Guid id, Customer customer);

    public Task<IResult<Customer, DomainError>> CreateCustomer(Customer customer, CancellationToken ct = default);

    public Task<IResult<DomainStatus, DomainError>> DeleteCustomer(Guid id);
}
