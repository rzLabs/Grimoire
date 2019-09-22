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

        public ListSelect(string title, List<IndexEntry> selections)
        {
            this.Text = title;
            InitializeComponent();
            populateList(selections);
            localize();
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
