namespace MyFancyBank.BusinessLogic.Services
{
    public class MyFancyCalculator
    {
        public double Add(double firstNumber, double secondNumber) => firstNumber + secondNumber;

        public double Subtract(double firstNumber, double secondNumber) => firstNumber - secondNumber;

        public double Multiply(double firstNumber, double secondNumber) => firstNumber * secondNumber;

        public double Divide(double firstNumber, double secondNumber)
        {
            if (secondNumber == 0)
            {
                throw new ArgumentException("Division by 0 is not possible");
            }

            return firstNumber / secondNumber;
        }
    }
}
