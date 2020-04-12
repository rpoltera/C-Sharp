

using System;

namespace Assessment_IT213M2_Section_X
{
    class Program
    {
        static void Main(string[] args)
        {
            //*********************************************************
            //****Assessment IT213M2 Section 1
            //*********************************************************
            int sum = 0, g = 0, n = 0;
            double avg = 0.0d;
            do
            {
                Console.Write("Enter Grade: ", g);
                n = Convert.ToInt32(Console.ReadLine());
                sum += n;
                ++g;
            } while (g < 10);

            avg = sum / 10.0;

            Console.WriteLine($"Total of all 10 grades is {sum}\nClass average is {avg}");

            //*********************************************************
            //****Assessment IT213M2 Section 2
            //*********************************************************
            for (int k = 5; k > 0; k--)
            {
                for (int i = 0; i <= 10; i += 2)
                    Console.WriteLine("k={0}, i={1} ", k, i);
            }
            //*********************************************************
            //****Assessment IT213M2 Section 3
            //*********************************************************
            {
                int s = 0, t = 0, p = 0;
                do
                {
                    Console.Write("Enter a positive number to be added to the total or -1 to end. ", t);
                    p = Convert.ToInt32(Console.ReadLine());
                    if (p < 0)
                    {
                        break;
                    }
                    s += p;
                    ++t;
                } while (t > 0);
                Console.WriteLine($"The sum of all numbers entered is {s}");

            }

        }
    }
}
