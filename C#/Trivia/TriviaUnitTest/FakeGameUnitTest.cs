using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trivia;

namespace TriviaUnitTest
{
	[TestClass]
	public class FakeGameUnitTest
	{
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
			IGame game = new Game(new DummyDisplay());

			PrivateObject privateFakeGame = new PrivateObject(game);

			int[] correctAnswerIds = this.GetCorrectAnswerIds();

			foreach (int id in correctAnswerIds)
			{
				Assert.IsTrue((bool)privateFakeGame.Invoke("IsCurrentAnswerCorrect", new object[] { id, id }));
			}
		}

		[TestMethod]
		public void Test_IsWrongAnswerId()
		{
			IGame game = new Game(new DummyDisplay());

			PrivateObject privateFakeGame = new PrivateObject(game);

			Assert.IsFalse((bool)privateFakeGame.Invoke("IsCurrentAnswerCorrect", new object[] { Game.WrongAnswerId, Game.WrongAnswerId }));
		}

		[TestMethod]
		public void Test_ItCanTellIfThereIsNoWinnerWhenACorrectAnswerIsProvided()
		{
			IGame fakeGame = new Trivia.Fakes.StubIGame()
			{
				WasCorrectlyAnswered = () => { return false; },
				WrongAnswer = () => { return true; },
				DidSomeoneWin = () => { return ACorrectAnswer; }
			};

			PrivateObject privateFakeGame = new PrivateObject(fakeGame);
			Assert.IsTrue(fakeGame.DidSomeoneWin());
		}

		[TestMethod]
		public void Test_ItCanTellIfThereIsNoWinnerWhenAWrongAnswerIsProvided()
		{
			IGame fakeGame = new Trivia.Fakes.StubIGame()
			{
				WasCorrectlyAnswered = () => { return false; },
				WrongAnswer = () => { return true; },
				DidSomeoneWin = () => { return AWrongAnswer; }
			};

			PrivateObject privateFakeGame = new PrivateObject(fakeGame);
			Assert.IsFalse(fakeGame.DidSomeoneWin());
		}

		private int[] GetCorrectAnswerIds()
		{
			List<int> result = new List<int>();
			for (int i = Game.MinAnswerId; i <= Game.MaxAnswerId; ++i)
			{
				if (i != Game.WrongAnswerId)
				{
					result.Add(i);
				}
			}

			return result.ToArray();
		}
	}
}
