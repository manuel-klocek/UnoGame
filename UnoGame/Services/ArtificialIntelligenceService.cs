using System;
using System.Collections.Generic;
using UnoGame.Repositories;

namespace UnoGame.Services
{
	public class ArtificialIntelligenceService
	{
		public ArtificialIntelligenceService()
		{
			
		}

		public Card? determineMove(Table table, int currentPlayerIndex)
		{
			var hand = table.playerHands[currentPlayerIndex];
			var allowedCards = new List<Card>();
			var stackCard = table.cardStack.GetRealLast();
			var logic = new Logic();

			hand.GetPlayerCards().ForEach(card =>
			{
				if (logic.Allowed(table.cardStack, card)) allowedCards.Add(card);
			});
			if (allowedCards.Count == 0) return null;

			var card = GetBestChoices(allowedCards)[0];

			return card;
		}

		private List<Card> GetBestChoices(List<Card> allowedCards)
		{
			var sameColorCards = new List<Card>();
			string[] colors = { "blue", "green", "yellow", "red" };

			int storageNumber = 0;
			string determinedColor = "black";
			foreach (var color in colors)
			{
				int number = 0;
				foreach(var card in allowedCards)
				{
					if (card.GetColor() == color) number++;
				}
				if (storageNumber < number)
				{
				 	storageNumber = number;
					determinedColor = color;
				}
			}

			allowedCards.ForEach(card =>
				{
					if (card.GetColor() == determinedColor) sameColorCards.Add(card);
				});

			return sameColorCards;
        }

		public string determineColor(List<Card> cards)
		{
            string[] colors = { "blue", "green", "yellow", "red" };
			var storageNumber = 0;
			var determinedColor = "";
            foreach (var color in colors)
			{
				var number = 0;
                foreach (var card in cards)
                {
                    if (card.GetColor() == color) number++;
                }

				if (storageNumber < number)
				{
					storageNumber = number;
					determinedColor = color;
				}
            }
			return determinedColor;
		}
	}
}