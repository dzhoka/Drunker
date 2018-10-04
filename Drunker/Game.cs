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
        IConsole console;

        public Game(List<Card> stack, string userName, int numberOfDrunkers, IConsole console)
        {
            this.console = console;
            this.stack = stack;
            user = new User(userName, console);
            players = new List<Player>();
            players.Add(user);
            for (int i = 0; i < numberOfDrunkers; i++)
            {
                players.Add(new Computer($"Drunker {i + 1}"));
            }
        }

        public List<Player> GetPlayers()
        {
            return players;
        }

        public void Play()
        {
            Card actionCard = TakeCardFromTop(stack);

            while (stack.Any() && HasEveryoneCards())
            {
                console.Clear();
                console.WriteLine($"Current card: {actionCard.Image()}");
                PrintUserCards();

                console.WriteLine("");
                foreach (var player in players)
                {
                    console.WriteLine($"{player.GetName()}: {player.GetCards().Count} in hand");
                }
                console.WriteLine("");

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
                    console.WriteLine(player.GetName() + " wins!");
                    return;
                }
            }
            console.WriteLine("No one wins!");
        }

        public void DealCardsEach(int numberOfCards)
        {
            foreach (var player in players)
            {
                for (int i = 0; i < numberOfCards; i++)
                {
                    MoveCard(stack, player.GetCards());
                }
            }
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
            console.Write("Your cards:");
            foreach (var card in user.GetCards())
                console.Write(" " + card.Image());
            console.WriteLine("");
        }

        public static List<Card> NewStack(int minRank, int maxRank)
        {
            List<Card> cards = new List<Card>();
            string[] suits = { "H", "T", "P", "C" };

            for (int r = minRank; r <= maxRank; r++)
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
