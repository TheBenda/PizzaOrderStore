using POS.Api.Endpoints.Customers.Create;
using POS.Api.Endpoints.Customers.Get;
using POS.Api.Endpoints.Customers.GetAll;
using POS.Api.Hateoas;
using POS.Domain.Entities;

namespace POS.Api.Endpoints.Customers;

public class CustomersLinkAssembler(LinkGenerator _linkGenerator)
{
    public GetCustomerResponse GetCustomerResponse(Customer customer, EndpointLinkDetails endpointLinkDetails)
    {
        return ToGetCustomerResponse(customer);
    }

    public CollectionModel<GetAllCustomersResponse, GetCustomerResponse> GetAllCustomersResponse(List<Customer> customers, EndpointLinkDetails endpointLinkDetails)
    {
        var collectionModdels = customers.Select(c => GetCustomerResponse(c, endpointLinkDetails)).ToList();
        return new CollectionModel<GetAllCustomersResponse, GetCustomerResponse>(collectionModdels);
    }

    private static CreateCustomerResult ToResponse(Customer customer) =>
        new(
            customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.Address,
            customer.Phone,
            customer.Email
        );

    private static GetCustomerResponse ToGetCustomerResponse(Customer customer) =>
        new()
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Address = customer.Address,
            Phone = customer.Phone,
            Email = customer.Email
        };
            
        
}
