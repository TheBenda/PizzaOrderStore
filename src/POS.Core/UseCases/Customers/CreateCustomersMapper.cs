using FastEndpoints;

using POS.Core.UseCases.Customers.Create;
using POS.Domain.Entities;

namespace POS.Core.UseCases.Customers;

public sealed class CreateCustomersMapper: Mapper<CreateCustomerRequest, CreateCustomerResponse, Customer>
{
    public override Customer ToEntity(CreateCustomerRequest req) =>
        new()
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Address = req.Address,
            Email = req.Email,
            Phone = req.Phone
        };
    

    public override CreateCustomerResponse FromEntity(Customer entity) =>
        new(entity.Id);
    
}
