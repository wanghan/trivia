using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
	public class GameRunner : IGameRunner
	{
		public Random Rand
		{
			get;
			private set;
		}

		public const int WrongAnswerId = 7;
		public const int MinAnswerId = 0;
		public const int MaxAnswerId = 9;

		private bool notAWinner;

		public GameRunner(int seed)
		{
			this.Rand = new Random(seed);
		}

		public void Run()
		{
			Game aGame = new Game(new ConsoleDisplay());

			aGame.AddPlayer("Chet");
			aGame.AddPlayer("Pat");
			aGame.AddPlayer("Sue");

			do
			{
				int dice = Rand.Next(5) + 1;
				aGame.roll(dice);
			} while (!this.DidSomeoneWin(aGame, this.IsCurrentAnswerCorrect()));
		}

		public bool DidSomeoneWin(IGame aGame, bool isCurrentAnswerCorrect)
		{
			if (!isCurrentAnswerCorrect)
			{
				return !aGame.WrongAnswer();
			}
			else
			{
				return !aGame.WasCorrectlyAnswered();
			}
		}

		public bool IsCurrentAnswerCorrect(int minAnswerId = MinAnswerId, int maxAnswerId = MaxAnswerId)
		{
			return this.Rand.Next(minAnswerId, maxAnswerId) != WrongAnswerId;
		}

		public static void Main(String[] args)
		{
			int seed = DateTime.Now.Millisecond;

			GameRunner runner = new GameRunner(seed);
			runner.Run();
		}
	}
}

