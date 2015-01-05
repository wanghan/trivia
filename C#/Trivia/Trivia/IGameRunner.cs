using System;
namespace Trivia
{
	public interface IGameRunner
	{
		bool IsCurrentAnswerCorrect(int minAnswerId, int maxAnswerId);
		void Run();
	}
}
