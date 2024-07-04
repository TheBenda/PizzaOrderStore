using Microsoft.AspNetCore.Http.HttpResults;
using POS.Api.Endpoints.Customers.Create;
using POS.AppHost.ServiceDefaults;
using POS.Core.UseCases.Customers.Create;
using POS.IntegrationTests.Customers;

namespace POS.IntegrationTests.Endpoints.Customers;

public class CustomerIntegrationTests(PostgresTestcontainer _app) : BaseIntegrationTest
{
    [Fact]
    public async Task Create_Valid_Customer()
    {
        var request = Fake.CreateCustomerRequest();
        var (rsp, res) = await _app.Client.POSTAsync<CreateCustomerEndpoint, CreateCustomerRequest, Results<Created<CreateCustomerResponse>, Conflict<DomainError>>>(request);
        
        rsp.IsSuccessStatusCode.Should().BeTrue();
    }
}
