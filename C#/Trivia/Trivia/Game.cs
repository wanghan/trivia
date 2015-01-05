﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
	public class Game : Trivia.IGame
	{
		List<string> players = new List<string>();

		int[] places = new int[6];
		int[] purses = new int[6];

		bool[] inPenaltyBox = new bool[6];

		LinkedList<string> popQuestions = new LinkedList<string>();
		LinkedList<string> scienceQuestions = new LinkedList<string>();
		LinkedList<string> sportsQuestions = new LinkedList<string>();
		LinkedList<string> rockQuestions = new LinkedList<string>();

		int currentPlayer = 0;
		bool isGettingOutOfPenaltyBox;

		public const int CategorySize = 50;
		public const int MinPlayerCount = 2;
		public const int BoardSize = 12;
		public const int NumberOfCoinsToWin = 6;

		public Game()
		{
			for (int i = 0; i < CategorySize; i++)
			{
				popQuestions.AddLast("Pop Question " + i);
				scienceQuestions.AddLast(("Science Question " + i));
				sportsQuestions.AddLast(("Sports Question " + i));
				rockQuestions.AddLast(("Rock Question " + i));
			}
		}

		public bool IsPlayable()
		{
			return (PlayerCount >= MinPlayerCount);
		}

		public bool AddPlayer(String playerName)
		{
			players.Add(playerName);
			SetDefaultPlayerParameterFor(this.PlayerCount);

			Console.WriteLine(playerName + " was added");
			Console.WriteLine("They are player number " + players.Count);
			return true;
		}

		private void SetDefaultPlayerParameterFor(int playerId)
		{
			places[playerId] = 0;
			purses[playerId] = 0;
			inPenaltyBox[playerId] = false;
		}

		public int PlayerCount
		{
			get
			{
				return players.Count;
			}
		}

		public void roll(int rolledNumber)
		{
			Console.WriteLine(players[currentPlayer] + " is the current player");
			Console.WriteLine("They have rolled a " + rolledNumber);

			if (inPenaltyBox[currentPlayer])
			{
				if (IsOdd(rolledNumber))
				{
					isGettingOutOfPenaltyBox = true;

					ShowPlayerGettingOutOfPenaltyBox();
					MovePlayer(rolledNumber);

					ShowPlayerNewLocaltion();
					ShowCurrentCategory();
					askQuestion();
				}
				else
				{
					ShowPlayerStaysInPenaltyBox();
					isGettingOutOfPenaltyBox = false;
				}
			}
			else
			{
				MovePlayer(rolledNumber);

				ShowPlayerNewLocaltion();
				ShowCurrentCategory();
				askQuestion();
			}
		}

		private void ShowPlayerStaysInPenaltyBox()
		{
			Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
		}

		private void ShowPlayerGettingOutOfPenaltyBox()
		{
			Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
		}

		private void MovePlayer(int rolledNumber)
		{
			places[currentPlayer] = places[currentPlayer] + rolledNumber;
			if (IsCurrentPlayerShouldStartANewLap()) places[currentPlayer] = places[currentPlayer] - BoardSize;
		}

		private void ShowPlayerNewLocaltion()
		{
			Console.WriteLine(players[currentPlayer]
								   + "'s new location is "
								   + places[currentPlayer]);
		}

		private void ShowCurrentCategory()
		{
			Console.WriteLine("The category is " + CurrentCategory());
		}

		private static bool IsOdd(int roll)
		{
			return roll % 2 != 0;
		}

		private bool IsCurrentPlayerShouldStartANewLap()
		{
			return places[currentPlayer] > BoardSize - 1;
		}

		private void askQuestion()
		{
			if (CurrentCategory() == "Pop")
			{
				Console.WriteLine(popQuestions.First());
				popQuestions.RemoveFirst();
			}
			if (CurrentCategory() == "Science")
			{
				Console.WriteLine(scienceQuestions.First());
				scienceQuestions.RemoveFirst();
			}
			if (CurrentCategory() == "Sports")
			{
				Console.WriteLine(sportsQuestions.First());
				sportsQuestions.RemoveFirst();
			}
			if (CurrentCategory() == "Rock")
			{
				Console.WriteLine(rockQuestions.First());
				rockQuestions.RemoveFirst();
			}
		}

		public String CurrentCategory()
		{
			if (places[currentPlayer] == 0) return "Pop";
			if (places[currentPlayer] == 4) return "Pop";
			if (places[currentPlayer] == 8) return "Pop";
			if (places[currentPlayer] == 1) return "Science";
			if (places[currentPlayer] == 5) return "Science";
			if (places[currentPlayer] == 9) return "Science";
			if (places[currentPlayer] == 2) return "Sports";
			if (places[currentPlayer] == 6) return "Sports";
			if (places[currentPlayer] == 10) return "Sports";
			return "Rock";
		}

		public bool WasCorrectlyAnswered()
		{
			if (inPenaltyBox[currentPlayer])
			{
				if (isGettingOutOfPenaltyBox)
				{
					Console.WriteLine("Answer was correct!!!!");
					purses[currentPlayer]++;
					Console.WriteLine(players[currentPlayer]
							+ " now has "
							+ purses[currentPlayer]
							+ " Gold Coins.");

					bool winner = DidPlayerNotWin();
					MoveNextPlayer();

					return winner;
				}
				else
				{
					MoveNextPlayer();
					return true;
				}
			}
			else
			{
				Console.WriteLine("Answer was corrent!!!!");
				purses[currentPlayer]++;
				Console.WriteLine(players[currentPlayer]
						+ " now has "
						+ purses[currentPlayer]
						+ " Gold Coins.");

				bool winner = DidPlayerNotWin();
				MoveNextPlayer();

				return winner;
			}
		}

		private void MoveNextPlayer()
		{
			currentPlayer++;
			if (currentPlayer == players.Count) currentPlayer = 0;
		}

		public bool WrongAnswer()
		{
			Console.WriteLine("Question was incorrectly answered");
			Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
			inPenaltyBox[currentPlayer] = true;

			currentPlayer++;
			if (currentPlayer == players.Count) currentPlayer = 0;
			return true;
		}

		private bool DidPlayerNotWin()
		{
			return !(purses[currentPlayer] == NumberOfCoinsToWin);
		}
	}
}
