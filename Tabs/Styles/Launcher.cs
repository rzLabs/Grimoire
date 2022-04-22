using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Grimoire.Configuration;
using Grimoire.Tabs;
using Grimoire.GUI;
using Grimoire.Utilities;
using Grimoire.Structures;

using DataCore.Functions;

using Serilog;
using Serilog.Events;
using Archimedes;

namespace Grimoire.Tabs.Styles
{
    public partial class Launcher : UserControl
    {
        #region Fields

        ConfigManager configMgr = GUI.Main.Instance.ConfigMgr;
        Tabs.TabManager tabMgr = Tabs.TabManager.Instance;
        DataCore.Core data;

        string structDir;

        StructureManager structMgr = StructureManager.Instance;

        Timer rdbBtn_check = new Timer() { Interval = 500 };

        Progress<int> taskProgress;

        Stopwatch actionSW = new Stopwatch();
        #endregion

        #region Properties

        bool rdbWorking
        {
            set => BeginInvoke(new MethodInvoker(delegate { rdb_status_lb.Text = (value) ? "Working..." : string.Empty; }));
        }

        bool hasherWorking
        {
            set => BeginInvoke(new MethodInvoker(delegate { hasher_status_lb.Text = (value) ? "Working..." : string.Empty; }));
        }

        #endregion

        public Launcher()
        {
            InitializeComponent();

            initLinks();

            enableDragNDrops();
        }

        #region Events

        private void launch_btn_Click(object sender, EventArgs e)
        {
            PictureBox btn = sender as PictureBox;
            Style style = Style.NONE;

            switch (btn.Name)
            {
                case "launch_data_btn":
                    style = Style.DATA;
                    break;

                case "launch_rdb_btn":
                    style = Style.RDB2;
                    break;

                case "launch_hash_btn":
                    style = Style.HASHER;
                    break;

                case "launch_item_btn":
                    style = Style.ITEM;
                    break;

                case "launch_market_btn":
                    style = Style.MARKET;
                    break;
            }

            if (style != Style.NONE)
                tabMgr.Create(style);
        }

        private void launch_data_btn_MouseDown(object sender, MouseEventArgs e) => launch_data_btn.BorderStyle = BorderStyle.Fixed3D;

        private void launch_data_btn_MouseUp(object sender, MouseEventArgs e) => launch_data_btn.BorderStyle = BorderStyle.FixedSingle;

        private void launch_rdb_btn_MouseDown(object sender, MouseEventArgs e) => launch_rdb_btn.BorderStyle = BorderStyle.Fixed3D;

        private void launch_rdb_btn_MouseUp(object sender, MouseEventArgs e) => launch_rdb_btn.BorderStyle = BorderStyle.FixedSingle;

        private void launch_hash_btn_MouseDown(object sender, MouseEventArgs e) => launch_hash_btn.BorderStyle = BorderStyle.Fixed3D;

        private void launch_hash_btn_MouseUp(object sender, MouseEventArgs e) => launch_hash_btn.BorderStyle = BorderStyle.FixedSingle;

        private void launch_item_btn_MouseDown(object sender, MouseEventArgs e) => launch_item_btn.BorderStyle = BorderStyle.Fixed3D;

        private void launch_item_btn_MouseUp(object sender, MouseEventArgs e) => launch_item_btn.BorderStyle = BorderStyle.FixedSingle;

        private void launch_market_btn_MouseDown(object sender, MouseEventArgs e) => launch_market_btn.BorderStyle = BorderStyle.Fixed3D;

        private void launch_market_btn_MouseUp(object sender, MouseEventArgs e) => launch_market_btn.BorderStyle = BorderStyle.FixedSingle;

