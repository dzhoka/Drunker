using System;

namespace Drunker
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.Write("Write your name please: ");
            string name = Console.ReadLine();
            Console.WriteLine($"Hi {name}!");

            Game game = new Game(name, GetNumberOfDrunkers());
            game.Play();
        }

        static int GetNumberOfDrunkers()
        {
            int numberOfDrunkers = 0;
            do
            {
                Console.Write("How many Drunkers you want to play with? ");
                string input = Console.ReadLine();
                Console.Clear();
                bool parsed = Int32.TryParse(input, out numberOfDrunkers);
                if (!parsed) Console.WriteLine("Sorry, it must be a number from 1 to 3.");
            } while (numberOfDrunkers < 1 || numberOfDrunkers > 3);

            return numberOfDrunkers;
        }
    }
}
