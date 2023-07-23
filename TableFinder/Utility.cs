using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;
namespace TableFinder
{
    public struct CellFinder
    {
        public readonly List<KeyValuePair<int, int>> ColumnRow;
        public readonly string WorksheetName;
        public readonly string FilePath;

        public CellFinder(int column, int row, string worksheetName, string filePath)
        {
            ColumnRow = new List<KeyValuePair<int, int>> {new KeyValuePair<int, int>(column, row)};
            WorksheetName = worksheetName;
            FilePath = filePath;
        }
    }

    public class Utility
    {
        public static void SelectCell(CellFinder cellFinder)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Open(cellFinder.FilePath);
            Excel.Worksheet activeSheet = null;

            string worksheetName = cellFinder.WorksheetName.Replace("$", "");
            foreach (Excel.Worksheet sheet in workbook.Sheets)
            {
                if (sheet.Name == worksheetName)
                {
                    activeSheet = sheet;
                    break;
                }
            }
            activeSheet?.Activate();

            List<Excel.Range> ranges = new List<Excel.Range>();
            string cellAddress = MakeColumnKey(cellFinder.ColumnRow[0].Key) + cellFinder.ColumnRow[0].Value.ToString();

            Excel.Range unionRange = activeSheet?.get_Range(cellAddress, cellAddress);
            for (int i = 1; i < cellFinder.ColumnRow.Count; i++)
            {
                cellAddress = MakeColumnKey(cellFinder.ColumnRow[i].Key) + cellFinder.ColumnRow[i].Value.ToString();
                unionRange = excelApp.Union(unionRange, activeSheet?.get_Range(cellAddress));
            }

            unionRange?.Select();
            excelApp.Visible = true;
        }

        static string MakeColumnKey(int column)
        {
            string columnString = "";
            decimal columnNumber = column;
            while (columnNumber > 0)
            {
                decimal currentLetterNumber = (columnNumber - 1) % 26;
                char currentLetter = (char)(currentLetterNumber + 65);
                columnString = currentLetter + columnString;
                columnNumber = (columnNumber - (currentLetterNumber + 1)) / 26;
            }
            return columnString;
        }
    }
}