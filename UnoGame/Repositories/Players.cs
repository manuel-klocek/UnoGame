using System;
using System.Collections.Generic;

namespace UnoGame
{
    internal class Players
    {
        readonly List<string> players;

        public Players(int playerCount)
        {
            var players = new List<string>();
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine("Gebe den Namen des Spielers {0} ein: ", i + 1);
                players.Add(Console.ReadLine());
            }
            this.players = players;
        }

        public List<string> GetPlayers()
        {
            return this.players;
        }

        public void ReversePlayers()
        {
            this.players.Reverse();
        }
    }
}