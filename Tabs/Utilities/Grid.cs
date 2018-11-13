using System;
using System.Collections.Generic;
using System.Windows.Forms;
using rdbCore.Structures;
using DataCore.Structures;

namespace Grimoire.Tabs.Utilities
{
    public class Grid
    {
        Manager tManager;

        public Grid()
        {
            tManager = Manager.Instance;
        }

        public void GenerateColumns()
        {
            int colCount = 0;
            DataGridViewTextBoxColumn[] columns;
            List<LuaField> fields = null;

            switch (tManager.Style)
            {
                case Style.RDB:
                    colCount = tManager.RDBTab.Core.FieldCount;
                    columns = new DataGridViewTextBoxColumn[colCount];
                    fields = tManager.RDBTab.Core.FieldList;

                    tManager.RDBTab.ProgressMax = fields.Count;

                    for (int i = 0; i < fields.Count; i++)
                    {
                        LuaField field = fields[i];

                        columns[i] = new DataGridViewTextBoxColumn()
                        {
                            Name = field.Name,
                            HeaderText = field.Name,
                            Width = 100,
                            Resizable = DataGridViewTriState.True,
                            Visible = field.Show
                        };

                        tManager.RDBTab.ProgressVal = i;
                    }

                    tManager.RDBTab.ResetProgress();
                    tManager.RDBTab.SetColumns(columns);
                    break;
            }

        }

        public void Grid_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            switch (tManager.Style)
            {
                case Style.RDB:
                    {
                        int rowCount = tManager.RDBCore.RowCount;
                        if (e.RowIndex == rowCount || e.RowIndex > rowCount) { return; }
                        if (e.RowIndex == 0 & rowCount == 0) { return; }
                        Row row = tManager.RDBTab.Core.GetRow(e.RowIndex);
                        e.Value = row[e.ColumnIndex];
                    }
                    break;

                case Style.DATA:
                    {
                        if (tManager.DataTab.Filtered)
                        {
                            int rowCount = tManager.DataTab.FilterCount;
                            if (e.RowIndex == rowCount || e.RowIndex > rowCount) { return; }
                            if (e.RowIndex == 0 & rowCount == 0) { return; }
                            IndexEntry entry = tManager.DataTab.FilteredIndex[e.RowIndex];
                            e.Value = entry.Name;
                        }
                        else
                        {
                            int rowCount = tManager.DataCore.RowCount;
                            if (e.RowIndex == rowCount || e.RowIndex > rowCount) { return; }
                            if (e.RowIndex == 0 & rowCount == 0) { return; }
                            IndexEntry entry = tManager.DataCore.Index[e.RowIndex];
                            e.Value = entry.Name;
                        }                       
                    }
                    break;
            }
        }

        public void Grid_CellPushed(object sender, DataGridViewCellValueEventArgs e)
        {
        }
    }
}
