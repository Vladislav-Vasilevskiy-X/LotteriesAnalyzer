using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Models
{
	public static class LaplaceFunctionValues
	{
		private static Dictionary<double,double> Values { get; }

		public static double GetTableValue(double x)
		{
			return Values[x];
		}

		static LaplaceFunctionValues()
		{
			Values = Serializer.ReadFromBinaryFile<Dictionary<double, double>>(Constants.TableValuesFilePath);
		}
	}
}
