using MyFancyBank.Data.Entities;

namespace MyFancyBank.Data.Interfaces
{
    public interface IRepository
    {
        Customer? GetCustomerById(Guid id);

        Customer CreateCustomer(Customer customer);

        Customer UpdateCustomer(Customer customer);
    }
}
