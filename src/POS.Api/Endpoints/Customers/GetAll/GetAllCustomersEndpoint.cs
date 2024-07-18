using FastEndpoints;

using POS.Api.Endpoints.Customers.Get;
using POS.Core.Repositories;
using POS.Api.Hateoas;

namespace POS.Api.Endpoints.Customers.GetAll;

public record GetAllCustomersResponse(List<GetCustomerResponse> GetCustomerResponses);
public sealed class GetAllCustomersEndpoint(ICustomersRepository _customersRepository, CustomersLinkAssembler _customersLinkAssembler): EndpointWithoutRequest<CollectionModel<GetAllCustomersResponse, GetCustomerResponse>>
{
    public override void Configure()
    {
        Get("/api/Customers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var details = this.EndpointLinkDetailsFromEndpoint();
        var customers = await _customersRepository.GetCustomersAsync(ct);

        var resourceCollection = _customersLinkAssembler.GetAllCustomersResponse(customers, details);
        await SendOkAsync(resourceCollection, ct);
    }
}
