using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
	interface IDeck
	{
		public Cards Cards { get; set; }
		public void Shuffle();
		public Card DrawCard();
	}
}
