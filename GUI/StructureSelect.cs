using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Newtonsoft.Json;
using Grimoire.Configuration;
using Grimoire.Structures;

using Grimoire.Utilities;
using Archimedes;

namespace Grimoire.GUI
{
    public partial class StructureSelect : Form
    {
        readonly ConfigManager configMgr = null;

        readonly StructureManager structMgr = null;

        EpicFlag globalEpic = EpicFlag.EPIC_ALL;

        public string SelectedText { get; private set; } = null;

        public string[] SelectedTexts { get; private set; } = null;

        public StructureSelect(ConfigManager configManager, StructureManager structureManager, bool multiSelect = false)
        {
            InitializeComponent();

            structMgr = structureManager;
            configMgr = configManager;

            loadStructs();

            structGrid.MultiSelect = multiSelect;
        }

        void loadStructs()
        {
            globalEpic = (EpicFlag)configMgr.Get<int>("Epic", "Grim");

            if (structGrid.Rows.Count > 0)
                structGrid.Rows.Clear();

            foreach (StructureObject structObj in structMgr)
            {
                bool add = false;
                string epicStr = "All Epics";

                if (structObj.Epic.Length > 1)
                    epicStr = (structObj.Epic[0] == 0) ? $"All Epics to {structObj.Epic[1]}" : $"Epic {structObj.Epic[0]}+";
                else if (structObj.Epic.Length == 1 && structObj.Epic[0] > 0)
                    epicStr = $"Epic {structObj.Epic[0]}";

                if ((int)globalEpic == 1)
                {
                    add = true;
                }
                else
                {
                    if (globalEpic.HasFlag(EpicFlag.EPIC_ALL) && structObj.Epic[0] == 0)
                        add = true;
                    else
                    {
                        for (int i = 0; i < structObj.Epic.Length; i++)
                        {
                            var enumStr = "EPIC_ALL";

                            if (structObj.Epic[i] > 0 && structObj.Epic[i] < 99)
                                enumStr = $"EPIC_{structObj.Epic[i].ToString().Replace(".", "_")}";

                            var epicFlag = (EpicFlag)Enum.Parse(typeof(EpicFlag), enumStr);

                            if (globalEpic.HasFlag(epicFlag))
                                add = true;
                        }
                    }
                }

                if (add)
                    structGrid.Rows.Add(structObj.Name, structObj.StructName, epicStr, structObj.Author);
            }

            if (configMgr.Get<bool>("RememberLast", "Structures", false))
            {
                string lastName = configMgr.Get<string>("LastSelected", "Structures", "");

                if (lastName is not null)
                {
                    for (int i = 0; i < structGrid.Rows.Count; i++)
                        if (structGrid.Rows[i].Cells[1].Value.ToString() == lastName)
                        {
                            structGrid.ClearSelection();
                            structGrid.CurrentCell = structGrid.Rows[i].Cells[0];
                            structGrid.Rows[i].Selected = true;
                        }
                }
            }

            ts_status_lb.Text = $"Showing {structGrid.Rows.Count} results for {globalEpic.ToString()}";
        }

        private void ts_sel_epics_btn_Click(object sender, EventArgs e)
        {
            using (BitFlag flagEditor = new BitFlag((int)globalEpic, "client_epic"))
            {
                flagEditor.ShowDialog(this);

                if ((int)globalEpic != flagEditor.Flag)
                {
                    configMgr["Epic", "Grim"]  = globalEpic = (EpicFlag)flagEditor.Flag;
                    _ = configMgr.Save();

                    loadStructs();
                }
            }
        }

        private void structGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (structGrid.SelectedRows.Count == 1)
                SelectedText = structGrid.SelectedRows[0].Cells[1].Value.ToString();
            else if (structGrid.SelectedRows.Count > 1)
            {
                SelectedTexts = new string[structGrid.SelectedRows.Count];

                for (int i = 0; i < SelectedTexts.Length; i++)
                    SelectedTexts[i] = structGrid.SelectedRows[i].Cells[1].Value.ToString();         
            }

            if (SelectedText?.Length > 0 || SelectedTexts?.Length > 0)
            {
                if (configMgr.Get<bool>("RememberLast", "Structures", false) && SelectedText?.Length > 0)
                {
                    configMgr["LastSelected", "Structures"] = SelectedText;

                    _ = configMgr.Save();
                }

                DialogResult = DialogResult.OK;

                Hide();
            }
        }

        private void ts_reload_struct_btn_Click(object sender, EventArgs e)
        {
            structGrid.Rows.Clear();

            structMgr.Load();

            loadStructs();
        }
    }
}
