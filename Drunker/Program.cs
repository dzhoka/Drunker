using System;

namespace Drunker
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var console = new GameConsole();
            console.Write("Write your name please: ");
            string name = console.ReadLine();
            console.WriteLine($"Hi {name}!");
            var stack = Game.NewStack(2, 10);
            int numberOfDrunkers = GetNumberOfDrunkers(console);

            Game game = new Game(stack, name, numberOfDrunkers, console);
            game.DealCardsEach(4);
            game.Play();
        }

        static int GetNumberOfDrunkers(IConsole console)
        {
            int numberOfDrunkers = 0;
            do
            {
                console.Write("How many Drunkers you want to play with? ");
                string input = console.ReadLine();
                console.Clear();
                bool parsed = Int32.TryParse(input, out numberOfDrunkers);
                if (!parsed) console.WriteLine("Sorry, it must be a number from 1 to 3.");
            } while (numberOfDrunkers < 1 || numberOfDrunkers > 3);

            return numberOfDrunkers;
        }
    }
}
