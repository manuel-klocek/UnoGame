using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnoGame.Repositories;
using UnoGame.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace UnoGame
{
    internal class Logic
    {
        readonly ArtificialIntelligenceService ai = new ArtificialIntelligenceService();

        public List<PlayerHand> StartGame(List<Card> cards, int playerCount)
        {
            var random = new Random();
            var playerHands = new List<PlayerHand>();
            for(int i = 0; i < playerCount; i++)
            {
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

        public bool evaluate(Table table, int currentPlayerIndex, Card? card, bool isBot)
        {
            if(card == null) ExecuteTake(table, currentPlayerIndex, isBot);
            else return ExecutePlace(table, card, currentPlayerIndex, isBot);
            return true;
        }

        private void ExecuteTake(Table table, int currentPlayerIndex, bool isBot)
        {
            var takeCard = table.takeStack.TakeOne();
            var hand = table.playerHands[currentPlayerIndex];
            if (!Allowed(table.cardStack, takeCard))
            {
                hand.Add(takeCard);
                Console.WriteLine($"Du hast die Karte {takeCard.GetColor()} {takeCard.GetSymbol()} aufgenommen!");
                return;
            }

            if (!isBot)
            {
                Console.WriteLine("{0} {1} ist dein Karte. Möchtest du sie legen? (ja/nein): ", takeCard.GetColor(), takeCard.GetSymbol());
                if (String.Compare(Console.ReadLine(), "ja") != 0) hand.Add(takeCard);
                else
                {
                    CheckForBlackColor(hand, takeCard, isBot);
                    table.cardStack.Add(takeCard);
                    Console.WriteLine($"Du hast die Karte {takeCard.GetColor()} {takeCard.GetSymbol()} gelegt!");
                }
                return;
            }

            CheckForBlackColor(hand, takeCard, isBot);
            table.cardStack.Add(takeCard);
            Console.WriteLine($"Du hast die Karte {takeCard.GetColor()} {takeCard.GetSymbol()} gelegt!");
        }

        private bool ExecutePlace(Table table, Card card, int currentPlayerIndex, bool isBot)
        {
            var hand = table.playerHands[currentPlayerIndex];
            if (Allowed(table.cardStack, card))
            {
                CheckForBlackColor(hand, card, isBot);
                table.cardStack.Add(card);
                hand.RemoveOne(card);
                Console.WriteLine($"Du hast die Karte {card.GetColor()} {card.GetSymbol()} gelegt!");
                return true;
            }
            Console.WriteLine("{0} {1} is not an allowed Card!", card.GetColor(), card.GetSymbol());
            return false;
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
            var lastPlacedCard = cardStack.GetRealLast();

            if ((card.GetSymbol() == "+2" && lastPlacedCard.GetSymbol() == "+2") ||
                (card.GetColor() == "black")) return true;
            else if ((card.GetSymbol() == lastPlacedCard.GetSymbol()) ||
                     (card.GetColor() == lastPlacedCard.GetColor())) return true;
            else return false;
        }

        private void CheckForBlackColor(PlayerHand hand, Card card, bool isBot)
        {
            if (card.GetColor() != "black") return;
            if (isBot)
            {
                card.SetColor(ai.determineColor(hand.GetPlayerCards()));
            }
            else
            {
                Console.WriteLine("Gib deine Wunschfarbe ein (green, red, yellow, blue): ");
                card.SetColor(Console.ReadLine());
            }
        }


        public bool CheckAndRunEventsThenSkip(Table table, int currentPlayerIndex)
        {
            var hand = table.playerHands[currentPlayerIndex];
            switch(table.cardStack.GetLast().GetSymbol())
            {
                case "+2":
                    if (CanForwardPenalty(table.cardStack, hand)) return true;
                    var takeNum = SearchForStreak(table.cardStack);
                    AddPenaltyToHand(takeNum, hand, table.takeStack);
                    break;
                case "+4":
                    if (CanForwardPenalty(table.cardStack, hand)) return true;
                    takeNum = SearchForStreak(table.cardStack);
                    AddPenaltyToHand(takeNum, hand, table.takeStack);
                    break;
                case "!!":
                    Console.WriteLine("Du musst aussetzen!");
                    break;
                case "<>":
                    Console.WriteLine("Richtungswechsel!");
                    table.rotation.Change();
                    break;
                default:
                    return false;
            }
            AddUnvisibleCard(table.cardStack);
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