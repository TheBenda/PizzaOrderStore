using FastEndpoints;

using POS.Api.Hateoas;
using POS.AppHost.ServiceDefaults;
using POS.Core.Repositories;

namespace POS.Api.Endpoints.Customers.Delete;

public sealed class DeleteCustomerEndpoint(ICustomersRepository _customersRepository): Endpoint<DeleteCustomerRequest, DeleteCustomerResponse>
{
    public override void Configure()
    {
        Delete("/api/Customer/{CustomerId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteCustomerRequest req, CancellationToken ct)
    {
        var deleteCustomerResult = await _customersRepository.DeleteCustomer(req.CustomerId, ct);
        if (deleteCustomerResult.IsSuccess)
            await SendOkAsync(DeleteCustomerResponse.Ok(deleteCustomerResult.Value), ct);
        else
            await SendNotFoundAsync(ct);
    }
}

public record DeleteCustomerRequest(Guid CustomerId);
public class DeleteCustomerResponse(string? message) : RepresentationModelBase
{
    public string? Message {init; get;}

    public static DeleteCustomerResponse Ok(DomainStatus domainStatus)
    {
        return new DeleteCustomerResponse(domainStatus.Message);
    }

    public static DeleteCustomerResponse Error(DomainError domainStatus)
    {
        return new DeleteCustomerResponse(domainStatus.ErrorMessage);
    }
}
