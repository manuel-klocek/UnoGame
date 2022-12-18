using System;
using System.Collections.Generic;
using UnoGame.Repositories;
using UnoGame.Services;

namespace UnoGame
{
    public class Players
    {
        readonly List<Player> players;
        readonly List<PlayerHand> playerHands;

        public Players(int playerCount, int botCount, List<PlayerHand> hands)
        {
            this.playerHands = hands;

            var players = new List<Player>();
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine("Gebe den Namen des Spielers {0} ein: ", i + 1);
                players.Add(new Player(Console.ReadLine()));
            }
            for(int i = 0; i < botCount; i++)
            {
                players.Add(new Player($"Bot {i + 1}", true));
            }

            this.players = players;
        }

        public List<Player> GetPlayers()
        {
            return this.players;
        }

        public List<PlayerHand> GetPlayerHands()
        {
            return this.playerHands;
        }
    }

    public class Player
    {
        string name;
        bool bot;

        public Player(string name, bool bot = false)
        {
            this.name = name;
            this.bot = bot;
        }

        public string GetName()
        {
            return this.name;
        }

        public bool GetBot()
        {
            return this.bot;
        }
    }

    public class PlayerHand
    {
        List<Card> playerCards = new List<Card>();
        readonly Sort sort = new Sort();

        public PlayerHand(List<Card> playerCards)
        {
            this.playerCards = playerCards;
        }


        public List<Card> GetPlayerCards()
        {
            return this.playerCards;
        }

        public void RemoveOne(Card card)
        {
            this.playerCards.Remove(card);
        }

        public void Add(Card card)
        {
            this.playerCards.Add(card);
            this.playerCards = sort.Hands(new List<PlayerHand> { new PlayerHand(this.playerCards) })[0].GetPlayerCards();
        }
    }
}