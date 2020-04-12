using System;

namespace SimpleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            InputConverter inputConverter = new InputConverter();
            CalculatorEngiene calculatorEngiene = new CalculatorEngiene();

            Console.WriteLine("enter first number"); 
            double firstNumber = inputConverter.ConvertInputToNumberic(Console.ReadLine());
            Console.WriteLine("enter second number"); 
            double secondNumber = inputConverter.ConvertInputToNumberic(Console.ReadLine());
            Console.WriteLine("enter the operation value");
            string operation = Console.ReadLine();
            double result = calculatorEngiene.Calculate(operation, firstNumber, secondNumber);

            Console.WriteLine(result);


        }
    }
}
