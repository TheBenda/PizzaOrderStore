using System.Text.Json.Serialization;

namespace POS.Core.UseCases.Customers.Create;

public record CreateCustomerResponse(Guid CustomerId);
