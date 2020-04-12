using System;

namespace Competency_Assessment__IT213M1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Displaying message for entring value
            Console.WriteLine("Please Enter Your First Name");
            string FirstName= Convert.ToString(Console.ReadLine()); //Variable for storing string value Of First Name
            Console.WriteLine("Please Enter Your Last Name");
            string LastName = Convert.ToString(Console.ReadLine()); //Variable for storing string value of last Name
            Console.WriteLine("Please Enter Your Street Address");
            string StreetAddress = Convert.ToString(Console.ReadLine()); //Variable for storing string value Of Street Address
            Console.WriteLine("Please Enter Your City");
            string City = Convert.ToString(Console.ReadLine()); //Variable for storing string value Of City
            Console.WriteLine("Please Enter Your State");
            string State = Convert.ToString(Console.ReadLine()); //Variable for storing string value Of State
            Console.WriteLine("Please Enter Your ZipCode");
            string ZipCode = Convert.ToString(Console.ReadLine()); //Variable for storing string value Of ZipCode
            Console.WriteLine("Please Enter Number of Units Taken");
            string Units = Convert.ToString(Console.ReadLine()); //Variable for storing string value Of Units Taken
            int intUnitsTaken = Convert.ToInt32(Units);
            Console.WriteLine(intUnitsTaken);
            const double PricePerUnit = (100.50); //Declare a constant numeric variable to contain the decimal value 100.50 for price per unit
            const double UnitDiscount = (150.00);//Declare a constant numeric variable to contain the decimal value for twenty unit-hour discount
            

           // if Statement for Discount check
            Double AfterDiscount = 0;
                            if (intUnitsTaken >= 20)
                            {
                                AfterDiscount = (intUnitsTaken * PricePerUnit) - UnitDiscount;
                                
                            }
                               else
                            {
                                AfterDiscount = (PricePerUnit * intUnitsTaken);

                            }


            //Displaying Output
            Console.WriteLine("Name: " + FirstName + " " + LastName);
            Console.WriteLine("Address: " + StreetAddress);
            Console.WriteLine("City: " + City);
            Console.WriteLine("State: " + State);
            Console.WriteLine("ZipCode: " + ZipCode);
            Console.WriteLine("The number of units taken is: " + intUnitsTaken);
            Console.WriteLine("The tuition before discount is: " + $"{intUnitsTaken * PricePerUnit:C}");
            Console.WriteLine("The tuition after twenty-unit discount is: " + $"{AfterDiscount:C}");
            Console.WriteLine("Your monthly payment is: " + $"{AfterDiscount / 12:C}");


        }
    }
}

