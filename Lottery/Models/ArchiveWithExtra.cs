using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Models
{
	public class ArchiveWithExtra
	{
		public List<ArchiveNumber> archive = new List<ArchiveNumber>();
		private List<ArchiveNumber> extras = new List<ArchiveNumber>();

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

			for (var i = 1; i <= Constants.ExtraNumbers; i++)
			{
				extras.Add(new ArchiveNumber
				{
					FacedTimes = 0,
					Value = i,
					Edition = 0
				});
			}
		}

		public ArchiveWithExtra()
		{
			Initialize();
		}

		public void AddSequence(int[] sequence)
		{
			extras[sequence[Constants.GameNumbersFallsOut] - 1].FacedTimes++;

			for (int i = 0; i < Constants.GameNumbersFallsOut - 1; i++)
			{
				archive[sequence[i] - 1].FacedTimes++;
			}
		}

		public void Append(ArchiveWithExtra archiveWE)
		{
			for (int i = 0; i < Constants.GameTotalNumbers; i++)
			{
				archive[i].FacedTimes += archiveWE.archive[i].FacedTimes;
			}

			for (int i = 0; i < Constants.ExtraNumbers; i++)
			{
				extras[i].FacedTimes += archiveWE.extras[i].FacedTimes;
			}
		}

		public void Print()
		{
			Console.WriteLine("Main:");
			foreach (var element in archive)
			{
				var message = $"{element.Value} - from total numbers: {Math.Round(element.FacedTimesFromTotalNumberOfFacings, 4) * 100}% -  Faced times:{element.FacedTimes}";
				Console.WriteLine(message);
			}

			Console.WriteLine("Extras:");
			foreach (var element in extras)
			{
				var message = $"{element.Value} - from total numbers: {Math.Round(element.FacedTimesFromTotalNumberOfFacings, 4) * 100}% -  Faced times:{element.FacedTimes}";
				Console.WriteLine(message);
			}
		}

		public int[] PredictSequenceWithExtra()
		{
			var predicted = new int[Constants.GameNumbersFallsOutWithExtra];
			var value = archive.OrderBy(e => e.FacedTimes).Select(e => e.Value).Take(Constants.GameNumbersFallsOut).ToArray();
			var extra = extras.OrderBy(e => e.FacedTimes).Select(e => e.Value).First();

			for (int j = 0; j < Constants.GameNumbersFallsOut; j++)
			{
				predicted[j] = value[j];
			}

			predicted[Constants.GameNumbersFallsOut] = extra;

			return predicted;
		}

		public int[][] PredictSequencesWithExtra(int numberOfBets, bool withRepeatNumbers)
		{
			var value = archive.OrderBy(e => e.FacedTimes).Select(e => e.Value).Take(Constants.GameNumbersFallsOut * numberOfBets).ToArray();
			var extra = extras.OrderBy(e => e.FacedTimes).Select(e => e.Value).First();
			var values = new int[numberOfBets][];

			//for (int i = 0; i < numberOfBets; i++)
			//{
			//	values[i] = new int[Constants.GameNumbersFallsOutWithExtra];
			//	for (int j = 0; j < Constants.GameNumbersFallsOut; j++)
			//	{
			//		values[i][j] = value[i + j];
			//	}

			//	values[i][Constants.GameNumbersFallsOut] = extra;
			//}

			for (int i = 0; i < numberOfBets; i++)
			{
				values[i] = new int[Constants.GameNumbersFallsOutWithExtra];
				for (int j = 0; j < Constants.GameNumbersFallsOut; j++)
				{
					var q = withRepeatNumbers ? i + j : i * Constants.GameNumbersFallsOut + j;
					values[i][j] = value[q];
				}

				values[i][Constants.GameNumbersFallsOut] = extra;
			}

			return values;
		}

	}
}
