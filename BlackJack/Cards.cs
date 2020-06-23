using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack
{
	class Cards : Stack<Card>
	{
		/// <summary>
		/// Creates a string value for all cards in a given stack of cards
		/// </summary>
		/// <returns>all the cards in a stack, face and value for each card</returns>
		public string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (Card card in this)
			{
				sb.Append($"{card.Face} {card.Value}");
				if (this.Count > 1)
				{
					sb.Append(", ");
				}
			}
			return sb.ToString();
		}
	}
}
