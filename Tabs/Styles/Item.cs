using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Utilities;
using Grimoire.Configuration;
using Grimoire.GUI;
using Grimoire.Structures;

using Serilog;
using Archimedes;
using Archimedes.Cells;
using DataCore;

namespace Grimoire.Tabs.Styles
{
    public partial class Item : UserControl
    {
        ConfigManager configMan = GUI.Main.Instance.ConfigMgr;
        StructureManager structMgr = StructureManager.Instance;

        Core data;

        DataTable selectionTbl = null;

        DataTable itemTbl = null;
        StructureObject itemRDB;

        DataTable stringTbl = null;
        StructureObject stringRDB;

        SPR spr;

        Dictionary<string, Dictionary<string, int>> enums = new Dictionary<string, Dictionary<string, int>>();

        bool structLoaded = true;
        bool useArenaPt = false;

        DataRow selectedItem = null;

        public Item()
        {
            InitializeComponent();
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

            TabManager.Instance.Text = "Item Utility";
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

            string statement = stringRDB.SelectStatement;

            try
            {
                await Task.Run(() => {
                    stringTbl = DatabaseUtility.GetDataTable(statement);
                });
            }
            catch (Exception ex)
            {
                string msg = $"An exception occured while fetching table data!\n\nSQL Statement:\n{statement}\n\nMessage:\n\t{ex.Message}\n\nStack-Trace:\n\t{ex.StackTrace}";

                Log.Error(msg);

                MessageBox.Show(msg, "Load Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        async void loadItems()
        {
            string statement = itemRDB.SelectStatement;

            await Task.Run(() => {
                itemTbl = DatabaseUtility.GetDataTable(statement);
            });

            Log.Information($"{itemTbl?.Rows.Count} items loaded from database.");

            ts_select.Enabled = true;
        }

        void loadSPR()
        {
            string sprDir = configMan.GetDirectory("SprDirectory", "Item");

            if (string.IsNullOrEmpty(sprDir) || !Directory.Exists(sprDir))
                return;

            Task.Run(() =>
            {
                foreach (string filename in Directory.GetFiles(sprDir))
                    if (Path.GetExtension(filename).Remove(0, 1) == "spr")
                        spr.LoadFromFile(filename);
            });
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
                    selectionTbl = DatabaseUtility.GetDataTable(statement);
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

            if (selectedID <= 0)
                return;

            try 
            {
                if (selectedItem != null)
                    selectedItem = null;

                selectedItem = itemTbl.Select($"id = {selectedID}")[0];

                populateControls();
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "selecting item", "Item Select Exception", Serilog.Events.LogEventLevel.Error);

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
            string itemName = stringTbl.Select($"code = {selectedItem["name_id"]}")[0]["value"].ToString();

            if (File.Exists(iconPath))
            {
                Image iconImg = Bitmap.FromFile(iconPath);
                icon_pbx.Image = iconImg;
            }

            Tabs.TabManager.Instance.Text = itemName;

            id_lb.Text = selectedItem["id"].ToString();
            itemName_lb.Text = itemName;

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

        RowObject getRow(RdbType type, string name, object value)
        {
            StructureObject structObj = (type == RdbType.Item) ? itemRDB : stringRDB;

            for (int r = 0; r < structObj.RowCount; r++)
            {
                RowObject row = structObj.Rows[r];

                CellBase cell = row.GetCell(name);
                object val = row[cell.Index];

                if (val.ToString() == value.ToString())
                    return row;
            }

            throw new KeyNotFoundException();
        }

        enum RdbType
        {
            String,
            Item
        }

        private async void Item_Load(object sender, EventArgs e)
        {
            useArenaPt = configMan["UseArenaPoint", "Item"];

            data = (configMan.Get<bool>("UseModifiedXOR", "Data", false)) ? new Core(false, Encoding.Default, configMan.GetByteArray("ModifiedXORKey")) : new Core(false, Encoding.Default);

            if (data.UseModifiedXOR)
            {
                Log.Information($"Using modified xor key:\n");

                if (GUI.Main.Instance.LogLevel.MinimumLevel >= Serilog.Events.LogEventLevel.Debug)
                    Log.Debug($"\n{StringExt.ByteArrayToString(data.XORKey)}");
            }

            spr = new SPR(data);

            int codepage = configMan.Get<int>("Codepage", "Grim");
            Encoding encoding = Encoding.GetEncoding(codepage);

            string rdbStruct = configMan.Get<string>("ItemStruct", "Item", "ItemResource73");
            string strStruct = configMan.Get<string>("StringStruct", "Item", "StringResource");

            itemRDB = await structMgr.GetStruct(rdbStruct);
            itemRDB.Encoding = encoding;
            stringRDB = await structMgr.GetStruct(strStruct);
            stringRDB.Encoding = encoding;

            // Use discard so that compiler doesn't complain that the call isn't awaited. We do not need to wait, continue on.
            _ = loadStrings();
            loadSPR();

            bindEnums();

            ts_status.Text = "Loading items...";

            loadItems();

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
                            catch (Exception ex)
                            {
                                LogUtility.MessageBoxAndLog(ex, "selecting item", "Item Select Exception", Serilog.Events.LogEventLevel.Error);

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