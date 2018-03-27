using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Lottery.Models;
using Microsoft.SolverFoundation.Common;

namespace Lottery
{
	public static class Calculator
	{
		public static Rational Factorial(int number, int to = 1)
		{

			Rational result = 1;
			for (int i = number; i > to; i--)
				result = result * i;

			return result;
		}

		public static Rational Combination(int n, int k)
		{
			if (n == 1 && k == 1) return 1;
			return Factorial(n, n - k) / (Factorial(k));
		}

		public static Rational HyperGeometricProbability(int totalBalls, int whiteBalls, int takenBalls, int desiredWhiteBalls)
		{
			return (Combination(whiteBalls, desiredWhiteBalls) * Combination(totalBalls - whiteBalls, takenBalls - desiredWhiteBalls)) / Combination(totalBalls, takenBalls);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static Rational Bernoulli(Rational eventProbability, int attemptsOfExperiement, int desiredNumberOfFacings)
		{
			Rational ratRes1, ratRes2;
			Rational.Power(eventProbability, desiredNumberOfFacings, out ratRes1);
			Rational.Power(1 - eventProbability, attemptsOfExperiement - desiredNumberOfFacings, out ratRes2);
			return Combination(attemptsOfExperiement, desiredNumberOfFacings) * ratRes1 * ratRes2;
		}

		public static void PrintProbabilityToOdds(Rational probability)
		{
			var odds = (int)Math.Round(1 / (double)probability, 2);
			Console.WriteLine($"1 : {odds.ToString("N1", CultureInfo.InvariantCulture)}");
		}

		public static void RotateRight(IList sequence, int count)
		{
			object tmp = sequence[count - 1];
			sequence.RemoveAt(count - 1);
			sequence.Insert(0, tmp);
		}

		public static IEnumerable<IList> Permutate(IList sequence, int count)
		{
			if (count == 1) yield return sequence;
			else
			{
				for (int i = 0; i < count; i++)
				{
					foreach (var perm in Permutate(sequence, count - 1))
						yield return perm;
					RotateRight(sequence, count);
				}
			}
		}

		public static List<int[]> CombinationsAsArray(int m, int n)
		{
			var res = new List<int[]>();
			foreach (var combo in Combinations(m, n))
			{
				res.Add((int[])combo.Clone());
			}

			return res;
		}

		public static IEnumerable<int[]> Combinations(int m, int n)
		{
			int[] result = new int[m];
			Stack<int> stack = new Stack<int>();
			stack.Push(0);

			while (stack.Count > 0)
			{
				int index = stack.Count - 1;
				int value = stack.Pop();

				while (value < n)
				{
					result[index++] = ++value;
					stack.Push(value);

					if (index == m)
					{
						yield return result;
						break;
					}
				}
			}
		}

		public static Rational NumberOfWinningsForSubset(int totalBalls, int ballsFromBucket, int desiredBalls)
		{
			return C(desiredBalls, ballsFromBucket) * C(ballsFromBucket - desiredBalls, totalBalls - ballsFromBucket);
		}

		private static Rational C(int x, int y)
		{
			var top = 1;
			var bottom = 1;
			for (int i = 0; i < x; i++)
			{
				top *= y - i;
				bottom *= i + 1;
			}

			return top / bottom;
		}

		public static Rational MoivreLaplace(double eventProbability, int attemptsOfExperiement, int desiredNumberOfFacings)
		{
			var res = Math.Sqrt(attemptsOfExperiement * eventProbability * (1 - eventProbability));
			var gauss = ((desiredNumberOfFacings - eventProbability * attemptsOfExperiement) / res);
			var tableValue = LaplaceFunctionValues.GetTableValue(Math.Round(gauss, 2));

			return gauss * tableValue / res;
		}
	}
}
