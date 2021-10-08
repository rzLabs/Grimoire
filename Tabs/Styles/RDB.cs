using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using Grimoire.Utilities;
using Grimoire.DB;
using Daedalus;
using Daedalus.Structures;
using Daedalus.Enums;
using Grimoire.Configuration;
using Grimoire.DB.Enums;

using Serilog;

namespace Grimoire.Tabs.Styles
{
    public partial class rdbTab : UserControl
    {
        #region Properties

        GUI.Main main = GUI.Main.Instance;
        Manager tManager = Manager.Instance;
        ConfigManager configMan = GUI.Main.Instance.ConfigMan;

        public Core core = new Core();
        DataCore.Core dCore = null;
        string structsDir = null;
        readonly string key = null;
        readonly Tabs.Utilities.Grid gridUtil;
        readonly Stopwatch actionSW = new Stopwatch();
        XmlManager xMan = XmlManager.Instance;
        DBHelper db = null;

        bool structLoaded { get { return (ts_struct_list.SelectedIndex != -1); } }
        string buildDir = string.Empty;

        public Core Core
        {
            get
            {
                if (core == null)
                    throw new Exception("Daedalus is null!");

                return core;
            }
        }

        public DataGridView Grid => grid;

        public int ProgressMax
        {
            set => Invoke(new MethodInvoker(delegate { 
                ts_prog.Maximum = value;
            }));
        }

        public int ProgressVal
        {
            set => Invoke(new MethodInvoker(delegate {
                ts_prog.Value = value;
            }));
        }

        public string Status
        {
            set => Invoke(new MethodInvoker(delegate { 
                statusLb.Text = value;
            }));
        }

        public bool UseASCII
        {
            get { return ts_save_w_ascii.Checked; }
            set { ts_save_w_ascii.Checked = value; }
        }

        public bool SaveEncrypted
        {
            get => ts_save_enc.Checked;
            set => ts_save_enc.Checked = value;
        }

        public string BuildDirectory
        {
            get => buildDir;
            set => buildDir = value;
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
            configureDB();
            configureCore();

        }

        private void configureDB()
        {
            DbConType sqlEngine = (DbConType)configMan["Engine", "DB"];
            db = new DBHelper(configMan, sqlEngine);

            db.ProgressMaxSet += (o, x) =>
            {
                ProgressMax = x.Maximum;

                if (x.Message != null)
                    Status = x.Message;
            };

            db.ProgressValueSet += (o, x) =>
            {
                ProgressVal = x.Value;

                if (x.Message != null)
                    Status = x.Message;
            };

            db.ProgressReset += (o, x) =>
            {
                ProgressMax = 100;
                ProgressVal = 0;
                statusLb.ResetText();
            };

            db.Message += (o, x) => Status = x.Message;

            db.Error += (o, x) =>
            {
                Log.Error(x.Message);

                MessageBox.Show(x.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }

        private void configureCore()
        {
            core.ProgressMaxChanged += (o, x) => { ProgressMax = x.Maximum; };
            core.ProgressValueChanged += (o, x) => { ProgressVal = x.Value; };
            core.MessageOccured += (o, x) =>
            {
                Log.Information(x.Message);

                Status = x.Message;
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

            Log.Information($"Encoding: {ts_enc_list.Text} set.");
        }

        private void ts_enc_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            core.SetEncoding(Encodings.GetByName(ts_enc_list.Text));

            Log.Information($"Encoding: {ts_enc_list.Text} set.");
        }

        private async void ts_struct_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = string.Format(@"{0}\{1}.lua", structsDir, ts_struct_list.Text);
            try
            {
                if (!File.Exists(path))
                {
                    Log.Error($"The structure file could not be found at path:\n\t- {path}");
                    return;
                }

                core.Initialize(path);

                ts_struct_status.Text = "Loaded";
                tManager.Text = string.Format("<{0}>", ts_struct_list.Text);

                Log.Information($"Structure file: {ts_struct_list.Text} successfully loaded on Tab: {tManager.Text}");


                ts_struct_list.Enabled = false;

                await Task.Run(() => { gridUtil.GenerateColumns(); });
            }
            catch (MoonSharp.Interpreter.SyntaxErrorException sEx) {
                Log.Error($"Exception Occured:\n\t- {LuaException.Print(sEx.DecoratedMessage, ts_struct_list.Text)}");
            }
            catch (Exception ex) { 
                Log.Error($"Exception Occured:\nMessage:\n\t{ex.Message}\nStack-Trace:\n\t{ex.StackTrace}");
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
                            if (input.ShowDialog(this) != DialogResult.OK)
                                return;

                            tablename = input.Value;
                        }

                        break;
                }
            }

