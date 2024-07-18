using Microsoft.AspNetCore.Http.HttpResults;
using POS.Api.Endpoints.Customers.Create;
using POS.Api.Endpoints.Customers.Get;
using POS.Api.Endpoints.Customers.GetAll;
using POS.Api.Hateoas;
using POS.AppHost.ServiceDefaults;
using POS.IntegrationTests.Customers;

namespace POS.IntegrationTests.Endpoints.Customers;

public class CustomerIntegrationTests(PostgresTestcontainer _app) : BaseIntegrationTest
{
    [Fact]
    public async Task Create_Valid_Customer()
    {
        var request = Fake.CreateCustomerRequest();
        var (rsp, createCustomerResult) = await _app.Client.POSTAsync<CreateCustomerEndpoint, CreateCustomerRequest, CreateCustomerResult>(request);
        
        rsp.IsSuccessStatusCode.Should().BeTrue();
        createCustomerResult.Should().NotBeNull();
        createCustomerResult.Id.Should().NotBeEmpty();
        createCustomerResult.Address.Should().BeEquivalentTo(request.Address);
        createCustomerResult.Email.Should().BeEquivalentTo(request.Email);
        createCustomerResult.FirstName.Should().BeEquivalentTo(request.FirstName);
        createCustomerResult.LastName.Should().BeEquivalentTo(request.LastName);
    }

    [Fact]
    public async Task Get_Valid_Customer()
    {
        var request = Fake.CreateCustomerRequest();
        var (rsp, createCustomerResult) = await _app.Client.POSTAsync<CreateCustomerEndpoint, CreateCustomerRequest, CreateCustomerResult>(request);
        
        rsp.IsSuccessStatusCode.Should().BeTrue();

        var getCustomerRequest = new GetCustomerRequest(createCustomerResult.Id);
        var (getCustomerResponseContent, getCustomerResponse) = await _app.Client.GETAsync<GetCustomerEndpoint, GetCustomerRequest, GetCustomerResponse>(getCustomerRequest);
        
        getCustomerResponseContent.IsSuccessStatusCode.Should().BeTrue();
        getCustomerResponse.Should().NotBeNull();
        getCustomerResponse.Id.Should().NotBeEmpty();
        getCustomerResponse.Address.Should().BeEquivalentTo(createCustomerResult.Address);
        getCustomerResponse.Email.Should().BeEquivalentTo(createCustomerResult.Email);
        getCustomerResponse.FirstName.Should().BeEquivalentTo(createCustomerResult.FirstName);
        getCustomerResponse.LastName.Should().BeEquivalentTo(createCustomerResult.LastName);
        getCustomerResponse.Links.Should().BeEmpty();
    }

    [Fact]
    public async Task Get_Valid_Customers()
    {
        var firstRequest = Fake.CreateCustomerRequest();
        var (frsp, firstResult) = await _app.Client.POSTAsync<CreateCustomerEndpoint, CreateCustomerRequest, CreateCustomerResult>(firstRequest);

        var secondRequest = Fake.CreateCustomerRequest();
        var (srsp, secondResult) = await _app.Client.POSTAsync<CreateCustomerEndpoint, CreateCustomerRequest, CreateCustomerResult>(secondRequest);
        

        var (getCustomersResponseContent, getCustomersResponse) = await _app.Client.GETAsync<GetAllCustomersEndpoint, CollectionModel<GetAllCustomersResponse, GetCustomerResponse>>();
        
        getCustomersResponseContent.IsSuccessStatusCode.Should().BeTrue();
        getCustomersResponse.Should().NotBeNull();
        getCustomersResponse.Links.Should().BeEmpty();
        getCustomersResponse.Embedded.Should().NotBeEmpty();
    }
}
