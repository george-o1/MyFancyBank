namespace MyFancyBank.Data.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Iban { get; set; }
        public double BankAccountAmount { get; set; }
    }
}
