using MyFancyBank.BusinessLogic.Interfaces;
using MyFancyBank.BusinessLogic.Models;
using MyFancyBank.Data.Entities;
using MyFancyBank.Data.Interfaces;

namespace MyFancyBank.BusinessLogic.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository _customerRepository;
        private readonly MyFancyCalculator myFancyCalculator;

        public CustomerService(IRepository customerRepository)
        {
            _customerRepository = customerRepository;
            myFancyCalculator = new MyFancyCalculator();
        }

        public CustomerModel GetCustomerById(Guid id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                throw new Exception($"Customer not found for id: {id}");
            }

            return CustomerModel.FromCustomer(customer);
        }

        public CustomerModel CreateCustomer(CustomerRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FirstName) || string.IsNullOrWhiteSpace(model.LastName))
            {
                throw new ArgumentException("First name and last name are required");
            }

            if (model.BankAccountAmount < 0)
            {
                throw new ArgumentException("BankAccountAmount needs to be greater than 0");
            }

            var rand = new Random();
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                BankAccountAmount = model.BankAccountAmount,
                Iban = $"my_iban_{rand.NextInt64(1, Int64.MaxValue)}",
            };

            var createdCustomer = _customerRepository.CreateCustomer(customer);

            return CustomerModel.FromCustomer(createdCustomer);
        }

        public CustomerModel UpdateCustomer(Guid id, CustomerRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FirstName) || string.IsNullOrWhiteSpace(model.LastName))
            {
                throw new ArgumentException("First name and last name are required");
            }

            if (model.BankAccountAmount < 0)
            {
                throw new ArgumentException("BankAccountAmount needs to be greater than 0");
            }

            var customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                throw new Exception($"Customer not found for id: {id}");
            }

            if (model.BankAccountAmount > customer.BankAccountAmount)
            {
                throw new ArgumentException("BankAccountAmount needs to be lower than the current BankAccountAmount of the customer");
            }

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.BankAccountAmount = myFancyCalculator.Subtract(customer.BankAccountAmount, model.BankAccountAmount);

            var updatedCustomer = _customerRepository.UpdateCustomer(customer);
            return CustomerModel.FromCustomer(updatedCustomer);
        }
    }
}
