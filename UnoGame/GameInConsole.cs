using System;
using System.Collections.Generic;
using UnoGame.Repositories;
using UnoGame.Services;

namespace UnoGame
{
	public class GameInConsole
	{
        readonly ArtificialIntelligenceService ai = new ArtificialIntelligenceService();

		public void Run()
		{
            Console.WriteLine("Geben Sie die Anzahl der Spieler an: ");
            var playerCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Geben Sie die Anzahl der KIs an: ");
            var botCount = Convert.ToInt32(Console.ReadLine());
            var logic = new Logic();
            var setup = new SetupCards();
            var sort = new Sort();
            var cards = setup.Run();
            var unsortedHands = logic.StartGame(cards, playerCount + botCount);
            var table = new Table(setup.setupCardStack(cards), new TakeStack(cards), new Rotation(), new Players(playerCount, botCount, sort.Hands(unsortedHands)));

            int currentPlayerIndex = 0;
            while (table.players.GetPlayers().Count > 1)
            {
                var playerCards = table.players.GetPlayerHands()[currentPlayerIndex].GetPlayerCards();
                var currentPlayer = table.players.GetPlayers()[currentPlayerIndex];

                Console.WriteLine();
                Console.WriteLine(table.players.GetPlayers()[currentPlayerIndex].GetName());


                var direction = table.rotation.Get();
                if (!logic.CheckAndRunEventsThenSkip(table, currentPlayerIndex, currentPlayer.GetBot()))
                {
                    Console.WriteLine("Stack: " + table.cardStack.GetRealLast().GetColor() + " " + table.cardStack.GetRealLast().GetSymbol());
                    Print(playerCards);

                    Card? card = null;
                    string? command = null;
                    if (!currentPlayer.GetBot()) command = GetUserCommand();
                    else card = ai.determineMove(table, currentPlayerIndex);

                    if (command != null && command == "place") card = playerCards[GetCardIndex()];

                    if (!logic.evaluate(table, currentPlayerIndex, card, currentPlayer.GetBot())) currentPlayerIndex--;
                }

                if (table.players.GetPlayerHands()[currentPlayerIndex].GetPlayerCards().Count == 0)
                {
                    var num = table.players.GetPlayers().Count;
                    table.players.GetPlayers().Remove(currentPlayer);
                    var num2 = table.players.GetPlayers().Count;
                    table.players.GetPlayerHands().Remove(table.players.GetPlayerHands()[currentPlayerIndex]);
                    Console.WriteLine();
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine($"        {currentPlayer.GetName()}: UNO UNO!");
                    Console.WriteLine("------------------------------------------");
                }

                if (direction != table.rotation.Get() && !table.rotation.Get()) currentPlayerIndex-= 2;
                else if (table.rotation.Get()) currentPlayerIndex++;
                else currentPlayerIndex--;

                var players = table.players.GetPlayers();
                if (currentPlayerIndex <= -1 && !table.rotation.Get()) currentPlayerIndex = players.Count - 1;
                else if (currentPlayerIndex >= players.Count && table.rotation.Get()) currentPlayerIndex = 0;
            }
        }

        private static int GetCardIndex()
        {
            Console.WriteLine("Gib den Index der gewählten Karte ein: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        private string GetUserCommand()
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