        private async void dumpClient_btn_DragDrop(object sender, DragEventArgs e)
        {
            Encoding encoding = Encoding.GetEncoding(configMgr.Get<int>("Codepage", "Grim"));
            bool backup = configMgr.Get<bool>("Backup", "data");

            data = new DataCore.Core(backup, encoding);

            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            string dataPath = null;
            string buildDir = configMgr.GetDirectory("BuildDirectory", "Grim");

            if (File.GetAttributes(paths[0]).HasFlag(FileAttributes.Directory))
            {
                dataPath = $"{paths[0]}\\data.000";

                if (!File.Exists(dataPath))
                {
                    LogUtility.MessageBoxAndLog($"Cannot locate data index at path:\n\t- {paths[0]}", "Client Export Exception", LogEventLevel.Error);

                    return;
                }
            }
            else
                dataPath = paths[0];

            data.CurrentMaxDetermined += (o, x) =>
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    data_prg.Maximum = (int)x.Maximum;
                }));
            };

            data.CurrentProgressChanged += (o, x) =>
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    data_prg.Value = (int)x.Value;
                }));
            };

            data.CurrentProgressReset += (o, x) =>
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    data_prg.Maximum = 100;
                    data_prg.Value = 0;
                }));
            };

            await Task.Run(() =>
            {
                data.Load(dataPath);
            });

            if (data.RowCount <= 0)
            {
                string exMsg = "No data has been loaded!";

                Log.Error(exMsg);

                MessageBox.Show(exMsg, "Client Dump Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

#pragma warning disable CS4014
            Task.Run(() => { data.ExportAllEntries(buildDir); });
#pragma warning restore CS4014
        }

        private void newClient_btn_Click(object sender, EventArgs e) => createClient();

        private void newClient_btn_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

            createClient(path);
        }

        private void launcher_control_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private async void launch_data_btn_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0] ?? "NULL";
            Encoding encoding = Encoding.GetEncoding(configMgr.Get<int>("Codepage", "Grim", 1252));
            bool backup = configMgr.Get<bool>("Backup", "data", true);

            DataCore.Core core = new DataCore.Core(backup, encoding);

            if (!File.Exists(path))
            {
                LogUtility.MessageBoxAndLog($"File cannot be found at path:\n\t- {path}", "Data Quick Launch Exception", LogEventLevel.Error);

                return;
            }

            try
            {
                await Task.Run(() => { core.Load(path); });
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog($"An exception occured during launch!\nMessage:\n\t{ ex.Message}\n\nStack - Trace:\n\t{ ex.StackTrace}", "Data Quick Launch Exception", LogEventLevel.Error);

                return;
            }

            string key = tabMgr.Create(Style.DATA);

            tabMgr.DataTabByKey(key).Load(core);

            tabMgr.SelectedIndex = tabMgr.Count - 1;
        }

        private async void launch_rdb_btn_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = ((string[])e.Data.GetData(DataFormats.FileDrop));

            for (int i = 0; i < paths.Length; i++)
            {
                string path = paths[i];

                if (!File.Exists(path))
                {
                    LogUtility.MessageBoxAndLog($"Could not hash {Path.GetFileName(path)} because its path does not exist!\n\t- {path}", "Hash Exception", LogEventLevel.Error);

                    continue;
                }

                try
                {
                    string filename = Path.GetFileName(path);
                    string structname = StructLinkUtility.GetStructName(filename);

                    if (string.IsNullOrEmpty(structname))
                    {
                        using (StructureSelect select = new StructureSelect())
                        {
                            if (select.ShowDialog(this) != DialogResult.OK)
                            {
                                Log.Verbose("User cancelled structure selection!");
                                return;
                            }

                            structname = select.SelectedText;
                        }
                    }

                    string key = tabMgr.Create(Style.RDB2);
                    RDB2 rdbTab = tabMgr.RDBTabByKey(key);

                    rdbTab.StructObject = await structMgr.GetStruct(structname);
                    rdbTab.GenerateColumns();

                    rdbTab.ReadFile(path);
                }
                catch (Exception ex)
                {
                    LogUtility.MessageBoxAndLog(ex, "quick launching rdb tab", "RDB Quick Launch Exception", LogEventLevel.Error);
                    return;
                }
            }
        }

        private async void rdb2sql_btn_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = e.Data.GetData(DataFormats.FileDrop) as string[];
            List<string> rdbs = new List<string>();
            if (paths.Length == 1)
            {
                if ((File.GetAttributes(paths[0]) & FileAttributes.Directory) == FileAttributes.Directory)
                    rdbs = new List<string>(Directory.GetFiles(paths[0]));
                else
                    rdbs.Add(paths[0]);
            }
            else
                rdbs = new List<string>(paths);

            for (int i = 0; i < rdbs.Count; i++)
                await Task.Run(() => { saveRDBToSQL(rdbs[i]); });
        }

        private async void sql2rdb_btn_Click(object sender, EventArgs e)
        {
            List<string> completedExports = new List<string>();

            string buildDir = configMgr.GetDirectory("BuildDirectory", "Grim");
            string structDir = configMgr.GetDirectory("Directory", "Structures");
            string[] selectedStructs = null;

            actionSW.Restart();

            using (StructureSelect select = new StructureSelect())
            {
                if (select.ShowDialog(this) == DialogResult.OK)
                    selectedStructs = select.SelectedStructures;
            }

            if (selectedStructs is null)
                return;

            foreach (string name in selectedStructs)
            {
                var structObj = await structMgr.GetStruct(name);

                if (structObj is null)
                {
                    LogUtility.MessageBoxAndLog($"Unable to get structure: {name}! Skipping...", "RDB To SQL Exception", LogEventLevel.Warning);

                    continue;
                }

                string filename = $"{buildDir}\\{structObj.RDBName}";

                taskProgress = new Progress<int>(onRDBProgress);

                try
                {

                    await DatabaseUtility.ReadTableData(structObj, taskProgress);

                    structObj.Write(filename);
                }
                catch (Exception ex)
                {
                    LogUtility.MessageBoxAndLog(ex, "exporting {name} rdb to sql", "RDB To SQL Exception", LogEventLevel.Error);

                    continue;
                }

                completedExports.Add(structObj.StructName);
            }

            actionSW.Stop();

            if (completedExports.Count > 0)
            {
                Log.Information($"SQL Export of {completedExports.Count} sql tables completed in {StringExt.MilisecondsToString(actionSW.ElapsedMilliseconds)}");

                DialogUtility.ShowMessageListBox("SQL Export Successful", "The following files have been successfully saved from SQL!", completedExports);
            }

            rdb_prg.Maximum = 100;
            rdb_prg.Value = 0;
        }

        private void rdb2sql_btn_Click(object sender, EventArgs e)
        {
            Paths.DefaultDirectory = configMgr.GetDirectory("LoadDirectory", "RDB");

            string path = Paths.FilePath;

            if (Paths.FileResult != DialogResult.OK)
            {
                LogUtility.MessageBoxAndLog("No path has been selected!", "RDB 2 SQL Exception", LogEventLevel.Error);

                return;
            }

            saveRDBToSQL(path);
        }

        async void hasher_drop_grpbx_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = ((string[])e.Data.GetData(DataFormats.FileDrop));
            List<string> toConvert = new List<string>();

            if (paths.Length == 1)
            {
                if ((File.GetAttributes(paths[0]) & FileAttributes.Directory) == FileAttributes.Directory)
                    foreach (string path in Directory.GetFiles(paths[0]))
                        toConvert.Add(path);
            }
            else
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    string path = paths[i];

                    if (!File.Exists(path))
                    {
                        LogUtility.MessageBoxAndLog($"Could not hash {Path.GetFileName(path)} because its path does not exist!\n\t- {path}", "Hash Exception", LogEventLevel.Error);

                        continue;
                    }

                    toConvert.Add(path);
                }
            }

            hash_prg.Maximum = toConvert.Count;

            taskProgress = new Progress<int>(onHashProgress);

            bool result = false;

            await Task.Run(() => { result = hashCollection(toConvert, taskProgress); });

            if (result)
                LogUtility.MessageBoxAndLog($"Successfully hashed {toConvert.Count} files!", "Hash Successful", LogEventLevel.Information);

            hash_prg.Maximum = 100;
            hash_prg.Value = 0;
        }

        private void launch_hash_btn_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = e.Data.GetData(DataFormats.FileDrop) as string[];
            List<string> toConvert = new List<string>();

            if (paths.Length == 1)
            {
                if ((File.GetAttributes(paths[0]) & FileAttributes.Directory) == FileAttributes.Directory)
                    toConvert = new List<string>(Directory.GetFiles(paths[0]));
                else
                    toConvert.Add(paths[0]);
            }
            else
                toConvert = new List<string>(paths);

            string key = Tabs.TabManager.Instance.Create(Style.HASHER);

            tabMgr.HashTabByKey(key).Add_Dropped_Files(toConvert.ToArray());
        }

        private void RdbBtn_check_Tick(object sender, EventArgs e) { }

        #endregion

        #region Private Methods

        void enableDragNDrops()
        {
            quickLaunch_grpbx.AllowDrop = true;
            ((Control)launch_data_btn).AllowDrop = true;
            ((Control)launch_rdb_btn).AllowDrop = true;
            newClient_btn.AllowDrop = true;
            dumpClient_btn.AllowDrop = true;

            ((Control)launch_hash_btn).AllowDrop = true;
            hasher_drop_grpbx.AllowDrop = true;

            rdbToSQL_grpBx.AllowDrop = true;
        }

        void initLinks() // may eventually need to be async (so simple I really doubt it)
        {
            StructLinkUtility.Parse();
        }

        async void createClient(string dumpDirectory = null)
        {
            Paths.Title = "Please select your client dump";

            Encoding encoding = Encoding.GetEncoding(configMgr.Get<int>("Codepage", "Grim", 1252));
            bool backup = configMgr.Get<bool>("Backup", "data", true);
            string dmpDir = (dumpDirectory is null) ? configMgr.GetDirectory("DumpDirectory", "Grim") ?? Paths.FolderPath : dumpDirectory;
            string buildDir = configMgr.GetDirectory("BuildDirectory", "Grim");

            if (!Directory.Exists(dmpDir))
            {
                LogUtility.MessageBoxAndLog($"Provided dump directory does not exist!\n\t- {dmpDir}", "Create Client Exception", LogEventLevel.Error);

                return;
            }

            data_status_lb.Text = "Working...";

            if (!await Paths.VerifyDump(dmpDir))
            {
                LogUtility.MessageBoxAndLog("Dump directory could not be verified! Check for invalid extensions such as .nfe inside /nfm/ sub-directory!", "Create Client Exception", LogEventLevel.Error);

                return;
            }

            if (data != null && data.RowCount > 0)
                data.Clear();

            if (data == null)
                data = new DataCore.Core(backup, encoding);

            data.CurrentMaxDetermined += (o, x) =>
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    data_prg.Maximum = (int)x.Maximum;
                }));
            };

            data.CurrentProgressChanged += (o, x) =>
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    data_prg.Value = (int)x.Value;
                }));
            };

            data.CurrentProgressReset += (o, x) =>
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    data_prg.Maximum = 100;
                    data_prg.Value = 0;
                }));
            };

            newClient_btn.Enabled = false;
            dumpClient_btn.Enabled = false;

            actionSW.Restart();

            try
            {
                await Task.Run(() => { data.BuildDataFiles(dmpDir, buildDir); });

                data.Save(buildDir);

                actionSW.Stop();

                LogUtility.MessageBoxAndLog($"New client successfully created in {StringExt.MilisecondsToString(actionSW.ElapsedMilliseconds)}\n\t- {buildDir}", "Information", LogEventLevel.Information);
            }
            catch (Exception ex)
            {
                actionSW.Stop();

                LogUtility.MessageBoxAndLog($"An exception occured during import!\nMessage:\n\t{ ex.Message}\n\nStack - Trace:\n\t{ ex.StackTrace}", "Create Client Exception", LogEventLevel.Error);

                return;
            }

            data_status_lb.ResetText();
        }

        async Task<string> saveSQLtoRDB(string structName)
        {
            string buildDir = configMgr.GetDirectory("BuildDirectory", "Grim");
            StructureObject structObj = await structMgr.GetStruct(structName.Remove(structName.Length - 4, 4));

            if (!Directory.Exists(buildDir))
            {
                Directory.CreateDirectory(buildDir);

                Log.Verbose($"Build Directory created.\n\t- {buildDir}");
            }

            string filename = StructLinkUtility.GetFileName(structName) ?? DialogUtility.RequestQuestionInput<string>("Input Required", $"No existing filename link for the provided structure: {structName} exists!\n\nWould you like to enter one manually?", "Enter the filename", DialogUtility.DialogType.Path);

            if (filename == null)
            {
                LogUtility.MessageBoxAndLog($"Failed to link structure: {structName} to a filename! Please verify the contents of your provided StructLinks.json", "SQL Save Exception", LogEventLevel.Error);

                return null;
            }

            string buildPath = $"{buildDir}\\{filename}";

            try
            {
                rdbWorking = true;

                if (structObj.TableName == null)
                    structObj.TableName = DialogUtility.RequestQuestionInput<string>("Input Required", "No table name has been defined!\n\nWould you like to input one now?", "Input desired table name");

                int rowCount = await DatabaseUtility.GetRowCount(structObj.TableName);

                if (rowCount == -99)
                {
                    LogUtility.MessageBoxAndLog($"Failed to get a valid row count for table: {structObj.TableName}", "Load SQL Exception", LogEventLevel.Error);

                    return null;
                }

                structObj.Rows = new List<RowObject>(await DatabaseUtility.ReadTableData(structObj, taskProgress));

                structObj.Write(buildPath);
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "saving to rdb", "SQL Save Exception", LogEventLevel.Error);

                return null;
            }
            finally
            {
                DatabaseUtility.Dispose();

                rdbWorking = false;
            }

            return filename;
        }

        async void saveRDBToSQL(string path)
        {
            if (!File.Exists(path))
            {
                LogUtility.MessageBoxAndLog($"Cannot find rdb at:\t\n- {path}", "SQL Export Exception", LogEventLevel.Error);

                return;
            }

            string filename = Path.GetFileName(path);

            if (StructLinkUtility.FilenameLinkExists(filename)) // Check to see if this file has already been linked to a structure
            {
                string structName = StructLinkUtility.GetStructName(filename);
                StructureObject structObj;

                if (string.IsNullOrEmpty(structDir))
                {
                    LogUtility.MessageBoxAndLog("Failed to get Structures Directory from the Config.json!", "SQL Export Exception", LogEventLevel.Error);

                    return;
                }

                if (string.IsNullOrEmpty(structName))
                {
                    LogUtility.MessageBoxAndLog("Structure name cannot be null!", "SQL Export Exception", LogEventLevel.Error);

                    return;
                }

                try
                {
                    rdbWorking = true;

                    structObj = await structMgr.GetStruct(structName);

                    structObj.Read(path);

                    taskProgress = new Progress<int>(onRDBProgress);

                    rdb_prg.Maximum = structObj.RowCount;

                    if (!await DatabaseUtility.WriteTableData(structObj, taskProgress))
                    {
                        LogUtility.MessageBoxAndLog($"Failed to save: {structObj.RDBName} to: {structObj.TableName}", "SQL Export Exception", LogEventLevel.Error);

                        return;
                    }

                    LogUtility.MessageBoxAndLog($"{structObj.RowCount} rows saved from: {structObj.RDBName} to: {structObj.TableName}", "Export Successful!", LogEventLevel.Information);
                }
                catch (Exception ex)
                {
                    LogUtility.MessageBoxAndLog(ex, "Saving RDB to SQL", "SQL Export Exception", LogEventLevel.Error);

                    return;
                }
                finally
                {
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        rdb_prg.Maximum = 100;
                        rdb_prg.Value = 0;
                    }));

                    rdbWorking = false;
                }
            }
        }

        bool hashCollection(List<string> paths, IProgress<int> progress)
        {
            hasherWorking = true;

            for (int i = 0; i < paths.Count; i++)
            {
                string path = paths[i];
                string dir = Path.GetDirectoryName(path);
                string filename = Path.GetFileName(path);
                string newname = (StringCipher.IsEncoded(filename)) ? StringCipher.Decode(filename) : StringCipher.Encode(filename);
                string newPath = $"{dir}\\{newname}";

                if (File.Exists(newPath))
                {
                    if (MessageBox.Show($"Converting {filename} to {newname} would result in a duplicate file! Would you like to overwrite the existing file?", "Input Required", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        LogUtility.MessageBoxAndLog($"Could not convert {filename} to {newname} because of file conflicts!", "Hash Exception", LogEventLevel.Error);

                        return false;
                    }

                    File.Delete(newPath);
                }

                File.Move(path, newPath);

                Log.Information($"Converted {filename} to {newname}");

                if ((i * 100 / paths.Count) != ((i - 1) * 100 / paths.Count))
                    progress.Report(i);
            }

            hasherWorking = false;

            return true;
        }

        // We do not need to call invoke here because the object is passed in and returned on the calling thread :)
        private void onRDBProgress(int value)
        {
            rdb_prg.Value = value;
        }

        private void onHashProgress(int value)
        {
            hash_prg.Value = value;
        }

        #endregion
    }
}