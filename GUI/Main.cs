using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grimoire.Tabs;
using Grimoire.Utilities;
using Grimoire.Configuration;
using Grimoire.Structures;

using Serilog;
using Serilog.Core;
using Serilog.Events;

// TODO: upgrade all config calls to use new overload 

namespace Grimoire.GUI
{
    public partial class Main : Form
    {
        readonly Tabs.TabManager tManager;
        
        readonly XmlManager xMan;

        public LoggingLevelSwitch LogLevel = new LoggingLevelSwitch(LogEventLevel.Verbose);

        public static Main Instance;
        public readonly ConfigManager ConfigMgr;

        public Main()
        {
            InitializeComponent();

            configLogger();

            Log.Information("Starting Grimoire...");

            Instance = this;

            ConfigMgr = new ConfigManager();

            tManager = Tabs.TabManager.Instance;
            xMan = XmlManager.Instance;

            setLogLevel();

            check_first_start();

            Log.Information("Starting Structure Manager...");

            // initialize Struct manager (we want it to execute without us waiting)
            Task.Run(() => { StructureManager.Instance.Load(ConfigMgr.GetDirectory("Directory", "Structures")); });

            localize();
        }

        void configLogger()
        {
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.ControlledBy(LogLevel)
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}]{Message:lj}{NewLine}{Exception}")
                    .WriteTo.File(".\\Logs\\Grimoire-Log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {Message:lj}{NewLine}{Exception}")
                    .CreateLogger();
        }

        void setLogLevel()
        {
            int logLv = ConfigMgr.Get<int>("Level", "Log");

            LogLevel.MinimumLevel = (LogEventLevel)logLv;
        }

        private void localize() => xMan.Localize(this, Localization.Enums.SenderType.GUI);

        private void check_first_start() //TODO: properly implement first start wizard
        {
            //if (Properties.Settings.Default.FirstStart)
            //{
            //    lManager.Enter(Logs.Sender.MAIN, Logs.Level.DEBUG, "First start detected!\n\tRunning setup...");

            //    using (Setup setup = new Setup())
            //        setup.ShowDialog(this);
            //}
        }

        public TabControl TabControl => tabs;

        bool hasTabs => tabs.TabPages.Count > 0;

        private void new_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (new_list.SelectedIndex != -1)
            {
                tManager.Create((Style)Enum.Parse(typeof(Style), new_list.Text));
                new_list.SelectedIndex = -1;
            }               
        }

        private void tabs_cMenu_close_Click(object sender, EventArgs e)
        {
            if (hasTabs)
                tManager.Destroy();
        }

        private void tabs_cMenu_clear_Click(object sender, EventArgs e)
        {
            if (hasTabs)
                tManager.Clear();
        }

        private void settings_Click(object sender, EventArgs e)
        {
            using (GUI.Settings settings = new GUI.Settings())
                settings.ShowDialog(this);
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);

            switch (tManager.Style)
            {
                case Style.RDB:
                    tManager.RDBTab.ReadFile(paths[0]);
                    break;

                case Style.DATA:
                    if (tManager.DataCore.RowCount > 0)
                        tManager.DataTab.Insert(paths);
                    else
                        tManager.DataTab.Load(paths[0]);
                    break;

                case Style.HASHER:
                    tManager.HashTab.Add_Dropped_Files(paths);
                    break;
            }
        }

        private void Main_Shown(object sender, EventArgs e) =>
            tManager.Create(Style.LAUNCHER);

        private void Main_FormClosing(object sender, FormClosingEventArgs e) => 
            Log.Information("Grimoire shutting down...");

        private void tabs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < tabs.TabCount; ++i)
                {
                    Rectangle r = tabs.GetTabRect(i);

                    if (r.Contains(e.Location)) /* && it is the header that was clicked*/
                    {
                        tManager.RightClick_TabIdx = i; // Set in case user intends to destroy the selected tab

                        //If this is a launcher tab, disable buttons
                        bool enabled = (tManager.RightClick_TabIdx == 0) ? false : true;

                        tabs_cMenu.Items[0].Enabled = enabled;
                        tabs_cMenu.Items[1].Enabled = enabled;

                        tabs_cMenu.Show(tabs, e.Location);
                        break;
                    }
                }
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.O)
            {
                switch (tManager.Style)
                {
                    case Style.DATA:
                        tManager.DataTab.TS_File_Load_Click(this, EventArgs.Empty);
                        break;

                    case Style.RDB:
                        tManager.RDBTab.TS_Load_Click(this, EventArgs.Empty);
                        break;
                }
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F) // TODO: implement searching the Arc instance for data
            {
                //if (tManager.ArcInstance.RowCount > 0)
                //{
                //    using (ListInput input = new ListInput("RDB Search", tManager.ArcInstance.VisibleCellNames))
                //        if (input.ShowDialog(this) == DialogResult.OK)
                //            tManager.RDBTab.Search(input.Field, input.Term);
                //}
                //else
                //    Log.Information("Cannot activate ListInput without loaded data!");
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            {
                if (tManager.Style == Style.RDB)
                    tManager.RDBTab.TS_Save_File_RDB_Click(this, EventArgs.Empty);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
            {
                if (tManager.Style == Style.DATA)
                    tManager.DataTab.TS_File_New_Click(this, EventArgs.Empty);
                else
                    tManager.Create(tManager.Style);
            }
            else if (e.Modifiers == Keys.ShiftKey && e.KeyCode == Keys.N)
            {
                if (tManager.Style == Style.DATA)
                    tManager.Create(Style.DATA);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.R)
            {
                if (tManager.Style == Style.DATA)
                    tManager.DataTab.TS_File_Rebuild_Click(this, EventArgs.Empty);
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.L)
            {
                xMan.RefreshLocale();

                xMan.Localize(this, Localization.Enums.SenderType.GUI);

                switch (tManager.Style)
                {
                    case Style.DATA:
                        tManager.DataTab.Localize();
                        break;

                    case Style.RDB:
                        tManager.RDBTab.Localize();
                        break;

                    case Style.HASHER:
                        tManager.HashTab.Localize();
                        break;
                }
            }
        }

        private void aboutLbl_Click(object sender, EventArgs e)
        {
            string gVersion = System.Windows.Forms.Application.ProductVersion;
            string dCore_Version = FileVersionInfo.GetVersionInfo("DataCore.dll").FileVersion;
            string rCore_Version = FileVersionInfo.GetVersionInfo("Daedalus.dll").FileVersion;
            string aboutStr = string.Format("Grimoire v{0}\nDataCore v{1}\nDaedalus v{2}\n\nWritten by: iSmokeDrow\n\n" + 
                                            "Third-Party Software:\n\t-Newtonsoft.JSON\n\t-SeriLog\n\t-MoonSharp\n\t-Be.HexBox\n\t-BrightIdeaSoftware.ObjectListView\n" +
                                            "\n\nSpecial Thanks:\n\t- Glandu2\n\t- Gangor\n\t- InkDevil\n\t- XavierDeFawks\n\t- ThunderNikk\n\t- Exterminator\n\t"+
                                            "- Medaion\n\t- AziaMafia\n\t- ADRENALINE\n\t- Musta2\n\t- OceanWisdom\n\t- Sandro\n\t- Smashley\n\t- Bernyy\n\n" +
                                            "And a very special thanks to everyone who uses Grimoire! Please report bugs you may find to iSmokeDrow#3102 on Discord!",
                                            gVersion, dCore_Version, rCore_Version);
            MessageBox.Show(aboutStr, "About Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ts_bitflag_editor_Click(object sender, EventArgs e)
        {
            using (BitFlag bitEditor = new BitFlag())
                bitEditor.ShowDialog(this);
        }

        private void ts_log_viewer_Click(object sender, EventArgs e)
        {
            // TODO: log viewer must be completely rewritten to use Serilog
        }

        private void ts_spr_gen_Click(object sender, EventArgs e)
        {
            using (SPRGenerator sprGenerator = new SPRGenerator())
                sprGenerator.ShowDialog(this);
        }

        private void ts_dump_updater_Click(object sender, EventArgs e)
        {
            using (DumpUpdater dumpUpdater = new DumpUpdater())
                dumpUpdater.ShowDialog(this);
        }

        private void ts_xor_editor_Click(object sender, EventArgs e)
        {
            using (XOREditor xorEditor = new XOREditor())
                xorEditor.ShowDialog(this);
        }

        private void ts_test_btn_Click(object sender, EventArgs e)
        {
            tManager.Create(Style.RDB2);
            //string structPath = $"{System.IO.Directory.GetCurrentDirectory()}\\Structures\\StringResource_TEST.lua";
            //string rdbPath = $"{System.IO.Directory.GetCurrentDirectory()}\\Output\\db_string.rdb";

            //Structures.StructureObject structObj = new Structures.StructureObject(structPath);

            //Stopwatch sw = new Stopwatch();

            //sw.Start();

            //structObj.Read(rdbPath);

            //sw.Stop();

            //MessageBox.Show($"{structObj.Rows.Count} rows loaded in {StringExt.MilisecondsToString(sw.ElapsedMilliseconds)}");
        }
    }
}