using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Utilities;

namespace Grimoire.GUI
{
    public partial class ListInput : Form
    {
        XmlManager xMan = XmlManager.Instance;

        public ListInput()
        {
            InitializeComponent();
            localize();
        }

        public ListInput(string description, string[] selections)
        {
            InitializeComponent();

            Text = description;
            list.Items.AddRange(selections);
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            if (list.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select a field first!", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (input.Text.Length == 0)
            {
                MessageBox.Show("You must enter a search term first!", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            DialogResult = DialogResult.OK;
            Hide();
        }

        private void localize()
        {
            xMan.Localize(this, Localization.Enums.SenderType.GUI);
        }

        private void ListInput_Shown(object sender, EventArgs e)
        {
            list.Text = (string)list.Items[0];
            input.Focus();
        }

        public string Field
        {
            get { return list.Text; }
        }

        public string Term
        {
            get { return input.Text; }
        }     
    }
}
