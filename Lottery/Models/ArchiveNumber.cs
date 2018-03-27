using Microsoft.SolverFoundation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Models
{
	[Serializable]
	public class ArchiveNumber
	{
		public int Value;

		public double ChanceToMeetNext;

		public int FacedTimes;

		public double FacedTimesFromTotalNumberOfFacings;

		public int Edition;

		[NonSerialized]
		public Rational BernuolliCoefToFaceNext;

		[NonSerialized]
		public Rational BernuolliCoefToNotFaceNext;

		[NonSerialized]
		public Rational BernuolliCoefDifference;
	}
}
