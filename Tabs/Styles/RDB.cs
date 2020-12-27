using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text;
using Grimoire.Utilities;
using Grimoire.DB;
using Daedalus;
using Daedalus.Structures;
using Daedalus.Enums;
using Grimoire.Logs.Enums;
using Grimoire.Configuration;
using Grimoire.DB.Enums;

namespace Grimoire.Tabs.Styles
{
    //TODO: Correct encoding
    public partial class rdbTab : UserControl
    {
        #region Properties

        GUI.Main main = GUI.Main.Instance;
        Logs.Manager lManager = Logs.Manager.Instance;
        Manager tManager = Manager.Instance;
        ConfigMan configMan = GUI.Main.Instance.ConfigMan;

        public Core core = new Core();
        DataCore.Core dCore = null;
        string structsDir = null;
        readonly string key = null;
        readonly Tabs.Utilities.Grid gridUtil;
        readonly Stopwatch actionSW = new Stopwatch();
        XmlManager xMan = XmlManager.Instance;

        bool structLoaded { get { return (ts_struct_list.SelectedIndex != -1); } }
        readonly string buildDir = string.Empty;

        public Core Core
        {
            get
            {
                if (core == null)
                    throw new Exception("Daedalus is null!");

                return core;
            }
        }

        public DataGridView Grid { get { return grid; } }

        public int ProgressMax
        {
            set => Invoke(new MethodInvoker(delegate { ts_prog.Maximum = value; }));
        }

        public int ProgressVal
        {
            set => Invoke(new MethodInvoker(delegate { ts_prog.Value = value; }));
        }

        public string Status { get => ts_struct_status.Text; set => ts_struct_status.Text = value; }

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
            buildDir = configMan.GetDirectory("BuildDirectory", "Grim");
            ts_save_enc.Checked = configMan["SaveHashed", "RDB"];
            ts_save_w_ascii.Checked = configMan["AppendASCII"];
            structsDir = configMan.GetDirectory("Directory", "RDB");
            localize();
        }

        #endregion

        #region Private Events

        private void rdbTab_Load(object sender, EventArgs e)
        {           
            loadEncodings();
            loadStructs();
            setChecks();
            core.ProgressMaxChanged += (o, x) => { ProgressMax = x.Maximum; };
            core.ProgressValueChanged += (o, x) => { ProgressVal = x.Value; };
            core.MessageOccured += (o, x) =>
            {
                lManager.Enter(Sender.RDB, Level.WARNING, x.Message);
            };
        }

        private void setChecks()
        {
            ts_save_w_ascii.Checked = configMan["AppendASCII", "RDB"];
            ts_save_enc.Checked = configMan["SaveHashed", "RDB"];
        }

        private void ts_enc_list_Click(object sender, EventArgs e)
        {
            if (ts_enc_list.SelectedIndex != -1)
                core.SetEncoding(Encodings.GetByName(ts_enc_list.Text));

            lManager.Enter(Sender.RDB, Level.NOTICE, "Encoding: {0} set.", ts_enc_list.Text);
        }

        private void ts_enc_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            core.SetEncoding(Encodings.GetByName(ts_enc_list.Text));

