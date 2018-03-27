using Lottery.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Common;

namespace Lottery.Sumilator
{
	[Serializable]
	public class Archive
	{

		public List<ArchiveNumber> archive = new List<ArchiveNumber>();

		public void Initialize()
		{
			for (var i = 1; i <= Constants.GameTotalNumbers; i++)
			{
				archive.Add(new ArchiveNumber
				{
					FacedTimes = 0,
					Value = i,
					Edition = 0
				});
			}
		}

		public Archive()
		{
			Initialize();
		}

		public int[] PredictSequence()
		{
			var value = archive.OrderBy(e => e.FacedTimes).ThenBy(e => e.Edition).Select(e => e.Value).Take(Constants.GameNumbersFallsOut).ToArray();
			return value;
		}

		public int[][] PredictSequences(int number)
		{
			var value = archive.OrderBy(e => e.FacedTimes).ThenBy(e => e.Edition).Select(e => e.Value).ToArray();
			var values = new int[number][];
			for (int i = 0; i < number; i++)
			{
				values[i] = new int[Constants.GameNumbersFallsOut];
				for (int j = 0; j < Constants.GameNumbersFallsOut; j++)
				{
					values[i][j] = value[i + j];
				}
			}

			//var values = new int[number][];
			//for (int i = 0; i < number; i++)
			//{
			//	values[i] = new int[Constants.GameSix];
			//	for (int j = 0; j < Constants.GameSix; j++)
			//	{
			//		values[i][j] = value[i * Constants.GameSix + j];
			//	}
			//}

			return values;
		}
		public int[] PredictSequenceWithBernoulli(int editions)
		{
			foreach (var number in archive)
			{
				//number.BernuolliCoefToFaceNext = Calculator.Bernoulli(Calculator.HyperGeometricProbability(Constants.GameTotalNumbers, 1, Constants.GameNumbersFallsOut, 1), editions + 1, number.FacedTimes + 1);
				//number.BernuolliCoefToNotFaceNext = Calculator.Bernoulli(Calculator.HyperGeometricProbability(Constants.GameTotalNumbers, 1, Constants.GameNumbersFallsOut, 1), editions + 1, number.FacedTimes);
				number.BernuolliCoefDifference = number.BernuolliCoefToFaceNext - number.BernuolliCoefToNotFaceNext;
				//if (Double.IsNaN(number.BernuolliCoefToFaceNext) || Double.IsNaN(number.BernuolliCoefToNotFaceNext))
				//	throw new Exception("Bernoulli is NAN");
			}

			var value = archive.OrderByDescending(e => e.BernuolliCoefDifference).Select(e => e.Value).Take(Constants.GameNumbersFallsOut).ToArray();

			return value;
		}

		public int[][] PredictSequencesWithBernoulli(int numberOfBets, int editions)
		{
			CalculateBernouli(editions);

			var value = archive.OrderByDescending(e => e.BernuolliCoefDifference).Select(e => e.Value).ToArray();
			var values = new int[numberOfBets][];
			for (int i = 0; i < numberOfBets; i++)
			{
				values[i] = new int[Constants.GameNumbersFallsOut];
				for (int j = 0; j < Constants.GameNumbersFallsOut; j++)
				{
					values[i][j] = value[i + j];
				}
			}

			return values;
		}

		public void CalculateBernouli(int editions)
		{
			foreach (var number in archive)
			{
				number.BernuolliCoefToFaceNext = Calculator.Bernoulli(Constants.probabilityToTakeOneDesiredBall, editions + 1, number.FacedTimes + 1);
				//number.BernuolliCoefToNotFaceNext = Calculator.Bernoulli(Calculator.HyperGeometricProbability(Constants.GameTotalNumbers, 1, Constants.GameNumbersFallsOut, 1), editions + 1, number.FacedTimes);
				//number.BernuolliCoefDifference = number.BernuolliCoefToFaceNext - number.BernuolliCoefToNotFaceNext;
				////if (Double.IsNaN(number.BernuolliCoefToFaceNext) || Double.IsNaN(number.BernuolliCoefToNotFaceNext))
				//	throw new Exception("Bernoulli is NAN");
			}
		}

		public void AddSequence(int[] sequence, int edition)
		{
			foreach (var element in sequence)
			{
				archive.First(e => e.Value == element).FacedTimes++;
				archive.First(e => e.Value == element).Edition = edition;
			}
		}

		public void CalculateStatistics(int totalNumbersPlayed)
		{
			foreach (var number in archive)
			{

				number.FacedTimesFromTotalNumberOfFacings = (double)number.FacedTimes / (double)totalNumbersPlayed;
			}
		}

		public void Print()
		{
			foreach (var element in archive)
			{
				var message = $"{element.Value} - from total numbers: {Math.Round(element.FacedTimesFromTotalNumberOfFacings, 4) * 100}% -  Faced times:{element.FacedTimes}";
				Console.WriteLine(message);
			}
		}

		public void PrintStatistics()
		{
			var message = $"Total sum of faced prob-s: {Math.Round(archive.Sum(e => e.FacedTimesFromTotalNumberOfFacings), 4) * 100}% -  Faced times:{archive.Sum(e => e.FacedTimes)}";
			Console.WriteLine(message);
		}

		public int[][] BestCombinations(int editions, int desiredCombinations)
		{
			CalculateBernouli(editions);
			var que = new Queue<Combination>();
			double lastQ = 0;
			var combintaions = Calculator.CombinationsAsArray(Constants.GameNumbersFallsOut, Constants.GameTotalNumbers);
			for (int c = 0; c < combintaions.Count(); c++)
			{

				double _q = 1;
				for (int i = 0; i < Constants.GameNumbersFallsOut; i++)
				{
					_q *= (double)archive[combintaions[c][i] - 1].BernuolliCoefToFaceNext * ((Constants.GameNumbersFallsOut - i) / (double)(Constants.GameTotalNumbers - i));
				}

				if (_q >= lastQ)
				{
					lastQ = _q;
					que.Enqueue(new Combination(combintaions[c], _q));
				}

				if (que.Count > desiredCombinations)
				{
					que.Dequeue();
				}

				if (c % 1000000 == 0) Console.WriteLine($"combination {c} from {combintaions.Count()}. {Math.Round((c + 1) / (double)combintaions.Count(), 2) * 100}% completed. Que has {que.Count} numbers.");
			}

			return que.Select(a => a.Value).ToArray();
		}

		public Combination BestCombination(int editions)
		{
			CalculateBernouli(editions);
			//int[] combination = new int[Constants.GameNumbersFallsOut];
			Combination combination = null;
			Rational lastQ = 0;
			var combintaions = Calculator.CombinationsAsArray(Constants.GameNumbersFallsOut, Constants.GameTotalNumbers);
			for (int c = 0; c < combintaions.Count(); c++)
			{

				Rational _q = 1;
				for (int i = 0; i < Constants.GameNumbersFallsOut; i++)
				{
					//_q *= ((double)archive[combintaions[c][i] - 1].BernuolliCoefToFaceNext * ((Constants.GameNumbersFallsOut - i) / (double)(Constants.GameTotalNumbers - i)));
					_q *= archive[combintaions[c][i] - 1].BernuolliCoefToFaceNext;

				}

				if (_q >= lastQ)
				{
					lastQ = _q;
					//combination = combintaions[c];
					combination = new Combination(combintaions[c], _q);
				}

				if (c % 1000 == 0) Console.WriteLine($"combination {c} from {combintaions.Count()}. {Math.Round((c + 1) / (double)combintaions.Count(), 2) * 100}% completed.");
			}

			return combination;
		}
	}
}
