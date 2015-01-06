using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trivia;
using System.Collections.Generic;

namespace TriviaUnitTest
{
	[TestClass]
	public class GameUnitTest
	{
		private IGame game;

		[TestInitialize]
		public void Test_Initialize()
		{
			this.game = new Game(new DummyDisplay());
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

		[TestMethod]
		public void Test_APlayersNextPositionIsCorrectlyDeterminedWhenNoNewLapIsInvolved()
		{
			PrivateObject privateGame = new PrivateObject(this.game);
			int currentPlayer = 0;
			int currentPlace = 2;
			int rollNumber = 1;

			privateGame.SetField("currentPlayer", currentPlayer);
			((bool[])privateGame.GetField("inPenaltyBox"))[currentPlayer] = false;
			((int[])privateGame.GetField("places"))[currentPlayer] = currentPlace;
			((List<string>)privateGame.GetField("players")).Add("Player1");

			this.game.Roll(rollNumber);

			Assert.AreEqual(3, ((int[])privateGame.GetField("places"))[currentPlayer], "Player1 is expected at position 3");
			Assert.AreEqual("Rock", game.CurrentCategory());
		}

		[TestMethod]
		public void Test_APlayerWillStartANewLapWhenNeeded()
		{
			PrivateObject privateGame = new PrivateObject(this.game);
			int currentPlayer = 0;
			int currentPlace = 11;
			int rollNumber = 2;

			privateGame.SetField("currentPlayer", currentPlayer);
			((bool[])privateGame.GetField("inPenaltyBox"))[currentPlayer] = false;
			((int[])privateGame.GetField("places"))[currentPlayer] = currentPlace;
			((List<string>)privateGame.GetField("players")).Add("Player1");

			this.game.Roll(rollNumber);

			Assert.AreEqual(1, ((int[])privateGame.GetField("places"))[currentPlayer], "Player1 is expected at position 1");
			Assert.AreEqual("Science", game.CurrentCategory());
		}

		[TestMethod]
		public void Test_APlayerWhoIsPenalizedAndRollsAnIOddNumberWillGetOutOfThePenaltyBox()
		{
			PrivateObject privateGame = new PrivateObject(this.game);
			int rollNumber = 3;

			SetupPlayerInPenaltyBox(privateGame);

			this.game.Roll(rollNumber);

			Assert.IsTrue((bool)privateGame.GetField("isGettingOutOfPenaltyBox"));
		}

		[TestMethod]
		public void Test_APlayerWhoIsPenalizedAndRollsAnEvenNumberWillStayInThePenaltyBox()
		{
			PrivateObject privateGame = new PrivateObject(this.game);
			int rollNumber = 2;
			SetupPlayerInPenaltyBox(privateGame);

			this.game.Roll(rollNumber);

			Assert.IsFalse((bool)privateGame.GetField("isGettingOutOfPenaltyBox"));
		}

		private static void SetupPlayerInPenaltyBox(PrivateObject privateGame)
		{
			int currentPlayer = 0;

			privateGame.SetField("currentPlayer", currentPlayer);
			((bool[])privateGame.GetField("inPenaltyBox"))[currentPlayer] = true;
			((List<string>)privateGame.GetField("players")).Add("Player1");
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
