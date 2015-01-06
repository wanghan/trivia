using Trivia;

namespace TriviaUnitTest
{
	class DummyDisplay : IDisplay
	{
		public void AskQuestion(string currentCategory)
		{
		}

		public void ShowAddPlayerInfo(string playerName, int playerCount)
		{
		}

		public void ShowCorrectAnswer()
		{
		}

		public void ShowCurrentCategory(string category)
		{
		}

		public void ShowPlayerCoins(string playerName, int coins)
		{
		}

		public void ShowPlayerGettingOutOfPenaltyBox(string playerName)
		{
		}

		public void ShowPlayerNewLocaltion(string playerName, int location)
		{
		}

		public void ShowPlayerPenalty(string playerName)
		{
		}

		public void ShowPlayerStaysInPenaltyBox(string playerName)
		{
		}

		public void ShowStatusBeforeRoll(string playerName, int rolledNumber)
		{
		}

		public void ShowWrongAnswer()
		{
		}
	}
}