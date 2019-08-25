using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Grimoire.Tabs;

namespace Grimoire.GUI
{
    public partial class Main : Form
    {
        readonly Tabs.Manager tManager;
        readonly Logs.Manager lManager;
        public static Main Instance;

        public Main()
        {
            InitializeComponent();
            Instance = this;
            tManager = Tabs.Manager.Instance;
            lManager = Logs.Manager.Instance;           
            Utilities.OPT.Load();
            check_first_start();
            generate_new_list();
            
        }

        private void check_first_start()
        {
            //if (Properties.Settings.Default.FirstStart)
            //{
            //    lManager.Enter(Logs.Sender.MAIN, Logs.Level.DEBUG, "First start detected!\n\tRunning setup...");

            //    using (Setup setup = new Setup())
            //        setup.ShowDialog(this);
            //}
        }

        private void generate_new_list()
        {
            string[] styles = Grimoire.Utilities.OPT.GetString("tab.styles").Trim().Split(',');

            if (styles.Length == 0)
            {
                string msg = "Setting: tab.styles is missing or empty, please add at-least one tab style!";
                MessageBox.Show(msg);
                lManager.Enter(Logs.Sender.MAIN, Logs.Level.ERROR, msg);
            }

            new_list.Items.Clear();
            foreach (string style in styles) { new_list.Items.Add(style); }
        }

        private void set_default_tab()
        {
            string styleName = Grimoire.Utilities.OPT.GetString("tab.default_style");
            bool useDefault = (styleName != null && styleName != "NONE");
            if (useDefault)
            {
                lManager.Enter(Logs.Sender.MAIN, Logs.Level.NOTICE, "Automatically loading default style.");

                tManager.Create((Style)Enum.Parse(typeof(Style), styleName));
            }
        }

        public TabControl TabControl { get { return tabs; } }

        bool hasTabs { get { return tabs.TabPages.Count > 0; } }

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
            string[] paths = ((string[])e.Data.GetData(DataFormats.FileDrop));

            switch (tManager.Style)
            {
                case Style.RDB:
                    tManager.RDBTab.LoadFile(paths[0]);
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

        private void Main_Shown(object sender, EventArgs e)
        {
            set_default_tab(); // If defined!
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            lManager.Enter(Logs.Sender.MAIN, Logs.Level.DEBUG, "Closing down...");
            Utilities.OPT.Save();
        }

        private void tabs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < tabs.TabCount; ++i)
                {
                    Rectangle r = tabs.GetTabRect(i);
                    if (r.Contains(e.Location) /* && it is the header that was clicked*/)
                    {
                        tManager.RightClick_TabIdx = i; // Set in case user intends to destroy the selected tab
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
                        tManager.RDBTab.TS_Load_File_Click(this, EventArgs.Empty);
                        break;
                }
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
            {
                if (tManager.RDBCore.RowCount > 0)
                {
                    using (ListInput input = new ListInput("RDB Search", tManager.RDBCore.CellNames))
                        if (input.ShowDialog(this) == DialogResult.OK)
                            tManager.RDBTab.Search(input.Field, input.Term);
                }
                else
                    lManager.Enter(Logs.Sender.MAIN, Logs.Level.NOTICE, "Cannot activate ListInput without loaded data!");
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            {
                if (tManager.Style == Style.RDB)
                    tManager.RDBTab.TS_Save_File_Click(this, EventArgs.Empty);
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
        }

        private void aboutLbl_Click(object sender, EventArgs e)
        {
            string gVersion = System.Windows.Forms.Application.ProductVersion;
            string dCore_Version = FileVersionInfo.GetVersionInfo("DataCore.dll").FileVersion;
            string rCore_Version = FileVersionInfo.GetVersionInfo("Daedalus.dll").FileVersion;
            string aboutStr = string.Format("Grimoire v{0}\nDataCore v{1}\nDaedalus v{2}\n\nWritten by: iSmokeDrow" + 
                                            "\n\nSpecial Thanks:\n\t- Glandu2\n\t- Gangor\n\t- InkDevil\n\t- XavierDeFawks\n\t- ThunderNikk\n\t- Exterminator\n\t"+
                                            "- Medaion\n\t- AziaMafia \n\n" +
                                            "And a very special thanks to everyone who uses Grimoire! Please report bugs you may find to iSmokeDrow#3102 on Discord!",
                                            gVersion, dCore_Version, rCore_Version);
            MessageBox.Show(aboutStr, "About Me", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
