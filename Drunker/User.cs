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

        public User(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;  
        }

        public Card Turn(List<Card> stack, Card actionCard)
        {
            while (true)
            {
                Console.Write("Put your card: ");
                string input = Console.ReadLine();
                if (input == "")
                {
                    if (stack.Count == 0) return actionCard;
                    Card card = Game.MoveCard(stack, hand);
                    Console.WriteLine("Your card: {0}", card.Image());
                    continue;
                }

                int index = IndexOfCardByImage(hand, input);
                if (index == -1) continue;

                Card playedCard = hand[index];

                if (!playedCard.Match(actionCard))
                {
                    Console.WriteLine("Card doesn't match");
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



