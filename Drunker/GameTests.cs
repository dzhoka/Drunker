using System;
using System.Collections.Generic;
using NUnit.Framework;
namespace Drunker
{
    [TestFixture]
    public class GameTests
    {
        class ConsoleStub : IConsole
        {
            string input;

            public ConsoleStub(string input) { this.input = input; }

            public string ReadLine() { return input; }

            public void Write(string text) {}

            public void WriteLine(string text) {}

            public void Clear() {}
        }

        [Test]
        public void CreateNewStack()
        {
            var console = new ConsoleStub("");
            List<Card> stack = Game.NewStack(2, 5);
            Game game = new Game(stack, "Bob", 2, console);
            var cardSet = new HashSet<string>(stack.ConvertAll(c => c.Image()));
            Assert.True(cardSet.Count == stack.Count);
            Assert.True(stack.Count == 16);
        }

        [Test]
        public void Play()
        {
            var console = new ConsoleStub("H5");
            var stack = new List<Card>
            {
                new Card("H", 5), // user
                new Card("P", 3), // player #0
                new Card("T", 5), // player #1
                new Card("H", 7), // action card at start
                new Card("C", 4)
            };
            Game game = new Game(stack, "Bob", 2, console);
            List<Player> players = game.GetPlayers();

            game.DealCardsEach(1);
            game.Play();

            Assert.True(players[0].GetCards().Count == 0);
            Assert.True(players[1].GetCards().Count == 2);
            Assert.True(players[2].GetCards().Count == 0);
            Assert.True(stack.Count == 0);
        }
    }
}
