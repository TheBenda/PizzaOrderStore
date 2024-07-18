using FastEndpoints;

using Microsoft.AspNetCore.Http.HttpResults;

using POS.Core.Repositories;
using POS.Domain.Entities;

namespace POS.Api.Endpoints.Customers.Create;

public sealed class CreateCustomerEndpoint(ICustomersRepository _customersRepository) : Endpoint<CreateCustomerRequest, CreateCustomerResult>
{
    public override void Configure()
    {
        Post("/api/Customer");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateCustomerRequest req, CancellationToken ct)
    {
        var createdCustomerResponse = await _customersRepository.CreateCustomer(ToEntity(req), ct);

        if(createdCustomerResponse.IsSuccess)
        {
            var response = ToResponse(createdCustomerResponse.Value);
            await SendOkAsync(response, ct);
        }
        else
            await SendErrorsAsync(409, ct);
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
    
    private static CreateCustomerResult ToResponse(Customer customer) =>
        new(
            customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.Address,
            customer.Phone,
            customer.Email
        );
}

public record CreateCustomerRequest(
    string FirstName,
    string LastName,
    string? Address,
    string? Phone,
    string? Email);

public record CreateCustomerResult(
    Guid Id,
    string FirstName,
    string LastName,
    string? Address,
    string? Phone,
    string? Email
);
