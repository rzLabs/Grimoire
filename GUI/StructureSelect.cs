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

            if (configMgr.Get<bool>("RememberSort", "Structures"))
            {
                ObjectListView lv_all = Controls["lv_all"] as ObjectListView;

                string sortcol = configMgr.Get<string>("SortColumn", "Structures", "name");

                lv_all.Sort(lv_all.AllColumns.Find(c => c.Name == sortcol), SortOrder.Ascending);
            }

            if (configMgr.Get<bool>("RememberLast", "Structures")) // Y U NO UPDATE ACTUAL FOCUS!?!?
            {
                string last_structname = configMgr.Get<string>("LastSelected", "Structures");

                if (last_structname != null)
                {
                    ObjectListView lv_all = Controls["lv_all"] as ObjectListView;
                    StructureObject structObj = lv_all.Objects.Cast<StructureObject>().FirstOrDefault(s => s.StructName == last_structname);

                    if (structObj != null)
                        lv_all.SelectObject(structObj, true);
                }
            }
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

            columns[2].AspectGetter = delegate (object rowObject) 
            {
                StructureObject structObj = rowObject as StructureObject;

                string epicStr = string.Empty;

                if (structObj.Epic.Length == 1 && structObj.Epic[0] == 0)
                    epicStr = "All";
                else if (structObj.Epic.Length == 1)
                    epicStr = structObj.Epic[0].ToString();
                else if (structObj.Epic.Length == 2)
                {
                    if (structObj.Epic[0] == 0) // min version
                        epicStr += $"All to {structObj.Epic[1]}";
                    else if (structObj.Epic[1] == 99) //max version
                        epicStr = $"{structObj.Epic[0]} +"; // Current version on
                    else
                        epicStr = $"{structObj.Epic[0]} - {structObj.Epic[1]}"; //Min version - Max version
                }

                return epicStr;
            };

            columns[3] = new OLVColumn() { Name = "author", Text = "Author", AspectName = "Author", Width = 120 };

            lv_all.AllColumns.AddRange(columns);

            lv_all.RebuildColumns();

            lv_all.SetObjects(structMgr);

            lv_all.DoubleClick += lv_Double_Click;

            lv_all.ColumnClick += Lv_all_ColumnClick;

            lv_all.GroupExpandingCollapsing += Lv_all_GroupExpandingCollapsing;

            Controls.Add(lv_all);
        }

        private void Lv_all_GroupExpandingCollapsing(object sender, GroupExpandingCollapsingEventArgs e)
        {
            string key = e.Group.Key as string;


            throw new NotImplementedException(); // TODO: implement saving 'structgroups' to .json (too much data to store inside the Config.json!)
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
    }
}
