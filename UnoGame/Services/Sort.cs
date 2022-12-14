using System;
using System.Collections.Generic;
using UnoGame.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace UnoGame.Services
{
	public class Sort
	{
		public Sort()
		{

		}

		public List<PlayerHand> Hands(List<PlayerHand> playerHands)
		{
			var colors = new List<string> { "black", "green", "blue", "yellow", "red" };

            var sortedHand = new List<PlayerHand>();
            foreach (var hand in playerHands)
			{
                var sortedCards = new List<Card>();
                foreach (var color in colors)
                {
                    foreach (var card in hand.GetPlayerCards())
                    {
                        if (card.GetColor() == color)
                        {
                            sortedCards.Add(card);
                        }
                    }
                }
                sortedHand.Add(new PlayerHand(sortedCards));
            }
			return sortedHand;
		}
	}
}