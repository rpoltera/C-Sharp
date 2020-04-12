using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Adventure_game
{
    class Program
    {
        static void Main(string[] args)
        {
            gameTitle();
        }

        public static void gameTitle()
        {
            Console.WriteLine("Welcome to my first game");
            Console.WriteLine("Press 'Enter' to begin.");
            Console.ReadLine();
            Console.Clear();
            first();
        }

        public static void first()
        {
            string choice;
            Console.WriteLine("Wakup in class");
            Console.WriteLine("what do you do?");
            Console.WriteLine("1. punch teacher");
            Console.WriteLine("2. cry");
            Console.WriteLine("3. pee pants");
            Console.Write("choice:");
            choice = Console.ReadLine().ToLower();
            Console.Clear();

            switch (choice)
            {

                case "1":
                case "Punch Teacher":
                case "punch":
                    {
                        Console.WriteLine("Your fist pounds into teacher's face.");
                        Console.WriteLine("All the other students in class cheer your name.");
                        Console.WriteLine("Another teacher hears you and comes in the room.");
                        Console.WriteLine("The other teacher calls the cops");
                        Console.WriteLine("Press 'Enter' to continue.");
                        Console.ReadLine();
                        gameOver();
                        break;
                    }
                case "2":
                case "cry":

                    {
                        Console.WriteLine("Teachers face becomes red.");
                        Console.WriteLine("You wana cry? go somewhere else and do it.");
                        Console.WriteLine("Press 'Enter' to continue.");
                        Console.ReadLine();
                        second();
                        break;
                    }

                case "3":
                case "pee":
                case "pee a little":

                    {
                        Console.WriteLine("Teachers smells air.");
                        Console.WriteLine("The teacher yells to leave room.");
                        Console.WriteLine("Press 'Enter' to continue.");
                        Console.ReadLine();
                        second();
                        break;
                    }
                default:
                    {
                            Console.WriteLine("I don't understand");
                            Console.WriteLine("Press 'Enter' to try again");
                            Console.ReadLine();
                            first();
                            break;
                    }
            }
        }

        public static void second()
        {
            Random rnd = new Random();
            string[] secOption = {"In the hallway, you see the cops.",
            "In the hallway, you see your stalker walking in your direction" ,
            "In the hallway, the fire alarm goes off"};
            int randomNumber = rnd.Next(0, 3);
            String secText = secOption[randomNumber];

            string secChoice;

            Console.WriteLine(secText);
            Console.WriteLine("Do you try to hide in the bathroom; Yes or No");
            Console.Write("Choice:");
            secChoice = Console.ReadLine().ToLower();

            if (secChoice == "Yes" || secChoice == "y")
            {
                third();
            }
            else if (secChoice == "No" || secChoice == "n")
            {
                Console.WriteLine("A metior kills you instently");
                Console.WriteLine("Press 'enter' to continue");
                Console.ReadLine();
                gameOver();
            }
            else
            {
                Console.WriteLine("I don't understand");
                Console.WriteLine("Press 'Enter' to try again");
                Console.ReadLine();
                second();
                
            }
       
        }
        public static void third()
        {
            int age;

            Console.WriteLine("You bust into the bathroom and all of your friends and family are there.");
            Console.WriteLine("They yell 'Suprise' and you remeber it is youtr birthday.");
            Console.WriteLine("How old are you today");
            Console.WriteLine("Age:");
            int.TryParse(Console.ReadLine(), out age);

             while (age < 100)
            { 
              Console.WriteLine("Seriously you look much older");
              Console.WriteLine("How old are you really");
              Console.Write("Age: ");
              int.TryParse(Console.ReadLine(), out age);
            }

            Console.WriteLine("Wow, you are really OLD!!!! You are really bad at school.");
            Console.WriteLine("Press 'Enter' to continue");
            Console.ReadLine();
            youWin();

        }
        public static void gameOver()
        {
            Console.Clear();
            Console.WriteLine("At your funeral they sing songs of your bravery.");
            Console.WriteLine("better luck next time");
            Console.WriteLine("Press 'enter' to try again");
            Console.ReadLine();
            Console.Clear();
            gameTitle();

        }
        public static void youWin()
        {
            Console.Clear();
            Console.WriteLine("Your birthday party was a big hit \nThe cake will feed everyone");
            Console.WriteLine("Great job!!! You Won!!!");
            Console.WriteLine("Press 'enter' to try again");
            Console.ReadLine();
            Console.Clear();
            gameTitle();
        }

    }
}
