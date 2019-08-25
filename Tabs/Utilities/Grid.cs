using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
            DataGridViewTextBoxColumn[] columns;
            Daedalus.Structures.Cell[] fields = new Daedalus.Structures.Cell[tManager.RDBCore.CellCount];

            switch (tManager.Style)
            {
                case Style.RDB:
                    columns = new DataGridViewTextBoxColumn[fields.Length];
                    fields = tManager.RDBCore.CellTemplate;

                    tManager.RDBTab.ProgressMax = fields.Length;

                    for (int i = 0; i < fields.Length; i++)
                    {
                        Daedalus.Structures.Cell field = fields[i];

                        columns[i] = new DataGridViewTextBoxColumn()
                        {
                            Name = field.Name,
                            HeaderText = field.Name,
                            Width = 100,
                            Resizable = DataGridViewTriState.True,
                            Visible = field.Visible, /*Show*/
                            SortMode = DataGridViewColumnSortMode.Programmatic
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
                        e.Value = tManager.RDBCore.Rows[e.RowIndex][e.ColumnIndex];
                    }
                    break;

                case Style.DATA:
                   {
                        int rowCount = tManager.DataTab.RowCount;

                        if (rowCount == 0)
                        {
                            string msg = "[Grimoire.Tabs.Utilities.Grid Grid_CellValueNeeded() expects rows, there are none!";

                            Logs.Manager.Instance.Enter(Logs.Sender.MAIN, Logs.Level.ERROR, msg);
                            MessageBox.Show(msg, "Grid Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (e.RowIndex >= rowCount)
                            return;

                        if (tManager.DataTab.Filtered && tManager.DataTab.Searching)
                            e.Value = tManager.DataTab.SearchIndex[e.RowIndex].Name;
                        else if (tManager.DataTab.Filtered && !tManager.DataTab.Searching)
                            e.Value = tManager.DataTab.FilteredIndex[e.RowIndex].Name;
                        else if (!tManager.DataTab.Filtered && tManager.DataTab.Searching)
                            e.Value = tManager.DataTab.SearchIndex[e.RowIndex].Name;
                        else
                            e.Value = tManager.DataCore.Index[e.RowIndex].Name;
                    }
                    break;
            }
        }

        public void Grid_CellPushed(object sender, DataGridViewCellValueEventArgs e)
        {
        }
    }
}
