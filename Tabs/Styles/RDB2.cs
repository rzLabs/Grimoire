using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using Grimoire.Configuration;
using Grimoire.GUI;
using Grimoire.Structures;
using Grimoire.Utilities;

using Serilog;
using Serilog.Events;

// TODO: show filtered tag and operator/term being processed
// TODO: update displayed rowcount if being filtered
namespace Grimoire.Tabs.Styles
{
    public partial class RDB2 : UserControl
    {
        #region Private fields

        string structName = null;

        StructureManager structMgr = StructureManager.Instance;      
        ConfigManager configMgr = Main.Instance.ConfigMgr;
        TabManager tabMgr = TabManager.Instance;
        XmlManager xMgr = XmlManager.Instance;

        Stopwatch actionsw = new Stopwatch();

        Progress<int> taskProgress;

        ILogger Log = Serilog.Log.ForContext<RDB2>();

        #endregion

        #region Public fields

        public StructureObject StructObject = null;

        public List<RowObject> FilteredRows = new List<RowObject>();

        public string BuildDirectory;

        #endregion

        public DataGridView Grid => grid;

        public RDB2()
        {
            InitializeComponent();

            BuildDirectory = configMgr.Get<string>("BuildDirectory", "Grim");

            ts_enc_list.Items.AddRange(Encodings.Names);
        }

        #region Public methods

        public void Clear()
        {
            if (StructObject.Rows.Count > 0)
                StructObject.Rows.Clear();

            grid.RowCount = 0;
        }

        public void Localize() => xMgr.Localize(this, Localization.Enums.SenderType.Tab);

        public void TS_Load_Click(object sender, EventArgs e)
        {
            Paths.DefaultDirectory = configMgr.GetDirectory("DefaultDirectory", "RDB");
            Paths.DefaultFileName = StructObject.RDBName;

            string rdbPath = Paths.FilePath;

            if (rdbPath == null)
                return;

            ReadFile(rdbPath);
        }

        public void TS_Save_File_RDB_Click(object sender, EventArgs e)
        {
            string rdbname = StructObject.RDBName;

            if (rdbname == null)
            {
                Paths.Description = "Please select a save location";
                Paths.DefaultDirectory = configMgr.GetDirectory("DefaultDirectory", "RDB");

                if (Paths.SaveResult == DialogResult.OK)
                    rdbname = Paths.SavePath;
            }

            string buildDir = configMgr.GetDirectory("BuildDirectory", "Grim") ?? $"{Directory.GetCurrentDirectory()}\\Output\\";

            if (!Directory.Exists(buildDir))
                if (MessageBox.Show($"The output directory:\n\t- {buildDir}\n\nDoes not exist! Would you like to create it?", "Input Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Directory.CreateDirectory(buildDir);

            string rdbpath = $"{buildDir}\\{rdbname}";

            WriteFile(rdbpath);
        }

        public void ReadFile(string filename)
        {
            actionsw.Restart();

            try
            {
                StructObject.Read(filename);
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "loading file", "Load File Exception", LogEventLevel.Error);

                return;
            }

            actionsw.Stop();

            DateString dateStr = (StructObject.HeaderType == HeaderType.Traditional) ? StructObject.Header["CreationDate"] as DateString : new DateString(DateTime.Now.ToString("yyyyMMdd"));

            date_txBx.Text = dateStr.ToString("yyyy.MM.dd");

            int rowCnt = StructObject.RowCount;

            rows_txBx.Text = rowCnt.ToString();

            grid.RowCount = rowCnt;

            ts_save.Enabled = rowCnt > 0;

            tabMgr.Text = Path.GetFileNameWithoutExtension(filename);

            Log.Information($"{tabMgr.Text} loaded in... {StringExt.MilisecondsToString(actionsw.ElapsedMilliseconds)}");
        }

        public void WriteFile(string filename)
        {
            actionsw.Restart();

            try
            {
                StructObject.Write(filename);
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "loading file", "Save File Exception", LogEventLevel.Error);

                return;
            }

            actionsw.Stop();

            LogUtility.MessageBoxAndLog($"{StructObject.Rows.Count} rows written to: {StructObject.RDBName} in {StringExt.MilisecondsToString(actionsw.ElapsedMilliseconds)}", "Save Successful", LogEventLevel.Information);
        }

