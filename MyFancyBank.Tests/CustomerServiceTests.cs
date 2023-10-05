using Moq;
using MyFancyBank.BusinessLogic.Models;
using MyFancyBank.BusinessLogic.Services;
using MyFancyBank.Data.Entities;
using MyFancyBank.Data.Interfaces;
using Xunit;

namespace MyFancyBank.Tests
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _target;
        private readonly Mock<IRepository> _repository;

        public CustomerServiceTests()
        {
            _repository = new Mock<IRepository>();
            _target = new CustomerService(_repository.Object);
        }

        [Fact]
        public void GetCustomerById_WhenCustomerIsFound_ShouldReturnExpectedModel()
        {
            //Arange
            var id = Guid.NewGuid();
            var customer = new Customer
            {
                Id = id,
                FirstName = "name",
                LastName = "lname",
                BankAccountAmount = 1,
                Iban = "my_iban_1"
            };

            _repository.Setup(s => s.GetCustomerById(id))
                .Returns(customer);

            //Act
            var actual = _target.GetCustomerById(id); ;

            //Assert
            Assert.Equal(customer.Id, actual.Id);
            Assert.Equal(customer.FirstName, actual.FirstName);
            Assert.Equal(customer.LastName, actual.LastName);
            Assert.Equal(customer.Iban, actual.Iban);
            Assert.Equal(customer.BankAccountAmount, actual.BankAccountAmount);
        }

        [Fact]
        public void GetCustomerById_WhenCustomerIsNotFound_ShouldThrowException()
        {
            //Arange
            var id = Guid.NewGuid();
            var expectedErrorMessage = $"Customer not found for id: {id}";

            //Act
            var exception = Assert.Throws<Exception>(() => _target.GetCustomerById(id));

            //Assert
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void CreateCustomer_WhenModelIsValid_ShouldCallRepository()
        {
            //Arange
            var customerRequestModel = new CustomerRequestModel
            {
                LastName = "name",
                FirstName = "fname",
                BankAccountAmount = 100
            };

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                LastName = "name",
                FirstName = "fname",
                BankAccountAmount = 100,
                Iban = "new_iban"
            };

            _repository.Setup(s => s.CreateCustomer(It.IsAny<Customer>()))
                .Returns(customer)
                .Verifiable();

            //Act
            var actual = _target.CreateCustomer(customerRequestModel); ;

            //Assert
            Assert.NotNull(actual);
            _repository.VerifyAll();
        }

        [Theory]
        [InlineData("", "test", 1, "First name and last name are required")]
        [InlineData(" ", "test", 1, "First name and last name are required")]
        [InlineData(null, "test", 1, "First name and last name are required")]
        [InlineData("test", "", 1, "First name and last name are required")]
        [InlineData("test", " ", 1, "First name and last name are required")]
        [InlineData("test", null, 1, "First name and last name are required")]
        [InlineData("test1", "test2", -1, "BankAccountAmount needs to be greater than 0")]
        public void CreateCustomer_WhenModelIsInvalid_ShouldThrowException(string firstName, string lastName, double bankAccountAmount, string expectedErrorMessage)
        {
            //Arange
            var customerRequestModel = new CustomerRequestModel
            {
                LastName = lastName,
                FirstName = firstName,
                BankAccountAmount = bankAccountAmount
            };

            //Act
            var exception = Assert.Throws<ArgumentException>(() => _target.CreateCustomer(customerRequestModel));

            //Assert
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void UpdateCustomer_WhenCustomerIsValid_ShouldUpdateTheCustomer()
        {
            //Arange
            var id = Guid.NewGuid();
            var customerRequestModel = new CustomerRequestModel
            {
                LastName = "name2",
                FirstName = "fname2",
                BankAccountAmount = 100
            };

            var customer = new Customer
            {
                Id = id,
                LastName = "name",
                FirstName = "fname",
                BankAccountAmount = 190,
                Iban = "new_iban"
            };

            var expectedCustomer = new Customer
            {
                Id = id,
                LastName = "name2",
                FirstName = "fname2",
                BankAccountAmount = 90,
                Iban = "new_iban"
            };

            _repository.Setup(s => s.GetCustomerById(id))
                .Returns(customer);

            _repository.Setup(s => s.UpdateCustomer(It.IsAny<Customer>()))
                .Returns(expectedCustomer);

            //Act
            var actual = _target.UpdateCustomer(id, customerRequestModel);

            //Assert
            Assert.Equal(expectedCustomer.FirstName, actual.FirstName);
            Assert.Equal(expectedCustomer.LastName, actual.LastName);
            Assert.Equal(expectedCustomer.BankAccountAmount, actual.BankAccountAmount);
        }

        [Theory]
        [InlineData("", "test", 1, "First name and last name are required")]
        [InlineData(" ", "test", 1, "First name and last name are required")]
        [InlineData(null, "test", 1, "First name and last name are required")]
        [InlineData("test", "", 1, "First name and last name are required")]
        [InlineData("test", " ", 1, "First name and last name are required")]
        [InlineData("test", null, 1, "First name and last name are required")]
        [InlineData("test1", "test2", -1, "BankAccountAmount needs to be greater than 0")]
        public void UpdateCustomer_WhenModelIsInvalid_ShouldThrowException(string firstName, string lastName, double bankAccountAmount, string expectedErrorMessage)
        {
            //Arange
            var id = Guid.NewGuid();
            var customerRequestModel = new CustomerRequestModel
            {
                LastName = lastName,
                FirstName = firstName,
                BankAccountAmount = bankAccountAmount
            };

            //Act
            var exception = Assert.Throws<ArgumentException>(() => _target.UpdateCustomer(id, customerRequestModel));

            //Assert
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void UpdateCustomer_WhenCustomerIsNotFound_ShouldThrowException()
        {
            //Arange
            var id = Guid.NewGuid();
            var customerRequestModel = new CustomerRequestModel
            {
                LastName = "name",
                FirstName = "fname",
                BankAccountAmount = 100
            };
            var expectedErrorMessage = $"Customer not found for id: {id}";

            //Act
            var exception = Assert.Throws<Exception>(() => _target.UpdateCustomer(id, customerRequestModel));

            //Assert
            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void UpdateCustomer_WhenCustomerBankAccountAmountIsLowerThanWithdrawal_ShouldThrowException()
        {
            //Arange
            var id = Guid.NewGuid();
            var customerRequestModel = new CustomerRequestModel
            {
                LastName = "name",
                FirstName = "fname",
                BankAccountAmount = 100
            };

            var customer = new Customer
            {
                Id = id,
                LastName = "name",
                FirstName = "fname",
                BankAccountAmount = 90,
                Iban = "new_iban"
            };

            var expectedErrorMessage = "BankAccountAmount needs to be lower than the current BankAccountAmount of the customer";

            _repository.Setup(s => s.GetCustomerById(id))
                .Returns(customer);

            //Act
            var exception = Assert.Throws<ArgumentException>(() => _target.UpdateCustomer(id, customerRequestModel));

            //Assert
            Assert.Equal(expectedErrorMessage, exception.Message);
        }
    }
}
