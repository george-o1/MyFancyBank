using MyFancyBank.BusinessLogic.Models;

namespace MyFancyBank.BusinessLogic.Interfaces
{
    public interface ICustomerService
    {
        CustomerModel GetCustomerById(Guid id);

        CustomerModel CreateCustomer(CustomerRequestModel customer);

        CustomerModel UpdateCustomer(Guid id, CustomerRequestModel customer);
    }
}
