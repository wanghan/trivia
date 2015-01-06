using System;
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

		int currentPlayer = 0;
		bool isGettingOutOfPenaltyBox;

		public const int MinPlayerCount = 2;
		public const int BoardSize = 12;
		public const int NumberOfCoinsToWin = 6;

		private IDisplay display;

		public Random Rand
		{
			get;
			private set;
		}

		public const int WrongAnswerId = 7;
		public const int MinAnswerId = 0;
		public const int MaxAnswerId = 9;

		public Game(IDisplay display)
		{
			this.display = display;
			this.Rand = new Random(DateTime.Now.Millisecond);
		}

		public Game(IDisplay display, Random rand)
		{
			this.display = display;
			this.Rand = rand;
		}

		public bool IsPlayable()
		{
			return (PlayerCount >= MinPlayerCount);
		}

		public bool AddPlayer(String playerName)
		{
			players.Add(playerName);
			SetDefaultPlayerParameterFor(this.PlayerCount);

			this.display.ShowAddPlayerInfo(playerName, players.Count);
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

		public void Roll()
		{
			int rolledNumber = Rand.Next(5) + 1;
			this.Roll(rolledNumber);
		}

		public void Roll(int rolledNumber)
		{
			this.display.ShowStatusBeforeRoll(players[currentPlayer], rolledNumber);

			if (inPenaltyBox[currentPlayer])
			{
				if (IsOdd(rolledNumber))
				{
					isGettingOutOfPenaltyBox = true;

					this.display.ShowPlayerGettingOutOfPenaltyBox(players[currentPlayer]);
					MovePlayer(rolledNumber);

					this.display.ShowPlayerNewLocaltion(players[currentPlayer], places[currentPlayer]);
					this.display.ShowCurrentCategory(this.CurrentCategory());
					this.display.AskQuestion(this.CurrentCategory());
				}
				else
				{
					this.display.ShowPlayerStaysInPenaltyBox(players[currentPlayer]);
					isGettingOutOfPenaltyBox = false;
				}
			}
			else
			{
				MovePlayer(rolledNumber);

				this.display.ShowPlayerNewLocaltion(players[currentPlayer], places[currentPlayer]);
				this.display.ShowCurrentCategory(this.CurrentCategory());
				this.display.AskQuestion(this.CurrentCategory());
			}
		}

		private void MovePlayer(int rolledNumber)
		{
			places[currentPlayer] = places[currentPlayer] + rolledNumber;
			if (IsCurrentPlayerShouldStartANewLap()) places[currentPlayer] = places[currentPlayer] - BoardSize;
		}

		private static bool IsOdd(int roll)
		{
			return roll % 2 != 0;
		}

		private bool IsCurrentPlayerShouldStartANewLap()
		{
			return places[currentPlayer] > BoardSize - 1;
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
					this.display.ShowCorrectAnswer();
					purses[currentPlayer]++;
					this.display.ShowPlayerCoins(players[currentPlayer], purses[currentPlayer]);

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
				this.display.ShowCorrectAnswer();
				purses[currentPlayer]++;
				this.display.ShowPlayerCoins(players[currentPlayer], purses[currentPlayer]);

				bool winner = DidPlayerNotWin();
				MoveNextPlayer();

				return winner;
			}
		}

		public bool DidSomeoneWin()
		{
			if (!this.IsCurrentAnswerCorrect())
			{
				return !this.WrongAnswer();
			}
			else
			{
				return !this.WasCorrectlyAnswered();
			}
		}

		private bool IsCurrentAnswerCorrect(int minAnswerId = MinAnswerId, int maxAnswerId = MaxAnswerId)
		{
			return this.Rand.Next(minAnswerId, maxAnswerId) != WrongAnswerId;
		}

		private void MoveNextPlayer()
		{
			currentPlayer++;
			if (currentPlayer == players.Count) currentPlayer = 0;
		}

		public bool WrongAnswer()
		{
			this.display.ShowWrongAnswer();
			this.display.ShowPlayerPenalty(players[currentPlayer]);
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
