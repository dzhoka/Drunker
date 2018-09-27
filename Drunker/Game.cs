using System;
using System.Collections.Generic;
using System.Linq;

namespace Drunker
{
    public class Game
    {
        List<Card> stack;
        User user;
        List<Player> players;

        public Game(string userName, int numberOfDrunkers)
        {
            stack = NewStack();
            user = new User(userName);
            players = new List<Player>();
            players.Add(user);
            for (int i = 0; i < numberOfDrunkers; i++)
            {
                players.Add(new Computer($"Drunker {i + 1}"));
            }
        }

        public void Play()
        {
            foreach (var player in players)
            {
                for (int i = 0; i < 4; i++)
                {
                    MoveCard(stack, player.GetCards());
                }
            }

            Card actionCard = TakeCardFromTop(stack);

            while (stack.Any() && HasEveryoneCards())
            {
                Console.Clear();
                Console.WriteLine($"Current card: {actionCard.Image()}");
                PrintUserCards();

                Console.WriteLine("");
                foreach (var player in players)
                {
                    Console.WriteLine($"{player.GetName()}: {player.GetCards().Count} in hand");
                }
                Console.WriteLine("");

                foreach (var player in players)
                {
                    actionCard = player.Turn(stack, actionCard);
                }
            }

            foreach (var player in players)
            {
                List<Card> cards = player.GetCards();
                if (cards.Count == 0)
                {
                    Console.WriteLine(player.GetName() + " wins!");
                    return;
                }
            }
            Console.WriteLine("No one wins!");
        }


        bool HasEveryoneCards()
        {
            foreach (var player in players)
            {
                if (!player.GetCards().Any()) return false;
            }
            return true;
        }

        void PrintUserCards()
        {
            Console.Write("Your cards:");
            foreach (var card in user.GetCards())
                Console.Write(" " + card.Image());
            Console.WriteLine("");
        }

        List<Card> NewStack()
        {
            List<Card> cards = new List<Card>();
            string[] suits = { "H", "T", "P", "C" };

            for (int r = 2; r <= 10; r++)
            {
                foreach (string s in suits)
                {
                    cards.Add(new Card(s, r));
                }
            }

            Random rnd = new Random();
            cards.Sort((x, y) => rnd.Next(-1, 1));

            return cards;
        }

        public static Card MoveCard(List<Card> from, List<Card> to)
        {
            Card card = TakeCardFromTop(from);
            to.Add(card);
            return card;
        }

        public static Card TakeCardFromTop(List<Card> list)
        {
            Card card = list[0];
            list.RemoveAt(0);
            return card;
        }
    }
}
