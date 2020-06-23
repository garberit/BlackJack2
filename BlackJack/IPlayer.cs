using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
	interface IPlayer
	{
		public double Score { get; set; }
		public string Name { get; set; }
		public Cards Hand { get; set; }
		public bool IsDealer { get; set; }
		public bool PlayerStands { get; set; }

		public int GetSumOfAllCards();
		public bool HasBlackJack();
		public bool IsBusted();

	}
}
