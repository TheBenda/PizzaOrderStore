using FastEndpoints;

using POS.Api.Hateoas;
using POS.Core.Repositories;

namespace POS.Api.Endpoints.Customers.Get;

public record GetCustomerRequest(Guid CustomerId);

public class GetCustomerResponse : RepresentationModelBase
{
    public required Guid Id {init; get;}
    public required string FirstName {init; get;}
    public required string LastName {init; get;}
    public string? Address {init; get;}
    public string? Phone {init; get;}
    public string? Email {init; get;}
}

public sealed class GetCustomerEndpoint(ICustomersRepository _customersRepository, CustomersLinkAssembler _customersLinkAssembler): Endpoint<GetCustomerRequest, GetCustomerResponse>
{
    public override void Configure()
    {
        Get("/api/Customer/{CustomerId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetCustomerRequest request, CancellationToken ct)
    {
        var foundCustomer = await _customersRepository.GetCustomerAsync(request.CustomerId, ct);

        if(foundCustomer.IsSuccess)
        {
            var details = this.EndpointLinkDetailsFromEndpoint();
            var resource = _customersLinkAssembler.GetCustomerResponse(foundCustomer.Value, details);
            await SendOkAsync(resource, ct);
        }
        else
        {
            await SendNotFoundAsync(ct);
        }
    }

}
