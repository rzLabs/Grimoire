using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Windows.Forms;

using Grimoire.Configuration;
using Grimoire.Structures;
using Grimoire.GUI;
using Grimoire.Utilities;

using Log = Serilog.Log;
using Serilog.Events;

namespace Grimoire.Tabs.Styles
{
    public partial class MarketEditor : UserControl
    {
        //TODO: implement reload
        public MarketEditor()
        {
            InitializeComponent();

            configureOptions();
        }

        #region Fields

        readonly string[] loadSelect = new string[]
        {
            "select m.[sort_id], m.[name], m.[code], s.[value], i.[price], m.[price_ratio], i.[huntaholic_point], m.[huntaholic_ratio] from dbo.MarketResource m left join dbo.ItemResource i on i.[id] = m.[code] left join dbo.StringResource s on s.[code] = i.[name_id]",
            "select m.[sort_id], m.[name], m.[code], s.[value], i.[price], m.[price_ratio], i.[huntaholic_point], m.[huntaholic_ratio], i.[arena_point], m.[arena_ratio] from dbo.MarketResource m left join dbo.ItemResource i on i.[id] = m.[code] left join dbo.StringResource s on s.[code] = i.[name_id]"
        };

        readonly string[] searchSelect = new string[]
        {
            "select i.[id], s.[value], i.[price], i.[huntaholic_point] from dbo.ItemResource i inner join dbo.StringResource s on s.[code] = i.[name_id] where s.[value] like @item_name",
            "select i.[id], i.[price], s.[value], i.[huntaholic_point], i.[arena_point] from dbo.ItemResource i inner join dbo.StringResource s on s.[code] = i.[name_id] where s.[value] like @item_name"
        };

        readonly string[] infoSelect = new string[]
        {
            "select s.[value], i.[price], i.[huntaholic_point] from dbo.ItemResource i left join dbo.StringResource s on s.[code] = i.[name_id] where i.[id] = @id",
            "select s.[value], i.[price], i.[huntaholic_point], i.[arena_point] from dbo.ItemResource i left join dbo.StringResource s on s.[code] = i.[name_id] where i.[id] = @id"
        };

        readonly string[] marketInsert = new string[]
        {
            "insert into dbo.MarketResource (sort_id, name, code, price_ratio, huntaholic_ratio) values (@sort_id, @name, @code, @price_ratio, @huntaholic_ratio)",
            "insert into dbo.MarketResource (sort_id, name, code, price_ratio, huntaholic_ratio, arena_ratio) values (@sort_id, @name, @code, @price_ratio, @huntaholic_ratio, @arena_ratio)"
        };

        readonly string[] marketUpdate = new string[]
        {
            "update dbo.MarketResource set code = @code, price_ratio = @price_ratio, huntaholic_ratio = @huntaholic_ratio where sort_id = @sort_id and name = @name",
            "update dbo.MarketResource set code = @code, price_ratio = @price_ratio, huntaholic_ratio = @huntaholic_ratio, arena_ratio = @arena_ratio where sort_id = @sort_id and name = @name"
        };

        readonly string marketDelete = "delete from dbo.MarketResource where sort_id = @sort_id and name = @name";

        readonly string[] itemUpdate = new string[]
        {
            "update dbo.ItemResource set price = @price, huntaholic_point = @huntaholic_point where id = @id",
            "update dbo.ItemResource set price = @price, huntaholic_point = @huntaholic_point, arena_point = @arena_point where id = @id"
        };

        ConfigManager configMgr = GUI.Main.Instance.ConfigMgr;

        bool useArena = false;

        List<MarketEntry> entries;

        List<EntryKey> inserts = new List<EntryKey>();
        List<EntryKey> updates = new List<EntryKey>();
        List<EntryKey> deletes = new List<EntryKey>();

        bool newEntry = false;
        bool editedEntry = false;

        MarketEntry selectedEntry = null;

        #endregion

        #region Properties

        string selected_key => (selectedEntry != null) ? $"{selectedEntry.SortID}:{selectedEntry.MarketName}" : null;

