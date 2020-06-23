using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("*********************");
			Console.WriteLine("Welcome to the Console Black Jack Game");
			
				string numOfPlayers = "";
				Int16 h;			
				while (!Int16.TryParse(numOfPlayers, out h))
				{
					Console.Write("How many players will play this game? ");
					try
					{
						numOfPlayers = Console.ReadLine();
						if (!Int16.TryParse(numOfPlayers, out h))
						{
							throw new ArgumentException($"{numOfPlayers} is invalid! Must specify a number");
						}
					}
					catch (ArgumentException ax)
					{
						Console.WriteLine(ax.Message);
					}
				}

			Players players = new Players();

			// adds new players, skips dealer at index 0 
			for (int i = 1; i < Int16.Parse(numOfPlayers) + 1; i++)
			{
				Player player = new Player();
				Console.Write($"Player {i}'s name:");
				player.Name = Console.ReadLine();
				players.Add(player);
			}
			
			var answer = new ConsoleKeyInfo();
			
			while (answer.KeyChar != '0')
			{
				Console.WriteLine("\r\n0 - Exit");
				Console.WriteLine("1 - Play");
				Console.Write("Your choice: ");

				try
				{
					answer = Console.ReadKey();
					Console.WriteLine();

					if (answer.KeyChar != '1' && answer.KeyChar != '0' && answer.Key != ConsoleKey.Escape)
					{

						throw new Exception($"\r\n{answer.KeyChar} is invalid. Please enter a valid selection");
					}

					switch (answer.KeyChar)
					{
						case '0':
							return;
						case (char)ConsoleKey.Escape:
							return;
						case '1':
							Game game = new Game(players);

							//if someone has blackjack game is over
							if (game.Players.Any(player => player.HasBlackJack()))
							{
								foreach (Player player in game.Players)
								{
									if (player.HasBlackJack())
									{
										Console.WriteLine($"{player.Name} has blackjack.");
										playerWon(player);
									}
									else
									{
										playerLost(player);
									}
								}
								break;
							}
							//everyone gets a turn now
							foreach (Player p in game.Players)
							{
								//dealer plays last
								if (p.IsDealer)
								{
									continue;
								}
								Console.WriteLine($"\r\n\r\nHi {p.Name}. Your cards: ");
								Console.WriteLine(p.Hand.ToString());
								//gives the sum of the hand for all players except dealer
								Console.WriteLine($"{p.Name}, your total is: {p.GetSumOfAllCards()}\r\n");
								Console.WriteLine($"Dealer's first card: {game.Dealer.Hand.ElementAt(0).Face}, {game.Dealer.Hand.ElementAt(0).Value}\r\n");

								//time to hit or stay								
								
								var answ = new ConsoleKeyInfo();
								
								while (answ.KeyChar != 's' && answ.Key != ConsoleKey.Escape && !p.IsBusted())
								{
									Console.Write($"\r\n{p.Name}, Hit or stay? h/s: ");
									try
									{
										answ = Console.ReadKey();
										if (answ.KeyChar != 's' && answ.Key != ConsoleKey.Escape && answ.KeyChar != 's')
										{
											throw new Exception($"\r\n{answ.KeyChar} is invalid. Please enter a valid selection");
										}
										switch (answ.KeyChar)
										{
											case 'h':
												game.HitPlayer(p);
												if (p.IsBusted())
												{
													Console.WriteLine($"{p.Name} busted.");
													p.PlayerStands = true;
													break;
												}
												Console.WriteLine($"\r\nYou got: {p.Hand.ToString()}. Your sum: {p.GetSumOfAllCards()}");
												break;
											case 's':
												break;
											default:
												break;
										}
									}
									catch (Exception e)
									{

										Console.WriteLine(e.Message);
									}
									
								}

							}

							//now dealer's turn
							if (!game.CheckAnyoneLeft())
							{
								while (game.Dealer.GetSumOfAllCards() <= 17)
								{
									game.HitDealer();
									if (game.Dealer.IsBusted())
									{
										Console.WriteLine($"{game.Dealer.Name} busted.");
										game.Dealer.PlayerStands = true;
										break;
									}
								}
							}


							Console.WriteLine("\r\nGame over!\r\n");

							//uf no one busted or had blackjack, winner is the player with the 
							//highest score

							//create a list of unbusted players
							List<Player> notBusted = new List<Player>();
							foreach (Player player1 in game.Players)
							{
								if (!player1.IsBusted())
								{
									notBusted.Add(player1);
								}
							}

							//gets the highest sum of cards and adds the players with that sum to the winners list
							int highestSum = notBusted.Max(pl => pl.GetSumOfAllCards());
							List<Player> winners = notBusted.FindAll(pl => pl.GetSumOfAllCards() == highestSum);
							//every winner gets a point
							foreach (Player winner in winners)
							{
								playerWon(winner);
								winner.Score++;
							}
							//list for everyone else, losers
							List<Player> losers = game.Players.FindAll(pl => pl.GetSumOfAllCards() != highestSum);
							foreach (Player loser in losers)
							{
								playerLost(loser);
								loser.Score--;
							}

							Console.WriteLine($"\r\nGAME OVER!");
							//All done. Time to show cards and sum all up game scores.
							foreach (Player player in game.Players)
							{
								Console.WriteLine($"{player.Name}: Hand: {player.Hand.ToString()}. Sum: {player.GetSumOfAllCards()}. Score: {player.Score}");
							}


							break;
						default:
							Console.WriteLine($"{answer} is an invalid selection");
							break;
					}
				}
				catch (Exception e)
				{

					Console.WriteLine(e.Message);
				}
													
			}
		}

		private static void playerLost(Player player)
		{
			Console.WriteLine($"{player.Name} lost. 1 point deducted from score");
			player.PlayerStands = true;
			
		}
		private static void playerWon(Player player)
		{
			Console.WriteLine($"{player.Name} won. 1 point added to score");
			player.PlayerStands = true;

		}
	}
}
