using System;
using System.Collections.Generic;
using DataCore.Structures;
using System.Windows.Forms;
using Grimoire.Utilities;

namespace Grimoire.GUI
{
    public partial class ListSelect : Form
    {
        XmlManager xMan = XmlManager.Instance;

        public ListSelect(string title, List<IndexEntry> selections, string selection = null)
        {
            InitializeComponent();

            this.Text = title;
            populateList(selections);
            localize();

            if (!string.IsNullOrEmpty(selection) && list.Items.Contains(selection))
            {
                for (int i = 0; i < list.Items.Count; i++)
                {
                    string strObj = list.Items[i] as string;

                    if (strObj == selection)
                        list.SelectedIndex = i;
                }
            }
        }

        public ListSelect(string title, string[] items)
        {
            InitializeComponent();

            localize();

            this.Text = title;
           
            list.Items.AddRange(items);

        }

        public ListSelect(string title, List<string> items)
        {
            InitializeComponent();

            localize();

            this.Text = title;

            list.Items.AddRange(items.ToArray());
        }

        public string SelectedText
        {
            get { return list.SelectedItem.ToString(); }
        }

        void populateList(List<IndexEntry> selections)
        {
            foreach (IndexEntry entry in selections)
            {
                list.Items.Add(entry.Name);
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void list_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void localize()
        {
            xMan.Localize(this, Localization.Enums.SenderType.GUI);
        }
    }
}
