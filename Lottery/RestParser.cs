using System;
using System.Collections.Generic;
using System.Globalization;
using Lottery.Models;

namespace Lottery
{
	public class RestParser
	{
		public ArchiveWithExtra ParseAsArchive(string input)
		{
			var cleared = input.Replace(@"\n", String.Empty).Replace(@"\t", string.Empty);
			var numbers = new List<int>();
			var buf = string.Empty;
			for (int i = 0; i < cleared.Length - 4; i++)
			{
				buf = string.Empty;
				if (cleared[i + 2] == '<' && cleared[i + 3] == '/' && cleared[i + 4] == 'b')
				{
					buf = cleared[i].ToString() + cleared[i + 1].ToString();
					numbers.Add(Int32.Parse(buf, NumberStyles.Any));
				}
			}

			var innerIndex = 0;
			var buffer = new int[Constants.GameNumbersFallsOutWithExtra];
			var archive = new ArchiveWithExtra();
			for (int i = 0; i < numbers.Count; i++)
			{
				buffer[innerIndex] = numbers[i];
				if (innerIndex == Constants.GameNumbersFallsOut)
				{
					archive.AddSequence(buffer);
					buffer = new int[Constants.GameNumbersFallsOutWithExtra];
					innerIndex = 0;
					continue;
				}

				innerIndex++;
			}

			return archive;
		}
	}
}
