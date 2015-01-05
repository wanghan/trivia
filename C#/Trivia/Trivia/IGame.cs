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

		int PlayerCount
		{
			get;
		}
	}
}
