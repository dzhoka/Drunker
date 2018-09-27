using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Drunker
{
    public interface Player
    {
        Card Turn(List<Card> stack, Card actionCard);
        List<Card> GetCards();
        string GetName();
    }
}
