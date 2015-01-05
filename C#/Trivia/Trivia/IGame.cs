using System;
namespace Trivia
{
	public interface IGame
	{
		bool WasCorrectlyAnswered();
		bool WrongAnswer();
	}
}
