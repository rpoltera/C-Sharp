using System;
using System.IO;
using System.Collections; 

namespace IT232_Poltera_M2_Program4
{
    class Program
    {
        static void Main(string[] args)
        {
            //*********************************************************

            //****Assessment 2 Program 4 Section 1

            //*********************************************************
            #region Section 1
            // Create an arrayList named produceList.
            ArrayList produceList = new ArrayList();
            //Add the following data to the produce list.
            produceList.Add("bananas 0.59");
            produceList.Add("grapes 2.99");
            produceList.Add("apples 1.49");
            produceList.Add("pears 1.39");
            produceList.Add("lettuce 0.99");
            produceList.Add("onions 0.79");
            produceList.Add("potatoes 0.59");
            produceList.Add("peaches 1.59");
            //Write the data from produceList to ProducePrice.txt
            using (StreamWriter writeFile = new StreamWriter(@"ProducePrice.txt"))
            {
                for (int i = 0; i < produceList.Count; i++)
                {
                    writeFile.WriteLine(produceList[i]);
                }
                writeFile.Close();
            }
            Console.WriteLine("There are " + FileLineCount("ProducePrice.txt") + " products in the file.");
            Console.WriteLine();
            #endregion
            //*********************************************************

            //****Assessment 2 Program 4 Section 2

            //*********************************************************
            #region Section 2

            //add four more items to the ProducePrice.txt file.
            using (StreamWriter appendFile = new StreamWriter(@"ProducePrice.txt", true))
            {
                appendFile.WriteLine("peppers 0.99");
                appendFile.WriteLine("celery 1.29");
                appendFile.WriteLine("cabbage 0.79");
                appendFile.WriteLine("tomatoes 1.19");
                appendFile.Close();
            }
            //Call the function, FileLineCount(), and display its return value as the number of items
            Console.WriteLine("There are " + FileLineCount("ProducePrice.txt") + " products in the file.");
            Console.WriteLine();
            #endregion
            //*********************************************************

            //****Assessment 2 Program 4 Section 3

            //*********************************************************
            #region Section 3

            //read the contents of the file ProducePrice.txt into a new arraylist called producePrices.
            //print its contents, inserting a line number for each item.
            ArrayList producePrices = new ArrayList();
            int lineCounter = 1;
            string line;
          using (StreamReader readFile = new StreamReader(@"ProducePrice.txt", true))
            {
                while ((line = readFile.ReadLine()) != null)
                {
                    producePrices.Add(line);
                    Console.WriteLine(lineCounter + ". " + line);
                    lineCounter++;
                }
                readFile.Close();
            }
            Console.WriteLine();
            Console.WriteLine("There are " + producePrices.Count + " products in the producePrices list.");

            Console.ReadLine();
        }
            static int FileLineCount(string filename)
            {
                int lineCounter = 0;
                 using (StreamReader readFile = new StreamReader(filename))
                {
                 while ((readFile.ReadLine()) != null)
                    {
                        lineCounter++;
                    }
                    readFile.Close();
                }
                return lineCounter;
            }
             #endregion
    }
}

