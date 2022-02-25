using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataCore;
using DataCore.Structures;
using Grimoire.Utilities;
using Grimoire.Configuration;

using Serilog;
using System.Linq;

namespace Grimoire.Tabs.Styles
{
    public partial class Data : UserControl
    {
        #region Properties

        string key;
        Core core;

        readonly Tabs.TabManager tManager;
        readonly ConfigManager configMan;
        readonly Stopwatch actionSW = new Stopwatch();

        public List<IndexEntry> FilteredIndex = new List<IndexEntry>();

        public bool Filtered => FilteredIndex.Count > 0;

        public int FilterCount => FilteredIndex.Count;
        public List<IndexEntry> SearchIndex = new List<IndexEntry>();

        public bool Searching => SearchIndex.Count > 0;

        public int SearchCount => SearchIndex.Count;

        public bool IndexLoaded => Core.Index.Count > 0;

        public int RowCount
        {
            get
            {
                if (!Filtered && !Searching)
                    return core.RowCount;
                else if (Filtered && !Searching)
                    return FilteredIndex.Count;
                else if (!Filtered && Searching)
                    return SearchIndex.Count;
                else
                    return 0;
            }
        }

        // TODO: this can be handled better and without throwing
        public DataCore.Core Core
        {
            get
            {
                if (core == null) 
                    throw new Exception("DataCore is null!");

                return core;
            }
        }

        private bool grid_cs_enabled
        {
            get
            {
                if (grid.SelectedRows.Count > 1)
                    return !grid_cs.Items[0].Enabled && grid_cs.Items[1].Enabled && grid_cs.Items[2].Enabled;

                return false;
            }
            set => grid_cs.Enabled = value;
        }

        private bool extensions_cs_enabled
        {
            get => extensions_cs.Items[0].Enabled;
            set => extensions_cs.Items[0].Enabled = value;
        }

        private bool search_enabled
        {
            get => searchInput.Enabled;
            set => searchInput.Enabled = value;
        }

        private bool tab_disabled
        {
            set
            {
                bool flipVal = (value) ? false : true;

                grid_cs_enabled = flipVal;
                extensions_cs_enabled = flipVal;
                search_enabled = flipVal;
            }
        }

        XmlManager xMan = XmlManager.Instance;

        readonly bool docked = false;

        #endregion

        #region Constructors

        public Data(string key, bool docked = true)
        {
            InitializeComponent();

            this.key = key;

            tManager = Tabs.TabManager.Instance;
            configMan = GUI.Main.Instance.ConfigMgr;

            initializeCore();

            this.docked = docked;

            if (this.docked)
            {
                localize();
            }
        }

        #endregion

        #region Events

        private void Core_MessageOccured(object sender, MessageArgs e)
        {
            Invoke(new MethodInvoker(delegate { 
                ts_status.Text = e.Message;
            }));

            Log.Information(e.Message);
        }

        private void Core_CurrentMaxDetermined(object sender, CurrentMaxArgs e)
        {
            Invoke(new MethodInvoker(delegate { 
                ts_progress.Maximum = (int)e.Maximum;
            }));
        }

        private void Core_CurrentProgressChanged(object sender, CurrentChangedArgs e)
        {
            Invoke(new MethodInvoker(delegate { 
                ts_progress.Value = (int)e.Value;
            }));
        }

        private void Core_CurrentProgressReset(object sender, CurrentResetArgs e)
        {
            Invoke(new MethodInvoker(delegate {
                ts_progress.Maximum = 100;
                ts_progress.Value = 0;
                ts_status.Text = string.Empty;
            }));
        }

