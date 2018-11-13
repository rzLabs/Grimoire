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
            if (tManager.Style == Style.RDB)
            {
                string filePath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

                tManager.RDBTab.LoadFile(filePath);
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
    }
}
