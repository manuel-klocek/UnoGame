using System;
using System.Collections.Generic;

namespace UnoGame.Repositories
{
	public class Table
	{
		public CardStack cardStack;
        public TakeStack takeStack;
        public Rotation rotation;
		public Players players;

		public Table(CardStack cardStack, TakeStack takeStack, Rotation rotation, Players players)
		{
			this.cardStack = cardStack;
			this.takeStack = takeStack;
			this.rotation = rotation;
			this.players = players;
		}
	}
}