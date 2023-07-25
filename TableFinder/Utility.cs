using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
namespace TableFinder
{
    public struct CellFinder
    {
        public readonly Dictionary<string, List<KeyValuePair<int, int>>> FindDataCollection;
        public readonly string WorksheetName;
        public readonly string FilePath;

        public CellFinder(int column, int row, string worksheetName, string filePath, string findData)
        {
            FindDataCollection = new Dictionary<string, List<KeyValuePair<int, int>>>();
            var columnRow = new List<KeyValuePair<int, int>> {new KeyValuePair<int, int>(column, row)};
            WorksheetName = worksheetName;
            FilePath = filePath;
            FindDataCollection.Add(findData, columnRow);
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

            string firstCellAddress = MakeColumnKey(cellFinder.FindDataCollection.First().Value.First().Key) + cellFinder.FindDataCollection.First().Value.First().Value.ToString();
            Excel.Range unionRange = activeSheet?.get_Range(firstCellAddress,firstCellAddress);
            foreach (var elem in cellFinder.FindDataCollection)
            {
                for (int i = 0; i < elem.Value.Count; i++)
                {
                    string cellAddress  = MakeColumnKey(elem.Value[i].Key) + elem.Value[i].Value.ToString();
                    unionRange = excelApp.Union(unionRange, activeSheet?.get_Range(cellAddress));
                }
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