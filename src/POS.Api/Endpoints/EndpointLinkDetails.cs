using FastEndpoints;

namespace POS.Api.Endpoints;

public record EndpointLinkDetails(HttpContext Context, string[]? Routes);

public static class EndpointLinkDetaisBuilder 
{
    public static EndpointLinkDetails EndpointLinkDetailsFromEndpoint<TRequest, TResponse>(this Endpoint<TRequest, TResponse> endpoint)  where TRequest : notnull
    {
        return new EndpointLinkDetails(endpoint.HttpContext, endpoint.Definition.Routes);
    }

    public static EndpointLinkDetails EndpointLinkDetailsFromEndpointWithoutRequest<TResponse>(this EndpointWithoutRequest<TResponse> endpointWithoutRequest)
    {
        return new EndpointLinkDetails(endpointWithoutRequest.HttpContext, endpointWithoutRequest.Definition.Routes);
    }
}
