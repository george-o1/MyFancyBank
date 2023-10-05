using FluentAssertions;
using MyFancyBank.BusinessLogic.Services;
using Xunit;

namespace MyFancyBank.Tests
{
    public class MyFancyCalculatorTests
    {

        private readonly MyFancyCalculator _calculator;

        public MyFancyCalculatorTests()
        {
            _calculator = new MyFancyCalculator();
        }

        [Theory]
        [InlineData(1,2,3)]
        [InlineData(1,0,1)]
        [InlineData(0,2,2)]
        [InlineData(0,0,0)]
        public void Add_WhenCalledWithTwoNumbers_ShouldReturnAsExpected(double firstNumber, double secondNumber, double expected)
        {
            //Arange

            //Act
            var actual = _calculator.Add(firstNumber, secondNumber);

            //Asert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 2, -1)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, -1)]
        [InlineData(0, 0, 0)]
        [InlineData(-1, 2, -3)]
        [InlineData(-1, -2, 1)]
        [InlineData(1, -2, 3)]
        public void Subtract_WhenCalledWithTwoNumbers_ShouldReturnAsExpected(double firstNumber, double secondNumber, double expected)
        {
            //Arange

            //Act
            var actual = _calculator.Subtract(firstNumber, secondNumber);

            //Asert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2, 2, 4)]
        [InlineData(2, -2, -4)]
        [InlineData(-2, 2, -4)]
        [InlineData(-2, -2, 4)]
        [InlineData(0, 2, 0)]
        [InlineData(2, 0, 0)]
        [InlineData(0, 0, 0)]
        public void Multiply_WhenCalledWithTwoNumbers_ShouldReturnAsExpected(double firstNumber, double secondNumber, double expected)
        {
            //Arange

            //Act
            var actual = _calculator.Multiply(firstNumber, secondNumber);

            //Asert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2, 2, 1)]
        [InlineData(2, -2, -1)]
        [InlineData(-2, 2, -1)]
        [InlineData(-2, -2, 1)]
        [InlineData(0, 2, 0)]
        public void Divide_WhenCalledWithTwoNumbers_ShouldReturnAsExpected(double firstNumber, double secondNumber, double expected)
        {
            //Arange

            //Act
            var actual = _calculator.Divide(firstNumber, secondNumber);

            //Asert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Divide_WhenSecondParameterIsZero_ShouldThrowException()
        {
            //Arange
            var expectedMessage = "Division by 0 is not possible";

            //Act
            Action action = () => _calculator.Divide(2, 0);
            
            //Asert
            action.Should()
                .Throw<ArgumentException>()
                .WithMessage(expectedMessage);
        }
    }
}
