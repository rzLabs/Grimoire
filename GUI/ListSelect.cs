using System;
using System.Collections;
using System.Collections.Generic;
using DataCore.Structures;
using System.Windows.Forms;
using Grimoire.Utilities;
using Grimoire.Tabs.Structures;

namespace Grimoire.GUI
{
    public partial class ListSelect : Form
    {
        XmlManager xMan = XmlManager.Instance;
        Dictionary<string, int> dictItems = null;
        List<ItemInfo> listItems = null;

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

        /// <summary>
        /// Constructs an instance of ListSelect driven by a Dictionary<string, int> of enums
        /// </summary>
        /// <param name="title">Text to be displayed on the titlebar</param>
        /// <param name="dictionary">Dictionary of enums</param>
        public ListSelect(string title, Dictionary<string, int> dictionary)
        {
            InitializeComponent();

            localize();

            this.Text = title;

            dictItems = dictionary;

            list.DataSource = new BindingSource(dictItems, null);
            list.DisplayMember = "Key";
            list.ValueMember = "Value";
        }

        public ListSelect(string title, List<ItemInfo> items)
        {
            InitializeComponent();

            localize();

            this.Text = title;

            listItems = new List<ItemInfo>(items);

            list.DataSource = new BindingSource(items, null);
            list.DisplayMember = "Name";
            list.ValueMember = "ID";
        }

        public string SelectedText
        {
            get => list.SelectedItem.ToString();
            set => list.SelectedValue = value;
        }

        public int SelectedIndex
        {
            get => list.SelectedIndex;
            set => list.SelectedIndex = value;
        }

        public int SelectedValue
        {
            get => (int)list.SelectedValue;
            set => list.SelectedValue = value;
        }

        void populateList(List<IndexEntry> selections)
        {
            foreach (IndexEntry entry in selections)
                list.Items.Add(entry.Name);
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
