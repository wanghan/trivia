using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trivia;

namespace TriviaUnitTest
{
	[TestClass]
	public class GameUnitTest
	{
		private IGame game;

		[TestInitialize]
		public void Test_Initialize()
		{
			this.game = new Game();
		}

		[TestMethod]
		public void Test_IsPlayable_AGameWithNotEnoughPlayersIsNotPlayable()
		{
			Assert.IsFalse(game.IsPlayable());

			this.AddJustNotEnoughPlayers();
			Assert.IsFalse(game.IsPlayable());

		}

		[TestMethod]
		public void Test_IsPlayable_AGameWithEnoughPlayersIsPlayable()
		{
			this.AddEnoughPlayers();
			Assert.IsTrue(game.IsPlayable());
		}

		[TestMethod]
		public void Test_CanAddANewPlayer()
		{
			Assert.AreEqual(0, this.game.PlayerCount);

			this.game.AddPlayer("player");

			Assert.AreEqual(1, this.game.PlayerCount);

			PrivateObject privateGame = new PrivateObject(this.game);
			Assert.AreEqual(0, ((int[])privateGame.GetField("places"))[this.game.PlayerCount]);
			Assert.AreEqual(0, ((int[])privateGame.GetField("purses"))[this.game.PlayerCount]);
			Assert.AreEqual(false, ((bool[])privateGame.GetField("inPenaltyBox"))[this.game.PlayerCount]);
		}

		[TestMethod]
		public void Test_PlayerWinsWithTheCorrectNumberOfCoins()
		{
			PrivateObject privateGame = new PrivateObject(this.game);
			privateGame.SetField("currentPlayer", 0);
			((int[])privateGame.GetField("purses"))[0] = Game.NumberOfCoinsToWin;

			Assert.IsFalse((bool)privateGame.Invoke("DidPlayerNotWin", null));
		}

		private void AddEnoughPlayers()
		{
			this.AddManyPlayers(this.game, Game.MinPlayerCount);
		}

		private void AddJustNotEnoughPlayers()
		{
			this.AddManyPlayers(this.game, Game.MinPlayerCount - 1);
		}

		private void AddManyPlayers(IGame game, int count)
		{
			for (int i = 0; i < count; ++i)
			{
				game.AddPlayer("player " + i);
			}
		}
	}
}
