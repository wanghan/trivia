using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
	internal class Display
	{
		public void WriteLine(string value)
		{
			Console.WriteLine(value);
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

	}
}
