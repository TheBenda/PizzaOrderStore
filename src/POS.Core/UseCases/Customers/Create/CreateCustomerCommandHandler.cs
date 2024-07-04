using CSharpFunctionalExtensions;

using FastEndpoints;

using POS.AppHost.ServiceDefaults;
using POS.Core.Repositories;
using POS.Domain.Entities;

namespace POS.Core.UseCases.Customers.Create;

public sealed class CreateCustomerCommandHandler(ICustomersRepository _customersRepository) : ICommandHandler<CreateCustomerRequest, IResult<CreateCustomerResponse, DomainError>>
{
    public async Task<IResult<CreateCustomerResponse, DomainError>> ExecuteAsync(CreateCustomerRequest createCustomerRequest, CancellationToken ct)
    {
        var createdCustomer = await _customersRepository.CreateCustomer(ToEntity(createCustomerRequest));
        return createdCustomer.IsSuccess ? 
            Result.Success<CreateCustomerResponse, DomainError>(FromEntity(createdCustomer.Value)) :
            Result.Failure<CreateCustomerResponse, DomainError>(createdCustomer.Error);
    }

    private static Customer ToEntity(CreateCustomerRequest req) =>
        new()
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Address = req.Address,
            Email = req.Email,
            Phone = req.Phone
        };
    

    private static CreateCustomerResponse FromEntity(Customer entity) =>
        new(entity.Id);
}
