using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Sumilator
{
	public static class Bettor
	{
		public static int PlayBet(Simulator sim, int[] numbersToBet)
		{
			var gameResult = sim.GenerateGameResult();
			return CheckResult(numbersToBet, gameResult);
		}

		public static int CheckResult(int[] bet, int[] gameResult)
		{
			var coinsidences = 0;
			var resultList = gameResult.ToList();
			for (int i = 0; i < Constants.GameNumbersFallsOut; i++)
			{
				if (resultList.Contains(bet[i])) coinsidences++;
			}

			if (coinsidences < Constants.CoinsidencesToWin) coinsidences = 0;
			return coinsidences;
		}

		public static int PrintResult(int[] bet, int[] gameResult)
		{
			var won = CheckResult(bet, gameResult);
			Console.Write("You'be bet: ");
			foreach (var number in bet)
			{
				var postfix = (Array.IndexOf(bet, number) == bet.Length - 1) ? ".\n" : ", ";
				Console.Write(number + postfix); 
			}

			Console.Write("Winning sequence: ");
			foreach (var number in gameResult)
			{
				var postfix = (Array.IndexOf(gameResult, number) == gameResult.Length - 1) ? ".\n" : ", ";
				Console.Write(number + postfix);
			}

			var message = won > 0 ? $"You won, {won} coinsedences." : "You lose.";
			Console.WriteLine(message);
			
			return won;
		}
	}
}
