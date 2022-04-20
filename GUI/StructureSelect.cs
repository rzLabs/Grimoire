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
    // TODO: implement remembering the sort direction of the last sorted column
    public partial class StructureSelect : Form
    {
        StructureManager structMgr = StructureManager.Instance;
        ConfigManager configMgr = Main.Instance.ConfigMgr;

        public string SelectedText;

        public StructureSelect()
        {
            InitializeComponent();

            generateEntries();
        }

        void generateEntries()
        {
            ObjectListView lv_all = new ObjectListView() 
            { 
                Name = "lv_all",
                Dock = DockStyle.None,
                FullRowSelect = true,
                MultiSelect = false,
                ShowGroups = true,
                HasCollapsibleGroups = true,
                Location = new Point(12, 12),
                Size = new Size(621, 312),
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom
            };

            OLVColumn[] columns = new OLVColumn[4];

            columns[0] = new OLVColumn() { Name = "name", Text = "Name", AspectName = "Name", Width = 160 };

            columns[0].GroupKeyGetter = delegate (object RowObject) {
                return ((StructureObject)RowObject)?.Name;
            };

            columns[0].GroupKeyToTitleConverter = delegate(object key) {
                return string.Empty;
            };

            columns[1] = new OLVColumn() { Name = "version", Text = "Version", AspectName = "Version", Width = 100 };
            columns[2] = new OLVColumn() { Name = "epic", Text = "Epic", AspectName = "Epic", Width = 100 };

            columns[2].AspectGetter = delegate (object rowObject) {
                return formatEpic(((StructureObject)rowObject).Epic);
            };

            columns[2].GroupKeyGetter = delegate (object rowObject) {
                return formatEpic(((StructureObject)rowObject)?.Epic);
            };

            columns[3] = new OLVColumn() { Name = "author", Text = "Author", AspectName = "Author", Width = 120 };

            lv_all.AllColumns.AddRange(columns);

            lv_all.RebuildColumns();

            lv_all.DoubleClick += lv_Double_Click;

            lv_all.ColumnClick += Lv_all_ColumnClick;

            lv_all.AboutToCreateGroups += Lv_all_AboutToCreateGroups;

            lv_all.GroupExpandingCollapsing += Lv_all_GroupExpandingCollapsing;

            lv_all.SetObjects(structMgr);

            Controls.Add(lv_all);
        }

        private void Lv_all_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            foreach (var group in e.Groups)
            {
                string key = group.Key.ToString() ?? "nil";

                if (!GroupUtility.Exists(key))
                    continue;

                group.Collapsed = !GroupUtility.Get(key); //OLV saves group status as 'IsExpanded' so !IsExpanded == collapsed
            }
        }

        string formatEpic(float[] epic)
        {
            string epicStr = string.Empty;

            if (epic.Length == 1 && epic[0] == 0)
                epicStr = "All";
            else if (epic.Length == 1)
                epicStr = epic[0].ToString();
            else if (epic.Length == 2)
            {
                if (epic[0] == 0) // min version
                    epicStr += $"All to {epic[1]}";
                else if (epic[1] == 99) //max version
                    epicStr = $"{epic[0]} +"; // Current version on
                else
                    epicStr = $"{epic[0]} - {epic[1]}"; //Min version - Max version
            }

            return epicStr;
        }

        private void Lv_all_GroupExpandingCollapsing(object sender, GroupExpandingCollapsingEventArgs e)
        {
            var key = e.Group.Key.ToString();

            GroupUtility.Set(key, e.IsExpanding); // If true, the group is expanded (open). Otherwise the group is collapsed (closed)

            Task.Run(() => GroupUtility.Save()); // We do not need to wait for this to finish
        }

        private void Lv_all_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ObjectListView lv_all = Controls["lv_all"] as ObjectListView;

            configMgr["SortColumn", "Structures"] = lv_all.Columns[e.Column].Name;
        }

        private void lv_Double_Click(object sender, EventArgs e)
        {
            StructureObject structObj = ((ObjectListView)sender).SelectedObject as StructureObject;

            setResult(structObj.StructName, DialogResult.OK);

            Hide();
        }

        void setResult(string result, DialogResult dlgResult)
        {
            SelectedText = result;

            if (configMgr.Get<bool>("RememberLast", "Structures", false))
                configMgr["LastSelected", "Structures"] = result;

            DialogResult = dlgResult;
        }

        private void struct_select_btn_Click(object sender, EventArgs e)
        {
            StructureObject structObj = ((ObjectListView)Controls["lv_all"]).SelectedObject as StructureObject;

            setResult(structObj.StructName, DialogResult.OK);

            Hide();
        }

        private void selectLast_CheckStateChanged(object sender, EventArgs e) => configMgr["RememberLast", "Structures"] = remSelect_chkBx.Checked;

        private void StructureSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (configMgr.Count > 0) // for some reason every once in a blue moon the ConfigMgr will save 0 settings. Destroying user configs. Lets not do that :(
                _ = configMgr.Save();
        }

        private void StructureSelect_Shown(object sender, EventArgs e)
        {
            if (configMgr.Get<bool>("RememberSort", "Structures"))
            {
                ObjectListView lv_all = Controls["lv_all"] as ObjectListView;

                string sortcol = configMgr.Get<string>("SortColumn", "Structures", "name");

                lv_all.ShowSortIndicators = true;
                lv_all.LastSortColumn = lv_all.AllColumns[lv_all.Columns[sortcol].Index];
                lv_all.Sort();
            }

            if (configMgr.Get<bool>("RememberLast", "Structures")) // Y U NO UPDATE ACTUAL FOCUS!?!?
            { // this shit actually works, the item gets selected! However the highlight bar doesn't get drawn until the user interacts with the control! Defeats the fucking purpose of pre-selecting dunnit!?
                string last_structname = configMgr.Get<string>("LastSelected", "Structures");

                if (last_structname != null)
                {
                    ObjectListView lv_all = Controls["lv_all"] as ObjectListView;

                    foreach (StructureObject structObj in lv_all.Objects)
                        if (structObj.StructName == last_structname)
                            lv_all.SelectObject(structObj, true);
                }
            }

        }

        private void StructureSelect_Load(object sender, EventArgs e)
        {
        }
    }
}
