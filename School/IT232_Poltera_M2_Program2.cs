using System;
using System.Collections;

namespace IT232_Poltera_M2_Program2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Section 1
            //*********************************************************

            //****Assessment 2 Program 2 Section 1

            //*********************************************************

            string[,] salesRegions = {  {"North","Bob" , "Stef", "Ron"},
                                        { "South" , "Sue", "Janice" , "Will" },
                                        { "East" , "Nathan" , "Henry", "Kimmy"},
                                        { "West", "Wanda" ,"Charles", "Pete" }
                                     };
            Console.WriteLine("Section 1: Two-dimensional Array.");
            for (int i = 0; i < salesRegions.GetLength(0); i++)
            {
                for (int j = 0; j < salesRegions.GetLength(1); j++)
                {
                    Console.WriteLine(salesRegions[i, j]);
                }

            }
            #endregion

            #region Section 2
            //*********************************************************

            //****Assessment 2 Program 2 Section X

            //*********************************************************
            ArrayList salesTeam = new ArrayList();

            Console.WriteLine("\nSection 2: Array List.");
            // adding names from North region
            for (int j = 1; j < salesRegions.GetLength(0); j++)
            {
                salesTeam.Add(salesRegions[0, j]);
            }
            Console.WriteLine("There are " + salesTeam.Count + " names in the salesTeam arraylist.");

            // adding persons from south region
            for (int j = 1; j < salesRegions.GetLength(1); j++)
            {
                salesTeam.Add(salesRegions[1, j]);
            }

            // chekcing whther steff is present or not
            if (salesTeam.Contains("Stef"))
                Console.WriteLine("Stef is present in the salesTeam arraylist.");
            else
                Console.WriteLine("Stef is not present in the salesTeam arraylist.");

            Console.WriteLine("There are " + salesTeam.Count + " names in the salesTeam arraylist.");

            // removing Janice and Ron
            salesTeam.Remove("Janice");
            salesTeam.Remove("Ron");
            Console.WriteLine("There are " + salesTeam.Count + " names in the salesTeam arraylist.");
            Console.WriteLine("Names currently in the salesTeam arraylist.");
            foreach (string name in salesTeam)
            {
                Console.WriteLine(name);
            }

            #endregion
        }
    }
}
