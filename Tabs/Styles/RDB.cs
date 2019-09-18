using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text;
using Grimoire.Utilities;
using Daedalus;
using Daedalus.Structures;
using Daedalus.Enums;
using System.Linq;

namespace Grimoire.Tabs.Styles
{
    public partial class rdbTab : UserControl
    {
        #region Properties

        GUI.Main main = GUI.Main.Instance;
        Logs.Manager lManager = Logs.Manager.Instance;
        Manager tManager = Manager.Instance;
        Database dManager = null;
        public Daedalus.Core core = new Daedalus.Core();
        DataCore.Core dCore = null;
        string structsDir = null;
        readonly string key = null;
        readonly Tabs.Utilities.Grid gridUtil;
        readonly Stopwatch actionSW = new Stopwatch();

        bool structLoaded { get { return (ts_struct_list.SelectedIndex != -1); } }
        readonly string buildDir = string.Format(@"{0}\Output\", Directory.GetCurrentDirectory());

        public Core Core
        {
            get
            {
                if (core == null) { throw new Exception("Daedalus is null!"); }
                return core;
            }
        }

        public DataGridView Grid { get { return grid; } }

        public int ProgressMax
        {
            set { this.Invoke(new MethodInvoker(delegate { ts_prog.Maximum = value; })); }
        }

        public int ProgressVal
        {
            set { this.Invoke(new MethodInvoker(delegate { ts_prog.Value = value; })); }
        }

        public bool UseASCII
        {
            get { return ts_save_w_ascii.Checked; }
            set { ts_save_w_ascii.Checked = value; }
        }

        #endregion

        #region Constructors

        public rdbTab(string key)
        {
            InitializeComponent();
            this.key = key;
            gridUtil = new Utilities.Grid();
            ts_save_enc.Checked = OPT.GetBool("rdb.save.hashed");
            ts_save_w_ascii.Checked = OPT.GetBool("rdb.use.ascii");
            structsDir = OPT.GetString("rdb.structure.directory") ?? string.Format(@"{0}\Structures\", Directory.GetCurrentDirectory());
            load_strings();
        }

        #endregion

        #region Private Events

        private void rdbTab_Load(object sender, EventArgs e)
        {
            loadEncodings();
            loadStructs();
            core.ProgressMaxChanged += (o, x) => { ProgressMax = x.Maximum; };
            core.ProgressValueChanged += (o, x) => { ProgressVal = x.Value; };
        }

        private void ts_enc_list_Click(object sender, EventArgs e)
        {
            if (ts_enc_list.SelectedIndex != -1)
                core.SetEncoding(Encodings.GetByName(ts_enc_list.Text));

            lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "Encoding: {0} set.", ts_enc_list.Text);
        }

        private async void ts_struct_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = string.Format(@"{0}\{1}.lua", structsDir, ts_struct_list.Text);
            try
            {
                if (!File.Exists(path))
                {
                    lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, "The structure file could not be found at path:\n\t- {0}", path);
                    return;
                }

                core.LuaPath = path;
                core.Initialize();
            }
            catch (MoonSharp.Interpreter.SyntaxErrorException sEx)
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, "Exception Occured:\n\t- {0}", LuaException.Print(sEx.DecoratedMessage, ts_struct_list.Text));
            }
            catch (Exception ex) { lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, ex); }
            finally
            {
                ts_struct_status.Text = "Loaded";
                tManager.Text = string.Format("<{0}>", ts_struct_list.Text);
                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "Structure file: {0} successfully loaded on Tab: {1}", ts_struct_list.Text, tManager.Text);

                ts_struct_list.Enabled = false;

                await Task.Run(() => { gridUtil.GenerateColumns(); });
            }
        }

        private async void ts_load_sql_Click(object sender, EventArgs e)
        {
            if (!structLoaded)
            {
                MessageBox.Show("You can not do that until a structure has been loaded!", "Structure Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            string tablename = core.TableName;
            if (string.IsNullOrEmpty(tablename))
            {
                DialogResult result = MessageBox.Show("It seems the structure lua does not have a table name listed, would you like to define one?", "Input Required", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Cancel:
                        return;

                    case DialogResult.No:
                        return;

                    case DialogResult.Yes:
                        using (GUI.InputBox input = new GUI.InputBox("Enter desired table name", false))
                        {
                            if (input.ShowDialog(this) != DialogResult.OK) { return; }

                            lManager.Enter(Logs.Sender.RDB, Logs.Level.WARNING, "User opted to provide Table name for load operation.\n\t- Table name provided: {0}", input.Value);
                            tablename = input.Value;
                        }

                        break;
                }
            }

            dManager = new Database();
            int rowCount = dManager.FetchRowCount(tablename);
            Daedalus.Structures.Row[] table_data = new Daedalus.Structures.Row[0];

            try
            {
                await Task.Run(() => { table_data = dManager.FetchTable(rowCount, tablename); });

                if (table_data.Length == 0)
                {
                    lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, "No results were loaded from the table!");
                    return;
                }

                core.SetData(table_data);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.SQL_ERROR, ex);
                return;
            }
            catch (Exception ex)
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, ex);
                return;
            }
            finally
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "{0} rows were loaded from table: {1} into tab: {2}", table_data.Length, tablename, tManager.Text);

                tManager.Text = Path.GetFileNameWithoutExtension(tablename);
                initializeGrid();
            }
        }

        private async void ts_save_sql_Click(object sender, EventArgs e)
        {
            if (!structLoaded)
            {
                MessageBox.Show("You can not do that until a structure has been loaded!", "Structure Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (MessageBox.Show("You are about to save to the SQL Table!\n\nAre you sure you want to do that?", "Input Required", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) != DialogResult.Yes)
                return;

            string tablename = core.TableName;
            if (string.IsNullOrEmpty(tablename))
            {
                DialogResult result = MessageBox.Show("It seems the structure lua does not have a table name listed, would you like to define one?", "Input Required", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Cancel:
                        return;

                    case DialogResult.No:
                        return;

                    case DialogResult.Yes:
                        using (GUI.InputBox input = new GUI.InputBox("Enter desired table name", false))
                        {
                            if (input.ShowDialog(this) != DialogResult.OK) { return; }

                            lManager.Enter(Logs.Sender.RDB, Logs.Level.WARNING, "User opted to provide Table name for save operation.\n\t- Table name provided: {0}", input.Value);
                            tablename = input.Value;
                        }

                        break;
                }
            }

            Database dManager = new Database();

            try
            {
                await Task.Run(() =>
                {
                    dManager.ExportToTable(tablename, tManager.RDBCore.Rows);
                });

                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "{0} rows were saved to table: {1} from tab: {2}", tManager.RDBCore.RowCount, tablename, tManager.Text);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //if (ex.Number == -2) <-- Timeout expired
                lManager.Enter(Logs.Sender.RDB, Logs.Level.SQL_ERROR, ex);
                return;
            }
            catch (Exception ex)
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, ex);
                return;
            }
        }

        private void ts_save_enc_Click(object sender, EventArgs e)
        {
            bool newVal = OPT.GetBool("rdb.save.hashed") ? false : true;
            OPT.Update("rdb.save.hashed", Convert.ToInt32(newVal).ToString());
            ts_save_enc.Checked = newVal;
            lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "Save Encoded: {0} for tab: {1}", (newVal) ? "Enabled" : "Disabled", tManager.Text);
        }

        private void ts_save_w_ascii_Click(object sender, EventArgs e)
        {
            bool newVal = OPT.GetBool("rdb.use.ascii") ? false : true;
            OPT.Update("rdb.use.ascii", Convert.ToInt32(newVal).ToString());
            ts_save_w_ascii.Checked = newVal;
            lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "Save With ASCII: {0} for tab: {1}", (newVal) ? "Enabled" : "Disabled", tManager.Text);
        }

        private void grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Cell cell = core.Rows[0].GetCell(grid.Columns[e.ColumnIndex].Name);
            SortOrder curOrder = grid.Columns[cell.Name].HeaderCell.SortGlyphDirection;
            SortOrder newOrder = SortOrder.None;

            switch (curOrder)
            {
                case SortOrder.None:
                    newOrder = SortOrder.Ascending;
                    break;

                case SortOrder.Ascending:
                    newOrder = SortOrder.Descending;
                    break;

                case SortOrder.Descending:
                    newOrder = SortOrder.Ascending;
                    break;
            }

            if (e.ColumnIndex >= 0)
                foreach (DataGridViewColumn dgvColumn in grid.Columns)
                    dgvColumn.HeaderCell.SortGlyphDirection = SortOrder.None;

            lManager.Enter(Logs.Sender.RDB, Logs.Level.DEBUG, "Sorting data-set by: {0} ({1}) on tab: {2}", cell.Name, newOrder.ToString(), tManager.Text);

            Sort(cell, newOrder);
        }

        private async void ts_save_file_csv_Click(object sender, EventArgs e)
        {
            actionSW.Reset();
            actionSW.Start();

            ts_prog.Maximum = core.RowCount;
            ts_prog.Value = 0;

            string csvStr = string.Empty;

            await Task.Run(() => { csvStr = generateCSV(); });

            actionSW.Stop();

            lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "\t-Ready! ({0}ms)", actionSW.ElapsedMilliseconds.ToString("D4"));

            string csvDir = OPT.GetString("rdb.csv.directory") ?? string.Concat(Directory.GetCurrentDirectory(), @"\CSV");
            string csvPath = string.Format(@"{0}\{1}_{2}.csv", csvDir, core.FileName, DateTime.UtcNow.ToString("MM-dd-yyyy"));

            if (!Directory.Exists(csvDir))
                Directory.CreateDirectory(csvDir);

            File.WriteAllText(csvPath, csvStr.ToString());

            string msg = string.Format("{0} written to .csv file at: {1}", tManager.Text, csvPath);
            lManager.Enter(Logs.Sender.RDB, Logs.Level.DEBUG, msg);

            ts_prog.Value = 0;
            ts_prog.Maximum = 100;

            MessageBox.Show(msg, "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

                Cell c = core.CellTemplate[e.ColumnIndex];

                if (c.Flag == FlagType.BIT_FLAG)
                {
                    bool overColumn = Convert.ToBoolean(grid.HitTest(e.X, e.Y).RowIndex);

                    if (overColumn)
                    {
                        var relativeMousePosition = grid.PointToClient(Cursor.Position);
                        grid_cs.Show(grid, relativeMousePosition);
                    }
                }
            }
        }

        private void grid_cs_open_flag_editor_Click(object sender, EventArgs e)
        {
            int bitflag = grid.SelectedCells[0].Value as int? ?? default(int);
            bool changed = false;

            if (bitflag > 0)
                using (GUI.BitFlag editor = new GUI.BitFlag(bitflag))
                {
                    editor.FormClosing += (o, x) =>
                    {
                        if (bitflag != editor.Flag)
                        {
                            changed = true;
                            bitflag = editor.Flag;
                        }
                    };

                    Cell cell = core.CellTemplate[grid.SelectedCells[0].ColumnIndex];

                    if (cell.ConfigOptions[0] != null)
                        editor.DefaultFlagFile = cell.ConfigOptions[0].ToString();

                    editor.ShowDialog(this);
                }

            if (changed)
                grid.SelectedCells[0].Value = bitflag;
        }

        private void load_strings()
        {
            ts_load.Text = strings.ts_load;
            ts_load_file.Text = strings.ts_load_file;
            ts_load_sql.Text = strings.ts_load_sql;
            ts_save.Text = strings.ts_save;
            ts_save_file.Text = strings.ts_save_file;
            ts_file_save_rdb.Text = strings.ts_file_save_rdb;
            ts_save_file_csv.Text = strings.ts_save_file_csv;
            ts_save_file_sql.Text = strings.ts_save_file_sql;
            ts_save_sql.Text = strings.ts_save_sql;
            ts_encLbl.Text = strings.ts_encLbl;
            ts_structLb.Text = strings.ts_structLb;
            ts_save_enc.Text = strings.ts_save_enc;
            ts_save_w_ascii.Text = strings.ts_save_w_ascii;
        }

        #endregion

        #region Public Events

        public void TS_Load_File_Click(object sender, EventArgs e)
        {
            if (!structLoaded)
            {
                MessageBox.Show("You can not do that until a structure has been loaded!", "Structure Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Paths.DefaultDirectory = OPT.GetString("rdb.load.directory");
            Paths.DefaultFileName = core.FileName;
            string fileName = Paths.FilePath;

            if (Paths.FileResult != DialogResult.OK)
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "User cancelled file load on tab: {0}", tManager.Text);
                return;
            }

            string ext = Path.GetExtension(fileName).ToLower();

            switch (ext)
            {
                case ".rdb":
                    lManager.Enter(Logs.Sender.RDB, Logs.Level.DEBUG, "Loading RDB from physical file with .rdb extension");
                    load_file(fileName);
                    break;

                case ".000":
                    lManager.Enter(Logs.Sender.RDB, Logs.Level.DEBUG, "Loading RDB from Rappelz Client");
                    load_data_file(fileName);
                    break;
            }
        }

        public async void TS_Save_File_Click(object sender, EventArgs e)
        {
            string buildPath = null;

            if (core is null)
            {
                MessageBox.Show("You cannot do that without having loaded data first!", "Save File Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            try
            {
                string filename = core.FileName;
                if (string.IsNullOrEmpty(filename))
                {
                    DialogResult result = MessageBox.Show("It seems the structure lua does not have a filename listed, would you like to define one?\n\nReminder: Choosing no will cause me use this tabs name instead!", "Input Required", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (result)
                    {
                        case DialogResult.Cancel:
                            return;

                        case DialogResult.No:
                            lManager.Enter(Logs.Sender.RDB, Logs.Level.DEBUG, "User opted to use the tab name as Filename for save operation.\n\t- Filename provided: {0}", tManager.Text);
                            filename = string.Format("{0}.rdb", tManager.Text);
                            break;

                        case DialogResult.Yes:
                            using (GUI.InputBox input = new GUI.InputBox("Enter desired filename", false))
                            {
                                if (input.ShowDialog(this) != DialogResult.OK)
                                    return;

                                lManager.Enter(Logs.Sender.RDB, Logs.Level.DEBUG, "User opted to provide Filename for save operation.\n\t- Filename provided: {0}", input.Value);
                                filename = input.Value;
                            }

                            break;
                    }
                }

                if (!filename.Contains("ascii") && ts_save_w_ascii.Checked)
                    filename = string.Format(@"{0}(ascii).rdb", filename.Split('.')[0]);

                if (ts_save_enc.Checked)
                    filename = DataCore.Functions.StringCipher.Encode(filename);

                buildPath = string.Format(@"{0}\{1}", buildDir, filename);

                if (!Directory.Exists(buildDir))
                    Directory.CreateDirectory(buildDir);

                if (File.Exists(buildPath))
                    File.Delete(buildPath);

                await Task.Run(() =>
                {
                    core.RdbPath = buildPath;
                    core.Write();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "RDB Save Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, ex);
            }
            finally
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "{0} entries written from tab: {1} into file: {2}", core.RowCount, tManager.Text, buildPath);
            }
        }

        #endregion

        #region Methods (Public)

        public void ResetProgress()
        {
            this.Invoke(new MethodInvoker(delegate { ts_prog.Maximum = 100; ts_prog.Value = 0; }));
        }

        public void SetColumns(DataGridViewTextBoxColumn[] columns)
        {
            lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "{0} columns set into tab: {1}", columns.Length, tManager.Text);
            this.Invoke(new MethodInvoker(delegate { grid.Columns.AddRange(columns); }));
        }

        public void Clear()
        {
            grid.Rows.Clear();
            grid.RowCount = 0;
        }

        public void LoadFile(string filePath) { load_file(filePath); }

        public void Search(string field, string term)
        {
            if (!structLoaded)
            {
                MessageBox.Show("You can not do that until a structure has been loaded!", "Structure Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (core.RowCount == 0)
            {
                MessageBox.Show("You can not search nothing for something!", "Search Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            for (int r = 0; r < core.RowCount; r++)
            {
                Row row = core.Rows[r];

                for (int c = 0; c < row.Length; c++)
                {
                    Cell cell = row.GetCell(c);

                    if (cell.Name == field && cell.Value.ToString() == term)
                    {
                        grid.CurrentCell = grid.Rows[r].Cells[c];
                        grid.Rows[r].Selected = true;
                    }
                }
            }
        }

        public void Sort(Cell cell, SortOrder order)
        {
            switch (cell.Type)
            {
                case CellType.TYPE_SHORT:
                case CellType.TYPE_INT_16:
                case CellType.TYPE_INT:
                case CellType.TYPE_INT_32:

                    if (order == SortOrder.Ascending)
                        Array.Sort(core.Rows, (a, b) => ((int)a[cell.Name]).CompareTo((int)b[cell.Name]));
                    else
                        Array.Sort(core.Rows, (a, b) => ((int)b[cell.Name]).CompareTo((int)a[cell.Name]));

                    break;

                case CellType.TYPE_FLOAT:
                case CellType.TYPE_FLOAT_32:
                case CellType.TYPE_SINGLE:

                    if (order == SortOrder.Ascending)
                        Array.Sort(core.Rows, (a, b) => ((float)a[cell.Name]).CompareTo((float)b[cell.Name]));
                    else
                        Array.Sort(core.Rows, (a, b) => ((float)b[cell.Name]).CompareTo((float)a[cell.Name]));

                    break;

                case CellType.TYPE_DOUBLE:
                case CellType.TYPE_INT_64:
                case CellType.TYPE_LONG:

                    if (order == SortOrder.Ascending)
                        Array.Sort(core.Rows, (a, b) => ((long)a[cell.Name]).CompareTo((long)b[cell.Name]));
                    else
                        Array.Sort(core.Rows, (a, b) => ((long)b[cell.Name]).CompareTo((long)a[cell.Name]));

                    break;

                case CellType.TYPE_STRING:

                    if (order == SortOrder.Ascending)
                        Array.Sort(core.Rows, (a, b) => a[cell.Name].ToString().CompareTo(b[cell.Name].ToString()));
                    else
                        Array.Sort(core.Rows, (a, b) => b[cell.Name].ToString().CompareTo(a[cell.Name].ToString()));

                    break;
            }

            grid.Columns[cell.Name].HeaderCell.SortGlyphDirection = order;

            grid.Rows.Clear();
            initializeGrid();
        }

        #endregion

        #region Methods (private)

        void loadEncodings()
        {
            ts_enc_list.Items.AddRange(Encodings.Names);
            ts_enc_list.SelectedIndex = 0;
            lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "{0} Encodings loaded.", Encodings.Count);
        }

        void loadStructs()
        {
            if (!Directory.Exists(structsDir))
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, "The structures directory does not exist!\n\t- Directory: {0}", structsDir);
                return;
            }
            else
            {
                string[] structs = Directory.GetFiles(structsDir);
                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "{0} Structure files loaded from:\n\t- {1}", structs.Length, structsDir);

                foreach (string filename in structs)
                {
                    string name = Path.GetFileNameWithoutExtension(filename);
                    ts_struct_list.Items.Add(name);
                }
            }
        }

        private void load_file(string filePath)
        {
            core.RdbPath = filePath;
            string fileName = Path.GetFileName(filePath);
            byte[] buffer = File.ReadAllBytes(filePath);
            load_file(fileName, buffer);
        }

        private async void load_file(string fileName, byte[] fileBytes)
        {
            try
            {
                actionSW.Reset();
                actionSW.Start();

                await Task.Run(() => { core.ParseBuffer(fileBytes); });
            }
            catch (Exception ex)
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, ex);
                MessageBox.Show(ex.Message, "RDB Load Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                actionSW.Stop();

                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "{0} entries loaded from: {1} ({2}) in {3}ms", core.RowCount, fileName, StringExt.FormatToSize(fileBytes.Length), actionSW.ElapsedMilliseconds.ToString("D4"));
                tManager.SetText(key, fileName);
                initializeGrid();
            }
        }

        private async void load_data_file(string filePath)
        {
            dCore = new DataCore.Core(Encodings.GetByName(ts_enc_list.Text));

            lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "RDB Tab: {0} attempting load file selection from index at path:\n\t- {1}", tManager.Text, filePath);

            List<DataCore.Structures.IndexEntry> results = null;

            actionSW.Reset();
            actionSW.Start();

            await Task.Run(() =>
            {
                dCore.Load(filePath);
                results = dCore.GetEntriesByExtensions("rdb", "ref");
            });

            lManager.Enter(Logs.Sender.RDB, Logs.Level.DEBUG, "File list retrived successfully! ({0}ms)\n\t- Selections available: {1}",
                                                                                            actionSW.ElapsedMilliseconds.ToString("D4"),
                                                                                                                         results.Count);

            using (Grimoire.GUI.ListSelect selectGUI = new GUI.ListSelect("Select RDB", results))
            {
                selectGUI.ShowDialog(Grimoire.GUI.Main.Instance);

                if (selectGUI.DialogResult == DialogResult.OK)
                {
                    string fileName = selectGUI.SelectedText;
                    DataCore.Structures.IndexEntry finalResult = results.Find(i => i.Name == fileName);
                    byte[] fileBytes = dCore.GetFileBytes(finalResult);

                    lManager.Enter(Logs.Sender.RDB, Logs.Level.DEBUG, "User selected rdb: {0}", fileName);

                    if (fileBytes.Length > 0)
                        load_file(fileName, fileBytes);
                }
                else
                {
                    lManager.Enter(Logs.Sender.RDB, Logs.Level.DEBUG, "User cancelled Data load on RDB Tab: {0}", tManager.Text);
                    return;
                }
            }
        }

        private void initializeGrid()
        {
            grid.VirtualMode = true;
            grid.CellValueNeeded += gridUtil.Grid_CellValueNeeded;
            grid.CellValuePushed += gridUtil.Grid_CellPushed;
            grid.RowCount = tManager.RDBCore.RowCount + 1; //core.Data.Count + 1;
        }

        private string generateCSV()
        {
            StringBuilder sb = new StringBuilder();

            string[] names = core.VisibleCellNames;
            sb.Append(string.Join(",", names));

            lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "Preparing to export {0} columns and {1} rows to CSV", names.Length, core.RowCount);

            for (int r = 0; r < core.RowCount; r++)
            {
                Row row = core[r];

                string valStr = null;

                int len = row.VisibleLength;
                Cell[] cells = row.VisibleCells;

                for (int c = 0; c < len; c++)
                {
                    if (c == len)
                        break;
                    else if (c == 0)
                        valStr = string.Concat(valStr, "\n");
                    else if (c > 0)
                        valStr = string.Concat(valStr, ",");

                    string cVal = cells[c].Value.ToString();
                    if (cVal.Contains(","))
                        cVal = string.Format("\"{0}\"", cVal);

                    valStr = string.Concat(valStr, cVal);
                }

                sb.Append(valStr);

                if ((r * 100 / core.RowCount) != ((r - 1) * 100 / core.RowCount))
                    GUI.Main.Instance.Invoke(new MethodInvoker(delegate { ts_prog.Value = r; }));
            }

            if (sb.Length == 0)
            {
                string msg = "generateCSV failed to generate a csv string!";

                lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, msg);
                MessageBox.Show(msg, "CSV Generate Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            return sb.ToString();
        }

        #endregion
    }
}