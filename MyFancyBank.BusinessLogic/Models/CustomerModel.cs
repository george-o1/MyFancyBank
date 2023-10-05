using MyFancyBank.Data.Entities;

namespace MyFancyBank.BusinessLogic.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Iban { get; set; }
        public double BankAccountAmount { get; set; }

        public static CustomerModel FromCustomer(Customer customer)
        {
            return new CustomerModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Iban = customer.Iban,
                BankAccountAmount = customer.BankAccountAmount
            };
        }
    }
}
