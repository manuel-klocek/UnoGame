using System;
using System.Collections.Generic;
using UnoGame.Repositories;

namespace UnoGame
{
	public class GameInConsole
	{
		public GameInConsole() {}

		public void Run()
		{
            Console.WriteLine("Geben Sie die Anzahl der Spieler an: ");
            var playerCount = Convert.ToInt32(Console.ReadLine());
            var logic = new Logic();
            var setup = new SetupCards();
            var playersSetup = new Players(playerCount);
            var clockwiseRotation = new Rotation();
            var players = playersSetup.GetPlayers();
            var cards = setup.Run();
            var playerHands = logic.StartGame(cards, playerCount);
            var cardStack = setup.setupCardStack(cards);
            var takeStack = new TakeStack(cards);


            int currentPlayer = 0;
            while (players.Count > 1)
            {
                Console.WriteLine();
                Console.WriteLine(players[currentPlayer]);
                var playerCards = playerHands[currentPlayer].GetPlayerCards();

                if (playerCards.Count == 0)
                {
                    players.Remove(players[currentPlayer]);
                }

                var direction = clockwiseRotation.Get();
                if (!logic.CheckAndRunEventsThenSkip(cardStack, playerHands[currentPlayer], takeStack, playersSetup, clockwiseRotation))
                {
                    Console.WriteLine("Stack: " + cardStack.GetRealLast().GetColor() + " " + cardStack.GetRealLast().GetSymbol());
                    Print(playerCards);
                    var command = GetUserCommand();
                    Card card = null;

                    if (command != "take") card = playerCards[GetCardIndex()];

                    if (!logic.evaluate(command, takeStack, cardStack, playerHands[currentPlayer], card))
                    {
                        currentPlayer--;
                    }
                }

                if (direction != clockwiseRotation.Get() && !clockwiseRotation.Get()) currentPlayer-= 2;
                else if (clockwiseRotation.Get()) currentPlayer++;
                else currentPlayer--;

                if (currentPlayer == -1 && !clockwiseRotation.Get()) currentPlayer = playerCount - 1;
                else if (currentPlayer == -2 && !clockwiseRotation.Get()) currentPlayer = playerCount - 2;
                else if (currentPlayer >= playerCount && clockwiseRotation.Get()) currentPlayer = 0;
            }
        }

        private static int GetCardIndex()
        {
            Console.WriteLine("Gib den Index der gewählten Karte ein: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        private static string GetUserCommand()
        {
            Console.WriteLine("Gib deine Aktion ein (place, take): ");
            return Console.ReadLine();
        }

        private static void Print(List<Card> playerCards)
        {
            Console.WriteLine("Die Karten deiner Hand sind: ");

            foreach (var card in playerCards)
            {
                Console.WriteLine(card.GetColor() + " " + card.GetSymbol());
            }
        }
    }
}