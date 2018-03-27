using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lottery.Sumilator;
using Lottery;
using Microsoft.SolverFoundation.Common;
using Lottery.Models;

namespace Tests
{
	[TestClass]
	public class Tests
	{

		private Simulator simulator = new Simulator();
		private int Editions = 100000;

		private TestContext testContextInstance;

		/// <summary>
		///  Gets or sets the test context which provides
		///  information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get { return testContextInstance; }
			set { testContextInstance = value; }
		}

		[TestMethod]
		public void OneBetManyTimes()
		{
			var bet = simulator.GenerateGameResult();
			var threeCons = 0;
			var fourCons = 0;
			var fiveCons = 0;
			var sixCons = 0;

			for (int i = 0; i < Editions; i++)
			{
				var res = Bettor.CheckResult(bet, simulator.GenerateGameResult());
				switch (res)
				{
					case 3:
						threeCons++;
						break;
					case 4:
						fourCons++;
						break;
					case 5:
						fiveCons++;
						break;
					case 6:
						sixCons++;
						break;
					default:
						break;
				}
			}

			var winsCount = threeCons + fourCons + fiveCons + sixCons;
			Console.WriteLine($"Total plays: {Editions}, total wins: {winsCount}, total loses: {Editions - winsCount}, win times: {Math.Round((double)winsCount / (double)Editions, 8) * 100}%");
			Console.WriteLine($"3 wons {threeCons}, times from total wins: {Math.Round((double)threeCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)threeCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"4 wons {fourCons}, times from total wins: {Math.Round((double)fourCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fourCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"5 wons {fiveCons}, times from total wins: {Math.Round((double)fiveCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fiveCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"6 wons {sixCons}, times from total wins: {Math.Round((double)sixCons / (double)winsCount, 8) * 100}%, chance to win: { Math.Round((double)sixCons / (double)Editions, 8) * 100}% ");
		}

		[TestMethod]
		public void PredictBetBernoulliTimes()
		{

			var archive = new Archive();
			var threeCons = 0;
			var fourCons = 0;
			var fiveCons = 0;
			var sixCons = 0;
			//var games = 511;

			//for (var i = 1; i <= games; i++)
			//{
			//	var resultArray = simulator.GenerateGameResult();
			//	archive.AddSequence(resultArray, i);
			//}
			var bet = archive.PredictSequence();
			for (int i = 1; i < Editions; i++)
			{
				var sequence = simulator.GenerateGameResult();
				//if (i % 10 == 0)
				//{
				bet = archive.PredictSequenceWithBernoulli(i);
				//}

				var result = Bettor.CheckResult(bet, sequence);
				archive.AddSequence(sequence, i);
				switch (result)
				{
					case 3:
						threeCons++;
						break;
					case 4:
						fourCons++;
						break;
					case 5:
						fiveCons++;
						break;
					case 6:
						sixCons++;
						break;
					default:
						break;
				}
			}
			var winsCount = threeCons + fourCons + fiveCons + sixCons;
			Console.WriteLine($"Total plays: {Editions}, total wins: {winsCount}, total loses: {Editions - winsCount}, win times: {Math.Round((double)winsCount / (double)Editions, 8) * 100}%");
			Console.WriteLine($"3 wons {threeCons}, times from total wins: {Math.Round((double)threeCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)threeCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"4 wons {fourCons}, times from total wins: {Math.Round((double)fourCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fourCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"5 wons {fiveCons}, times from total wins: {Math.Round((double)fiveCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fiveCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"6 wons {sixCons}, times from total wins: {Math.Round((double)sixCons / (double)winsCount, 8) * 100}%, chance to win: { Math.Round((double)sixCons / (double)Editions, 8) * 100}% ");
		}

		[TestMethod]
		public void PredictBetEachTenTimes()
		{

			var archive = new Archive();
			var threeCons = 0;
			var fourCons = 0;
			var fiveCons = 0;
			var sixCons = 0;
			//var games = 511;

			//for (var i = 1; i <= games; i++)
			//{
			//	var resultArray = simulator.GenerateGameResult();
			//	archive.AddSequence(resultArray, i);
			//}
			var bet = archive.PredictSequence();
			for (int i = 1; i < Editions; i++)
			{
				var sequence = simulator.GenerateGameResult();
				//if (i % 10 == 0)
				//{
				bet = archive.PredictSequence();
				//}

				var result = Bettor.CheckResult(bet, sequence);
				archive.AddSequence(sequence, i);
				switch (result)
				{
					case 3:
						threeCons++;
						break;
					case 4:
						fourCons++;
						break;
					case 5:
						fiveCons++;
						break;
					case 6:
						sixCons++;
						break;
					default:
						break;
				}
			}
			var winsCount = threeCons + fourCons + fiveCons + sixCons;
			Console.WriteLine($"Total plays: {Editions}, total wins: {winsCount}, total loses: {Editions - winsCount}, win times: {Math.Round((double)winsCount / (double)Editions, 8) * 100}%");
			Console.WriteLine($"3 wons {threeCons}, times from total wins: {Math.Round((double)threeCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)threeCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"4 wons {fourCons}, times from total wins: {Math.Round((double)fourCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fourCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"5 wons {fiveCons}, times from total wins: {Math.Round((double)fiveCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fiveCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"6 wons {sixCons}, times from total wins: {Math.Round((double)sixCons / (double)winsCount, 8) * 100}%, chance to win: { Math.Round((double)sixCons / (double)Editions, 8) * 100}% ");
		}

		[TestMethod]
		public void ThreeBetsRetryManyTimes()
		{
			var betsToPlayOnce = 3;
			var bets = simulator.GenerateBets(betsToPlayOnce);
			var threeCons = 0;
			var fourCons = 0;
			var fiveCons = 0;
			var sixCons = 0;

			for (int i = 0; i < Editions; i++)
			{
				var gameRes = simulator.GenerateGameResult();
				for (int b = 0; b < betsToPlayOnce; b++)
				{
					var res = Bettor.CheckResult(bets[b], gameRes);
					switch (res)
					{
						case 3:
							threeCons++;
							break;
						case 4:
							fourCons++;
							break;
						case 5:
							fiveCons++;
							break;
						case 6:
							sixCons++;
							break;
						default:
							break;
					}
				}
			}

			var winsCount = threeCons + fourCons + fiveCons + sixCons;
			Console.WriteLine($"Total plays: {Editions}, total wins: {winsCount}, total loses: {Editions - winsCount}, win times: {Math.Round((double)winsCount / (double)Editions, 8) * 100}%");
			Console.WriteLine($"3 wons {threeCons}, times from total wins: {Math.Round((double)threeCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)threeCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"4 wons {fourCons}, times from total wins: {Math.Round((double)fourCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fourCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"5 wons {fiveCons}, times from total wins: {Math.Round((double)fiveCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fiveCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"6 wons {sixCons}, times from total wins: {Math.Round((double)sixCons / (double)winsCount, 8) * 100}%, chance to win: { Math.Round((double)sixCons / (double)Editions, 8) * 100}% ");
		}

		[TestMethod]
		public void EightBetsRetryManyTimes()
		{
			var betsToPlayOnce = 8;
			var bets = simulator.GenerateBets(betsToPlayOnce);
			var threeCons = 0;
			var fourCons = 0;
			var fiveCons = 0;
			var sixCons = 0;

			for (int i = 0; i < Editions; i++)
			{
				var gameRes = simulator.GenerateGameResult();
				for (int b = 0; b < betsToPlayOnce; b++)
				{
					var res = Bettor.CheckResult(bets[b], gameRes);
					switch (res)
					{
						case 3:
							threeCons++;
							break;
						case 4:
							fourCons++;
							break;
						case 5:
							fiveCons++;
							break;
						case 6:
							sixCons++;
							break;
						default:
							break;
					}
				}
			}

			var winsCount = threeCons + fourCons + fiveCons + sixCons;
			Console.WriteLine($"Total plays: {Editions}, total wins: {winsCount}, total loses: {Editions - winsCount}, win times: {Math.Round((double)winsCount / (double)Editions, 8) * 100}%");
			Console.WriteLine($"3 wons {threeCons}, times from total wins: {Math.Round((double)threeCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)threeCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"4 wons {fourCons}, times from total wins: {Math.Round((double)fourCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fourCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"5 wons {fiveCons}, times from total wins: {Math.Round((double)fiveCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fiveCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"6 wons {sixCons}, times from total wins: {Math.Round((double)sixCons / (double)winsCount, 8) * 100}%, chance to win: { Math.Round((double)sixCons / (double)Editions, 8) * 100}% ");
		}

		[TestMethod]
		public void PredictThreeBetsEachTenTimes()
		{
			var archive = new Archive();

			var betsToPlayOnce = 3;
			var bets = archive.PredictSequences(betsToPlayOnce);
			var threeCons = 0;
			var fourCons = 0;
			var fiveCons = 0;
			var sixCons = 0;

			for (int i = 0; i < Editions; i++)
			{
				var sequence = simulator.GenerateGameResult();
				if (i % 10 == 0)
				{
					bets = archive.PredictSequences(betsToPlayOnce);
				}

				archive.AddSequence(sequence, i);
				for (int b = 0; b < betsToPlayOnce; b++)
				{
					var res = Bettor.CheckResult(bets[b], sequence);
					switch (res)
					{
						case 3:
							threeCons++;
							break;
						case 4:
							fourCons++;
							break;
						case 5:
							fiveCons++;
							break;
						case 6:
							sixCons++;
							break;
						default:
							break;
					}
				}
			}

			var winsCount = threeCons + fourCons + fiveCons + sixCons;
			Console.WriteLine($"Total plays: {Editions}, total wins: {winsCount}, total loses: {Editions - winsCount}, win times: {Math.Round((double)winsCount / (double)Editions, 8) * 100}%");
			Console.WriteLine($"3 wons {threeCons}, times from total wins: {Math.Round((double)threeCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)threeCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"4 wons {fourCons}, times from total wins: {Math.Round((double)fourCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fourCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"5 wons {fiveCons}, times from total wins: {Math.Round((double)fiveCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fiveCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"6 wons {sixCons}, times from total wins: {Math.Round((double)sixCons / (double)winsCount, 8) * 100}%, chance to win: { Math.Round((double)sixCons / (double)Editions, 8) * 100}% ");
		}

		[TestMethod]
		public void PredictEightBetsEachTenTimes()
		{
			var archive = new Archive();

			var betsToPlayOnce = 42;
			var bets = archive.PredictSequences(betsToPlayOnce);
			var threeCons = 0;
			var fourCons = 0;
			var fiveCons = 0;
			var sixCons = 0;

			for (int i = 0; i < Editions; i++)
			{
				var sequence = simulator.GenerateGameResult();
				//if (i % 1 == 0)
				//{
				bets = archive.PredictSequences(betsToPlayOnce);
				//}

				archive.AddSequence(sequence, i);
				for (int b = 0; b < betsToPlayOnce; b++)
				{
					var res = Bettor.CheckResult(bets[b], sequence);
					switch (res)
					{
						case 3:
							threeCons++;
							break;
						case 4:
							fourCons++;
							break;
						case 5:
							fiveCons++;
							break;
						case 6:
							sixCons++;
							break;
						default:
							break;
					}
				}
			}

			var winsCount = threeCons + fourCons + fiveCons + sixCons;
			Console.WriteLine($"Total plays: {Editions}, total wins: {winsCount}, total loses: {Editions - winsCount}, win times: {Math.Round((double)winsCount / (double)Editions, 8) * 100}%");
			Console.WriteLine($"3 wons {threeCons}, times from total wins: {Math.Round((double)threeCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)threeCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"4 wons {fourCons}, times from total wins: {Math.Round((double)fourCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fourCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"5 wons {fiveCons}, times from total wins: {Math.Round((double)fiveCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fiveCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"6 wons {sixCons}, times from total wins: {Math.Round((double)sixCons / (double)winsCount, 8) * 100}%, chance to win: { Math.Round((double)sixCons / (double)Editions, 8) * 100}% ");
		}

		[TestMethod]
		public void PredictBernoulliEightBetsEachTenTimes()
		{
			var archive = new Archive();
			var betsToPlayOnce = 42;
			var threeCons = 0;
			var fourCons = 0;
			var fiveCons = 0;
			var sixCons = 0;
			int[][] bets = new int[betsToPlayOnce][];

			for (int i = 0; i < Editions; i++)
			{
				var sequence = simulator.GenerateGameResult();

				if (i >= 1000)
				{
					if (i % 500 == 0)
						bets = archive.PredictSequencesWithBernoulli(betsToPlayOnce, i + 1);
				}
				
				
				archive.AddSequence(sequence, i);
				if (i >= 1000)
				{
					for (int b = 0; b < betsToPlayOnce; b++)
					{
						var res = Bettor.CheckResult(bets[b], sequence);
						switch (res)
						{
							case 3:
								threeCons++;
								break;
							case 4:
								fourCons++;
								break;
							case 5:
								fiveCons++;
								break;
							case 6:
								sixCons++;
								break;
							default:
								break;
						}
					}
				}
			}

			var winsCount = threeCons + fourCons + fiveCons + sixCons;
			Console.WriteLine($"Total plays: {Editions}, total wins: {winsCount}, total loses: {Editions - winsCount}, win times: {Math.Round((double)winsCount / (double)Editions, 8) * 100}%");
			Console.WriteLine($"3 wons {threeCons}, times from total wins: {Math.Round((double)threeCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)threeCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"4 wons {fourCons}, times from total wins: {Math.Round((double)fourCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fourCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"5 wons {fiveCons}, times from total wins: {Math.Round((double)fiveCons / (double)winsCount, 8) * 100}%, chance to win: {Math.Round((double)fiveCons / (double)Editions, 8) * 100}%");
			Console.WriteLine($"6 wons {sixCons}, times from total wins: {Math.Round((double)sixCons / (double)winsCount, 8) * 100}%, chance to win: { Math.Round((double)sixCons / (double)Editions, 8) * 100}% ");
		}

		[TestMethod]
		public void ExcelParse()
		{
			var archive = ExcelParser.getExcelArchiveFile("");
			var bets = archive.PredictSequences(8);
			archive.CalculateStatistics(512 * 6);
			archive.Print();
			archive.PrintStatistics();

			for (int i = 0; i < 8; i++)
			{
				foreach (var number in bets[i])
				{
					var postfix = (Array.IndexOf(bets[i], number) == bets[i].Length - 1) ? ".\n" : ", ";
					Console.Write(number + postfix);
				}
			}
		}

		[TestMethod]
		public void SerializeFromExcel()
		{
			var filePath = @"D:\SerializedArchive" + DateTime.Now.Ticks + ".dat";
			var archive = ExcelParser.getExcelArchiveFile(@"D:\results.xlsx");
			Serializer.WriteToBinaryFile(filePath, archive);

			var fromBin = Serializer.ReadFromBinaryFile<Archive>(filePath);

			Assert.AreEqual(archive.archive.Count, fromBin.archive.Count);
		}

		[TestMethod]
		public void SerializeFromExcelGauss()
		{
			var filePath = @"E:\GaussTableValues.dat";
			var table = ExcelParser.getGaussTable(@"E:\gaus.xlsx");
			Serializer.WriteToBinaryFile(filePath, table);

			var fromBin = Serializer.ReadFromBinaryFile<Dictionary<double, double>>(filePath);

			Assert.AreEqual(table.Count, fromBin.Count);
		}

		[TestMethod]
		public void GetTable()
		{
			var filePath = @"..\Debug\Tables\GaussTableValues.dat";
			
			var table = Serializer.ReadFromBinaryFile<Dictionary<double, double>>(filePath);

			Assert.IsNotNull(table);
		}

		[TestMethod]
		public void GetARchive()
		{
			var fromBin = Serializer.ReadFromBinaryFile<Archive>(@"D:\SerializedArchive636569246508737491.dat");
			var filePath = @"D:\SerializedArchive" + DateTime.Now.Ticks + ".dat";

		}

		[TestMethod]
		public void PredictToBelSixFromFN()
		{
			var fromBin = Serializer.ReadFromBinaryFile<Archive>(@"E:\SerializedArchive636571570016373992.dat");
			var seqs = fromBin.PredictSequencesWithBernoulli(44, 512);

			for (int i = 0; i < 44; i++)
			{
				foreach (var number in seqs[i])
				{
					var postfix = (Array.IndexOf(seqs[i], number) == seqs[i].Length - 1) ? ".\n" : ", ";
					Console.Write(number + postfix);
				}
			}

			var filePath = @"D:\SerializedArchive" + DateTime.Now.Ticks + ".dat";
		}

		[TestMethod]
		public void CompareLaplaceAndBern()
		{
			var rational = 1/ (Rational)2;
			var bern = Calculator.Bernoulli(rational, 100, 51);
			var lap = Calculator.MoivreLaplace(0.5, 100, 51);

			Rational res;
			Rational.Power(9, 1 / (Rational) 2, out res);

			Assert.AreEqual(bern, lap);
		}
	}
}