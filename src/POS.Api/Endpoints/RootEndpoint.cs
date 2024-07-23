using FastEndpoints;

using POS.Api.Hateoas;

namespace POS.Api.Endpoints;

public sealed class RootEndpoint(LinkGenerator _linkGenerator): EndpointWithoutRequest<BaseEndpointResponse>
{
    public override void Configure()
    {
        Get("/api");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var details = this.EndpointLinkDetailsFromEndpointWithoutRequest();
    }
}

public class BaseEndpointResponse() : RepresentationModelBase;
