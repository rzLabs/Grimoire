using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;
using System.Windows.Forms;
using Grimoire.Structures;
using Grimoire.Configuration;
using Grimoire.DB;

namespace Grimoire.GUI
{
    public partial class SPRGenerator : Form
    {
        string sprName = "01_equip.spr";
        string[] tableName = new string[] { "ItemResource" };
        string sqlQuery = "SELECT icon_file_name FROM dbo.";
        string condition = " WHERE wear_type >= 0";
        Size defSize = new Size(34, 34);
        bool ignoreSize = false;

        List<SprInfo> sprEntries = new List<SprInfo>();

        List<string> ignored = new List<string>();

        ConfigManager configMgr = Main.Instance.ConfigMan;

        public SPRGenerator()
        {
            InitializeComponent();
        }

        private void genBtn_Click(object sender, EventArgs e)
        {
            if (itemBtn.Checked)
            {
                sprName = "02_item.spr";
                condition = " WHERE wear_type < 0";
            }
            else if (skillBtn.Checked)
            {
                sprName = "03_skill.spr";
                tableName = new string[]
                {
                    "SkillResource",
                    "StateResource"
                };
                condition = string.Empty;
                defSize = new Size(20, 20);
                ignoreSize = true;
            }

            if (!ignoreSize)
                ignoreSize = configMgr["IgnoreSize", "SPR"];

            statusLb.ResetText();

            loadEntries();
        }

        async void loadEntries()
        {
            DBHelper dbHelper = new DBHelper(configMgr);
            string sqlCmd = null;

            foreach (string table in tableName)
            {
                statusLb.Text = $"Loading icons from dbo.{table}...";

                sqlCmd = $"{sqlQuery}{table}{condition}";

                if (!await dbHelper.OpenConnection())
                    MessageBox.Show("Failed to connect to the Database!", "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //TODO: review me?

                int rowCount = await dbHelper.Execute($"SELECT COUNT(icon_file_name) FROM dbo.{table}", DB.Enums.DbCmdType.Scalar);
                DbDataReader dbRdr = await dbHelper.Execute(sqlCmd, DB.Enums.DbCmdType.Reader);

                if (dbRdr == null || !dbRdr.HasRows || rowCount <= 0)
                    MessageBox.Show($"Failed to load icons from {table}!", "Load Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                prgBar.Maximum = rowCount;

                int idx = 0;

                while (dbRdr.Read())
                {
                    string name = dbRdr.GetString(0);

                    if (string.IsNullOrEmpty(name))
                        MessageBox.Show($"icon_file_name cannot be blank!", "Load Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        if (sprEntries.FindIndex(s => s.Name == name) == -1)
                            sprEntries.Add(new SprInfo(name));
                    }

                    if ((idx * 100 / rowCount) != ((idx - 1) * 100 / rowCount))
                        prgBar.Value = idx;

                    idx++;
                }

                dbHelper.CloseConnection();

                statusLb.Text = $"{sprEntries.Count} icons loaded!";

                resetProgress();
            }

            compareEntries(); 
        }

        async void compareEntries()
        {
            string dumpDir = configMgr.GetDirectory("DumpDirectory", "Grim");

            if (string.IsNullOrEmpty(dumpDir))
            {
                MessageBox.Show("Dump Directory cannot be empty! Please fill it in properly in the settings menu and try again!", "Invalid Dump Directory", MessageBoxButtons.OK, MessageBoxIcon.Error);

                statusLb.ResetText();

                resetProgress();
                
                return;
            }

            string jpgDir = $"{dumpDir}\\jpg\\";
            int total = sprEntries.Count;
            int prg = 0;

            bool showWarnings = configMgr["ShowWarnings", "SPR"];

            statusLb.Text = "Validating icon entries...";

            prgBar.Maximum = total;

            for (int i = sprEntries.Count - 1; i >= 0; i--)
            {
                SprInfo sprInfo = sprEntries[i];

                string filename = $"{jpgDir}{sprInfo.Frames[0]}";

                if (File.Exists(filename))
                {
                    await Task.Run(() => {
                        using (Bitmap bmp = new Bitmap(filename))
                        {
                            if (bmp != null)
                            {
                                Size size = bmp.Size;

                                sprInfo.SetSize(bmp.Size);

                                if (size != defSize)
                                {
                                    if (!ignoreSize && showWarnings)
                                    {
                                        ignored.Add(sprInfo.Name);
                                        sprEntries.Remove(sprInfo);

                                        MessageBox.Show($"Image size is invalid! {filename}\n\nImage Size: {size.Height},{size.Width}\nExpected Size: {defSize.Height},{defSize.Width}\n\nEntry has been removed!", "Invalid Image Size", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Failed to load image {filename}", "Image Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ignored.Add(sprInfo.Name);
                                sprEntries.Remove(sprInfo);
                            }
                        }
                    });
                }
                else
                {
                    ignored.Add(sprInfo.Name);

                    if (showWarnings)
                        MessageBox.Show($"No matching image for {sprInfo.Name} located in the dump directory!\n\nEntry has been removed!", "Missing Image Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    sprEntries.Remove(sprInfo);
                }

                prg = total - i;

                if ((prg * 100 / total) != ((prg - 1) * 100 / total))
                    prgBar.Value = prg;

                total = sprEntries.Count;
                prgBar.Maximum = total;
            }

            statusLb.Text = $"{sprEntries.Count} icons verified!";

            resetProgress();

            save();
        }

        async void save()
        {
            string buildDir = configMgr.GetDirectory("BuildDirectory", "Grim");
            string sprPath = $"{buildDir}\\{sprName}";

            statusLb.Text = $"Writing {sprName} to disk";

            prgBar.Maximum = sprEntries.Count;

            using (StreamWriter sw = new StreamWriter(sprPath, false))
            {
                for (int i = 0; i < sprEntries.Count; i++)
                {
                    SprInfo sprInfo = sprEntries[i];

                    await Task.Run(() => {
                        sw.WriteLine(sprInfo.Name);
                        sw.WriteLine(sprInfo.FrameCount);
                        sw.WriteLine(sprInfo.Frames[0]);
                        sw.WriteLine($"{sprInfo.Height},{sprInfo.Width}");
                        sw.WriteLine(sprInfo.Unused);
                    });
                    
                    if ((i * 100 / sprEntries.Count) != ((i - 1) * 100 / sprEntries.Count))
                        prgBar.Value = i;
                }
            }

            resetProgress();

            System.Threading.Thread.Sleep(1000);

            statusLb.Text = $"{sprEntries.Count} icons written to {sprName} successfully!";

            showIgnored();

            statusLb.Text = string.Empty;
        }

        void showIgnored()
        {
            using (MessageListBox msgListBox = new MessageListBox("Ignored Icons", "The following icons could not be verified and were ignored", ignored))
                msgListBox.ShowDialog(this);
        }

        void resetProgress()
        {
            prgBar.Maximum = 100;
            prgBar.Value = 0;
        }
    }
}
