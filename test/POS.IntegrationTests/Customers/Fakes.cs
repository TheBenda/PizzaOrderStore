using Bogus;

using POS.Core.UseCases.Customers.Create;

namespace POS.IntegrationTests.Customers;

public static class Fakes
{
    internal static CreateCustomerRequest CreateCustomerRequest(this Faker faker)
        => new(
            faker.Name.FirstName(),
            faker.Name.LastName(),
            faker.Address.FullAddress(),
            null,
            faker.Internet.Email());
}
