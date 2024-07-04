using CSharpFunctionalExtensions;

using FastEndpoints;

using POS.AppHost.ServiceDefaults;

namespace POS.Core.UseCases.Customers.Create;

public record CreateCustomerRequest(
    string FirstName,
    string LastName,
    string? Address,
    string? Phone,
    string? Email) : ICommand<IResult<CreateCustomerResponse, DomainError>>;

