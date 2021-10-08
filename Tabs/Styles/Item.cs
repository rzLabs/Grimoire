using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.DB;
using Grimoire.Tabs.Enums;
using Grimoire.Tabs.Structures;
using Grimoire.Utilities;
using Daedalus;
using Daedalus.Structures;
using DataCore;
using DataCore.Structures;
using Grimoire.Configuration;
using Grimoire.GUI;

using Serilog;

namespace Grimoire.Tabs.Styles
{
    public partial class Item : UserControl
    {
        ConfigManager configMan = GUI.Main.Instance.ConfigMan;

        DataCore.Core data = new DataCore.Core(Encoding.Default); // TODO: Load encoding from config

        DataTable selectionTbl = null;

        DataTable itemTbl = null;
        Daedalus.Core itemRDB = new Daedalus.Core();

        DataTable stringTbl = null;
        Daedalus.Core stringRDB = new Daedalus.Core();

        SPR spr;

        DBHelper db = null;

        Dictionary<string, Dictionary<string, int>> enums = new Dictionary<string, Dictionary<string, int>>();

        bool structLoaded = false;
        bool useArenaPt = false;

        DataRow selectedItem = null;

        public Item()
        {
            InitializeComponent();

            configureCores();

            useArenaPt = configMan["UseArenaPoint", "Item"];
        }

        public void Clear()
        {
            selectedItem = null;

            id_lb.Text = string.Empty;
            itemName_lb.Text = string.Empty;

            icon_pbx.Image = null;

            rankInput.Value = 0;
            lvInput.Value = 0;
            encInput.Value = 0;
            socketInput.Value = 0;

            useMinLvInput.Value = 0;
            useMaxLvInput.Value = 0;
            trgMinLvInput.Value = 0;
            trgMaxLvInput.Value = 0;

            type_lst.SelectedIndex = -1;
            class_lst.SelectedIndex = -1;
            wear_type_lst.SelectedIndex = -1;
            group_lst.SelectedIndex = -1;
            effectID_txt.Text = string.Empty;
            weightInput.Value = 0;
            rangeInput.Value = 0;
            summonID_txt.Text = string.Empty;
            materialInput.Value = 0;
            skillID_txt.Text = string.Empty;

            goldInput.Value = 0;
            arenaInput.Value = 0;
            genInput.Value = 0;

            stateID_txt.Text = string.Empty;
            stateTimeInput.Value = 0;
            stateLv.Value = 0;

            cdInput.Value = 0;
            cdGroupInput.Value = 0;
            availablePeriodInput.Value = 0;
            decrease_type_lst.SelectedIndex = -1;
        }

        void configureCores()
        {
            data.UseModifiedXOR = configMan.GetOption<bool>("Data", "UseModifiedXor");
            data.SetXORKey(configMan.GetByteArray("ModifiedXORKey"));

            // TODO: gotta put the modified key error catching here bud

            stringRDB.ProgressMaxChanged += (o, x) =>
            {
                this.Invoke(new MethodInvoker(delegate { 
                    ts_prog_bar.Maximum = x.Maximum;
                }));
            };

            stringRDB.ProgressValueChanged += (o, x) =>
            {
                this.Invoke(new MethodInvoker(delegate {
                    ts_prog_bar.Value = x.Value;
                }));
            };

            itemRDB.ProgressMaxChanged += (o, x) =>
            {
                this.Invoke(new MethodInvoker(delegate {
                    ts_prog_bar.Maximum = x.Maximum; 
                }));
            };
            itemRDB.ProgressValueChanged += (o, x) =>
            {
                this.Invoke(new MethodInvoker(delegate {
                    ts_prog_bar.Value = x.Value;
                }));
            };

            spr = new SPR(data);

            db = new DBHelper(configMan);
        }

