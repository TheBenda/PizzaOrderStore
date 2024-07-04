namespace POS.AppHost.ServiceDefaults;

public record DomainStatus(string? Message, StatusType StatusType)
{
    public static DomainStatus OK(string? message = "Operation resulted in status OK") => new(message, StatusType.OK);
}

public enum StatusType
{
    OK
}