        public void Filter()
        {
            using (RdbSearch search = new RdbSearch(StructObject) { StartPosition = FormStartPosition.CenterParent })
            {
                if (search.ShowDialog(this) != DialogResult.OK)
                {
                    Serilog.Log.Verbose("User cancelled the filter process!");
                    return;
                }

                FilteredRows.Clear();

                try
                {
                    for (int i = 0; i < search.Results.Length; i++)
                    {
                        var indexPair = search.Results[i];

                        FilteredRows.Add(StructObject.Rows[indexPair.Key]);
                    }

                    Grid.Rows.Clear();
                    Grid.RowCount = FilteredRows.Count;
                }
                catch (Exception ex)
                {
                    LogUtility.MessageBoxAndLog(ex, "filtering the grid.", "Filter Exception", LogEventLevel.Error);
                    return;
                }
            }
        }

        #endregion

        #region Private methods

        DataGridViewTextBoxColumn[] generateGridColumns()
        {
            DataGridViewTextBoxColumn[] columns = new DataGridViewTextBoxColumn[StructObject.DataCells.Count];

            for (int i = 0; i < StructObject.DataCells.Count; i++)
            {
                CellBase cell = StructObject.DataCells[i];

                columns[i] = new DataGridViewTextBoxColumn()
                {
                    Name = cell.Name,
                    HeaderText = cell.Name,
                    Width = 100,
                    Resizable = DataGridViewTriState.True,
                    Visible = !cell.Flags.HasFlag(CellFlags.Hidden),
                    SortMode = DataGridViewColumnSortMode.Programmatic
                };
            }

            return columns;
        }

        void saveAsSqlScript(SqlStringType type)
        {
            if (StructObject is null)
                return;

            string buildDir = configMgr.GetDirectory("SQL_Directory", "RDB");
            string path = $"{buildDir}\\{StructObject.TableName}_{DateTime.Now:yyyy-MM-dd}.sql";
            string[] statements = new string[StructObject.Rows.Count];
            string whereColumn = null;

            try
            {
                if (!Directory.Exists(buildDir))
                    Directory.CreateDirectory(buildDir);

                if (type == SqlStringType.Update)
                    whereColumn = DialogUtility.RequestInput<string>("Input Required", "Please select the column to base the where select upon!", DialogUtility.DialogType.ListSelect, StructObject.VisibleCellNames);

                for (int i = 0; i < StructObject.Rows.Count; i++)
                {
                    RowObject row = StructObject.Rows[i];

                    statements[i] = row.ToSqlString(StructObject.TableName, type, whereColumn);
                }

                File.WriteAllLines(path, statements);
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "saving to t-sql", "Save File Exception", LogEventLevel.Error);

                return;
            }

            LogUtility.MessageBoxAndLog($"{StructObject.Rows.Count} rows saved to disk!", "Save Successful", LogEventLevel.Information);
        }

        private void onRDBProgress(int obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Events

        private async void ts_sel_struct_btn_Click(object sender, EventArgs e)
        {
            using (StructureSelect selectGUI = new StructureSelect())
            {
                if (selectGUI.ShowDialog(this) != DialogResult.OK)
                    return;

                structName = selectGUI.SelectedText;
            }

            StructObject = await structMgr.GetStruct(structName);

            ts_sel_struct_btn.Text = StructObject.Name;
            ts_sel_struct_btn.Checked = true;
            ts_sel_struct_btn.Enabled = false;

            ts_enc_list.SelectedIndex = Encodings.GetIndex(StructObject.Encoding.CodePage);

            // Create the original task to generate the columns
            Task<DataGridViewTextBoxColumn[]> generateColumns = Task.Run(() => generateGridColumns());

#pragma warning disable CS4014
            // We do not need to wait for the columns to be generated to proceed. Chain a new task to the original generateColumns call, then invoke the calling thread to update it with the results.
            generateColumns.ContinueWith(t => Invoke((MethodInvoker)(() =>
            {
                grid.Columns.AddRange(t.Result);

                ts_load.Enabled = grid.Columns.Count > 0;
            })));
#pragma warning restore CS4014 
        }

        private async void ts_load_sql_Click(object sender, EventArgs e)
        {
            if (StructObject == null)
                return;

            taskProgress = new Progress<int>(onRDBProgress);

            actionsw.Restart();

            RowObject[] rows = null;

            try
            {
                rows = await DatabaseUtility.ReadTableData(StructObject, taskProgress);
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "loading sql", "SQL Load Exception", LogEventLevel.Error);

                return;
            }

            actionsw.Stop();

            StructObject.SetHeader(new DateString(DateTime.Now.ToString("yyyyMMdd")), new byte[120], rows.Length);

            StructObject.Rows = new List<RowObject>(rows);

            grid.RowCount = StructObject.Rows.Count;

            date_txBx.Text = ((DateString)StructObject.Header["CreationDate"]).ToString("yyyy.MM.dd");

            rows_txBx.Text = rows.Length.ToString();

            ts_save.Enabled = rows.Length > 0;

            Log.Information($"{StructObject.TableName} loaded from sql in {StringExt.MilisecondsToString(actionsw.ElapsedMilliseconds)}");
        }

