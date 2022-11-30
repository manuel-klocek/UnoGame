using System.Collections.Generic;

namespace UnoGame.Models
{
	internal class PlayerCards
	{
        List<Card> playerCards;

        public PlayerCards(List<Card> playerCards)
		{
			this.playerCards = playerCards;
		}


		public List<Card> GetPlayerCards()
		{
			return this.playerCards;
		}

		public void SetPlayerCards(List<Card> playerCards)
		{
			this.playerCards = playerCards;
		}
	}
}

