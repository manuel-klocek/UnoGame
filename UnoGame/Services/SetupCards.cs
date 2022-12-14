using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnoGame.Repositories;

namespace UnoGame
{
	public class SetupCards
	{
        public List<Card> Run()
        {
            var cards = new List<Card>();

            string[] colors = { "blue", "green", "yellow", "red" };

            foreach (var color in colors)
            {
                //0
                cards.Add(new Card(color, "0"));

                for (int j = 1; j < 10; j++)
                {
                    //1-9 twice
                    cards.Add(new Card(color, j.ToString()));
                    cards.Add(new Card(color, j.ToString()));
                }

                //Special Cards (each twice): +2 (2 Aufnehmen); !! (Aussetzen); && (Richtungswechsel)
                cards.Add(new Card(color, "+2"));
                cards.Add(new Card(color, "+2"));
                cards.Add(new Card(color, "!!"));
                cards.Add(new Card(color, "!!"));
                cards.Add(new Card(color, "<>"));
                cards.Add(new Card(color, "<>"));
            }

            //Black Cards: +4 und Farbwahl (==)
            cards.Add(new Card("black", "+4"));
            cards.Add(new Card("black", "+4"));
            cards.Add(new Card("black", "+4"));
            cards.Add(new Card("black", "+4"));

            cards.Add(new Card("black", "=="));
            cards.Add(new Card("black", "=="));
            cards.Add(new Card("black", "=="));
            cards.Add(new Card("black", "=="));

            return Shuffle(cards); 
        }


        public List<Card> Shuffle(List<Card> cards)
        {
            var random = new Random();
            var newShuffledList = new List<Card>();
            var cardsCount = cards.Count;
            for (int i = 0; i < cardsCount; i++)
            {
                var randomElementInList = random.Next(0, cards.Count);
                newShuffledList.Add(cards[randomElementInList]);
                cards.Remove(cards[randomElementInList]);
            }
            return newShuffledList;
        }

        public CardStack setupCardStack(List<Card> cards)
        {
            foreach(var card in cards)
            {
                if (card.GetColor() != "black" &&
                    card.GetSymbol() != "!!" &&
                    card.GetSymbol() != "<>" &&
                    card.GetSymbol() != "+2") return new CardStack(card);
            }
            return new CardStack(new Card("", ""));
        }
    }
}