            lManager.Enter(Sender.RDB, Level.NOTICE, "Encoding: {0} set for tab: {1}", ts_enc_list.Text, tManager.Text);
        }

        private async void ts_struct_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = string.Format(@"{0}\{1}.lua", structsDir, ts_struct_list.Text);
            try
            {
                if (!File.Exists(path))
                {
                    lManager.Enter(Sender.RDB, Level.ERROR, "The structure file could not be found at path:\n\t- {0}", path);
                    return;
                }

                core.Initialize(path);
            }
            catch (MoonSharp.Interpreter.SyntaxErrorException sEx)
            {
                lManager.Enter(Sender.RDB, Level.ERROR, "Exception Occured:\n\t- {0}", LuaException.Print(sEx.DecoratedMessage, ts_struct_list.Text));
            }
            catch (Exception ex) { lManager.Enter(Sender.RDB, Level.ERROR, ex); }
            finally
            {
                ts_struct_status.Text = "Loaded";
                tManager.Text = string.Format("<{0}>", ts_struct_list.Text);
                lManager.Enter(Sender.RDB, Level.NOTICE, "Structure file: {0} successfully loaded on Tab: {1}", ts_struct_list.Text, tManager.Text);

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

                            lManager.Enter(Sender.RDB, Level.WARNING, "User opted to provide Table name for load operation.\n\t- Table name provided: {0}", input.Value);
                            tablename = input.Value;
                        }

                        break;
                }
            }

            DbConType sqlEngine = (DbConType)configMan["Engine", "DB"];
            DBHelper dbHelper = new DBHelper(configMan, sqlEngine);
            Row[] table_data = await dbHelper.ReadTable(tablename);

            if (table_data.Length == 0)
            {
                lManager.Enter(Sender.RDB, Level.ERROR, "No results were loaded from the table!");
                return;
            }

            core.SetData(table_data);

            lManager.Enter(Sender.RDB, Level.NOTICE, "{0} rows were loaded from table: {1} into tab: {2}", table_data.Length, tablename, tManager.Text);

            tManager.Text = Path.GetFileNameWithoutExtension(tablename);
            initializeGrid();
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

                            lManager.Enter(Sender.RDB, Level.WARNING, "User opted to provide Table name for save operation.\n\t- Table name provided: {0}", input.Value);
                            tablename = input.Value;
                        }

                        break;
                }
            }

            DbConType sqlEngine = (DbConType)configMan["Engine", "DB"];
            DBHelper dbHelper = new DBHelper(configMan, sqlEngine);
            await dbHelper.WriteTable(tablename, tManager.RDBCore.Rows);

            lManager.Enter(Sender.RDB, Level.NOTICE, "{0} rows were saved to table: {1} from tab: {2}", tManager.RDBCore.RowCount, tablename, tManager.Text);

            MessageBox.Show("Database export successful!", "Export Successful!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void ts_save_file_sql_Click(object sender, EventArgs e)
        {

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

            lManager.Enter(Sender.RDB, Level.DEBUG, "Sorting data-set by: {0} ({1}) on tab: {2}", cell.Name, newOrder.ToString(), tManager.Text);

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

            lManager.Enter(Sender.RDB, Level.NOTICE, "\t-Ready! ({0}ms)", actionSW.ElapsedMilliseconds.ToString("D4"));

            string csvDir = configMan.GetDirectory("CSV_Directory");
            string csvPath = string.Format(@"{0}\{1}_{2}.csv", csvDir, core.FileName, DateTime.UtcNow.ToString("MM-dd-yyyy"));

            if (!Directory.Exists(csvDir))
                Directory.CreateDirectory(csvDir);

            File.WriteAllText(csvPath, csvStr.ToString());

            string msg = string.Format("{0} written to .csv file at: {1}", tManager.Text, csvPath);
            lManager.Enter(Sender.RDB, Level.DEBUG, msg);

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
            int bitflag = Convert.ToInt32(grid.SelectedCells[0].Value);
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

        private async void ts_save_enc_Click(object sender, EventArgs e)
        {
            bool flip = (ts_save_enc.Checked) ? false : true;
            ts_save_enc.Checked = flip;
            configMan["SaveHashed"] = flip;
            await configMan.Save();
        }

        private async void ts_save_w_ascii_Click(object sender, EventArgs e)
        {
            bool flip = (ts_save_w_ascii.Checked) ? false : true;
            ts_save_w_ascii.Checked = flip;
            configMan["AppendASCII"] = flip;
            await configMan.Save();
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

            Paths.DefaultDirectory = configMan["LoadDirectory"];
            Paths.DefaultFileName = core.FileName;
            string fileName = Paths.FilePath;

            if (Paths.FileResult != DialogResult.OK)
            {
                lManager.Enter(Sender.RDB, Level.DEBUG, "User cancelled file load on tab: {0}", tManager.Text);
                return;
            }

            string ext = Path.GetExtension(fileName).ToLower();

            switch (ext)
            {
                case ".rdb":
                    lManager.Enter(Sender.RDB, Level.DEBUG, "Loading RDB from physical file with .rdb extension");
                    load_file(fileName);
                    break;

                case ".000":
                    lManager.Enter(Sender.RDB, Level.DEBUG, "Loading RDB from Rappelz Client");
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
                    DialogResult result = MessageBox.Show("It seems the structure lua does not have a filename listed, would you like to define one?\n\nReminder: Choosing no will cause me to use this tabs name instead!",
                        "Input Required", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (result)
                    {
                        case DialogResult.Cancel:
                            return;

                        case DialogResult.No:
                            lManager.Enter(Sender.RDB, Level.DEBUG, "User opted to use the tab name as Filename for save operation.\n\t- Filename provided: {0}", tManager.Text);
                            filename = string.Format("{0}.rdb", tManager.Text);
                            break;

                        case DialogResult.Yes:
                            using (GUI.InputBox input = new GUI.InputBox("Enter desired filename", false))
                            {
                                if (input.ShowDialog(this) != DialogResult.OK)
                                    return;

                                lManager.Enter(Sender.RDB, Level.DEBUG, "User opted to provide Filename for save operation.\n\t- Filename provided: {0}", input.Value);
                                filename = input.Value;
                            }

                            break;
                    }
                }

                if (!filename.Contains("ascii") && ts_save_w_ascii.Checked)
                    filename = string.Format(@"{0}(ascii).rdb", filename.Split('.')[0]);
                else if (filename.Contains("ascii") && !ts_save_w_ascii.Checked)
                    filename = filename.Remove(filename.Length - 11, 7);

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
                lManager.Enter(Sender.RDB, Level.ERROR, ex);
            }
            finally
            {
                lManager.Enter(Sender.RDB, Level.NOTICE, "{0} entries written from tab: {1} into file: {2}", core.RowCount, tManager.Text, buildPath);
            }
        }

        #endregion

        #region Methods (Public)

        public void ResetProgress() => Invoke(new MethodInvoker(delegate {
            ts_prog.Maximum = 100;
            ts_prog.Value = 0; }));

        public void SetColumns(DataGridViewTextBoxColumn[] columns)
        {         
            this.Invoke(new MethodInvoker(delegate { grid.Columns.AddRange(columns); }));
            lManager.Enter(Sender.RDB, Level.NOTICE, "{0} columns set into tab: {1}", columns.Length, tManager.Text);
        }

        public void Clear()
        {       
            grid.Rows.Clear();
            grid.RowCount = 0;
            core.ClearData();
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

        public void Localize() { localize(); }

        #endregion

        #region Methods (private)

        void localize() => xMan.Localize(this, Localization.Enums.SenderType.Tab);

        void loadEncodings()
        {
            ts_enc_list.Items.AddRange(Encodings.Names);

            string encStr = configMan["Encoding", "RDB"];

            ts_enc_list.SelectedIndex = (encStr != null) ? Encodings.GetIndex(encStr) : 0;

            lManager.Enter(Sender.RDB, Level.NOTICE, "{0} Encodings loaded.", Encodings.Count);
        }

        void loadStructs()
        {
            if (!Directory.Exists(structsDir))
            {
                lManager.Enter(Sender.RDB, Level.ERROR, "The structures directory does not exist!\n\t- Directory: {0}", structsDir);
                return;
            }
            else
            {
                string[] structs = Directory.GetFiles(structsDir);
                lManager.Enter(Sender.RDB, Level.NOTICE, "{0} Structure files loaded from:\n\t- {1}", structs.Length, structsDir);

                foreach (string filename in structs)
                {
                    string name = Path.GetFileNameWithoutExtension(filename);
                    ts_struct_list.Items.Add(name);
                }
            }
        }

        void load_file(string filePath)
        {
            core.RdbPath = filePath;
            string fileName = Path.GetFileName(filePath);
            byte[] buffer = File.ReadAllBytes(filePath);
            load_file(fileName, buffer);
        }

        async void load_file(string fileName, byte[] fileBytes)
        {
            try
            {
                actionSW.Reset();
                actionSW.Start();

                await Task.Run(() => { core.ParseBuffer(fileBytes); });
            }
            catch (Exception ex)
            {
                lManager.Enter(Sender.RDB, Level.ERROR, ex);
                MessageBox.Show(ex.Message, "RDB Load Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                actionSW.Stop();

                lManager.Enter(Sender.RDB, Level.NOTICE, "{0} entries loaded from: {1} ({2}) in {3}ms", core.RowCount, fileName, StringExt.FormatToSize(fileBytes.Length), actionSW.ElapsedMilliseconds.ToString("D4"));
                tManager.SetText(key, fileName);
                initializeGrid();
            }
        }

        async void load_data_file(string filePath)
        {
            dCore = new DataCore.Core(Encodings.GetByName(ts_enc_list.Text));

            dCore.UseModifiedXOR = configMan["UseModifiedXOR", "Data"];
            if (dCore.UseModifiedXOR)
                dCore.SetXORKey(configMan.GetByteArray("ModifiedXORKey"));

            lManager.Enter(Sender.RDB, Level.NOTICE, "RDB Tab: {0} attempting load file selection from index at path:\n\t- {1}", tManager.Text, filePath);

            List<DataCore.Structures.IndexEntry> results = null;

            actionSW.Reset();
            actionSW.Start();

            await Task.Run(() =>
            {
                dCore.Load(filePath);
                results = dCore.GetEntriesByExtensions("rdb", "ref");
            });

            lManager.Enter(Sender.RDB, Level.DEBUG, "File list retrived successfully! ({0}ms)\n\t- Selections available: {1}",
                                                                                            actionSW.ElapsedMilliseconds.ToString("D4"),
                                                                                                                         results.Count);

            using (Grimoire.GUI.ListSelect selectGUI = new GUI.ListSelect("Select RDB", results, core.FileName))
            {
                selectGUI.ShowDialog(Grimoire.GUI.Main.Instance);

                if (selectGUI.DialogResult == DialogResult.OK)
                {
                    string fileName = selectGUI.SelectedText;
                    DataCore.Structures.IndexEntry finalResult = results.Find(i => i.Name == fileName);
                    byte[] fileBytes = dCore.GetFileBytes(finalResult);

                    lManager.Enter(Sender.RDB, Level.DEBUG, "User selected rdb: {0}", fileName);

                    if (fileBytes.Length > 0)
                        load_file(fileName, fileBytes);
                }
                else
                {
                    lManager.Enter(Sender.RDB, Level.DEBUG, "User cancelled Data load on RDB Tab: {0}", tManager.Text);
                    return;
                }
            }
        }

        void initializeGrid()
        {
            grid.VirtualMode = true;
            grid.CellValueNeeded += gridUtil.Grid_CellValueNeeded;
            grid.CellValuePushed += gridUtil.Grid_CellPushed;
            grid.RowCount = tManager.RDBCore.RowCount + 1; //core.Data.Count + 1;
        }

        string generateCSV()
        {
            StringBuilder sb = new StringBuilder();

            string[] names = core.VisibleCellNames;
            sb.Append(string.Join(",", names));

            lManager.Enter(Sender.RDB, Level.NOTICE, "Preparing to export {0} columns and {1} rows to CSV", names.Length, core.RowCount);

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

                lManager.Enter(Sender.RDB, Level.ERROR, msg);
                MessageBox.Show(msg, "CSV Generate Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            return sb.ToString();
        }

        #endregion

    }
}