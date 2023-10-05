using MyFancyBank.Data.Entities;
using MyFancyBank.Data.Interfaces;

namespace MyFancyBank.Data.Repositories
{
    internal class CustomerRepository : IRepository
    {
        private static readonly Dictionary<Guid, Customer> _store = new Dictionary<Guid, Customer>();

        public Customer CreateCustomer(Customer customer)
        {
            _store.Add(customer.Id, customer);

            return customer;
        }

        public Customer? GetCustomerById(Guid id)
        {
            _store.TryGetValue(id, out var customer);
            
            return customer;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            _store[customer.Id] = customer;

            return customer;
        }
    }
}
