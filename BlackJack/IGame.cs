using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
	interface IGame
	{
		public Deck GameDeck { get; set; }
		public Players Players { get; set; }
		public bool IsMultiPlayer { get; set; }
		public Player Dealer { get; set; }

		public void InitializeNewGame();
		public void DealFirstRound();
		public void HitPlayer(Player player);
		public void HitDealer();
		public bool CheckAnyoneLeft();
	}
}