        private void ts_select_struct_btn_Click(object sender, EventArgs e)
        {
            string structDir = configMan.GetDirectory("Directory", "RDB");
            string structName = configMan["ItemStruct"];
            string structPath = null;

            using (OpenFileDialog ofDlg = new OpenFileDialog()
            {
                InitialDirectory = structDir,
                DefaultExt = ".lua",
                Title = "Select Structure Definition",
                FileName = $"{structName}.lua"
            })
            {
                if (ofDlg.ShowDialog(this) == DialogResult.OK)
                {
                    if (File.Exists(ofDlg.FileName))
                    {
                        structPath = ofDlg.FileName;

                        ts_struct_name.Text = Path.GetFileNameWithoutExtension(ofDlg.FileName);

                        if (ts_struct_name.Text != configMan["ItemStruct", "Item"])
                        {
                            configMan["ItemStruct", "Item"] = ts_struct_name.Text;
                            configMan.Save();
                        }

                        ts_status.Text = string.Empty;
                    }
                }
                else
                    return;

                Log.Debug($"User selected:\n\t- {ofDlg.FileName}");

                itemRDB.Initialize(ofDlg.FileName);
            }

            if (structPath != null)
            {
                bindEnums();

                structLoaded = true;
            }

            ts_status.Text = "Loading items...";

            loadItems();

            ts_status.Text = string.Empty;

        }

