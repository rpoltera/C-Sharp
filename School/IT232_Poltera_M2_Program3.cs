using System;
using System.Collections;
using System.Collections.Generic;


namespace IT232_Poltera_M2_Program3
{
    class Program
    {
        //create a structure with the string fields for Make and Model and one integer field for ModelYear
        struct structCar
        {
            public string make;
            public string model;
            public int modelYear;
        }

        static void Main(string[] args)
        {

            //*********************************************************

            //****Assessment 2 Program 3 Section 1

            //*********************************************************
            #region Section 1

            //Create an array of structures
            structCar[] cars = new structCar[3];
            Console.WriteLine("Section 1: Array of Structures");

            //add values to array Ford
            cars[0] = new structCar();
            cars[0].make = "Ford,";
            cars[0].model = "Mustang,";
            cars[0].modelYear = 2010;
            //add values to array Chevrolet
            cars[1] = new structCar();
            cars[1].make = "Chevrolet,";
            cars[1].model = "Silverado,";
            cars[1].modelYear = 2008;
            //add values to array Dodge
            cars[2] = new structCar();
            cars[2].make = "Dodge,";
            cars[2].model = "Charger,";
            cars[2].modelYear = 2012;

            //display the full contents of each structure in the array
            for (int i = 0; i < cars.Length; i++)
            {
                Console.WriteLine(cars[i].make + " " + cars[i].model + " " + cars[i].modelYear);
            }
            #endregion
            //*********************************************************

            //****Assessment 2 Program 3 Section 2

            //*********************************************************

            #region Section 2
            //create a dictionary that will use the car model as the key and quantity as the value
            Dictionary<string, int> inventoryCount = new Dictionary<string, int>();

            //populate the dictionary
            inventoryCount.Add("Mustang", 9);
            inventoryCount.Add("Silverado", 13);
            inventoryCount.Add("Charger", 4);

            Console.WriteLine();
            Console.WriteLine("Section 2: Inventory Count.");

            //Display inventory count from the dictionary
            Console.WriteLine("There are " + inventoryCount["Mustang"] + " Mustangs.");
            Console.WriteLine("There are " + inventoryCount["Silverado"] + " Silverados.");
            Console.WriteLine("There are " + inventoryCount["Charger"] + " Chargers.");
            #endregion

            //*********************************************************

            //****Assessment 2 Program 3 Section 3

            //*********************************************************
            #region Section 3

            //Create an arraylist and add all the days of the week to it
            ArrayList daysOfWeek = new ArrayList();
            daysOfWeek.Add("Sunday");
            daysOfWeek.Add("Monday");
            daysOfWeek.Add("Tuesday");
            daysOfWeek.Add("Wednesday");
            daysOfWeek.Add("Thursday");
            daysOfWeek.Add("Friday");
            daysOfWeek.Add("Saturday");

            Console.WriteLine();
            Console.WriteLine("Section 3: Days of the Week");

            //display the days from the arraylist
            for (int i = 0; i < daysOfWeek.Count; i++)
            {
                Console.WriteLine(daysOfWeek[i]);
            }
            //display the days from the arraylist in reverse order
            for (int i = daysOfWeek.Count - 1; i >= 0; i--)
            {
                //Console.Write(i + " ");
                Console.WriteLine(daysOfWeek[i]);
            }

            //Copy into another arraylist called workDays
            ArrayList workDays = new ArrayList();
            workDays.AddRange(daysOfWeek);

            //Remove Saturday and Sunday
            workDays.Remove("Saturday");
            workDays.Remove("Sunday");
            //print the contents of the workDays array
            for (int i = 0; i < workDays.Count; i++)
            {
                Console.WriteLine(workDays[i]);
            }
            #endregion

            //*********************************************************

            //****Assessment 2 Program 3 Section 4

            //*********************************************************
            #region Section 4

            Console.WriteLine();
            Console.WriteLine("Section 4: Stack");

            //create a stack
            Stack<int> myStack = new Stack<int>();

            //push 10, push 24, push 31, push 45, push 19, push 76
            myStack.Push(10);
            myStack.Push(24);
            myStack.Push(31);
            myStack.Push(45);
            myStack.Push(19);
            myStack.Push(76);

            //How many items are in the myStack?
            Console.WriteLine("There are " + myStack.Count + " items in the stack.");

            //Pop, pop, pop.
            myStack.Pop();
            myStack.Pop();
            myStack.Pop();

            //How many items are on the stack now?
            Console.WriteLine("There are " + myStack.Count + " items in the stack.");

            //What is the value of the next item to be popped?
            Console.WriteLine("The next item to be popped from the stack is " + myStack.Peek() + ".");
            #endregion

            //*********************************************************

            //****Assessment 2 Program 3 Section 5

            //*********************************************************
            #region Section 5
            Console.WriteLine();
            Console.WriteLine("Section 5: Queue");

            //Create a queue.
            Queue<int> q = new Queue<int>();

            //Enqueue 10, Enqueue 24, Enqueue 31, Enqueue 45, Enqueue 19, Enqueue 76
            q.Enqueue(10);
            q.Enqueue(24);
            q.Enqueue(31);
            q.Enqueue(45);
            q.Enqueue(19);
            q.Enqueue(76);

            //How many items are in the queue?
            Console.WriteLine("There are " + q.Count + " items in the queue.");

            //Dequeue 3 items
            q.Dequeue();
            q.Dequeue();
            q.Dequeue();

            //How many items are in the queue now?
            Console.WriteLine("There are " + q.Count + " items in the queue.");

            //What is the value of the next item that will be Dequeued?
            Console.WriteLine("The next item to be dequeued from the queue is " + q.Peek() + ".");

            Console.ReadLine();
            #endregion
        }
    }
}