            Row[] table_data = await db.ReadTable(tablename);

            if (table_data.Length == 0)
            {
                Log.Error("No results were read from the database table!");

                return;
            }

            core.SetData(table_data);

            Log.Information($"{table_data.Length} rows loaded from {tablename} into {tManager.Text}");

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
                            if (input.ShowDialog(this) != DialogResult.OK)
                                return;

                            tablename = input.Value;
                        }

                        break;
                }
            }
            
            await db.WriteTable(tablename, tManager.RDBCore.Rows);

            Log.Information($"{tManager.RDBCore.RowCount} rows saved to {tablename} from {tManager.Text}");

            MessageBox.Show("Database export successful!", "Export Successful!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void ts_save_file_sql_Click(object sender, EventArgs e)
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
                            if (input.ShowDialog(this) != DialogResult.OK)
                                return;

                            tablename = input.Value;
                        }

                        break;
                }
            }

            string[] statements = new string[core.RowCount];

            statusLb.Text = "Preparing SQL statements...";
            ts_prog.Maximum = core.RowCount;
            ts_prog.Value = 0;

            for (int r = 0; r < core.RowCount; r++)
            {
                statements[r] = core.Rows[r].ToSQLString(tablename);

                if ((r * 100 / core.RowCount) != ((r - 1) * 100 / core.RowCount))
                    GUI.Main.Instance.Invoke(new MethodInvoker(delegate { ts_prog.Value = r; }));
            }

            ts_prog.Maximum = 100;
            ts_prog.Value = 0;

            string scriptsDir = configMan.GetDirectory("ScriptsDirectory", "DB");
            string scriptPath = $"{scriptsDir}\\{tablename}.sql";

            if (!Directory.Exists(scriptsDir))
                Directory.CreateDirectory(scriptsDir);

            statusLb.Text = "Writing statements to disk...";

            Log.Debug($"Writing {statements.Length} statements to disk...");

            File.WriteAllLines(scriptPath, statements);

            statusLb.ResetText();

            string msg = string.Format("{0} written to disk at\n\t- {1}", tManager.Text, scriptPath);

            Log.Information(msg);

            MessageBox.Show(msg, "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            Log.Information($"Sorting data-set by: {cell.Name} ({newOrder.ToString()}) on tab: {tManager.Text}");

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

            Log.Information($"\t- Ready! ({StringExt.MilisecondsToString(actionSW.ElapsedMilliseconds)})");

            string csvDir = configMan.GetDirectory("CSV_Directory");
            string csvPath = string.Format(@"{0}\{1}_{2}.csv", csvDir, core.FileName, DateTime.UtcNow.ToString("MM-dd-yyyy"));

            if (!Directory.Exists(csvDir))
                Directory.CreateDirectory(csvDir);

            File.WriteAllText(csvPath, csvStr.ToString());

            string msg = string.Format("{0} written to .csv file at:\n\t- {1}", tManager.Text, csvPath);

            Log.Information(msg);

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

                switch (c.Flag)
                {
                    case FlagType.BIT_FLAG:
                    case FlagType.ENUM:
                        {
                            bool overColumn = Convert.ToBoolean(grid.HitTest(e.X, e.Y).RowIndex);

                            if (overColumn)
                            {
                                var relativeMousePosition = grid.PointToClient(Cursor.Position);
                                grid_cs.Show(grid, relativeMousePosition);
                            }
                        }
                        break;
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

        private void grid_cs_open_enum_editor_Click(object sender, EventArgs e)
        {
            int selRowIdx = grid.SelectedCells[0].RowIndex;
            int selColIdx = grid.SelectedCells[0].ColumnIndex;
            int enumVal = Convert.ToInt32(tManager.RDBCore[selRowIdx][selColIdx]);
            bool changed = false;

            Cell cell = core.CellTemplate[grid.SelectedCells[0].ColumnIndex];

            if (cell.ConfigOptions[0] == null)
            {
                string msg = $"Cell: {cell.Name} defines flag=ENUM but does not define required enum list!";

                Log.Error(msg);

                MessageBox.Show(msg, "Exception Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            string enumName = cell.ConfigOptions[0].ToString();
            Dictionary<string, int> enumDict = tManager.RDBCore.GetEnum(enumName);

            using (GUI.ListSelect editor = new GUI.ListSelect("Make a selection", enumDict))
            {
                editor.FormClosing += (o, x) =>
                {
                    if (editor.SelectedIndex != enumVal)
                    {
                        changed = true;
                        enumVal = editor.SelectedIndex;

                        if (changed)
                            grid.SelectedCells[0].Value = editor.SelectedValue;
                    }
                };

                editor.SelectedValue = enumVal;

                editor.ShowDialog(this);
            }
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
                Log.Information($"User cancelled load on tab: {tManager.Text}");

                return;
            }

            try
            {

                string ext = Path.GetExtension(fileName).ToLower();

                switch (ext)
                {
                    case ".rdb":
                        Log.Information("Loading RDB from physical file with .rdb extension");
                        load_file(fileName);
                        break;

                    case ".000":
                        Log.Information("Loading RDB from Rappelz Client");
                        load_data_file(fileName);
                        break;
                }
            }
            catch (Exception ex)
            {
                string msg = $"An exception has occured loading {Path.GetFileName(fileName)}\n\nMessage: {ex.Message}\n\nStack-Trace: {ex.StackTrace}";

                Log.Error(msg);

                MessageBox.Show(msg, "RDB Load Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                                                           
                            filename = string.Format("{0}.rdb", tManager.Text);
                            break;

                        case DialogResult.Yes:
                            using (GUI.InputBox input = new GUI.InputBox("Enter desired filename", false))
                            {
                                if (input.ShowDialog(this) != DialogResult.OK)
                                    return;

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

                Log.Information($"{core.RowCount} entries written from {tManager.Text} into:\n\t- {buildPath}");
            }
            catch (Exception ex)
            {
                Log.Error($"An Exception has occured:\nMessage:\n\t{ex.Message}\nStack-Trace:\n\t{ex.StackTrace}");

                MessageBox.Show(ex.Message, "RDB Save Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Methods (Public)

        public void ResetProgress() => Invoke(new MethodInvoker(delegate {
            ts_prog.Maximum = 100;
            ts_prog.Value = 0; 
        }));

        public void SetColumns(DataGridViewTextBoxColumn[] columns)
        {         
            this.Invoke(new MethodInvoker(delegate { 
                grid.Columns.AddRange(columns);
            }));

            Log.Information($"{columns.Length} columns set into {tManager.Text} display grid.");
        }

        public void Clear()
        {       
            grid.Rows.Clear();
            grid.RowCount = 0;
            core.ClearData();
        }

        public void LoadFile(string filePath) => load_file(filePath);

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

        public void Localize() => localize();

        #endregion

        #region Methods (private)

        void localize() => xMan.Localize(this, Localization.Enums.SenderType.Tab);

        void loadEncodings()
        {
            ts_enc_list.Items.AddRange(Encodings.Names);

            string encStr = configMan["Encoding", "RDB"];

            int encIdx = Encodings.GetIndex(encStr);

            if (encIdx == -1)
            {
                string msg = $"{encStr} is not a valid encryption string!";

                Log.Error(msg);

                MessageBox.Show(msg, "Encoding Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            ts_enc_list.SelectedIndex = (encStr != null) ? Encodings.GetIndex(encStr) : 0;

            Log.Information($"{Encodings.Count} Encodings loaded.");
        }

        void loadStructs()
        {
            if (!Directory.Exists(structsDir))
            {
                Log.Error($"The structures directory does not exist!\n\t- Directory: {structsDir}");
                return;
            }

            string[] structs = Directory.GetFiles(structsDir);

            Log.Information($"{structs.Length} Structure files loaded from:\n\t- {structsDir}");

            foreach (string filename in structs)
            {
                string name = Path.GetFileNameWithoutExtension(filename);
                ts_struct_list.Items.Add(name);
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

                await Task.Run(() => { 
                    core.ParseBuffer(fileBytes);
                });

                Log.Information($"{core.RowCount} entries loaded from: {fileName} ({StringExt.SizeToString(fileBytes.Length)}) in {StringExt.MilisecondsToString(actionSW.ElapsedMilliseconds)}");
                
                tManager.SetText(key, fileName);
                initializeGrid();
            }
            catch (Exception ex)
            {
                Log.Error($"An exception has occured:\nMessage:\n\t{ex.Message}\nStack-Trace:\n\t{ex.StackTrace}");

                MessageBox.Show(ex.Message, "RDB Load Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                actionSW.Stop();
            }
        }

        async void load_data_file(string filePath)
        {
            dCore = new DataCore.Core(Encodings.GetByName(ts_enc_list.Text));

            dCore.UseModifiedXOR = configMan["UseModifiedXOR", "Data"];

            if (dCore.UseModifiedXOR)
            {
                byte[] modifiedKey = configMan.GetByteArray("ModifiedXORKey");

                if (modifiedKey == null || modifiedKey.Length != 256)
                {
                    Log.Fatal("Invalid XOR Key!");
                    return;
                }

                dCore.SetXORKey(modifiedKey);

                if (!dCore.ValidXOR)
                {
                    string msg = "The provided ModifiedXORKey is invalid!";

                    Log.Error(msg);

                    MessageBox.Show(msg, "XOR Key Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                Log.Information($"Using modified xor key:\n");

                if (GUI.Main.Instance.LogLevel.MinimumLevel <= Serilog.Events.LogEventLevel.Debug)
                    Log.Debug($"\n{StringExt.ByteArrayToString(modifiedKey)}");
            }

            Log.Information($"{tManager.Text} attempting to load file selected from index at\n\t- {filePath}");

            List<DataCore.Structures.IndexEntry> results = null;

            actionSW.Reset();
            actionSW.Start();

            await Task.Run(() =>
            {
                dCore.Load(filePath);
                results = dCore.GetEntriesByExtensions("rdb", "ref");
            });

            if (results.Count == 0)
            {
                string msg = "No results returned for extension search: rdb, ref!";

                Log.Error(msg);

                MessageBox.Show(msg, "Data Load Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            Log.Information($"File List retreived successfully! ({StringExt.MilisecondsToString(actionSW.ElapsedMilliseconds)})\n\t- Selections available: {results.Count}");

            using (Grimoire.GUI.ListSelect selectGUI = new GUI.ListSelect("Select RDB", results, core.FileName))
            {
                selectGUI.ShowDialog(Grimoire.GUI.Main.Instance);

                if (selectGUI.DialogResult == DialogResult.OK)
                {
                    string fileName = selectGUI.SelectedText;
                    DataCore.Structures.IndexEntry finalResult = results.Find(i => i.Name == fileName);
                    byte[] fileBytes = dCore.GetFileBytes(finalResult);

                    Log.Information($"User selected: {fileName}");

                    if (fileBytes.Length > 0)
                        load_file(fileName, fileBytes);
                }
                else
                {
                    Log.Error($"User canceled Data load on {tManager.Text}");

                    return;
                }
            }
        }

        void initializeGrid()
        {
            grid.VirtualMode = true;
            grid.CellValueNeeded += gridUtil.Grid_CellValueNeeded;
            grid.CellValuePushed += gridUtil.Grid_CellPushed;
            grid.RowCount = tManager.RDBCore.RowCount + 1;
        }

        string generateCSV()
        {
            StringBuilder sb = new StringBuilder();

            string[] names = core.VisibleCellNames;
            sb.Append(string.Join(",", names));


            Log.Information($"Preparing to export {names.Length} columns and {core.RowCount} rows to .csv...");

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

                Log.Error(msg);

                MessageBox.Show(msg, "CSV Generate Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            return sb.ToString();
        }

        #endregion
    }
}