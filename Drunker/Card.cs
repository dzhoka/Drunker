using System;
namespace Drunker
{
    public class Card
    {
        private string suit;
        public int rank;

        public Card(string suit, int rank)
        {
            this.suit = suit;
            this.rank = rank;
        }

        public string Image()
        {
            return $"{suit}{rank}";
        }

        public bool Match(Card other)
        {
            return (suit == other.suit || rank == other.rank);
        }
    }
}
