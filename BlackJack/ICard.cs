using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
	interface ICard
	{
		public string Face { get; set; }
		public int Value { get; set; }
	}
}
