using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc;

namespace POS.AppHost.ServiceDefaults;

public class DomainError(string? errorMessage, ErrorType errorType, Exception? exception = null)
{
    public string? ErrorMessage { get; init; } = errorMessage;
    [JsonIgnore]
    public ErrorType ErrorType { get; init; } = errorType;
    [JsonIgnore]
    public Exception? Exception { get; init; } = exception;

    public bool HasException() => Exception is not null;   

    public static DomainError NotFound(string? message = "Given element not found", Exception? exception = null) => new(message, ErrorType.NotFound, exception);
    public static DomainError Conflict(string? message = "Conflict operation", Exception? exception = null) => new(message, ErrorType.Conflict, exception);
}

public enum ErrorType
{
    NotFound,
    Conflict
}
