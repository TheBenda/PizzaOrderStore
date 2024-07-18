using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace POS.Api.Hateoas;

public abstract class RepresentationModelBase
{
    [JsonPropertyName("_links")]
    public List<Link> Links {init; get;} = [];
}

public class CollectionModel<TResponse, TCollectionModel> : RepresentationModelBase 
    where TResponse : notnull 
    where TCollectionModel : notnull
{
    [JsonPropertyName("_embedded")]
    public List<TCollectionModel> Embedded {init; get;} = [];

    public CollectionModel([NotNull] List<TCollectionModel> embedded) => (Embedded) = (embedded);
}

public record Link(string Href, string Rel, string Method);