        int selected_sort_id
        {
            get
            {
                int sort_id = -1;

                if (selectedEntry != null)
                    sort_id = selectedEntry.SortID;
                else
                {
                    if (marketList.SelectedItems.Count > 0)
                        if (!int.TryParse(marketList.SelectedItems?[0].Name.Split(':')?[0], out sort_id))
                            MessageBox.Show("Failed to parse selected sort_id from the market list!", "Sort ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return sort_id;
            }
        }

        string selected_market_name => (selectedEntry != null) ? selectedEntry.MarketName : null;

        string selected_item_name
        {
            get
            {
                if (marketList.SelectedItems.Count > 0)
                {
                    string item_name = marketList.SelectedItems[0].Text;

                    if (item_name.EndsWith(" (edited)"))
                        item_name = item_name.Replace(" (edited)", string.Empty);
                    else if (item_name.EndsWith(" (new)"))
                        item_name = item_name.Replace(" (new)", string.Empty);

                    return item_name;
                }

                return null;
            }
        }

        int max_sort_id
        {
            get
            {
                int max_id = -1;

                foreach (MarketEntry entry in entries)
                {
                    if (entry.SortID > max_id)
                        max_id = entry.SortID;
                }

                return max_id + 1;
            }
        }

        #endregion

        #region Events

        private void ts_market_list_SelectedIndexChanged(object sender, EventArgs e)
        {

            clear_selected_entry();

            clear(true);

            string marketName = ts_market_list.Text;

            if (!string.IsNullOrEmpty(marketName))
            {
                List<MarketEntry> matches = entries.FindAll(m => m.MarketName == marketName);

                foreach (MarketEntry entry in matches)
                    marketList.Items.Add(new ListViewItem() { Text = entry.ItemName, Name = $"{entry.SortID}:{entry.MarketName}" });
            }
        }

        private async void ts_load_btn_Click(object sender, EventArgs e)
        {
            DatabaseObject dbObj = new DatabaseObject(DatabaseUtility.ConnectionString);

            if (((ToolStripButton)sender).Text == "Reload")
            {
                ts_market_list.SelectedIndex = -1;
                clear_selected_entry();
                clear(true);
            }

            ts_load_btn.Enabled = false;

            dbObj.CommandText = "select count(*) from dbo.MarketResource";

            if (!await dbObj.Connect())
            {
                MessageBox.Show("Failed to connect to the Database!", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int count = await dbObj.ExecuteScalar<int>();

            if (count <= 0)
                MessageBox.Show("No results can be loaded from the MarketResource table!", "Load Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                progressBar.Maximum = count;

                entries = new List<MarketEntry>(count);

                string cmd = (!useArena) ? loadSelect[0] : loadSelect[1];

                dbObj.CommandText = cmd;

                using (DbDataReader dbRdr = await dbObj.ExecuteReader())
                {
                    int idx = 0;

                    while (dbRdr.Read())
                    {
                        MarketEntry entry = new MarketEntry()
                        {
                            SortID = dbRdr.GetInt32(0),
                            MarketName = dbRdr.GetString(1),
                            Code = dbRdr.GetInt32(2),
                            ItemName = dbRdr.GetString(3),
                            Price = dbRdr.GetInt32(4),
                            PriceRatio = dbRdr.GetDecimal(5),
                            HuntaholicPoint = dbRdr.GetInt32(6),
                            HuntaholicRatio = dbRdr.GetDecimal(7),
                        };

                        if (useArena)
                        {
                            entry.ArenaPoint = dbRdr.GetInt32(8);
                            entry.ArenaRatio = dbRdr.GetDecimal(9);
                        }

                        entries.Add(entry);

                        if ((idx * 100 / count) != ((idx - 1) * 100 / count))
                            progressBar.Value = idx;

                        idx++;
                    }

                    progressBar.Maximum = 100;
                    progressBar.Value = 0;
                }
            }

            dbObj.Disconnect();

            if (entries == null || entries.Count == 0)
            {
                MessageBox.Show("No results loaded from the MarketResource table!", "Load Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (MarketEntry entry in entries)
            {
                if (!ts_market_list.Items.Contains(entry.MarketName))
                    ts_market_list.Items.Add(entry.MarketName);
            }

            if (ts_market_list.Items.Count > 0)
            {
                ts_load_btn.Text = "Reload";
                ts_load_btn.Enabled = true;
                ts_market_list.Enabled = true;
                ts_new_btn.Enabled = true;
                addMarket_btn.Enabled = true;
                remMarket_btn.Enabled = true;
                marketList.Enabled = true;
            }
        }

        private void ts_new_btn_Click(object sender, EventArgs e)
        {
            string marketName = null;

            clear(true);

            using (InputBox input = new InputBox("Enter new Market Name", false))
                if (input.ShowDialog(this) == DialogResult.OK)
                    marketName = input.Value;

            if (!string.IsNullOrEmpty(marketName))
            {
                if (ts_market_list.Items.Contains(marketName))
                {
                    MessageBox.Show("The market name you're attempting to create already exists!", "Duplicate Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ts_market_list.SelectedIndex = ts_market_list.Items.IndexOf(marketName);
                }
                else
                {
                    ts_market_list.Items.Add(marketName);
                    ts_market_list.SelectedIndex = ts_market_list.Items.Count - 1;
                }
            }
        }

        private void ts_save_btn_Click(object sender, EventArgs e)
        {
            if (inserts.Count > 0)
                process_inserts();

            if (updates.Count > 0)
                process_updates();

            if (deletes.Count > 0)
                process_deletes();

            ts_save_btn.Enabled = false;
        }

        private void marketList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (marketList.SelectedIndices.Count == 0 || marketList.SelectedIndices[0] == -1)
                return;

            MarketEntry entry = null;

            if (!newEntry && selectedEntry != null)
                clear_selected_entry();

            if (newEntry && selectedEntry != null && selected_item_name != selectedEntry.ItemName)
                clear_pending_entry();

            if (!newEntry && selectedEntry == null) //User has selected an item from marketList
                entry = entries.Find(m => m.SortID == selected_sort_id && m.ItemName == selected_item_name);

            if (entry != null && selectedEntry == null)
                selectedEntry = entry;
            else if (entry == null && selectedEntry == null)
                return;

            populate_info();
        }

        private async void addMarket_btn_Click(object sender, EventArgs e)
        {
            clear_selected_entry();

            clear();

            int sort_id = max_sort_id;
            string marketName = ts_market_list.Text;

            if (sort_id != -1 && !string.IsNullOrEmpty(marketName))
            {
                if (exists(sort_id, marketName))
                {
                    MessageBox.Show("The Market sort_id, name combination already exists!", "Add Market Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MarketEntry entry = await create_new_entry(sort_id, marketName);

                if (!entry.HasData)
                {
                    MessageBox.Show("Failed to create new market entry!", "Entry Create Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    add_new_entry(entry);

                    // Select the added entry on the display list
                    marketList.Items[selected_key].Selected = true;
                }
            }
        }

        private void remMarket_btn_Click(object sender, EventArgs e)
        {
            if (selectedEntry != null)
            {
                if (MessageBox.Show("You are about to delete a Market Entry!\n\nAre you sure you want to continue?", "Input Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                if (editedEntry)
                    set_edit_state(false);

                deletes.Add(new EntryKey(selectedEntry.SortID, selectedEntry.MarketName));

                bool empty = entries.FindIndex(m => m.MarketName == selectedEntry.MarketName) == -1;

                if (MessageBox.Show($"There are no entries left for {selectedEntry.MarketName}!\n\nWould you like to remove it from the list?", "Input Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    ts_market_list.Items.Remove(selectedEntry.MarketName);

                entries.Remove(selectedEntry);
                marketList.Items.RemoveByKey(selected_key);

                clear_selected_entry();

                if (marketList.Items.Count > 0)
                    marketList.Items[marketList.Items.Count - 1].Selected = true;
            }
        }

        private void save_edits_btn_Click(object sender, EventArgs e)
        {
            if (selectedEntry != null)
            {
                if (newEntry)
                {
                    if (!exists((int)sortID.Value, marketName.Text))
                    {
                        update_selected_entry();
                        entries.Add(selectedEntry);
                        inserts.Add(new EntryKey(selectedEntry.SortID, selectedEntry.MarketName));
                        set_new_state(false);
                    }
                    else
                    {
                        MessageBox.Show("The Market sort_id / market name combination you have used already exists!\n\nPlease try another sort_id!", "Duplicate Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else if (editedEntry)
                {
                    MarketEntry entry = selectedEntry;

                    if (entry != null)
                    {
                        update_selected_entry();

                        //Need to update the stored entry
                        int storedIdx = entries.FindIndex(m => m.SortID == entry.SortID && m.MarketName == entry.MarketName);

                        if (storedIdx == -1)
                            MessageBox.Show("Failed to fetch the index of the stored entry!", "Stored Index Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            entries[storedIdx] = entry;

                            updates.Add(new EntryKey(selectedEntry.SortID, selectedEntry.MarketName));
                            set_edit_state(false);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to locate the original market entry!", "Save Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                save_edits_btn.Enabled = false;
                ts_save_btn.Enabled = true;
            }
        }

        private async void search_btn_Click(object sender, EventArgs e)
        {
            SearchResult result = await query_item_by_name();

            if (result != null)
            {
                itemCode.Text = result.ID.ToString();
                price.Value = result.Price;
                huntaholicPoint.Value = result.HuntaholicPoint;

                if (useArena)
                    arenaPoint.Value = result.ArenaPoint;

                if (marketList.Items.Count > 0)
                    if (marketList.Items[selected_key].Text != result.Name)
                        marketList.Items[selected_key].Text = $"{result.Name} (edited)";
            }
        }

        private void sortID_ValueChanged(object sender, EventArgs e) => set_edit_state(check_for_edits());

        private void marketName_TextChanged(object sender, EventArgs e) => set_edit_state(check_for_edits());

        private void itemCode_TextChanged(object sender, EventArgs e) => set_edit_state(check_for_edits());

        private void price_ValueChanged(object sender, EventArgs e) => set_edit_state(check_for_edits());

        private void priceRatio_ValueChanged(object sender, EventArgs e) => set_edit_state(check_for_edits());

        private void huntaholicPoint_ValueChanged(object sender, EventArgs e) => set_edit_state(check_for_edits());

        private void huntaholicRatio_ValueChanged(object sender, EventArgs e) => set_edit_state(check_for_edits());

        private void arenaPoint_ValueChanged(object sender, EventArgs e) => set_edit_state(check_for_edits());

        private void arenaRatio_ValueChanged(object sender, EventArgs e) => set_edit_state(check_for_edits());

        #endregion

        #region Methods

        bool exists(int sort_id, string marketName) => entries.FindIndex(m => m.SortID == sort_id && m.MarketName == marketName) != -1;

        void update_selected_entry()
        {
            if (selectedEntry != null)
            {
                selectedEntry.SortID = (int)sortID.Value;
                selectedEntry.MarketName = marketName.Text;
                selectedEntry.Code = Convert.ToInt32(itemCode.Text);
                selectedEntry.ItemName = selected_item_name;
                selectedEntry.Price = (int)price.Value;
                selectedEntry.PriceRatio = priceRatio.Value;
                selectedEntry.HuntaholicPoint = (int)huntaholicPoint.Value;
                selectedEntry.HuntaholicRatio = huntaholicRatio.Value;

                if (useArena)
                {
                    selectedEntry.ArenaPoint = (int)arenaPoint.Value;
                    selectedEntry.ArenaRatio = arenaRatio.Value;
                }
            }
        }

        void clear(bool clearList = false)
        {
            if (clearList)
                marketList.Items.Clear();

            sortID.ResetText();
            marketName.ResetText();
            itemCode.ResetText();
            price.Value = 0;
            priceRatio.Value = 0;
            huntaholicPoint.Value = 0;
            huntaholicRatio.Value = 0;

            if (useArena)
            {
                arenaPoint.Value = 0;
                arenaRatio.Value = 0;
            }
        }

        void add_new_entry(MarketEntry entry)
        {
            // Tag this entry as being selected
            selectedEntry = entry;

            marketList.Items.Add(new ListViewItem() { Name = selected_key, Text = entry.ItemName });

            set_new_state(true);
        }

        void populate_info()
        {
            if (selectedEntry == null)
            {
                MessageBox.Show("No market entry selected!", "Populate Info Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            sortID.Text = selectedEntry.SortID.ToString();
            marketName.Text = selectedEntry.MarketName;
            itemCode.Text = selectedEntry.Code.ToString();
            price.Value = selectedEntry.Price;
            priceRatio.Value = selectedEntry.PriceRatio;
            huntaholicPoint.Value = selectedEntry.HuntaholicPoint;
            huntaholicRatio.Value = selectedEntry.HuntaholicRatio;

            if (useArena)
            {
                arenaPoint.Value = selectedEntry.ArenaPoint;
                arenaRatio.Value = selectedEntry.ArenaRatio;
            }
        }

        void configureOptions()
        {
            useArena = configMgr["UseArenaPoint", "Market"];

            if (useArena)
            {
                arenaPoint.Enabled = true;
                arenaRatio.Enabled = true;
            }
        }

        async Task<SearchResult> query_item_by_name()
        {
            string itemName = null;

            while (string.IsNullOrEmpty(itemName))
            {
                using (InputBox input = new InputBox("Please enter the item name", false))
                    if (input.ShowDialog(this) == DialogResult.OK)
                        itemName = input.Value;

                if (string.IsNullOrEmpty(itemName))
                    if (MessageBox.Show("You must enter an item name to continue!", "Empty Name", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) == DialogResult.Cancel)
                        break;
            }

            string selectedName = null;

            if (string.IsNullOrEmpty(itemName))
                MessageBox.Show("You have entered an invalid item name!", "Invalid Item Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                List<SearchResult> searchResults = await queryDBForItems(itemName);

                if (searchResults.Count == 0)
                    MessageBox.Show("Failed to query items from the database!", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    List<string> resultNames = searchResults.Select(r => r.Name).ToList();

                    if (resultNames.Count == 0)
                        MessageBox.Show("Failed to query item names from search results!", "Result Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        using (ListSelect listSelect = new ListSelect("Please select an item", resultNames))
                            if (listSelect.ShowDialog(this) == DialogResult.OK)
                                selectedName = listSelect.SelectedText;

                        if (string.IsNullOrEmpty(selectedName))
                            MessageBox.Show("Invalid selected name!", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            return searchResults.Find(r => r.Name == selectedName);
                    }
                }
            }

            return null;
        }

        async Task<MarketEntry> create_new_entry(int sort_id, string market_name)
        {
            MarketEntry entry = new MarketEntry(sort_id, market_name);

            DialogResult dlgResult = MessageBox.Show("Would you like to search for the item code by name?", "Input Required", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (dlgResult == DialogResult.Cancel)
                return null;
            else if (dlgResult == DialogResult.Yes)
            {
                SearchResult result = await query_item_by_name();

                if (result != null)
                {
                    entry.ItemName = result.Name;
                    entry.Code = result.ID;
                    entry.Price = result.Price;
                    entry.HuntaholicPoint = result.HuntaholicPoint;

                    if (useArena)
                        entry.ArenaPoint = result.ArenaPoint;
                }
            }
            else if (dlgResult == DialogResult.No)
            {
                int itemCode = 0;

                while (itemCode == 0)
                {
                    using (InputBox input = new InputBox("Please enter the item code", false))
                    {
                        if (input.ShowDialog(this) == DialogResult.OK)
                        {
                            if (int.TryParse(input.Value, out itemCode))
                                entry.Code = itemCode;
                            else
                            {
                                if (MessageBox.Show("You have enter an invalid item code!\n\nPlease try again.", "Invalid Code", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                                    break;
                            }
                        }
                    }
                }

                if (itemCode == 0)
                    MessageBox.Show("Invalid Item Code!", "Invalid Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    entry.Code = itemCode;

                    SearchResult result = await queryItemInfo(itemCode);

                    if (result == null)
                        MessageBox.Show("Failed to item information from the database!", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        entry.ItemName = result.Name;
                        entry.Price = result.Price;
                        entry.HuntaholicPoint = result.HuntaholicPoint;

                        if (useArena)
                            entry.ArenaPoint = result.ArenaPoint;
                    }
                }
            }

            return entry;
        }

        private async Task<SearchResult> queryItemInfo(int id)
        {
            string selectCmd = (!useArena) ? infoSelect[0] : infoSelect[1];

            DatabaseObject dbObj = new DatabaseObject(DatabaseUtility.ConnectionString, selectCmd);

            dbObj.Parameters.Clear();
            dbObj.Parameters.Add("@id", SqlDbType.Int).Value = id;

            if (!await dbObj.Connect())
            {
                LogUtility.MessageBoxAndLog("Failed to connect to the database!", "Item Query Failed", LogEventLevel.Error);

                return null;
            }

            using (DbDataReader dbRdr = await dbObj.ExecuteReader())
            {
                while (dbRdr.Read())
                {
                    SearchResult result = new SearchResult();
                    result.ID = id;
                    result.Name = dbRdr.GetString(0);
                    result.Price = dbRdr.GetInt32(1);
                    result.HuntaholicPoint = dbRdr.GetInt32(2);

                    if (useArena)
                        result.ArenaPoint = dbRdr.GetInt32(3);

                    return result;
                }
            }

            dbObj.Disconnect();

            //db.ClearParameters();
            //db.NewCommand(selectCmd);
            //dbObj.Parameters.Add("@id", id, SqlDbType.Int);

            //if (!await db.OpenConnection())
            //    MessageBox.Show("Failed to open a connection to the database!", "SQL Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //else
            //{
            //    using (DbDataReader dbRdr = await db.Execute(DbCmdType.Reader))
            //    {
            //        if (dbRdr.HasRows)
            //        {
            //            while (dbRdr.Read())
            //            {
            //                SearchResult result = new SearchResult();
            //                result.ID = id;
            //                result.Name = dbRdr.GetString(0);
            //                result.Price = dbRdr.GetInt32(1);
            //                result.HuntaholicPoint = dbRdr.GetInt32(2);

            //                if (useArena)
            //                    result.ArenaPoint = dbRdr.GetInt32(3);

            //                return result;
            //            }
            //        }
            //    }

            //    db.CloseConnection();
            //}

            return null;
        }

        private async Task<List<SearchResult>> queryDBForItems(string item_name)
        {
            List<SearchResult> results = new List<SearchResult>();

            string selectCmd = (!useArena) ? searchSelect[0] : searchSelect[1];

            DatabaseObject dbObj = new DatabaseObject(DatabaseUtility.ConnectionString);

            dbObj.CommandText = selectCmd;
            dbObj.Parameters.Add("@item_name", SqlDbType.NVarChar).Value = $"%{item_name}%";

            if (!await dbObj.Connect())
            {
                LogUtility.MessageBoxAndLog("Failed to connect to the database!", "Item Query Failed", LogEventLevel.Error);

                return null;
            }

            using (DbDataReader dbRdr = await dbObj.ExecuteReader())
            {
                if (dbRdr.HasRows)
                {
                    while (dbRdr.Read())
                    {
                        SearchResult result = new SearchResult()
                        {
                            ID = dbRdr.GetInt32(0),
                            Name = dbRdr.GetString(1),
                            Price = dbRdr.GetInt32(2),
                            HuntaholicPoint = dbRdr.GetInt32(3)
                        };

                        if (useArena)
                            result.ArenaPoint = dbRdr.GetInt32(4);

                        results.Add(result);
                    }
                }
            }

            dbObj.Disconnect();

            //db.NewCommand(selectCmd);
            //dbObj.Parameters.Add("@item_name", $"%{item_name}%", SqlDbType.VarChar);

            //if (!await db.OpenConnection())
            //    MessageBox.Show("Failed to open a connection to the database!", "SQL Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //else
            //{
            //    using (DbDataReader dbRdr = await db.Execute(DbCmdType.Reader))
            //    {
            //        if (dbRdr.HasRows)
            //        {
            //            while (dbRdr.Read())
            //            {
            //                SearchResult result = new SearchResult()
            //                {
            //                    ID = dbRdr.GetInt32(0),
            //                    Name = dbRdr.GetString(1),
            //                    Price = dbRdr.GetInt32(2),
            //                    HuntaholicPoint = dbRdr.GetInt32(3)
            //                };

            //                if (useArena)
            //                    result.ArenaPoint = dbRdr.GetInt32(4);

            //                results.Add(result);
            //            }
            //        }
            //    }

            //    db.CloseConnection();
            //}

            return results;
        }

        private bool check_for_edits()
        {
            if (selectedEntry != null)
                if (sortID.Value != selectedEntry.SortID || marketName.Text != selectedEntry.MarketName || itemCode.Text != selectedEntry.Code.ToString() || price.Value != selectedEntry.Price || priceRatio.Value != selectedEntry.PriceRatio ||
                    huntaholicPoint.Value != selectedEntry.HuntaholicPoint || huntaholicRatio.Value != selectedEntry.HuntaholicRatio || useArena && arenaPoint.Value != selectedEntry.ArenaPoint || useArena && arenaRatio.Value != selectedEntry.ArenaRatio) {
                    return true;
                }

            return false;
        }

        void set_edit_state(bool state)
        {
            if (selectedEntry != null && !newEntry) //We don't want to enable the edited state for a new entry
            {
                editedEntry = state;

                if (state && !selected_item_name.EndsWith(" (new)"))
                    marketList.Items[selected_key].Text = $"{selectedEntry.ItemName} (edited)";
                else if (!state && selected_item_name != null)
                    marketList.Items[selected_key].Text = selectedEntry.ItemName;

                save_edits_btn.Enabled = state;
            }
        }

        void set_new_state(bool state)
        {
            newEntry = state;

            if (selectedEntry != null)
            {
                if (state)
                    marketList.Items[selected_key].Text = $"{selectedEntry.ItemName} (new)";
                else
                    marketList.Items[selected_key].Text = selectedEntry.ItemName;

                save_edits_btn.Enabled = true;
            }
        }

        void clear_selected_entry()
        {
            if (selectedEntry != null)
            {
                set_edit_state(false);
                selectedEntry = null;
            }
        }

        void clear_pending_entry()
        {
            marketList.Items.RemoveByKey(selected_key);

            newEntry = false;
            selectedEntry = null;

            if (marketList.SelectedItems.Count > 0)
                marketList.Items[marketList.SelectedItems[0].Index].Selected = true;
            else if (marketList.Items.Count > 0)
                marketList.Items[marketList.Items.Count - 1].Selected = true;
            //TODO: else we should ask if the user wants to delete this market name
        }

        async void process_inserts()
        {
            DatabaseObject dbObj = new DatabaseObject(DatabaseUtility.ConnectionString);

            if (!await dbObj.Connect())
            {
                MessageBox.Show("Failed to open a connection to the database!", "SQL Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int total = inserts.Count;
            int progress = 0;

            progressBar.Maximum = total;

            for (int i = total - 1; i >= 0; i--)
            {
                EntryKey key = inserts[i];

                if (key != null)
                {
                    MarketEntry entry = entries.Find(m => m.SortID == key.SortID && m.MarketName == key.MarketName);

                    if (entry != null)
                    {
                        string cmd = (!useArena) ? marketInsert[0] : marketInsert[1];

                        dbObj.CommandText = cmd;
                        dbObj.Parameters.Add("@sort_id", SqlDbType.Int).Value = entry.SortID;
                        dbObj.Parameters.Add("@name", SqlDbType.VarChar).Value = entry.MarketName;
                        dbObj.Parameters.Add("@code", SqlDbType.Int).Value = entry.Code;
                        dbObj.Parameters.Add("@price_ratio", SqlDbType.Decimal).Value = entry.PriceRatio;
                        dbObj.Parameters.Add("@huntaholic_ratio", SqlDbType.Decimal).Value = entry.HuntaholicRatio;

                        if (useArena)
                            dbObj.Parameters.Add("@arena_ratio", SqlDbType.Decimal).Value = entry.ArenaRatio;

                        if (await dbObj.ExecuteNonQuery() == -99) 
                        {
                            LogUtility.MessageBoxAndLog($"Insert for {entry.MarketName} - {entry.Code} failed!", "Process Insert Exception", LogEventLevel.Error);

                            return;
                        }

                        cmd = (!useArena) ? itemUpdate[0] : itemUpdate[1];

                        dbObj.Parameters.Clear();
                        dbObj.CommandText = cmd;
                        dbObj.Parameters.Add("@price", SqlDbType.Int).Value = entry.Price;
                        dbObj.Parameters.Add("@huntaholic_point", SqlDbType.Int).Value = entry.HuntaholicPoint;
                        dbObj.Parameters.Add("@id", SqlDbType.Int).Value = entry.Code;

                        if (useArena)
                            dbObj.Parameters.Add("@arena_point", SqlDbType.Int).Value = entry.ArenaPoint;

                        int rows = await dbObj.ExecuteNonQuery();

                        Log.Debug($"{rows} inserted into the MarketResource table.");
                    }
                }

                inserts.RemoveAt(i);

                progress = total - i;

                if ((i * 100 / total) != ((i - 1) * 100 / total))
                    progressBar.Value = progress;
            }

            dbObj.Disconnect();

            reset_progress();
        }

        async void process_updates()
        {
            DatabaseObject dbObj = new DatabaseObject(DatabaseUtility.ConnectionString);

            if (!await dbObj.Connect())
            {
                MessageBox.Show("Failed to open a connection to the database!", "SQL Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int total = updates.Count;
            int progress = 0;

            progressBar.Maximum = total;

            for (int i = total - 1; i >= 0; i--)
            {
                EntryKey key = updates[i];

                if (key != null)
                {
                    MarketEntry entry = entries.Find(m => m.SortID == key.SortID && m.MarketName == key.MarketName);

                    if (entry != null)
                    {
                        string cmd = (!useArena) ? marketUpdate[0] : marketUpdate[1];

                        dbObj.CommandText = cmd;
                        dbObj.Parameters.Add("@sort_id", SqlDbType.Int).Value = entry.SortID;
                        dbObj.Parameters.Add("@name", SqlDbType.VarChar).Value = entry.MarketName;
                        dbObj.Parameters.Add("@code", SqlDbType.Int).Value = entry.Code;
                        dbObj.Parameters.Add("@price_ratio", SqlDbType.Decimal).Value = entry.PriceRatio;
                        dbObj.Parameters.Add("@huntaholic_ratio", SqlDbType.Decimal).Value = entry.HuntaholicRatio;

                        if (useArena)
                            dbObj.Parameters.Add("@arena_ratio", SqlDbType.Decimal).Value = entry.ArenaRatio;

                        int rows = await dbObj.ExecuteNonQuery();

                        Log.Debug($"{rows} updated in the MarketResource table.");

                        cmd = (!useArena) ? itemUpdate[0] : itemUpdate[1];

                        dbObj.Parameters.Clear();
                        dbObj.CommandText = cmd;
                        dbObj.Parameters.Add("@price", SqlDbType.Int).Value = entry.Price;
                        dbObj.Parameters.Add("@huntaholic_point", SqlDbType.Int).Value = entry.HuntaholicPoint;
                        dbObj.Parameters.Add("@id", SqlDbType.Int).Value = entry.Code;

                        if (useArena)
                            dbObj.Parameters.Add("@arena_point", SqlDbType.Int).Value = entry.ArenaPoint;

                        rows = await dbObj.ExecuteNonQuery();

                        Log.Debug($"{rows} inserted into the ItemResource table.");
                    }

                    updates.RemoveAt(i);

                    progress = total - i;

                    if ((i * 100 / total) != ((i - 1) * 100 / total))
                        progressBar.Value = progress;
                }
            }

            dbObj.Disconnect();

            reset_progress();
        }

        async void process_deletes()
        {
            DatabaseObject dbObj = new DatabaseObject(DatabaseUtility.ConnectionString);

            if (!await dbObj.Connect())
            {
                MessageBox.Show("Failed to open a connection to the database!", "SQL Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int total = inserts.Count;
            int progress = 0;

            progressBar.Maximum = total;

            for (int i = total - 1; i >= 0; i--)
            {
                EntryKey key = deletes[i];

                if (key != null)
                {
                    MarketEntry entry = entries.Find(m => m.SortID == key.SortID && m.MarketName == key.MarketName);

                    if (entry != null)
                    {
                        string cmd = marketDelete;

                        dbObj.CommandText = cmd;
                        dbObj.Parameters.Add("@sort_id", SqlDbType.Int).Value = entry.SortID;
                        dbObj.Parameters.Add("@name", SqlDbType.VarChar).Value = entry.MarketName;

                        int rows = await dbObj.ExecuteNonQuery();

                        Log.Debug($"{rows} deleted from the MarketResource table.");
                    }
                }

                inserts.RemoveAt(i);

                progress = total - i;

                if ((i * 100 / total) != ((i - 1) * 100 / total))
                    progressBar.Value = progress;
            }

            dbObj.Disconnect();

            reset_progress();
        }

        void reset_progress()
        {
            progressBar.Maximum = 100;
            progressBar.Value = 0;
        }

        #endregion
    }
}