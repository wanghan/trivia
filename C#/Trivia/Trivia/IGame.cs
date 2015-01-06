using System;

namespace Trivia
{
	public interface IGame
	{
		bool WasCorrectlyAnswered();
		bool WrongAnswer();
		bool IsPlayable();
		bool AddPlayer(String playerName);
		String CurrentCategory();
		bool DidSomeoneWin();
		void Roll();
		void Roll(int rollNumber);

		int PlayerCount
		{
			get;
		}
	}
}
