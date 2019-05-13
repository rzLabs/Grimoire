using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Tabs.Enums;
using Grimoire.Tabs.Structures;
using Grimoire.Utilities;
using System.Data.SqlClient;
using DataCore;
using DataCore.Structures;

namespace Grimoire.Tabs.Styles
{
    public partial class Item : UserControl
    {
        ImageList imageList = null;
        ItemInfo[] items;
        Database dManager;
        public Core Core = null;
        int rowCount = 0;
        public int RowCount { get { return rowCount; } }
        SPR spr = null;

        public Item()
        {
            InitializeComponent();
            imageList = new ImageList();
            imageList.ImageSize = new Size(34, 34);
            bool backup = OPT.GetBool("data.backup");
            int codepage = OPT.GetInt("data.encoding");
            Encoding encoding = Encoding.GetEncoding(codepage);
            Core = new Core(backup, encoding);
            spr = new SPR();
            loadCore();
            itemList.LargeImageList = imageList;
            setEnums();
        }

        void loadCore()
        {
            Paths.DefaultDirectory = OPT.GetString("data.load.directory");

            string filePath = Paths.FilePath;
            if (Paths.FileResult != DialogResult.OK)
                return;

            Core.Load(filePath);
        }

        void setEnums()
        {
            wearType_lst.DataSource = Enum.GetValues(typeof(ItemBase.WearType));
            class_lst.DataSource = Enum.GetValues(typeof(ItemBase.Class));
            group_lst.DataSource = Enum.GetValues(typeof(ItemBase.Group));
        }

        void loadList()
        {
            dManager = new Database();
            rowCount = dManager.FetchRowCount("ItemResource");
            items = new ItemInfo[rowCount];

            using (SqlDataReader sqlRdr = dManager.ExecuteReader(Database.SelectItemInfo))
            {
                int rowIdx = 0;

                while (sqlRdr.Read())
                {
                    ItemInfo newItem = new ItemInfo()
                    {
                        ID = sqlRdr[0] as int? ?? default(int),
                        Name = sqlRdr[1].ToString(),
                        Icon_FileName = sqlRdr[2].ToString()
                    };

                    items[rowIdx] = newItem;
                    rowIdx++;
                }
            }

            itemList.VirtualListSize = items.Length;
        }

        Image bytesToImage(byte[] buffer)
        {
            Image ret;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(buffer))
            {
                ret = Image.FromStream(ms);
            }

            return ret;
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            
            loadList();
            spr.LoadAll();
        }

        private void itemList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            
            ItemInfo info = items[e.ItemIndex];
            bool setImageIdx = false;

            string sprName = spr.GetFileName(info.Icon_FileName);
            if (sprName != null)
            {
                sprName = sprName.ToLower();

                IndexEntry entry = Core.GetEntryNoLocale(sprName);
                if (entry != null)
                {
                    byte[] imageBuffer = Core.GetFileBytes(entry);
                    Image image = bytesToImage(imageBuffer);
                    if (!imageList.Images.ContainsKey(info.Icon_FileName))
                    {
                        imageList.Images.Add(image);
                        setImageIdx = true;
                    }
                }
            }

            ListViewItem item = new ListViewItem() { Name = info.ID.ToString(), Text = info.Name };

            if (setImageIdx)
                item.ImageIndex = imageList.Images.Count;

            e.Item = item;
        }

        private void itemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemList.SelectedIndices.Count == 0)
                return;

            int idx = itemList.SelectedIndices[0];
            int id = items[idx].ID;
            
            SqlParameter sqlParam = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            using (SqlDataReader sqlRdr = dManager.ExecuteReader(Database.Select_Item, sqlParam))
            {
                while (sqlRdr.Read())
                {
                    tooltip.Text = sqlRdr[0].ToString();
                    rankInput.Value = sqlRdr[1] as int? ?? 0;
                    lvInput.Value = sqlRdr[2] as int? ?? 0;
                    encInput.Value = sqlRdr[3] as int? ?? 0;
                    socketInput.Value = sqlRdr[4] as int? ?? 0;
                    int wt = sqlRdr[5] as int? ?? 0;
                    wearType_lst.Text = ((ItemBase.WearType)wt).ToString();
                    int cl = sqlRdr[6] as int? ?? 0;
                    class_lst.Text = Enum.GetName(typeof(ItemBase.Class), cl);
                    int grp = sqlRdr[7] as int? ?? 0;
                    group_lst.Text = Enum.GetName(typeof(ItemBase.Group), grp);
                }
            }
        }
    }
}
