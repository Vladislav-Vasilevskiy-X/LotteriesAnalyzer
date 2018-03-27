using Lottery.Sumilator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Lottery.Models;

namespace Lottery
{
	public class ExcelParser
	{
		public static Archive getExcelArchiveFile(string fileLocation)
		{
			var archive = new Archive();
			//Create COM Objects. Create a COM object for everything that is referenced
			Excel.Application xlApp = new Excel.Application();
			Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileLocation);
			Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
			Excel.Range xlRange = xlWorksheet.UsedRange;

			int rowCount = xlRange.Rows.Count;
			int colCount = xlRange.Columns.Count;

			//iterate over the rows and columns and print to the console as it appears in the file
			//excel is not zero based!!
			for (int i = 1; i <= rowCount; i++)
			{
				int[] seqence = new int[6];
				for (int j = 1; j <= colCount; j++)
				{
					//new line
					if (j == 1)
						Console.Write("\r\n");

					if (i > 1 && j >= 3 && j <= 8)
					{
						seqence[j - 3] = Int32.Parse(xlRange.Cells[i, j].Value2.ToString());
					}

					//write the value to the console
					if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
						Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
				}

				if (i > 1)
					archive.AddSequence(seqence, i - 1);
			}

			//cleanup
			GC.Collect();
			GC.WaitForPendingFinalizers();

			//rule of thumb for releasing com objects:
			//  never use two dots, all COM objects must be referenced and released individually
			//  ex: [somthing].[something].[something] is bad

			//release com objects to fully kill excel process from running in the background
			Marshal.ReleaseComObject(xlRange);
			Marshal.ReleaseComObject(xlWorksheet);

			//close and release
			xlWorkbook.Close();
			Marshal.ReleaseComObject(xlWorkbook);

			//quit and release
			xlApp.Quit();
			Marshal.ReleaseComObject(xlApp);

			return archive;
		}

		public static Dictionary<double, double> getGaussTable(string fileLocation)
		{
			var archive = new Archive();
			//Create COM Objects. Create a COM object for everything that is referenced
			Excel.Application xlApp = new Excel.Application();
			Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileLocation);
			Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
			Excel.Range xlRange = xlWorksheet.UsedRange;

			int rowCount = xlRange.Rows.Count;
			int colCount = xlRange.Columns.Count;

			Dictionary<double, double> values = new Dictionary<double, double>();

			//iterate over the rows and columns and print to the console as it appears in the file
			//excel is not zero based!!
			for (int i = 2; i <= rowCount; i++)
			{
				for (int j = 2; j <= colCount; j++)
				{
					if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
					{
						string b = xlRange.Cells[i, 1].Value2.ToString();
						string v = xlRange.Cells[j, 1].Value2.ToString();
						var stringValue = b.Replace(",", ".") + v.Replace(",", string.Empty).Replace("0", string.Empty);
						values.Add(Double.Parse(stringValue), Double.Parse(xlRange.Cells[i, j].Value2.ToString()));
					}
				}
			}

			//cleanup
			GC.Collect();
			GC.WaitForPendingFinalizers();

			//rule of thumb for releasing com objects:
			//  never use two dots, all COM objects must be referenced and released individually
			//  ex: [somthing].[something].[something] is bad

			//release com objects to fully kill excel process from running in the background
			Marshal.ReleaseComObject(xlRange);
			Marshal.ReleaseComObject(xlWorksheet);

			//close and release
			xlWorkbook.Close();
			Marshal.ReleaseComObject(xlWorkbook);

			//quit and release
			xlApp.Quit();
			Marshal.ReleaseComObject(xlApp);

			return values;
		}
	}
}
