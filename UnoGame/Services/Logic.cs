using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnoGame.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace UnoGame
{
    internal class Logic
    {

        public List<PlayerHand> StartGame(List<Card> cards, int playerCount)
        {
            var random = new Random();
            //for each player
            var playerHands = new List<PlayerHand>();
            for(int i = 0; i < playerCount; i++)
            {
                //7 cards per player
                var hand = new List<Card>();
                for(int j = 0; j < 7; j++)
                {
                    var randomCard = random.Next(0, cards.Count);
                    hand.Add(cards[randomCard]);
                    cards.Remove(cards[randomCard]);
                }
                playerHands.Add(new PlayerHand(hand));
            }
            return playerHands;
        }

        public bool evaluate(string command, TakeStack takeStack, CardStack cardStack, PlayerHand hand, Card card = null)
        {
            Card takeCard;
            switch (command) {
                case "take":
                    takeCard = takeStack.TakeOne();
                    if (Allowed(cardStack, takeCard))
                    {
                        Console.WriteLine("{0} {1} ist dein Karte. Möchtest du sie legen? (ja/nein): ", takeCard.GetColor(), takeCard.GetSymbol());
                        if (String.Compare(Console.ReadLine(), "ja") == 0)
                        {
                            CheckForBlackColor(takeCard);
                            cardStack.Add(takeCard);
                        }
                        else hand.Add(takeCard);
                    }
                    break;
                case "place":
                    if (Allowed(cardStack, card))
                    {
                        CheckForBlackColor(card);
                        cardStack.Add(card);
                        hand.RemoveOne(card);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("{0} {1} is not an allowed Card!", card.GetColor(), card.GetSymbol());
                        return false;
                    }
            }
            return true;
        }

        private void AddPenaltyToHand(int takeNum, PlayerHand hand, TakeStack takeStack)
        {
            for(int i = 0; i < takeNum; i++)
            {
                var taken = takeStack.TakeOne();
                hand.Add(taken);
                Console.WriteLine("Du hast diese Karte gezogen: {0} {1}", taken.GetColor(), taken.GetSymbol());
            }
        }

        
        public bool Allowed(CardStack cardStack, Card card)
        {
            bool allowed;
            var lastPlacedCard = cardStack.GetRealLast();

            if ((card.GetSymbol() == "+2" && lastPlacedCard.GetSymbol() == "+2") ||
                (card.GetColor() == "black")) allowed = true;
            else if ((card.GetSymbol() == lastPlacedCard.GetSymbol()) ||
                     (card.GetColor() == lastPlacedCard.GetColor())) allowed = true;
            else allowed = false;
            return allowed;
        }

        private void CheckForBlackColor(Card card)
        {
            if(card.GetColor() == "black") askUserForColor(card);
        }


        private void askUserForColor(Card card)
        {
            Console.WriteLine("Gib deine Wunschfarbe ein (green, red, yellow, blue): ");
            card.SetColor(Console.ReadLine());
        }


        public bool CheckAndRunEventsThenSkip(CardStack stack, PlayerHand hand, TakeStack takeStack, Players playersSetup, Rotation rotation)
        {
            var takeNum = 0;
            switch(stack.GetLast().GetSymbol())
            {
                case "+2":
                    if (CanForwardPenalty(stack, hand)) return true;
                    takeNum = SearchForStreak(stack);
                    AddPenaltyToHand(takeNum, hand, takeStack);
                    break;
                case "+4":
                    if (CanForwardPenalty(stack, hand)) return true;
                    takeNum = SearchForStreak(stack);
                    AddPenaltyToHand(takeNum, hand, takeStack);
                    break;
                case "!!":
                    Console.WriteLine("Du musst aussetzen!");
                    break;
                case "<>":
                    Console.WriteLine("Richtungswechsel!");
                    rotation.Change();
                    break;
                default:
                    return false;
            }
            AddUnvisibleCard(stack);
            return true;
        }

        private bool CanForwardPenalty(CardStack stack, PlayerHand hand)
        {
            var cards = hand.GetPlayerCards();

            foreach(var card in cards)
            {
                if (card.GetSymbol() == stack.GetLast().GetSymbol())
                {
                    Console.WriteLine("Möchtest du die Strafe weiterleiten? Wenn ja gib den Index der Karte ein: ");
                    var cardIndex = Convert.ToInt32(Console.ReadLine());
                    if (cards[cardIndex].GetSymbol() == stack.GetLast().GetSymbol())
                    {
                        stack.Add(cards[cardIndex]);
                        hand.RemoveOne(cards[cardIndex]);
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        private int SearchForStreak(CardStack stack)
        {
            var cards = stack.GetStackCards();
            var takeNum = 0;

            for(int i = cards.Count - 1; i >= 0; i--)
            {
                var card = cards[i].GetSymbol();
                if (card.Contains("+")) takeNum += Convert.ToInt32(card.Substring(1));
                else break;
            }
            return takeNum;
        }

        private void AddUnvisibleCard(CardStack stack)
        {
            stack.Add(new Card("", ""));
        }
    }       
}