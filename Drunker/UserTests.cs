using System;
using System.Collections.Generic;
using NUnit.Framework;
namespace Drunker
{
    [TestFixture]
    public class UserTests
    {
        class ConsoleStub : IConsole
        {
            List<string> inputs;

            public ConsoleStub(List<string> inputs)
            {
                this.inputs = inputs;
            }

            public string ReadLine()
            {
                string input = inputs[0];
                inputs.RemoveAt(0);
                return input;
            }

            public void Write(string text) {}

            public void WriteLine(string text) {}

            public void Clear() {}
        }

        [Test]
        public void TurnWhenCardFromHandToPlay()
        {
            var console = new ConsoleStub(new List<string>{"H7"});

            var user = new User("Bob", console);
            List<Card> userHand = user.GetCards();
            userHand.Add(new Card("T", 5));
            userHand.Add(new Card("H", 7));
            var actionCard = new Card("P", 7);

            Card actual = user.Turn(new List<Card>(), actionCard);
            Assert.True(actual.Image() == "H7");
            Assert.True(userHand.Count == 1);
        }

        [Test]
        public void TurnWhenCardFromStackToPlay()
        {
            var console = new ConsoleStub(new List<string> {"", "P2"});

            var user = new User("Bob", console);
            List<Card> userHand = user.GetCards();
            userHand.Add(new Card("T", 5));
            List<Card> stack = new List<Card> { new Card("P", 2) };
            var actionCard = new Card("P", 7);

            Card expected = stack[0];
            Assert.AreEqual(expected, user.Turn(stack, actionCard));
            Assert.True(stack.Count == 0);
        }

        [Test]
        public void TurnWhenStackEmpty()
        {
            var console = new ConsoleStub(new List<string>{""});
            var user = new User("Bob", console);
            var actionCard = new Card("H", 7);
            Card actual = user.Turn(new List<Card>(), actionCard);
            Assert.AreEqual(actionCard, actual);
        }
    }
}