        private void ts_save_file_Click(object sender, EventArgs e)
        {
            SaveFileType saveType = (SaveFileType)configMgr.Get<int>("DefaultSaveType", "RDB");

            if (saveType.HasFlag(SaveFileType.RDB) || saveType == SaveFileType.ALL)
                TS_Save_File_RDB_Click(this, EventArgs.Empty);

            if (saveType.HasFlag(SaveFileType.SQL) || saveType == SaveFileType.ALL)
                ts_save_file_sql_Click(this, EventArgs.Empty);

            if (saveType.HasFlag(SaveFileType.CSV) || saveType == SaveFileType.ALL)
                ts_save_file_csv_Click(this, EventArgs.Empty);
        }

        private void ts_save_file_sql_Click(object sender, EventArgs e)
        {
            var type = (SaveSqlFileType)configMgr.Get<int>("DefaultSqlSaveType", "RDB", SaveSqlFileType.Insert);

            switch (type)
            {
                case SaveSqlFileType.Insert:
                    ts_save_file_sql_insert_Click(this, EventArgs.Empty);
                    break;

                case SaveSqlFileType.Update:
                    ts_save_file_sql_update_Click(this, EventArgs.Empty);
                    break;
            }
        }

        private async void ts_save_file_csv_Click(object sender, EventArgs e)
        {
            if (StructObject is null)
                return;

            string buildDir = configMgr.GetDirectory("CSV_Directory", "RDB");
            string path = $"{buildDir}\\{StructObject.TableName}_{DateTime.Now:yyyy-MM-dd}.csv";
            string csv = null;

            try
            {
                await Task.Run(() => { csv = StructObject.GenerateCSV(); });

                File.WriteAllText(path, csv);
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "saving to csv", "Save File Exception", LogEventLevel.Error);

                return;
            }

            LogUtility.MessageBoxAndLog($"{StructObject.Rows.Count} rows saved to disk!", "Save Successful", LogEventLevel.Information);
        }

        private void ts_save_file_sql_insert_Click(object sender, EventArgs e) => saveAsSqlScript(SqlStringType.Insert);

        private void ts_save_file_sql_update_Click(object sender, EventArgs e) => saveAsSqlScript(SqlStringType.Update);

        private void ts_save_sql_Click(object sender, EventArgs e)
        {
            if (StructObject is null)
                return;

            taskProgress = new Progress<int>(onRDBProgress);

            DatabaseUtility.WriteTableData(StructObject, taskProgress).ContinueWith(_ => LogUtility.MessageBoxAndLog($"{StructObject.RowCount} written to sql table: {StructObject.TableName}", "SQL Export Successful", LogEventLevel.Information));
        }

        private void ts_search_Click(object sender, EventArgs e)
        {
            if (FilteredRows.Count > 0)
            {
                FilteredRows.Clear();
                Grid.Rows.Clear();
                Grid.RowCount = StructObject.RowCount;
            }

            if (StructObject?.RowCount > 0)
                Filter();
            else
                Serilog.Log.Verbose("Cannot filter no data!");
        }

        private void rowGrid_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            CellBase cell = cell = StructObject.DataCells[e.ColumnIndex];
            dynamic val = null;

            if (FilteredRows.Count > 0) // The loaded data has been filtered
            {
                if (e.RowIndex < 0 || e.RowIndex >= FilteredRows.Count ||
                    e.ColumnIndex < 0 || e.ColumnIndex >= StructObject.DataCells.Count)
                    return;

                if (cell.PrimaryType == typeof(string))
                    val = StructObject.Encoding.GetString((byte[])FilteredRows[e.RowIndex][e.ColumnIndex]);
                else
                    val = FilteredRows[e.RowIndex][e.ColumnIndex];
            }
            else
            {
                if (e.RowIndex < 0 || e.RowIndex >= StructObject.Rows.Count ||
                    e.ColumnIndex < 0 || e.ColumnIndex >= StructObject.DataCells.Count)
                    return;

                if (cell.PrimaryType == typeof(string))
                    val = StructObject.Encoding.GetString((byte[])StructObject.Rows[e.RowIndex][e.ColumnIndex]);
                else
                    val = StructObject.Rows[e.RowIndex][e.ColumnIndex];
            }

            e.Value = val;       
        }

        #endregion
    }

    [Flags]
    public enum SaveFileType
    {
        RDB = 1,
        SQL = 2,
        CSV = 4,
        ALL = RDB | SQL | CSV
    }

    public enum SaveSqlFileType
    {
        Insert,
        Update
    }
}