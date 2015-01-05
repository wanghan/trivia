using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trivia;

namespace TriviaUnitTest
{
	[TestClass]
	public class GameRunnerUnitTest
	{
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
