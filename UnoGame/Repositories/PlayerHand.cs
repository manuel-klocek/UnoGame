using System;
using System.Collections.Generic;

namespace UnoGame.Repositories
{
	public class PlayerHand
	{
        readonly List<Card> playerCards;

        public PlayerHand(List<Card> playerCards)
		{
			this.playerCards = playerCards;
		}


		public List<Card> GetPlayerCards()
		{
			return this.playerCards;
		}

        public void RemoveOne(Card card)
        {
			this.playerCards.Remove(card);
        }

        public void Add(Card card)
        {
			this.playerCards.Add(card);
        }
    }
}