using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
	class Player : IPlayer
	{
		public double Score { get; set; }
		public string Name { get; set; }
		public Cards Hand { get; set; }
		public bool IsDealer { get; set; }
		public bool PlayerStands { get; set; }

		/// <summary>
		/// Starts each player with a 0 score. If the player is the dealer,
		/// The name will be set to Dealer
		/// </summary>
		public Player()
		{
			Score = 0;
			PlayerStands = false;
			Hand = new Cards();
			if (IsDealer == true)
			{
				Name = "Dealer";
			}
		}
		/// <summary>
		/// Summarizes all the values of a given stack(Hand).
		/// For Ace, performs a calculation based on other cards in the given Hand.
		/// </summary>
		/// <returns>The sum of the card values in a hand</returns>
		public int GetSumOfAllCards()
		{
			int sum = 0;
			foreach (var card in Hand)
			{
				switch (card.Value)
				{
					case 2:
						sum += 2;
						break;
					case 3:
						sum += 3;
						break;
					case 4:
						sum += 4;
						break;
					case 5:
						sum += 5;
						break;
					case 6:
						sum += 6;
						break;
					case 7:
						sum += 7;
						break;
					case 8:
						sum += 8;
						break;
					case 9:
						sum += 9;
						break;
					case 10:
						sum += 10;
						break;
					case 1:
						if (sum >= 11)
						{
							sum += 1;
						}
						else
						{
							sum += 11;
						}
						break;
					default:
						break;
				}
			}
			return sum;
		}

		/// <summary>
		/// Checks if the player has blackjack
		/// </summary>
		/// <returns>Tue for blackjack and false otherwise</returns>
		public bool HasBlackJack()
		{
			return Hand.Count == 2 && GetSumOfAllCards() == 21 ? true : false;
		}

		/// <summary>
		/// Checks if player's sum of card is greater than 21.
		/// </summary>
		/// <returns>True for busted, false otherwise</returns>
		public bool IsBusted()
		{
			return GetSumOfAllCards() > 21 ? true : false;
		}
	}
}
