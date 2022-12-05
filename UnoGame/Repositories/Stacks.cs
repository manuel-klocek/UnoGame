using System;
using System.Collections.Generic;

namespace UnoGame.Repositories
{
	public class CardStack
    {

        //Stack of played cards
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

        //Stack of cards to take from stack
        List<Card> stack;

        public TakeStack(List<Card> stackCards)
        {
            this.stack = stackCards;
        }


        public List<Card> GetTakeCards()
        {
            return this.stack;
        }

        public void UpdateTakeCards(List<Card> stack)
        {
            //TODO
            this.stack = stack;
        }

        public Card TakeOne()
        {
            var card = this.stack[0];
            this.stack.Remove(card);
            return card;
        }
    }
}