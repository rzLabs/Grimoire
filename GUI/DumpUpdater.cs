using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Configuration;
using Grimoire.Utilities;

namespace Grimoire.GUI
{
    public partial class DumpUpdater : Form
    {
        ConfigManager configMan = GUI.Main.Instance.ConfigMgr;

        string dumpDir; 

        public DumpUpdater()
        {
            InitializeComponent();

            grid.Columns[0].Width = 275;
            grid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dumpDir = configMan["DumpDirectory", "Grim"];
        }

        private void DumpUpdater_Load(object sender, EventArgs e)
        {
            dumpDirTxtBox.Text = dumpDir;
        }

        private void DumpUpdater_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private async void DumpUpdater_DragDrop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);

            if ((File.GetAttributes(filenames[0]) & FileAttributes.Directory) == FileAttributes.Directory)
                filenames = Directory.GetFiles(filenames[0]);

            statusLb.Text = "Indexing...";

            prgBar.Maximum = filenames.Length;

            await Task.Run(() => { process_files(filenames); });

            prgBar.Maximum = 100;
            prgBar.Value = 0;

            statusLb.Text = $"{filenames.Length} files indexed";
        }

        void process_files(string[] filenames)
        {
            for (int i = 0; i < filenames.Length; i++)
            {
                string filename = filenames[i];
                string name = Path.GetFileName(filename).ToLower();
                string dir = Path.GetDirectoryName(filename);
                int extOffset = (name[name.Length - 3] == '.') ? 2 : 3;
                string dest = name.Substring(name.Length - extOffset);
                bool exists = File.Exists($"{dumpDir}//{dest}//{name}");

                if (!grid.Disposing)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        statusLb.Text = $"Indexing: {name}...";

                        DataGridViewRow dgvr = (DataGridViewRow)grid.RowTemplate.Clone();
                        dgvr.CreateCells(grid);
                        dgvr.Cells[0].Value = name;
                        dgvr.Cells[1].Value = dir;
                        dgvr.Cells[2].Value = dest;
                        dgvr.Cells[3].Value = (exists) ? "Yes" : "No";

                        if (exists)
                            dgvr.DefaultCellStyle.BackColor = Color.FromArgb(255, 124, 124);
                        else
                            dgvr.DefaultCellStyle.BackColor = Color.PaleGreen;

                        grid.Rows.Add(dgvr);

                        if (i * 100 / filenames.Length != ((i - 1) * 100 / filenames.Length))
                            prgBar.Value = i;

                        if (grid.Rows.Count > 0)
                            copyBtn.Enabled = true;
                    }));
                }
            }
        }

        private async void copyBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to copy all the listed files into the choosen dump directory!\n\nDo you want to continue?", "Input Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            bool copy = default;

            int total = grid.Rows.Count;
            int count = total;
            int failCount = 0;

            prgBar.Maximum = total;

            while (count > 0)
            {
                DataGridViewRow row = grid.Rows[0];

                copy = false;

                string name = row.Cells["name"].Value.ToString();
                string sourceFldr = row.Cells["source"].Value.ToString();
                string extFldr = row.Cells["destination"].Value.ToString();
                string destFldr = $"{dumpDir}\\{extFldr}";
                string exists = row.Cells["exists"].Value.ToString();

                string source = $"{sourceFldr}\\{name}";
                string destination = $"{dumpDir}\\{extFldr}\\{name}";

                if (exists == "Yes")
                {
                    if (configMan["OverwriteExisting", "DumpUpdater"])
                        copy = true;

                    if (!copy)
                    {
                        FileInfo srcInfo = new FileInfo(source);
                        FileInfo destInfo = new FileInfo(destination);

                        string srcSize = StringExt.SizeToString(srcInfo.Length);
                        string destSize = StringExt.SizeToString(destInfo.Length);

                        string srcDate = srcInfo.CreationTime.ToString("yyyy-MM-dd");
                        string destDate = destInfo.CreationTime.ToString("yyyy-MM-dd");

                        if (MessageBox.Show($"{name} already exists in the dump!\n\n" +
                            $"Existing File: Created: {destDate} Size: {destSize}\n" +
                            $"New Info: Created: {srcDate} Size: {srcSize}\n\n" +
                            $"Do you want to overwrite it?", "File Conflict", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            copy = true;
                    }
                }
                else
                    copy = true;

                if (copy)
                {
                    if (File.Exists(destination))
                        File.Delete(destination);

                    if (!Directory.Exists(destFldr))
                    {
                        copy = false;
                        MessageBox.Show($"The destination folder: {destFldr} does not exist!", "Directory Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (copy)
                    {
                        try
                        {
                            statusLb.Text = $"Copying {name}...";

                            await Task.Run(() => { File.Copy(source, destination); });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Could not copy {name}\n\nMessage: {ex.Message}", "Exception Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        failCount++;
                }

                grid.Rows.Remove(row);

                prgBar.Value = total - count;

                count--;
            }

            if (failCount > 0)
                MessageBox.Show($"{failCount} files could not be copied into the dump directory!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            prgBar.Maximum = 100;
            prgBar.Value = 0;
            statusLb.Text = string.Empty;
        }

        private void grid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (e.RowCount == 0)
                copyBtn.Enabled = false;
        }
    }
}
