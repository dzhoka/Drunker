using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Drunker
{
    public class Computer : Player
    {
        List<Card> hand = new List<Card>();

        string name;

        public Computer(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public Card Turn(List<Card> stack, Card actionCard)
        {
            int index = FindBestCardToPlay(actionCard);
            if (index != -1) 
            {
                actionCard = hand[index];
                hand.RemoveAt(index);
                return actionCard;
            }

            while (true)
            {
                if (stack.Count == 0) return actionCard;
                Card fromStack = Game.TakeCardFromTop(stack);
                if (actionCard.Match(fromStack)) return fromStack;
                hand.Add(fromStack);
            }
        }

        public List<Card> GetCards()
        {
            return hand;
        }

        public int FindBestCardToPlay(Card actionCard)
        {
            List<int> candidates = new List<int>();
            List<int> matches = new List<int>();

            for (int i = 0; i < hand.Count; i++)
            {
                if (actionCard.Match(hand[i]))
                {
                    candidates.Add(i);
                }
            }
            if (candidates.Count == 0) return -1;


            for (int i = 0; i < candidates.Count; i++)
            {
                int index = candidates[i];
                Card candidate = hand[index];
                int match = 0;
                foreach (var card in hand)
                {
                    if (candidate.Match(card)) match++;                     
                }
                matches.Add(match);
            }

            int result = 0;
            int max = 0;
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i] > max)
                {
                    max = matches[i];
                    result = candidates[i];
                }
            }

            return result;
        }

    }
}
