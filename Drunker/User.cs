using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Drunker
{
    public class User : Player
    {
        List<Card> hand = new List<Card>();
        string name;
        IConsole console;

        public User(string name, IConsole console)
        {
            this.name = name;
            this.console = console;
        }

        public string GetName()
        {
            return name;  
        }

        public Card Turn(List<Card> stack, Card actionCard)
        {
            while (true)
            {
                console.Write("Put your card: ");
                string input = console.ReadLine();
                if (input == "")
                {
                    if (stack.Count == 0) return actionCard;
                    Card card = Game.MoveCard(stack, hand);
                    console.WriteLine("Your card: " + card.Image());
                    continue;
                }

                int index = IndexOfCardByImage(hand, input);
                if (index == -1) continue;

                Card playedCard = hand[index];

                if (!playedCard.Match(actionCard))
                {
                    console.WriteLine("Card doesn't match");
                    continue;
                }

                actionCard = playedCard;
                hand.RemoveAt(index);

                return actionCard;
            }
        }

        public List<Card> GetCards()
        {
            return hand;
        }

        int IndexOfCardByImage(List<Card> cards, string image)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (image == cards[i].Image()) return i;
            }
            return -1;
        }

    }
}



