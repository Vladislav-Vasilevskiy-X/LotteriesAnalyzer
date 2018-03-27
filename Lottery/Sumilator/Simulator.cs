using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Sumilator
{
	public class Simulator
	{
		Random random = new Random();
		private List<int> NumbersToPLay { get; }

		public Simulator()
		{
			NumbersToPLay = new List<int>();
			for (var i = 1; i <= Constants.GameTotalNumbers; i++)
			{
				NumbersToPLay.Add(i);
			}
		}

		public int[] GenerateGameResult()
		{
			int[] tempSetArray = new int[Constants.GameTotalNumbers];
			NumbersToPLay.CopyTo(tempSetArray);
			var tempSet = tempSetArray.ToList();
			int[] resultNumbers = new int[Constants.GameNumbersFallsOut];

			for (int i = 0; i < Constants.GameNumbersFallsOut; i++)
			{
				var foundNumber = tempSet[random.Next(0, tempSet.Count)];
				tempSet.Remove(foundNumber);
				resultNumbers[i] = foundNumber;
			}

			return resultNumbers;
		}

		public int[][] GenerateBets(int bets, bool noIntersections = true)
		{
			int[] tempSetArray = new int[Constants.GameTotalNumbers];
			NumbersToPLay.CopyTo(tempSetArray);
			var tempSet = tempSetArray.ToList();
			int[][] resultNumbers = new int[bets][];
			for (int i = 0; i < bets; i++)
			{
				resultNumbers[i] = new int[Constants.GameNumbersFallsOut];
			}

			for (int bet = 0; bet < bets; bet++)
			{
				for (int i = 0; i < Constants.GameNumbersFallsOut; i++)
				{
					var foundNumber = tempSet[random.Next(0, tempSet.Count)];
					tempSet.Remove(foundNumber);
					resultNumbers[bet][i] = foundNumber;
				}
			}

			return resultNumbers;
		}

		
	}
}
