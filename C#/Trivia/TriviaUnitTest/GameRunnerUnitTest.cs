using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trivia;

namespace TriviaUnitTest
{
	[TestClass]
	public class GameRunnerUnitTest
	{
		private IGame FakeGame
		{
			get
			{
				return new Trivia.Fakes.StubIGame()
				{
					WasCorrectlyAnswered = () => { return false; },
					WrongAnswer = () => { return true; }
				};
			}
		}

		private bool AWrongAnswer
		{
			get
			{
				return false;
			}
		}

		private bool ACorrectAnswer
		{
			get
			{
				return true;
			}
		}


		[TestMethod]
		public void Test_IsCorrectAnswerId()
		{
			GameRunner runner = new GameRunner(DateTime.Now.Millisecond);

			int[] correctAnswerIds = this.GetCorrectAnswerIds();

			foreach (int id in correctAnswerIds)
			{
				Assert.IsTrue(runner.IsCurrentAnswerCorrect(id, id));
			}
		}

		[TestMethod]
		public void Test_IsWrongAnswerId()
		{
			GameRunner runner = new GameRunner(DateTime.Now.Millisecond);

			Assert.IsFalse(runner.IsCurrentAnswerCorrect(GameRunner.WrongAnswerId, GameRunner.WrongAnswerId));
		}

		[TestMethod]
		public void Test_ItCanTellIfThereIsNoWinnerWhenACorrectAnswerIsProvided()
		{
			GameRunner runner = new GameRunner(DateTime.Now.Millisecond);
			Assert.IsTrue(runner.DidSomeoneWin(this.FakeGame, this.ACorrectAnswer));
		}

		[TestMethod]
		public void Test_ItCanTellIfThereIsNoWinnerWhenAWrongAnswerIsProvided()
		{
			GameRunner runner = new GameRunner(DateTime.Now.Millisecond);
			Assert.IsFalse(runner.DidSomeoneWin(this.FakeGame, this.AWrongAnswer));
		}

		private int[] GetCorrectAnswerIds()
		{
			List<int> result = new List<int>();
			for (int i = GameRunner.MinAnswerId; i <= GameRunner.MaxAnswerId; ++i)
			{
				if (i != GameRunner.WrongAnswerId)
				{
					result.Add(i);
				}
			}

			return result.ToArray();
		}
	}
}
