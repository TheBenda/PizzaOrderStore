using FastEndpoints;

using Microsoft.AspNetCore.Http.HttpResults;

using POS.AppHost.ServiceDefaults;
using POS.Core.UseCases.Customers.Create;

namespace POS.Api.Endpoints.Customers.Create;

public sealed class CreateCustomerEndpoint : Endpoint<CreateCustomerRequest, Results<Created, Conflict>>
{
    public override void Configure()
    {
        Post("/api/Customer");
        AllowAnonymous();
    }

    public override async Task<Results<Created, Conflict>> ExecuteAsync(CreateCustomerRequest req, CancellationToken ct)
    {
        var res = await req.ExecuteAsync(ct);
        
        Results<Created, Conflict> apiResult = res.IsSuccess ? 
            TypedResults.Created($"/api/Customer/{res.Value.CustomerId}") :
            TypedResults.Conflict();
        
        return apiResult;
    }
}
