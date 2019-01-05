using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Grimoire.Utilities;

namespace Grimoire.Tabs.Styles
{
    // TODO: Reorganize my events/methods into read/write file/sql/data!
    public partial class rdbTab : UserControl
    {
        #region Properties

        GUI.Main main = GUI.Main.Instance;
        Logs.Manager lManager = Logs.Manager.Instance;
        Manager tManager = Manager.Instance;
        Database dManager = null;
        rdbCore.Core core = new rdbCore.Core();
        DataCore.Core dCore = null;
        string structsDir = null;
        readonly string key = null;
        readonly Tabs.Utilities.Grid gridUtil;
        readonly Stopwatch actionSW = new Stopwatch();

        bool structLoaded { get { return (ts_struct_list.SelectedIndex != -1); } }
        readonly string buildDir = string.Format(@"{0}\Output\", Directory.GetCurrentDirectory());

        public rdbCore.Core Core
        {
            get
            {
                if (core == null) { throw new Exception("rdbCore is null!"); }
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
            structsDir = OPT.GetString("rdb.structures.directory") ?? string.Format(@"{0}\Structures\", Directory.GetCurrentDirectory());
        }

        #endregion

        #region Events

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

                await Task.Run(() => { core.Initialize(path); });
            }
            catch (MoonSharp.Interpreter.SyntaxErrorException sEx) { lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, "Exception Occured:\n\t- {0}", LuaException.Print(sEx.DecoratedMessage, ts_struct_list.Text)); }
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

        private void ts_load_file_Click(object sender, EventArgs e)
        {
            if (!structLoaded)
            {
                MessageBox.Show("You can not do that until a structure has been loaded!", "Structure Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Paths.DefaultDirectory = OPT.GetString("rdb.load.directory");
            string fileName = Paths.FilePath;

            if (Paths.FileResult != DialogResult.OK)
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "User cancelled file load on tab: {0}", tManager.Text);
                return;
            }

            load_file(fileName);
        }

        private async void ts_load_data_Click(object sender, EventArgs e)
        {
            if (!structLoaded)
            {
                MessageBox.Show("You can not do that until a structure has been loaded!", "Structure Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            dCore = new DataCore.Core(Encodings.GetByName(ts_enc_list.Text));
            Paths.Title = "Select client data.000";
            string dataPath = Paths.FilePath;
            if (Paths.FileResult == DialogResult.OK)
            {

                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "RDB Tab: {0} attempting load file selection from index at path:\n\t- {1}", tManager.Text, dataPath);

                List<DataCore.Structures.IndexEntry> results = null;

                await Task.Run(() =>
                {
                    dCore.Load(dataPath);
                    results = dCore.GetEntriesByExtensions("rdb", "ref");
                });

                lManager.Enter(Logs.Sender.RDB, Logs.Level.DEBUG, "File list retrived successfully!\n\t- Selections available: {0}", results.Count);

                using (Grimoire.GUI.ListSelect selectGUI = new GUI.ListSelect("Select RDB", results))
                {
                    selectGUI.FormClosing += (o, x) =>
                    {
                        if (selectGUI.DialogResult == DialogResult.OK)
                        {
                            string fileName = selectGUI.SelectedText;
                            DataCore.Structures.IndexEntry finalResult = results.Find(i => i.Name == fileName);
                            byte[] fileBytes = dCore.GetFileBytes(finalResult);

                            if (fileBytes.Length > 0)
                                load_file(fileName, fileBytes);
                        }
                        else
                        {
                            lManager.Enter(Logs.Sender.RDB, Logs.Level.DEBUG, "User cancelled Data load on RDB Tab: {0}", tManager.Text);
                            return;
                        }
                    };
                    selectGUI.ShowDialog(Grimoire.GUI.Main.Instance);
                }
            }
        }

        private async void ts_save_file_Click(object sender, EventArgs e)
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
                            lManager.Enter(Logs.Sender.RDB, Logs.Level.WARNING, "User opted to use the tab name as Filename for save operation.\n\t- Filename provided: {0}", tManager.Text);
                            filename = string.Format("{0}.rdb", tManager.Text);
                            break;

                        case DialogResult.Yes:
                            using (GUI.InputBox input = new GUI.InputBox("Enter desired filename", false))
                            {
                                if (input.ShowDialog(this) != DialogResult.OK)
                                    return;

                                lManager.Enter(Logs.Sender.RDB, Logs.Level.WARNING, "User opted to provide Filename for save operation.\n\t- Filename provided: {0}", input.Value);
                                filename = input.Value;
                            }

                            break;
                    }
                }

                if (!filename.Contains("ascii") && ts_save_w_ascii.Checked)
                {
                    filename = string.Format(@"{0}(ascii).rdb", filename.Split('.')[0]);
                }
                if (ts_save_enc.Checked) { filename = DataCore.Functions.StringCipher.Encode(filename); }

                buildPath = string.Format(@"{0}\{1}", buildDir, filename);

                if (!Directory.Exists(buildDir)) { Directory.CreateDirectory(buildDir); }
                if (File.Exists(buildPath)) { File.Delete(buildPath); }

                await Task.Run(() => { core.WriteRDB(buildPath); });
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
            List<rdbCore.Structures.Row> table_data = new List<rdbCore.Structures.Row>();

            try
            {
                await Task.Run(() => { table_data = dManager.FetchTable(rowCount, tablename); });

                if (table_data.Count == 0)
                {
                    lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, "No results were loaded from the table!");
                    return;
                }

                await Task.Run(() => { core.SetData(table_data); });
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
                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "{0} rows were loaded from table: {1} into tab: {2}", table_data.Count, tablename, tManager.Text);

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

            try { await Task.Run(() => { dManager.ExportToTable(tablename, tManager.RDBCore.Data); }); }
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
                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "{0} rows were saved to table: {1} from tab: {2}", tManager.RDBCore.RowCount, tablename, tManager.Text);
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

        public void Clear() { grid.Rows.Clear(); }

        public void LoadFile(string filePath) { load_file(filePath); }

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

        private async void load_file(string filePath)
        {
            try
            {
                await Task.Run(() => { core.ParseRDB(filePath); });
            }
            catch (Exception ex)
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, ex);
                MessageBox.Show(ex.Message, "RDB Load Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "{0} entries loaded from {1}", core.RowCount, Path.GetFileName(filePath));
                tManager.Text = Path.GetFileNameWithoutExtension(filePath);
                initializeGrid();
            }
        }

        private async void load_file(string fileName, byte[] fileBytes)
        {
            try
            {
                await Task.Run(() => { core.ParseRDB(fileBytes); });
            }
            catch (Exception ex)
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.ERROR, ex);
                MessageBox.Show(ex.Message, "RDB Load Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lManager.Enter(Logs.Sender.RDB, Logs.Level.NOTICE, "{0} entries loaded from {1}", core.RowCount, fileName);
                tManager.Text = fileName;
                initializeGrid();
            }
        }

        private void initializeGrid()
        {
            grid.VirtualMode = true;
            grid.CellValueNeeded += gridUtil.Grid_CellValueNeeded;
            grid.CellValuePushed += gridUtil.Grid_CellPushed;
            grid.RowCount = core.Data.Count + 1;
        }

        #endregion
    }
}