using System;
using System.Collections.Generic;

namespace UnoGame.Services
{
	public class Helpers
	{
        public static void Print(List<Card> playerCards)
        {
            Console.WriteLine("Die Karten deiner Hand sind: ");

            foreach (var card in playerCards)
            {
                Console.WriteLine(card.GetColor() + " " + card.GetSymbol());
            }
        }
    }
}