        void bindEnums()
        {
            try {
                enums["item_type"] = itemRDB.GetEnum(configMan["type_enum"]);
                enums["item_group"] = itemRDB.GetEnum(configMan["group_enum"]);
                enums["item_class"] = itemRDB.GetEnum(configMan["class_enum"]);
                enums["item_wear_type"] = itemRDB.GetEnum(configMan["wear_type_enum"]);
                enums["item_decrease_type"] = itemRDB.GetEnum(configMan["decrease_type_enum"]);

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

                decrease_type_lst.DataSource = new BindingSource(enums["item_decrease_type"], null);
                decrease_type_lst.DisplayMember = "Key";
                decrease_type_lst.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                string msg = $"An exception occured while binding the required enums!\nMessage: {ex.Message}\nStack-Trace: {ex.StackTrace}";

                Log.Error(msg);

                MessageBox.Show(msg, "Binding Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        async Task<bool> loadStrings()
        {
            string structDir = configMan.GetDirectory("Directory", "RDB");
            string structName = configMan["StringStruct", "Item"];
            string structPath = $"{structDir}\\{structName}.lua";

            if (File.Exists(structPath))
                stringRDB.Initialize(structPath);

            string statement = generateSelect(RdbType.String);

            await Task.Run(() => { //TODO: I need to be in a try block
                stringTbl = db.GetDataTable(statement);
            });

            ts_select_struct_btn.Enabled = true;

            return true;
        }

        async void loadItems()
        {
            string statement = generateSelect(RdbType.Item);

            await Task.Run(() => {
                itemTbl = db.GetDataTable(statement);
            });

            Log.Information($"{itemTbl?.Rows.Count} items loaded from database.");

            ts_select.Enabled = true;
        }

#pragma warning disable 4014
        async void loadSPR()
        {
            string sprDir = configMan.GetDirectory("SprDirectory", "Item");

            if (string.IsNullOrEmpty(sprDir) || !Directory.Exists(sprDir))
                return;

            Task.Run(() =>
            {
                foreach (string filename in Directory.GetFiles(sprDir)) //TODO: it should be made sure the file is actually a .spr
                    spr.LoadFromFile(filename);
            });
        }
#pragma warning restore 4014

        async void loadData()
        {
            string dataDir = configMan.GetDirectory("DataDirectory", "Item");

            if (Directory.Exists(dataDir))
                await Task.Run(() => {
                    try {
                        data.Load(dataDir);
                    }
                    catch (Exception ex) {
                        Log.Error($"An exception occured loading data index!\nMessage:\n\t{ex.Message}\n\nStack-Trace:\n\t{ex.StackTrace}");
                    }
                });
            else
            {
                Log.Error($"Failed to load data index because {dataDir} does not exist!");
            }
        }

        string generateSelect(RdbType type)
        {
            string statement = "SELECT ";

            Cell[] fieldList = (type == RdbType.Item) ? itemRDB.VisibleCells : stringRDB.VisibleCells;

            foreach (Cell field in fieldList)
                statement += $"[{field.Name}],";

            string tableName = (type == RdbType.Item) ? "ItemResource" : "StringResource"; //TODO: these names should come from config

            statement = string.Format("{0} FROM dbo.{1} with (NOLOCK)", statement.Remove(statement.Length - 1, 1), tableName);

            Log.Debug($"Select statement generated for type: {type.ToString()}\nStatement:\n\t- {statement}");

            return statement;
        }

        private async void ts_select_item_selector_Click(object sender, EventArgs e)
        {
            if (!structLoaded)
            {
                MessageBox.Show("You must select a structure first!", "Structure Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            ts_status.Text = "Loading item selection...";

            string statement = "select i.[id], nStr.[value] as 'name', tStr.[value] as 'tooltip', i.[type], i.[group], i.[class], i.[wear_type], i.[script_text], i.[icon_file_name] from dbo.ItemResource i " +
                               "left join dbo.StringResource nStr on nStr.[code] = i.[name_id] " +
                               "left join dbo.StringResource tStr on tStr.[code] = i.[tooltip_id]";

            await Task.Run(() => {
                try { 
                    selectionTbl = db.GetDataTable(statement);
                }
                catch (Exception ex)
                {
                    Log.Error($"An exception occured while loading the item selection!\nMessage:\n\t{ex.Message}\n\nStack-Trace:\n\t{ex.StackTrace}");
                    return;
                }
            });

            ts_status.Text = string.Empty;

            int selectedID = 0;

            using (GUI.ItemSelect select = new GUI.ItemSelect("Item Selector", selectionTbl, enums))
                if (select.ShowDialog(this) == DialogResult.OK)
                    selectedID = select.SelectedValue;

            if (selectedID == -1)
            {
                string msg = $"Invalid item id: {selectedID}";

                Log.Error(msg);

                MessageBox.Show(msg, "ID Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try 
            {
                if (selectedItem != null)
                    selectedItem = null;

                selectedItem = itemTbl.Select($"id = {selectedID}")[0];

                populateControls();
            }
            catch (Exception ex)
            {
                string msg = $"An exception occured while preparing item id: {selectedID}\n\nMessage:\n\t-ex.Message\n\nStack-Trace:\n\t{ex.StackTrace}";
                
                Log.Error(msg);

                MessageBox.Show(msg, "Search Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    
        void populateControls()
        {
            if (selectedItem == null)
            {
                string msg = "No selected item!";

                Log.Error(msg);

                MessageBox.Show(msg, "Item Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            string dumpDir = $"{configMan.GetDirectory("DumpDirectory", "Grim")}\\jpg";
            string icon_file_name = spr.GetFileName(selectedItem["icon_file_name"].ToString());
            string iconPath = $"{dumpDir}\\{icon_file_name}";

            Image iconImg = Bitmap.FromFile(iconPath);

            icon_pbx.Image = iconImg;

            id_lb.Text = selectedItem["id"].ToString();
            itemName_lb.Text = stringTbl.Select($"code = {selectedItem["name_id"]}")[0]["value"].ToString();

            rankInput.Value = Convert.ToInt32(selectedItem["rank"]);
            lvInput.Value = Convert.ToInt32(selectedItem["level"]);
            encInput.Value = Convert.ToInt32(selectedItem["enhance"]);
            socketInput.Value = Convert.ToInt32(selectedItem["socket"]);

            useMinLvInput.Value = Convert.ToInt32(selectedItem["use_min_level"]);
            useMaxLvInput.Value = Convert.ToInt32(selectedItem["use_max_level"]);
            trgMinLvInput.Value = Convert.ToInt32(selectedItem["target_min_level"]);
            trgMaxLvInput.Value = Convert.ToInt32(selectedItem["target_max_level"]);

            type_lst.SelectedValue = Convert.ToInt32(selectedItem["type"]);
            class_lst.SelectedValue = Convert.ToInt32(selectedItem["class"]);
            wear_type_lst.SelectedValue = Convert.ToInt32(selectedItem["wear_type"]);
            group_lst.SelectedValue = Convert.ToInt32(selectedItem["group"]);
            weightInput.Value = Convert.ToInt32(selectedItem["weight"]);
            rangeInput.Value = Convert.ToInt32(selectedItem["range"]);
            effectID_txt.Text = selectedItem["effect_id"].ToString();
            summonID_txt.Text = selectedItem["summon_id"].ToString();
            skillID_txt.Text = selectedItem["skill_id"].ToString();

            goldInput.Value = Convert.ToInt32(selectedItem["price"]);
            genInput.Value = Convert.ToInt32(selectedItem["huntaholic_point"]);

            stateID_txt.Text = selectedItem["state_id"].ToString();
            stateTimeInput.Value = Convert.ToInt32(selectedItem["state_time"]);
            stateLv.Value = Convert.ToInt32(selectedItem["state_level"]);

            cdInput.Value = Convert.ToInt32(selectedItem["cool_time"]);
            cdGroupInput.Value = Convert.ToInt32(selectedItem["cool_time_group"]);
            availablePeriodInput.Value = Convert.ToInt32(selectedItem["available_period"]);

            decrease_type_lst.SelectedValue = Convert.ToInt32(selectedItem["decrease_type"]);
        }

        Row getRow(RdbType type, string name, object value)
        {
            Daedalus.Core core = (type == RdbType.Item) ? itemRDB : stringRDB;

            for (int r = 0; r < core.RowCount; r++)
            {
                Row row = core[r];

                Cell cell = row.GetCell(name);

                if (cell.Value.ToString() == value.ToString())
                    return row;
            }

            throw new KeyNotFoundException();
        }

        enum RdbType
        {
            String,
            Item
        }

        private void Item_Load(object sender, EventArgs e)
        {
            loadStrings();
            loadSPR();

            ts_status.Text = string.Empty;
        }

        private void editUseFlag_btn_Click(object sender, EventArgs e)
        {
            bool changed = false;
            int useFlag = Convert.ToInt32(selectedItem["item_use_flag"]);
            string flagFile = configMan["ItemUseFlagList", "Item"];

            using (BitFlag flagEditor = new BitFlag(useFlag, flagFile))
                if (flagEditor.ShowDialog(this) == DialogResult.OK && flagEditor.Flag > 0 && flagEditor.Flag != useFlag)
                {
                    changed = true;
                    useFlag = flagEditor.Flag;
                }

            if (changed)
                itemTbl.Select($"id = {selectedItem["id"]}")[0]["item_use_flag"] = useFlag;
        }

        private void ts_select_item_id_Click(object sender, EventArgs e)
        {
            int id = 0;

            using (InputBox input = new InputBox("Enter Item ID", false))
            {
                if (input.ShowDialog(this) == DialogResult.OK)
                {
                    if (input.Value.Length > 0)
                    {
                        if (int.TryParse(input.Value, out id))
                        {
                            if (selectedItem != null)
                                selectedItem = null;

                            try
                            {
                                selectedItem = itemTbl.Select($"id = {id}")[0];

                                populateControls();
                            }
                            catch
                            {
                                string msg = $"An exception occured while preparing item id: {id}";

                                Log.Error(msg);

                                MessageBox.Show(msg, "Search Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;
                            }

                        }
                        else
                        {
                            string msg = "Failed to parse a number from the entered value!";

                            Log.Error(msg);

                            MessageBox.Show(msg, "Input Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }
                    }
                }
            }
        }
    }
}
