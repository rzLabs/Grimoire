using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Daedalus.Structures;

namespace Grimoire.GUI
{
    public partial class ListInput : Form
    {
        Cell[] cells = null;

        public ListInput()
        {
            InitializeComponent();
        }

        public ListInput(string description, Cell[] cells)
        {
            InitializeComponent();

            Text = description;
            this.cells = cells;

            populateList();
        }

        public ListInput(string description, string[] selections)
        {
            InitializeComponent();

            Text = description;
            list.Items.AddRange(selections);
        }

        void populateList()
        {
            if (cells == null)
                throw new NullReferenceException("cells is null!");

            if (cells.Length == 0)
                throw new ArgumentNullException("cells does not contain any cells!");

            foreach (Cell cell in cells)
                list.Items.Add(cell.Name);
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
