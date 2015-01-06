using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
	internal class ConsoleDisplay : IDisplay
	{
		LinkedList<string> popQuestions = new LinkedList<string>();
		LinkedList<string> scienceQuestions = new LinkedList<string>();
		LinkedList<string> sportsQuestions = new LinkedList<string>();
		LinkedList<string> rockQuestions = new LinkedList<string>();

		public const int CategorySize = 50;

		public ConsoleDisplay()
		{
			for (int i = 0; i < CategorySize; i++)
			{
				popQuestions.AddLast("Pop Question " + i);
				scienceQuestions.AddLast(("Science Question " + i));
				sportsQuestions.AddLast(("Sports Question " + i));
				rockQuestions.AddLast(("Rock Question " + i));
			}
		}

		public void ShowAddPlayerInfo(String playerName, int playerCount)
		{
			this.WriteLine(playerName + " was added");
			this.WriteLine("They are player number " + playerCount);
		}

		public void ShowStatusBeforeRoll(string playerName, int rolledNumber)
		{
			this.WriteLine(playerName + " is the current player");
			this.WriteLine("They have rolled a " + rolledNumber);
		}


		public void ShowPlayerStaysInPenaltyBox(string playerName)
		{
			this.WriteLine(playerName + " is not getting out of the penalty box");
		}

		public void ShowPlayerGettingOutOfPenaltyBox(string playerName)
		{
			this.WriteLine(playerName + " is getting out of the penalty box");
		}


		public void ShowPlayerNewLocaltion(string playerName, int location)
		{
			this.WriteLine(playerName
								   + "'s new location is "
								   + location);
		}

		public void ShowCurrentCategory(string category)
		{
			this.WriteLine("The category is " + category);
		}

		public void ShowCorrectAnswer()
		{
			this.WriteLine("Answer was corrent!!!!");
		}

		public void ShowPlayerCoins(string playerName, int coins)
		{
			this.WriteLine(playerName
				+ " now has "
				+ coins
				+ " Gold Coins.");
		}

		public void ShowPlayerPenalty(string playerName)
		{
			this.WriteLine(playerName + " was sent to the penalty box");
		}

		public void ShowWrongAnswer()
		{
			this.WriteLine("Question was incorrectly answered");
		}

		public void AskQuestion(string currentCategory)
		{
			if (currentCategory == "Pop")
			{
				this.WriteLine(popQuestions.First());
				popQuestions.RemoveFirst();
			}
			if (currentCategory == "Science")
			{
				this.WriteLine(scienceQuestions.First());
				scienceQuestions.RemoveFirst();
			}
			if (currentCategory == "Sports")
			{
				this.WriteLine(sportsQuestions.First());
				sportsQuestions.RemoveFirst();
			}
			if (currentCategory == "Rock")
			{
				this.WriteLine(rockQuestions.First());
				rockQuestions.RemoveFirst();
			}
		}

		private void WriteLine(string value)
		{
			Console.WriteLine(value);
		}
	}
}
