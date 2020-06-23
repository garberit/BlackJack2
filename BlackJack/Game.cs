using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
	class Game : IGame
	{
		public Deck GameDeck { get; set; }
		public Players Players { get; set; } //think of sorting based on score
		public bool IsMultiPlayer { get; set; }
		public Player Dealer { get; set; }


		/// <summary>
		/// Creates a new deck and calls InitializeNewGame
		/// </summary>
		public Game()
		{
			GameDeck = new Deck();
			InitializeNewGame();

		}

		/// <summary>
		/// Creates a new deck and calls InitializeNewGame with multiplayer
		/// </summary>
		/// <param name="numberOfPlayers">How many players will play</param>
		public Game(int numberOfPlayers)
		{
			GameDeck = new Deck();
			InitializeNewGame(numberOfPlayers);
		}

		/// <summary>
		/// Creates a new deck and calls InitializeNewGame with already defined players
		/// </summary>
		/// <param name="players">A predefined list of players</param>
		public Game(Players players)
		{
			GameDeck = new Deck();
			InitializeNewGame(players);
		}

		/// <summary>
		/// Immutable Single player game.
		/// Shuffles the deck of cards
		/// Creates a list of players
		/// Adds Dealer as player[0]
		/// Adds another player
		/// Deals initial cards
		/// </summary>
		public void InitializeNewGame()
		{
			GameDeck.Shuffle();
			Players = new Players();
			Dealer = new Player();
			Dealer.Name = "Dealer";
			Players.Add(Dealer);			
			Players.Add(new Player());
			DealFirstRound();
		}

		/// <summary>
		/// Mutable Single player game.
		/// Shuffles the deck of cards
		/// Creates a list of players
		/// Adds Dealer as player[0]
		/// Adds another player
		/// Deals initial cards
		/// </summary>
		/// <param name="NumberOfPlayers">Defaults to 1 but can be changed</param>
		public void InitializeNewGame(int NumberOfPlayers = 1)
		{
			GameDeck.Shuffle();
			Players = new Players();
			Dealer = new Player();
			Dealer.Name = "Dealer";
			Players.Add(Dealer);
			for (int i = 0; i < NumberOfPlayers; i++)
			{
				Players.Add(new Player());
			}
			DealFirstRound();
			if (NumberOfPlayers > 1)
			{
				IsMultiPlayer = true;
			}
		}

		

		/// <summary>
		/// Shuffles the deck of cards
		/// Creates a list of players
		/// Adds another player
		/// Deals initial cards
		/// </summary>
		/// <param name="players">Already existing list of players</param>
		public void InitializeNewGame(Players players)
		{
			GameDeck.Shuffle();
			Players = new Players();		
			Dealer = new Player();
			Dealer.Name = "Dealer";
			Dealer.IsDealer = true;
			Players.Add(Dealer);		

			foreach (Player player in players)
			{
				player.Hand = new Cards();
				Players.Add(player);
			}				
			
			DealFirstRound();
			if (players.Count > 2)
			{
				IsMultiPlayer = true;
			}
		}
		/// <summary>
		/// Counds how many players besides the dealer has't stood or busted yet(is still in the game)
		/// Dealer plays last so this is the check
		/// </summary>
		/// <returns>True if still playing and dealer shoukd wait and false if dealer's turn</returns>
		public bool CheckAnyoneLeft()
		{
			int playerCount = Players.Count - 1;
			foreach (Player player in Players)
			{
				if (player.IsDealer)
				{
					continue;
				}

				if (player.PlayerStands || player.IsBusted())
				{
					playerCount--;
				}
			}

			return playerCount > 0 ? true : false;
		}

		/// <summary>
		/// 2 rounds of single card dealing to each player in the game
		/// </summary>
		public void DealFirstRound()
		{
			for (int i = 0; i < 2; i++)
			{
				foreach (Player player in Players)
				{
					player.Hand.Push(GameDeck.DrawCard());
				}
			}
		}

		/// <summary>
		/// deals a card to the dealer, only if sum of dealer's card is smaller or equal to 17.
		/// Every card dealt is taken out of the game deck.
		/// </summary>
		public void HitDealer()
		{
			if (Dealer.GetSumOfAllCards() <= 17)
			{
				Dealer.Hand.Push(GameDeck.Cards.Pop());
			}
		}

		/// <summary>
		/// Deals a card to the player.
		/// Every card dealt is taken out of the game deck.
		/// </summary>
		/// <param name="player">the player which a card is being dealt to</param>
		public void HitPlayer(Player player)
		{
			player.Hand.Push(GameDeck.Cards.Pop());
		}

		/// <summary>
		/// Checks all players with the same sum of cards 
		/// </summary>
		/// <param name="players">The players which are int he game</param>
		/// <returns>A list of players with same sum of cards to later be determined how to score</returns>
		public List<Player> IsPush(Players players)
		{
			List<Player> playersWithPush = new List<Player>();
			List<Player> migrationList = players;
			migrationList.OrderByDescending(p => p.GetSumOfAllCards());
			foreach (Player playa in migrationList)
			{
				if (playa.GetSumOfAllCards() == migrationList[0].GetSumOfAllCards())
				{
					playersWithPush.Add(playa);
				}
			}
			return playersWithPush;
		}

		/// <summary>
		/// Sorts all players by their scores in a new list (to not break dealer at [0] position)
		/// </summary>
		/// <returns>A list of sorted highest to lowest scores</returns>
		public List<Player> GetHighestScore()
		{
			List<Player> playersWithHighScore = Players;
			playersWithHighScore.OrderByDescending(p => p.Score);			
			return playersWithHighScore;
		}
	}
}
