using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
	public class GameRunner : IGameRunner
	{
		private IGame game;

		public GameRunner(int seed)
		{
			Random rand = new Random(seed);
			game = new Game(new ConsoleDisplay(), rand);
		}

		public void Run()
		{
			game.AddPlayer("Chet");
			game.AddPlayer("Pat");
			game.AddPlayer("Sue");

			do
			{
				game.Roll();
			} while (!game.DidSomeoneWin());
		}

		public static void Main(String[] args)
		{
			int seed = DateTime.Now.Millisecond;

			GameRunner runner = new GameRunner(seed);
			runner.Run();
		}
	}
}

