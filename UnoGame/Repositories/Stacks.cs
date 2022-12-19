using System;
using System.Collections.Generic;

namespace UnoGame.Repositories
{
	public class CardStack
    {

        readonly List<Card> stack;

        public CardStack(Card firstCard)
        {
            this.stack = new List<Card>();
            this.stack.Add(firstCard);
        }


        public List<Card> GetStackCards()
        {
            return this.stack;
        }

        public void Add(Card takeCard)
        {
            this.stack.Add(takeCard);
        }

        public Card GetLast()
        {
            return this.stack[this.stack.Count - 1];
        }

        public Card GetRealLast()
        {
            var last = this.stack[this.stack.Count - 1];
            if (last.GetColor() != "") return last;
            return this.stack[this.stack.Count - 2];
        }
    }

    public class TakeStack
    {
        List<Card> takeStack;

        public TakeStack(List<Card> stackCards)
        {
            this.takeStack = stackCards;
        }


        public List<Card> GetTakeCards()
        {
            return this.takeStack;
        }

        public void UpdateTakeCards(Table table)
        {
            var hands = table.players.GetPlayerHands();

            var cardsInGame = new List<Card>();
            hands.ForEach(hand => hand.GetPlayerCards().ForEach(card => cardsInGame.Add(card)));

            var cards = new SetupCards().Run();

            cards.ForEach(card => { if (cardsInGame.Contains(card)) cards.Remove(card); });

            this.takeStack = cards;
        }

        public Card TakeOne(Table table)
        {
            if (this.takeStack.Count <= 1) UpdateTakeCards(table);
            var card = this.takeStack[0];
            this.takeStack.Remove(card);
            return card;
        }
    }
}