using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SolverFoundation.Common;

namespace Lottery.Models
{
	public class Combination
	{
		public int[] Value { get; set; }
		public Rational ChanceToMeet { get; set; }

		public Combination(int[] elements, Rational chanceToMeet)
		{
			Value = elements;
			ChanceToMeet = chanceToMeet;
		}
	}
}
