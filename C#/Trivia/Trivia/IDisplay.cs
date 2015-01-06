using System;

namespace Trivia
{
	interface IDisplay
	{
		void AskQuestion(string currentCategory);
		void ShowAddPlayerInfo(string playerName, int playerCount);
		void ShowCorrectAnswer();
		void ShowCurrentCategory(string category);
		void ShowPlayerCoins(string playerName, int coins);
		void ShowPlayerGettingOutOfPenaltyBox(string playerName);
		void ShowPlayerNewLocaltion(string playerName, int location);
		void ShowPlayerPenalty(string playerName);
		void ShowPlayerStaysInPenaltyBox(string playerName);
		void ShowStatusBeforeRoll(string playerName, int rolledNumber);
		void ShowWrongAnswer();
	}
}