        public async void TS_File_New_Click(object sender, EventArgs e)
        {
            Paths.Description = "Please select a Dump Directory";
            string dumpDirectory = Paths.FolderPath;

            if (Paths.FolderResult != DialogResult.OK)
                return;

            string buildDirectory = configMan.GetDirectory("BuildDirectory", "Grim");
            string msg = null;

            if (!await Paths.VerifyDump(dumpDirectory))
            {
                msg = "There are issues with your dump structure! Please verify that files are in their proper extension folder (e.g. .nfe in /nfe/ folder!)";

                Log.Error(msg);

                MessageBox.Show(msg, "Create Client Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            msg = $"Building new client at:\n\t-{buildDirectory}";

            Log.Information(msg);

            ts_status.Text = msg;

            tab_disabled = true;

            try
            {
                if (core.Index.Count > 0)
                    core.Clear();

                core.Backups = false;

                await Task.Run(() => {
                    core.BuildDataFiles(dumpDirectory, buildDirectory);
                });

                msg = "Client build completed!";

                Log.Information(msg);

                MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if (configMan.Get<bool>("ClearOnCreate", "Data", true)) // bool is weird with index accessor, use .Get<T> to ensure type compatibility.
                    core.Clear();
                else
                    display_data();

                core.Backups = true;
            }
            catch (Exception ex)
            {
                Log.Error($"An exception occured while building!\nMessage:\n\t{ex.Message}\n\nStack-Trace:{ex.StackTrace}");

                MessageBox.Show(ex.Message, "Build Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
         
            ts_status.Text = string.Empty;

            tab_disabled = false;
        }

        public void TS_File_Load_Click(object sender, EventArgs e)
        {
            Paths.DefaultDirectory = configMan["LoadDirectory", "Data"];
            Paths.DefaultFileName = "data.000";

            string filePath = Paths.FilePath;

            if (Paths.FileResult != DialogResult.OK)
                return;

            if (!filePath.EndsWith("000"))
            {
                MessageBox.Show("You have selected an invalid file! Please select data.000!", "Invalid Index", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }    

            load(filePath);
        }

        public void TS_File_Rebuild_Click(object sender, EventArgs e)
        {
            unhook_core_events();

            using (GUI.DataRebuild rebuildGUI = new GUI.DataRebuild())
                rebuildGUI.ShowDialog(GUI.Main.Instance);

            hook_core_events();
        }

        private void extensions_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            long extSize = tManager.DataCore.GetExtensionSize(e.Node.Text);
            string formattedSize = StringExt.SizeToString(extSize);

            if (e.Node.Text != "all")
                extensions.Nodes[e.Node.Text].Nodes[1].Text = $"Size: {formattedSize}";
        }

        private void extensions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string ext = e.Node.Text;

            if (ext.Length > 3) // Will catch accidental info clicks (Count, Size)
                return;

            if (ext != "all")
            {
                FilteredIndex = tManager.DataCore.GetEntriesByExtension(ext, SortType.Name);
                grid.Rows.Clear();
                grid.RowCount = FilteredIndex.Count;
            }
            else if (ext == "all")
            {
                if (Filtered)
                    FilteredIndex.Clear();

                if (Searching)
                    SearchIndex.Clear();

                grid.Rows.Clear();
                grid.RowCount = tManager.DataCore.RowCount;
            }

        }

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            grid_cs.Items[2].Enabled = true;
            grid_cs.Items[3].Enabled = true;

            int rowCount = grid.SelectedRows.Count;

            if (rowCount == 1)
            {
                if (grid.SelectedRows[0].Cells[0].Value != null)
                {
                    IndexEntry entry = tManager.DataCore.GetEntry(grid.SelectedRows[0].Cells[0].Value.ToString());

                    populate_selection_info(entry);

                    grid_cs.Items[0].Enabled = true;
                    grid_cs.Items[1].Enabled = true;
                    grid_cs.Items[2].Text = "Export";
                }
            }
            else
            {
                populate_selection_info();

                grid_cs.Items[0].Enabled = grid_cs.Items[3].Enabled = false;
                grid_cs.Items[2].Text = string.Format("Export {0}", rowCount);
            }
        }

        private async void grid_cs_delete_Click(object sender, EventArgs e)
        {
            string filename = grid.SelectedRows[0].Cells[0].Value.ToString();

            if (!string.IsNullOrEmpty(filename))
            {
                IndexEntry entry = core.GetEntry(filename);

                if (entry != null)
                {
                    if (MessageBox.Show($"You are about to delete\n\n{filename}!!!\n\nYou should be absolutely sure you want to do this!\n\nDo you want to continue?", "Input Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        await Task.Run(() => {
                            core.DeleteFileEntry(entry.DataID, (int)entry.Offset, entry.Length);
                        });
                    }
                }
            }
        }

        private async void grid_cs_export_Click(object sender, EventArgs e)
        {
            if (grid.Rows.Count == 0)
                return;

            string buildDir = configMan.GetDirectory("BuildDirectory", "Grim");

            for (int i = 0; i < grid.SelectedRows.Count; i++)
            {
                IndexEntry entry = core.GetEntry(grid.SelectedRows[i].Cells[0].Value.ToString());

                ts_status.Text = string.Format("Exporting: {0}...", entry.Name);

                Log.Information($"Exporting: {entry.Name} to:\n\t- {buildDir}\n\t- Size: {StringExt.SizeToString(entry.Length)}");

                try
                {
                    await Task.Run(() => {
                        core.ExportFileEntry(buildDir, entry);
                    });
                }
                catch (Exception ex)
                {
                    Log.Error($"An exception occured during export!\nMessage:\n\t{ex.Message}\n\nStack-Trace:\n\t-{ex.StackTrace}");

                    MessageBox.Show(ex.Message, "Export Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);                 
                }

                ts_status.Text = string.Empty;
            }
        }

        private async void extensions_cs_export_Click(object sender, EventArgs e)
        {
            string buildDirectory = configMan.GetDirectory("BuildDirectory", "Grim");
            string ext = extensions.SelectedNode.Text;
            
            if (ext.Length >= 2)
            {
                List<IndexEntry> entries = core.GetEntriesByExtension(ext);

                ts_status.Text = $"Exporting: {ext}...";

                tab_disabled = true;

                try
                {
                    await Task.Run(() =>
                    {
                        if (ext == "all")
                            core.ExportAllEntries(buildDirectory);
                        else
                        {
                            buildDirectory += $@"\{ext}\";

                            if (!Directory.Exists(buildDirectory))
                                Directory.CreateDirectory(buildDirectory);

                            core.ExportExtEntries(buildDirectory, ext);
                        }
                    });

                    Log.Information($"Exported {entries.Count} rows from {tManager.Text}");
                }
                catch (Exception ex)
                {
                    Log.Error($"An exception occured during export!\nMessage:\n\t{ex.Message}\n\nStack-Trace\n\t{ex.StackTrace}");

                    MessageBox.Show(ex.Message, "Extension Export Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }

                ts_status.Text = string.Empty;

                tab_disabled = false;
            }
        }

        private void grid_cs_compare_Click(object sender, EventArgs e)
        {
            string compareFile = Paths.FilePath;
            string filename = grid.SelectedRows[0].Cells[0].Value.ToString();
            string externalHash = null;
            string internalHash = null;
            byte[] internalBuffer = null;
            byte[] externalBuffer = null;

            if (Paths.FileResult != DialogResult.OK)
                return;

            try
            {
                IndexEntry storedEntry = core.GetEntry(filename);

                bool isRDB = storedEntry.Extension == "rdb";

                internalBuffer = (isRDB) ? core.GetFileBytes(storedEntry, 128) : core.GetFileBytes(storedEntry);
                externalBuffer = (isRDB) ? File.ReadAllBytes(compareFile).Skip(128).ToArray() : File.ReadAllBytes(compareFile);

                // This shit takes forever! remember its 408 thousand bytes for fieldpropresource, imagine item
                //Log.Verbose($"Internal buffer:\n{StringExt.ByteArrayToString(internalBuffer)}");
                //Log.Verbose($"External buffer:\n{StringExt.ByteArrayToString(externalBuffer)}");

                internalHash = DataCore.Functions.Hash.GetSHA512Hash(internalBuffer, internalBuffer.Length);
                externalHash = DataCore.Functions.Hash.GetSHA512Hash(externalBuffer, externalBuffer.Length);

                string result = (externalHash == internalHash) ? "MATCH" : "MISMATCH";
                string msg = $"Internal Filename:\n\n{filename}\n\nExternal File:\n\n{compareFile}\n\nResult: {result}\n";

                Log.Information(msg);

                MessageBox.Show(msg, "Comparison Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Log.Error($"An exception occured during the comparison!\nMessage:\n\t{ex.Message}\n\nStack-Trace:\n\t{ex.StackTrace}");

                MessageBox.Show(ex.Message, "Compare Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grid_cs_insert_Click(object sender, EventArgs e)
        {
            Paths.FileMultiSelect = true;
            string[] filePaths = Paths.FilePaths;

            if (Paths.FileResult == DialogResult.OK)
                insert_files(filePaths);
        }

        private void searchInput_TextChanged(object sender, EventArgs e)
        {
            if (searchInput.Text == "Search")
                return;

            if (searchInput.Text.Length > 2)
            {
                SearchIndex = (Filtered) ? FilteredIndex.FindAll(i => i.Name.Contains(searchInput.Text)) : core.GetEntriesByPartialName(searchInput.Text);

                grid.Rows.Clear();
                grid.RowCount = SearchIndex.Count;
            }
            else if (searchInput.Text.Length == 8) // data.001
            {
                string input = searchInput.Text;

                if (input.StartsWith("data"))
                {
                    int dataId = 0;

                    if (!int.TryParse(searchInput.Text.Substring(searchInput.Text.Length - 3, 3), out dataId))
                    {
                        Log.Error("Failed to get valid substring value from input!");

                        return;
                    }

                    SearchIndex = (Filtered) ? FilteredIndex.FindAll(i => i.DataID == dataId).OrderBy(i => i.Offset).ToList() : core.GetEntriesByDataId(dataId);

                    grid.Rows.Clear();
                    grid.RowCount = SearchIndex.Count;
                }
            }
            else if (searchInput.Text.Length == 0)
            {
                SearchIndex.Clear();
                grid.Rows.Clear();
                grid.RowCount = tManager.DataCore.RowCount;
            }

        }

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 1)
                grid_cs_export_Click(null, EventArgs.Empty);
        }

        private void extensions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (extensions.Nodes.Count == 0)
                return;

            extensions.SelectedNode = e.Node;

            if (e.Button == MouseButtons.Right)
                extensions_cs.Show(extensions, e.Location);
        }

        private void grid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (grid.Rows.Count == 0)
                return;

            if (e.Button == MouseButtons.Right)
            {
                if (grid.SelectedRows.Count == 1)
                {
                    grid.ClearSelection();
                    grid.Rows[e.RowIndex].Selected = true;
                }

                grid_cs.Show(grid, grid.PointToClient(Cursor.Position));
            }
        }

        private void searchInput_Click(object sender, EventArgs e)
        {
            if (searchInput.Text == "Search")
                searchInput.Text = string.Empty;
        }

        private void searchInput_Leave(object sender, EventArgs e)
        {
            if (searchInput.Text.Length == 0)
                searchInput.Text = "Search";
        }

        #endregion

        #region Methods (Public)

        public void Clear()
        {
            grid.Rows.Clear();
            extensions.Nodes.Clear();
            ts_file_load.Enabled = true;
            ts_file_new.Enabled = true;
        }

        public void Hook_Core_Events() => hook_core_events();

        public void Unhook_Core_Events() => unhook_core_events();

        new public void Load(string path) => load(path);

        new public void Load(DataCore.Core core)
        {
            this.core = core;
            display_data();
        }

        public void Insert(string[] filePaths) => insert_files(filePaths);

        public void Localize() => localize();

        #endregion

        #region Methods (private)

        void initializeCore()
        {
            bool backup = configMan["Backup", "Data"];
            int codepage = configMan.Get<int>("Codepage", "Grim");
            Encoding encoding = Encoding.GetEncoding(codepage);
            core = new Core(backup, encoding);
            
            core.UseModifiedXOR = configMan["UseModifiedXOR", "Data"];

            if (core.UseModifiedXOR)
            {
                byte[] modifiedKey = configMan.GetByteArray("ModifiedXORKey");

                if (modifiedKey == null || modifiedKey.Length != 256)
                {
                    Log.Fatal("Invalid XOR Key!");
                    return;
                }

                core.SetXORKey(modifiedKey);

                if (!core.ValidXOR)
                {
                    string msg = "The provided ModifiedXORKey is invalid!";

                    Log.Error(msg);

                    MessageBox.Show(msg, "XOR Key Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                Log.Information($"Using modified xor key:\n");

                if (GUI.Main.Instance.LogLevel.MinimumLevel >= Serilog.Events.LogEventLevel.Debug)
                    Log.Debug($"\n{StringExt.ByteArrayToString(modifiedKey)}");
            }

            hook_core_events();
        }

        void hook_core_events()
        {
            core.CurrentMaxDetermined += Core_CurrentMaxDetermined;
            core.CurrentProgressChanged += Core_CurrentProgressChanged;
            core.CurrentProgressReset += Core_CurrentProgressReset;
            core.MessageOccured += Core_MessageOccured;
        }

        void unhook_core_events()
        {
            core.CurrentMaxDetermined -= Core_CurrentMaxDetermined;
            core.CurrentProgressChanged -= Core_CurrentProgressChanged;
            core.CurrentProgressReset -= Core_CurrentProgressReset;
            core.MessageOccured -= Core_MessageOccured;
        }

        void populate_selection_info()
        {
            dataId.ResetText();
            offset.ResetText();
            size.ResetText();
            encrypted.ResetText();
            extension.ResetText();
        }

        void populate_selection_info(IndexEntry entry)
        {
            Invoke(new MethodInvoker(delegate {
                string ext = entry.Extension;

                dataId.Text = entry.DataID.ToString();
                offset.Text = entry.Offset.ToString();
                size.Text = StringExt.SizeToString(entry.Length);
                encrypted.Text = tManager.DataCore.ExtensionEncrypted(ext).ToString();
                extension.Text = ext;
                uploadPath.Text = entry.DataPath;
            }));

        }

        async void load(string path)
        {
            tab_disabled = true;

            try
            {
                actionSW.Start();

                await Task.Run(() => { core.Load(path); });

                actionSW.Stop();

                ts_file_load.Enabled = false;
                ts_file_new.Enabled = false;

                Log.Information($"{core.RowCount} entries loaded from data.000 to {tManager.Text} in {StringExt.MilisecondsToString(actionSW.ElapsedMilliseconds)} from path:\n\t- {path}");

                if (docked)
                {
                    display_data();

                    ts_file_new.Visible = false;
                    ts_file_rebuild.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"An exceeption occured during load!\nMessage:\n\t{ex.Message}\n\nStack-Trace:\n\t{ex.StackTrace}");
            }

            tab_disabled = false;
        }

        async void display_data()
        {
            grid.RowCount = core.RowCount;
            grid.VirtualMode = true;
            grid.CellValueNeeded += (o, e) =>
            {
                if (tManager.DataTab.Filtered && tManager.DataTab.Searching || !tManager.DataTab.Filtered && tManager.DataTab.Searching)
                    e.Value = tManager.DataTab.SearchIndex[e.RowIndex].Name;
                else if (tManager.DataTab.Filtered && !tManager.DataTab.Searching)
                {
                    if (e.RowIndex < tManager.DataTab.FilterCount)
                        e.Value = tManager.DataTab.FilteredIndex[e.RowIndex].Name;
                }
                else
                    e.Value = tManager.DataCore.Index[e.RowIndex].Name;
            };

            await Task.Run(() => { populate_selection_info(tManager.DataCore.Index[0]); });

            extStatus.Text = "Analyzing index...";

            extensions.Nodes.Add("all", "all");
            extensions.Nodes["all"].Nodes.Add(string.Format("Count: {0}", tManager.DataCore.RowCount));

            await Task.Run(() => {
                foreach (ExtensionInfo extInfo in Core.ExtensionList)
                {
                    this.Invoke(new MethodInvoker(delegate {
                        extensions.Nodes.Add(extInfo.Type, extInfo.Type);
                        extensions.Nodes[extInfo.Type].Nodes.Add(string.Format("Count: {0}", extInfo.Count));
                        extensions.Nodes[extInfo.Type].Nodes.Add("Size: ");
                    }));
                }
            });

            extStatus.ResetText();
        }

        private async void insert_files(string[] filePaths)
        {
            using (GUI.MessageListBox msgbox = new GUI.MessageListBox("Review Files", "You are about to import the following files!\r\n\r\nAre you sure you want to do that?", filePaths))
            {
                msgbox.ShowDialog(GUI.Main.Instance);
                if (msgbox.DialogResult == DialogResult.Cancel)
                    return;
            }

            try
            {
                tab_disabled = true;

                if (filePaths.Length == 1)
                {
                    string path = filePaths[0];
                    string msg = $"Importing: {Path.GetFileName(path)}";

                    ts_status.Text = msg;

                    Log.Information(msg);

                    await Task.Run(() => {
                        core.ImportFileEntry(path);
                    });
                }
                else if (filePaths.Length > 1)
                    await Task.Run(() => {
                        core.ImportFileEntries(filePaths);
                    });

                core.Save();
            }
            catch (Exception ex)
            {
                Core_CurrentProgressReset(null, new CurrentResetArgs(false));

                Log.Error($"An exception occured during import!\nMessage:\n\t{ex.Message}\n\nStack-Trace:\n\t{ex.StackTrace}");
                
                MessageBox.Show(ex.Message, "Import Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            finally
            {
                tab_disabled = false;
                ts_status.Text = string.Empty;
            }
        }

        private void localize() => xMan.Localize(this, Localization.Enums.SenderType.Tab);

        #endregion
    }
}
