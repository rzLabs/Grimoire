﻿using System;
using System.IO;
using System.Windows.Forms;
using DataCore.Functions;
using Grimoire.Utilities;
using Grimoire.Configuration;
using System.Threading.Tasks;

using Serilog;

namespace Grimoire.Tabs.Styles
{
    public partial class Hasher : UserControl
    {
        #region Properties
        XmlManager xMan = XmlManager.Instance;
        ConfigManager configMan = GUI.Main.Instance.ConfigMgr;
        Tabs.TabManager tMan = Tabs.TabManager.Instance;
        #endregion

        #region Constructors
        public Hasher()
        {
            InitializeComponent();

            Log.Information("Hasher Utility started!");
            
            set_checks();
            
            localize();
        }
        #endregion

        #region Events

        private void input_TextChanged(object sender, EventArgs e)
        {
            if (input.Text.Length > 3)
                output.Text = (StringCipher.IsEncoded(input.Text)) ? StringCipher.Decode(input.Text) : StringCipher.Encode(input.Text);
            else if (input.Text.Length == 0)
                output.ResetText();
        }

        private void cMenu_clear_Click(object sender, EventArgs e) => fileGrid.Rows.Clear();

        private void flipBtn_Click(object sender, EventArgs e)
        {
            if (output.Text.Length > 3)
                input.Text = output.Text;
        }

        private void cMenu_add_file_Click(object sender, EventArgs e)
        {
            string path = Paths.FilePath;

            if (Paths.FileResult != DialogResult.OK)
                return;

            add_file_to_grid(path);
        }

        private void cMenu_convert_Click(object sender, EventArgs e)
        {
            if (fileGrid.SelectedRows.Count > 0)
                convertEntry(fileGrid.SelectedRows[0].Index);

            if (autoClear_chk.Checked)
                fileGrid.Rows.Remove(fileGrid.SelectedRows[0]);
        }

        private void cMenu_convert_all_Click(object sender, EventArgs e) => convertAllEntries();

        private void autoClear_chk_CheckStateChanged(object sender, EventArgs e) =>
            configMan["AutoConvert", "Hash"] = autoClear_chk.Checked;

        private void opt_auto_convert_CheckStateChanged(object sender, EventArgs e) =>
            configMan["AutoClear", "Hash"] = autoClear_chk.Checked;

        private void cMenu_add_folder_Click(object sender, EventArgs e)
        {
            string directory = Grimoire.Utilities.Paths.FolderPath;

            if (Paths.FolderResult != DialogResult.OK)
                return;

            foreach (string path in Directory.GetFiles(directory))
                add_file_to_grid(path);
        }

        #endregion

        #region Methods (Public)

        public void Add_Dropped_Files(string[] paths)
        {
            if (paths.Length == 1)
                if (File.GetAttributes(paths[0]).HasFlag(FileAttributes.Directory))
                    paths = Directory.GetFiles(paths[0]);

            foreach (string path in paths)
                add_file_to_grid(path);

            if (autoConvert_chk.Checked)
                convertAllEntries();
        }

        public void Localize() => localize();

        #endregion

        #region Methods (private)

        private void set_checks()
        {
            autoClear_chk.Checked = configMan["AutoClear", "Hash"];
            autoConvert_chk.Checked = configMan["AutoConvert", "Hash"];

            switch (configMan["Type", "Hash"])
            {
                case 1:
                    optAppend_ascii_rBtn.Checked = true;
                    break;

                case 2:
                    optRemove_ascii_rBtn.Checked = true;
                    break;

                default:
                    optNone_rBtn.Checked = true;
                    break;
            }
        }

        private void convertAllEntries()
        {
            prgBar.Maximum = fileGrid.Rows.Count;

            for (int i = 0; i < fileGrid.Rows.Count; i++)
            {
                DataGridViewRow row = fileGrid.Rows[i];

                if (!convertEntry(i))
                    return;

                prgBar.Value = i;
            }

            prgBar.Maximum = 100;
            prgBar.Value = 0;

            if (autoClear_chk.Checked)
                cMenu_clear_Click(null, EventArgs.Empty);
        }

        private bool convertEntry(int rowIndex)
        {
            DataGridViewRow row = fileGrid.Rows[rowIndex];
            // [0] - Original Name
            // [1] - Converted Name
            // [2] - File Directory
            // [3] - Conversion Status

            string originalPath = string.Format(@"{0}\{1}", (string)row.Cells[2].Value, (string)row.Cells[0].Value);
            string convertedPath = string.Format(@"{0}\{1}", (string)row.Cells[2].Value, (string)row.Cells[1].Value);

            if (File.Exists(convertedPath))
            {
                if (MessageBox.Show("The conversion of this file: {0} will result in overwriting a file with the same converted name.\nDo you wish to continue?", "File Already Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
                    return false;

                Log.Information($"Converted path already exists, deleting file at:\n\t- {convertedPath}");

                File.Delete(convertedPath);                
            }

            if (File.Exists(originalPath))
            {
                File.Move(originalPath, convertedPath);
                
                fileGrid.Rows[rowIndex].Cells[3].Value = "Complete";

                Log.Information($"Filename: {(string)row.Cells[0]?.Value} converted to: {(string)row.Cells[1]?.Value}");
                
                return true;
            }
            else
            {
                string msg = $"Cannot find the original file: {originalPath}!";

                Log.Error(msg);
                
                MessageBox.Show(msg, "Hasher Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private void add_file_to_grid(string path)
        {
            string originalName = Path.GetFileName(path);
            bool IsEncoded = StringCipher.IsEncoded(originalName);
            string outNameEncoded = HandleAscii(originalName);
            string outName = IsEncoded ? outNameEncoded : StringCipher.Encode(outNameEncoded);

            fileGrid.Rows.Add(originalName, outName, Path.GetDirectoryName(path), "Pending");
 
        }

        private string HandleAscii(string filename)
        {
            string convertedName = StringCipher.IsEncoded(filename) ? StringCipher.Decode(filename) : filename;
            string fileWithoutExt = Path.GetFileNameWithoutExtension(convertedName);
            string ext = Path.GetExtension(convertedName);

            if (optAppend_ascii_rBtn.Checked)
                if (!convertedName.Contains("(ascii)"))
                    return $"{fileWithoutExt}(ascii){ext}";

            if (optRemove_ascii_rBtn.Checked)
                if (convertedName.Contains("(ascii)"))
                    return fileWithoutExt.Replace("(ascii)", string.Empty) + ext;

            return convertedName;
        }

        internal void Clear() => fileGrid.Rows.Clear();

        private void localize() =>
            xMan.Localize(this, Localization.Enums.SenderType.Tab);

        #endregion
    }
}
