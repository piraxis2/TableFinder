using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;
using System.Linq;

namespace TableFinder
{
    public partial class TableFinderForm : Form
    {
        private Thread _findThread = null; 
        private readonly Dictionary<string,CellFinder> _cellFinders = new Dictionary<string,CellFinder>();
        private string _folderPath = null;
        private OleDbConnection _connection = null;
        public TableFinderForm()
        {
            InitializeComponent();

            Load += new EventHandler((sender, args) =>
            {
                string filePath = "folderPath.txt";
                if (File.Exists(filePath))
                {
                    string loadData = File.ReadAllText(filePath);
                    FilePathBox.Text = loadData;
                    _folderPath = loadData.Replace(@"\", @"\\");
                }
            });

        }

        private void findButton_Click(object sender, EventArgs e)
        {
            RequestFind();
        }

        private void FinderOnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RequestFind();
            }
        }

        private void RequestFind()
        {
            _connection?.Close();
            _connection = null;
            _findThread?.Abort();
            _findThread = null;
            listBox.Items.Clear();
            _cellFinders.Clear();

            KillExcel();

            _findThread = new Thread(Find)
            {
                IsBackground = true
            };
            _findThread?.Start(Finder.Text);
        }

        private void Find(object searchString)
        {
            string[] separatingStrings = {","};
            var searchStrings = searchString.ToString().Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);

            Regex fileNameExtraction = new Regex(@"[^\\]+(?=\.[^.]+($|\?))");
            // Get all Excel files in the folder
            string[] filePaths = Directory.GetFiles(_folderPath, "*.xlsx");
            for (int i = 0; i < filePaths.Length; i++)
            {
                Invoke(new Action(delegate ()
                {
                    toolStripProgressBar.Value = (int) (((double) (i + 1) / filePaths.Length) * 100);
                }));
                string filePath = filePaths[i];
                string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"";
                string fileName = fileNameExtraction.Match(filePath).Value;
                if (fileName.Contains("~"))
                    continue;

                using (_connection = new OleDbConnection(connectionString)) 
                {
                    _connection.Open();
                    DataTable schemaTable = _connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    foreach (DataRow schemaRow in schemaTable.Rows)
                    {
                        string worksheetName = schemaRow["TABLE_NAME"].ToString();
                        if (worksheetName.Contains("#")) 
                            continue;

                        Console.WriteLine($"{worksheetName}");
                        string commandText = $"SELECT * FROM [{worksheetName}]";
                        using (OleDbCommand command = new OleDbCommand(commandText, _connection))
                        {
                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                int row = 1;
                                while (reader.Read())
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        string cellValue = reader.GetValue(col)?.ToString();
                             
                                        if (searchStrings.Contains(cellValue)) 
                                        {
                                            if (_cellFinders.TryGetValue(worksheetName, out var finder))
                                            {
                                                if (cellValue != null)
                                                {
                                                    if (finder.FindDataCollection.TryGetValue(cellValue, out var columnRow))
                                                        columnRow.Add(new KeyValuePair<int, int>(col + 1, row + 1));
                                                    else
                                                        finder.FindDataCollection.Add(cellValue, new List<KeyValuePair<int, int>>() {new KeyValuePair<int, int>(col + 1, row + 1)});
                                                }
                                            }
                                            else
                                                _cellFinders.Add(worksheetName, new CellFinder(col + 1, row + 1, worksheetName, filePath,cellValue));
                                        }
                                    }
                                    Invoke(new Action(delegate ()
                                    {
                                        if (_cellFinders.TryGetValue(worksheetName, out var finder))
                                        {
                                            if (finder.FindDataCollection.Count == searchStrings.Length) 
                                                if (!listBox.Items.Contains(worksheetName))
                                                    listBox.Items.Add(worksheetName);
                                        }
                           
                                    }));
                                    row++;
                                }
                            }
                        }
                    }
                    _connection.Close();
                }
            }
        }

        private void KillExcel()
        {
            foreach (var process in  Process.GetProcesses())
            {
                if (process.ProcessName.Contains("EXCEL"))
                    process.Kill();
            }
        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            if (listBox.SelectedItems.Count > 1) 
                return;

            string worksheetName = listBox.SelectedItem?.ToString();
            if (worksheetName != null)
                if (_cellFinders.TryGetValue(worksheetName, out var finder))
                {
                    Utility.SelectCell(finder);
                }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _findThread?.Abort();
        }

        private void FilePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog.SelectedPath;
                string filePath = "folderPath.txt";
                File.WriteAllText(filePath, selectedFolderPath);
                _folderPath = selectedFolderPath.Replace(@"\",@"\\" );
                FilePathBox.Text = selectedFolderPath;
            }
        }
    }
}