using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
	class Deck : IDeck
	{
		public Cards Cards { get; set; }

		/// <summary>
		/// Creates 52 unique cards in a deck which is a stack of cards. Does not shuffle.
		/// </summary>
		public Deck()
		{
			Cards = new Cards();						
			foreach (string face in Enum.GetNames(typeof(Face)))
			{
				foreach (int value in Enum.GetValues(typeof(Value)))
				{
					Card c = new Card()
					{
						Face = face,
						Value = value
					};
					Cards.Push(c);
				}
			}			
		}

		/// <summary>
		/// Creates 52 unique cards in a deck which is a stack of cards. Does not shuffle.
		/// </summary>
		/// <param name="numberOfDecks">Is defaulted to 1 however can be changed. Putting any 
		/// other integer value will double the number of decks in a game accordingly</param>
		public Deck(int numberOfDecks = 1)
		{
			Cards = new Cards();
			for (int i = 0; i < numberOfDecks; i++)
			{
				foreach (string face in Enum.GetNames(typeof(Face)))
				{
					foreach (int value in Enum.GetValues(typeof(Value)))
					{
						Card c = new Card()
						{
							Face = face,
							Value = value
						};
						Cards.Push(c);
					}
				}
			}
		}

		/// <summary>
		/// Draws(removes) a card from the deck
		/// </summary>
		/// <returns>The drawn card</returns>
		public Card DrawCard()
		{
			return Cards.Pop();
		}

		/// <summary>
		/// Shuffles the cards in a chosen.
		/// </summary>
		public void Shuffle()
		{
			Random r = new Random();
			List<Card> theCards = Cards.ToList();
			for (int n = theCards.Count - 1; n > 0; --n)
			{
				int k = r.Next(n + 1);
				Card temp = theCards[n];
				theCards[n] = theCards[k];
				theCards[k] = temp;
			}
			Cards.Clear();
			foreach (Card card in theCards)
			{
				Cards.Push(card);
			};
		}
	}
}
