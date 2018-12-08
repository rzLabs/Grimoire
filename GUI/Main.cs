using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Tabs;
using Grimoire.Logs;

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
            generate_new_list();
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
            {
                settings.ShowDialog(this);
            }
        }

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            //TODO: Move Hasher to here so that we don't have a drag operation per tab style
            // Make sure (FOR HASHER) that we provide proper string[] of paths from possible dir drop
            string[] paths = ((string[])e.Data.GetData(DataFormats.FileDrop));

            switch (tManager.Style)
            {
                case Style.RDB:
                    tManager.RDBTab.LoadFile(paths[0]);
                    break;

                case Style.DATA:
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
            Utilities.OPT.Save();
            lManager.Write();
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
                        tabs_cMenu.Show(tabs, e.Location);
                        break;
                    }
                }
            }
        }
    }
}
