using System;
using System.Collections.Generic;
using NUnit.Framework;
namespace Drunker
{
    [TestFixture]
    public class ComputerTests
    {
        [Test]
        public void FindBestCard()
        {
            Computer computer = new Computer("testDrunker");
            List<Card> computerHand = computer.GetCards();
            computerHand.Add(new Card("P", 5)); // 0
            computerHand.Add(new Card("C", 8)); // 1
            computerHand.Add(new Card("P", 8)); // 2

            var actionCard = new Card("P", 2);
            Assert.True(computer.FindBestCardToPlay(actionCard) == 2);

            actionCard = new Card("C", 6);
            Assert.True(computer.FindBestCardToPlay(actionCard) == 1);

            actionCard = new Card("H", 7);
            Assert.True(computer.FindBestCardToPlay(actionCard) == -1);
        }

        [Test]
        public void TurnWhenHaveCardToPlay()
        {
            Computer computer = new Computer("testDrunker");
            List<Card> computerHand = computer.GetCards();
            computerHand.Add(new Card("P", 5)); // 0
            computerHand.Add(new Card("C", 8)); // 1

            var actionCard = new Card("P", 2);
            Assert.AreEqual(computerHand[0], computer.Turn(new List<Card>(), actionCard));
            Assert.True(computer.GetCards().Count == 1);
        }

        [Test]
        public void TurnWhenCardFromStackToPlay()
        {
            Computer computer = new Computer("testDrunker");
            List<Card> computerHand = computer.GetCards();
            computerHand.Add(new Card("P", 5));
            computerHand.Add(new Card("C", 8));

            var cardFromStack = new Card("H", 7);
            List<Card> stack = new List<Card>{cardFromStack};

            var actionCard = new Card("H", 4);
            Assert.AreEqual(cardFromStack, computer.Turn(stack, actionCard));
            Assert.True(stack.Count == 0);
        }

        [Test]
        public void TurnWhenStackIsEmpty()
        {
            Computer computer = new Computer("testDrunker");
            var actionCard = new Card("H", 4);
            Assert.AreEqual(actionCard, computer.Turn(new List<Card>(), actionCard));
        }
    }
}
