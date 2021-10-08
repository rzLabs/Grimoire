using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.GUI
{
    public partial class ItemSelect : Form
    {
        Dictionary<string, Dictionary<string, int>> enums = null;
        DataTable items = null;
        DataTable searchResults = null;
        DataTable filterResults = null;

        bool searching
        {
            get
            {
                if (searchResults != null)
                    if (searchResults.Rows.Count > 0)
                        return true;

                return false;
            }
        }

        DataTable gridTbl
        {
            get
            {
                if (searching)
                    return searchResults;
                else if (filtered)
                    return filterResults;

                return items;
            }
        }

        bool filtered
        {
            get
            {
                if (filterResults != null)
                    if (filterResults.Rows.Count > 0)
                        return true;

                return false;
            }
        }

        public int SelectedValue
        {
            get
            {
                if (itemGrid.SelectedRows.Count == 0)
                    return -1;

                DataGridViewRow dgvRow = itemGrid.SelectedRows[0];

                int id = Convert.ToInt32(dgvRow.Cells["id"].Value);

                return id;
            }
        }

        public ItemSelect(string title, DataTable items, Dictionary<string, Dictionary<string, int>> enums)
        {
            InitializeComponent();

            Text = title;

            this.items = items;

            this.enums = enums;

            bindEnums();

            configureColumnMapping();

            itemGrid.DataSource = gridTbl;

            itemGrid.Columns["id"].Width = 95;
            itemGrid.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        void bindEnums()
        {
            type_lst.DataSource = new BindingSource(enums["item_type"], null);
            type_lst.DisplayMember = "Key";
            type_lst.ValueMember = "Value";

            wear_type_lst.DataSource = new BindingSource(enums["item_wear_type"], null);
            wear_type_lst.DisplayMember = "Key";
            wear_type_lst.ValueMember = "Value";

            class_lst.DataSource = new BindingSource(enums["item_class"], null);
            class_lst.DisplayMember = "Key";
            class_lst.ValueMember = "Value";

            group_lst.DataSource = new BindingSource(enums["item_group"], null);
            group_lst.DisplayMember = "Key";
            group_lst.ValueMember = "Value";
        }

        private void itemGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.RowIndex > gridTbl.Rows.Count)
                return;

            DataRow row = gridTbl.Rows[e.RowIndex];

            string tooltipStr = $"{row["name"]}\n--------------------------------";

            tooltipStr += $"\n\nTooltip:\n\n{row["tooltip"]}";

            if (row["script_text"].ToString() != " ")
                tooltipStr += $"\n\nScript Text:\n-------------------\n\n{row["script_text"]}";

            tooltipStr += $"\n\nIcon File Name:\n----------------\n\n{row["icon_file_name"]}";

            itemGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = tooltipStr;
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string searchCol = searchType.Text;
            string input = inputTxt.Text;

            if (searchType.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a search type!", "Search Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Invalid input!", "Search Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            string searchStr = $"{searchCol} ";

            if (matchesBtn.Checked)
                searchStr += $"= '{input}'";
            else if (notEqual_btn.Checked)
                searchStr += $"not '{input}'";
            else if (containsBtn.Checked)
                searchStr += $"like '%{input}%'";
            else if (geBtn.Checked)
                searchStr += $"> {input}";
            else if (leBtn.Checked)
                searchStr += $"< {input}";

            searchResults = items.Select(searchStr).CopyToDataTable();

            configureColumnMapping();

            itemGrid.DataSource = gridTbl;
        }

        void configureColumnMapping()
        {
            gridTbl.Columns["tooltip"].ColumnMapping = MappingType.Hidden;
            gridTbl.Columns["type"].ColumnMapping = MappingType.Hidden;
            gridTbl.Columns["group"].ColumnMapping = MappingType.Hidden;
            gridTbl.Columns["class"].ColumnMapping = MappingType.Hidden;
            gridTbl.Columns["wear_type"].ColumnMapping = MappingType.Hidden;
            gridTbl.Columns["script_text"].ColumnMapping = MappingType.Hidden;
            gridTbl.Columns["icon_file_name"].ColumnMapping = MappingType.Hidden;
        }

        private void inputTxt_TextChanged(object sender, EventArgs e)
        {
            if (inputTxt.Text.Length > 2)
                clearSearch_btn.Enabled = true;
            else
                clearSearch_btn.Enabled = false;
        }

        private void clearSearch_btn_Click(object sender, EventArgs e)
        {
            inputTxt.Clear();

            itemGrid.DataSource = items;

            searchResults.Clear();
            searchResults = null;
        }

        private void applyFilters_btn_Click(object sender, EventArgs e)
        {
            string searchStr = string.Empty;

            if (applyTypeFilter_btn.Checked)
            {
                if (type_lst.SelectedIndex == -1)
                {
                    MessageBox.Show("You must select a type to apply a filter with your selected settings!", "Filter Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                searchStr += $"type = {type_lst.SelectedValue} ";
            }

            if (applyGroupFilter_btn.Checked)
            {
                if (group_lst.SelectedIndex == -1)
                {
                    MessageBox.Show("You must select a group to apply a filter with your selected settings!", "Filter Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                if (searchStr.Length > 0)
                    searchStr += "AND ";

                searchStr += $"group = {group_lst.SelectedValue} ";
            }

            if (applyClassFilter_btn.Checked)
            {
                if (class_lst.SelectedIndex == -1)
                {
                    MessageBox.Show("You must select a class to apply a filter with your selected settings!", "Filter Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                if (searchStr.Length > 0)
                    searchStr += "AND ";

                searchStr += $"class = {class_lst.SelectedValue} ";
            }

            if (applyWearType_btn.Checked)
            {
                if (wear_type_lst.SelectedIndex == -1)
                {
                    MessageBox.Show("You must select a class to apply a filter with your selected settings!", "Filter Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                if (searchStr.Length > 0)
                    searchStr += "AND ";

                searchStr += $"wear_type = {wear_type_lst.SelectedValue} ";
            }

            if (searchStr.Length == 0)
            {
                MessageBox.Show("You must select filters to apply first!", "Filter Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            try { 
                filterResults = gridTbl.Select(searchStr).CopyToDataTable();
            } 
            catch {

                MessageBox.Show("No results return for that filter combination, please try another.", "Filter Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            configureColumnMapping();

            itemGrid.DataSource = gridTbl;
        }

        private void clearFilters_btn_Click(object sender, EventArgs e)
        {
            applyTypeFilter_btn.Checked = false;
            applyGroupFilter_btn.Checked = false;
            applyClassFilter_btn.Checked = false;
            applyWearType_btn.Checked = false;

            if (searching)
                itemGrid.DataSource = searchResults;
            else
                itemGrid.DataSource = items;

            filterResults.Clear();
            filterResults = null;
        }

        private void itemGrid_cs_select_btn_Click(object sender, EventArgs e) =>
            DialogResult = DialogResult.OK;

        private void ItemSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (itemGrid.SelectedRows.Count == 1) { 
                    DialogResult = DialogResult.OK;
                    return;
                    }

            MessageBox.Show($"No item selected!", "Select Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void itemGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (itemGrid.SelectedRows.Count == 1) {
                DialogResult = DialogResult.OK;
                return;
                }

            MessageBox.Show($"No item selected!", "Select Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
}
