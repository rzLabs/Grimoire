using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Archimedes;
using Archimedes.Cells;
using Archimedes.Enums;
using Grimoire.Structures;
using Grimoire.Utilities;

using Serilog.Events;

namespace Grimoire.GUI
{
    // TODO: remove whitespace from beginning and end of the input box
    // TODO: check for the presence of , in the input (denoting multiple values or a range, given the checked radio button)
    // TODO: if white-space between value and , remove it
    public partial class RdbSearch : Form
    {
        public KeyValuePair<int, int>[] Results;

        public RdbSearch(StructureObject structObject)
        {
            InitializeComponent();

            structObj = structObject;

            cells_lst.Items.AddRange(structObj.VisibleCellNames);

            cells_lst.SelectedIndex = 0;
        }

        void search_btn_Click(object sender, EventArgs e)
        {
            if (structObj == null)
                return;

            if (cells_lst.SelectedIndex == -1)
                return;

            CellBase cell = structObj.DataCells.Find(c => c.Name == cells_lst.Text);

            if (cell == null)
            {
                LogUtility.MessageBoxAndLog($"Cannot find cell named {cells_lst.Text}", "Search Exception", Serilog.Events.LogEventLevel.Error);

                return;
            }         

            SearchOperator searchOp = SearchOperator.Equal;

            if (notEqual_btn.Checked)
                searchOp = SearchOperator.NotEqual;
            else if (like_btn.Checked)
                searchOp = SearchOperator.Like;
            else if (notlike_btn.Checked)
                searchOp = SearchOperator.NotLike;
            else if (above_btn.Checked)
                searchOp = SearchOperator.Above;
            else if (below_btn.Checked)
                searchOp = SearchOperator.Below;
            else if (between_btn.Checked)
                searchOp = SearchOperator.Between;

            List<object> inputObjects = new List<object>();

            try
            {
                if (input_txtBx.Text.Contains(","))
                {
                    string[] inputStrings = input_txtBx.Text.Split(new char[] { ',' });

                    for (int i = 0; i < inputStrings.Length; i++)
                        inputObjects.Add(Convert.ChangeType(inputStrings[i], cell.PrimaryType));
                }
                else
                    inputObjects.Add(Convert.ChangeType(input_txtBx.Text, cell.PrimaryType));

                Array inputs = Array.CreateInstance(cell.PrimaryType, inputObjects.Count);
                Array.Copy(inputObjects.ToArray(), inputs, inputObjects.Count);

                Results = (KeyValuePair<int, int>[])structObj.Search(cell.Name, inputs, searchOp, SearchReturn.Indicies);

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "searching the Archimedes instance", "Search Exception", LogEventLevel.Error);
                return;
            }

            Hide();
        }

        StructureObject structObj;
    }
}
