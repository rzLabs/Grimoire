using System;
using System.IO;
using System.Windows.Forms;
using DataCore.Functions;

namespace Grimoire.Tabs.Styles
{
    public partial class Hasher : UserControl
    {
        #region Properties
        Logs.Manager lManager;
        #endregion

        #region Constructors
        public Hasher()
        {
            InitializeComponent();
            lManager = Logs.Manager.Instance;
            lManager.Enter(Logs.Sender.HASHER, Logs.Level.NOTICE, "Hasher Utility Started.");
            set_checks();
        }
        #endregion

        #region Events

        private void input_TextChanged(object sender, EventArgs e)
        {
            if (input.Text.Length > 3)
            {
                output.Text = (StringCipher.IsEncoded(input.Text)) ? StringCipher.Decode(input.Text) : StringCipher.Encode(input.Text);
            }
            else if (input.Text.Length == 0) { output.ResetText(); }
        }

        private void cMenu_clear_Click(object sender, EventArgs e)
        {
            fileGrid.Rows.Clear();
        }

        private void flipBtn_Click(object sender, EventArgs e)
        {
            if (output.Text.Length > 3)
                input.Text = output.Text;

            lManager.Enter(Logs.Sender.HASHER, Logs.Level.NOTICE, "User flipped input/output");
        }

        private void cMenu_add_file_Click(object sender, EventArgs e)
        {
            string path = Grimoire.Utilities.Paths.FilePath;

            if (Grimoire.Utilities.Paths.FileResult != DialogResult.OK)
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

        private void cMenu_convert_all_Click(object sender, EventArgs e)
        {
            convertAllEntries();

            if (autoClear_chk.Checked)
                cMenu_clear_Click(null, EventArgs.Empty);
        }

        private void autoClear_chk_CheckStateChanged(object sender, EventArgs e)
        {
            Grimoire.Utilities.OPT.Update("hash.auto_clear", Convert.ToInt32(autoClear_chk.Checked).ToString());
        }

        private void opt_auto_convert_CheckStateChanged(object sender, EventArgs e)
        {
            Grimoire.Utilities.OPT.Update("hash.auto_convert", Convert.ToInt32(opt_auto_convert.Checked).ToString());
        }

        private void cMenu_add_folder_Click(object sender, EventArgs e)
        {
            string directory = Grimoire.Utilities.Paths.FolderPath;

            if (Grimoire.Utilities.Paths.FolderResult != DialogResult.OK)
                return;

            foreach (string path in Directory.GetFiles(directory))
                add_file_to_grid(path);
        }

        #endregion

        #region Methods (Public)

        public void Add_Dropped_Files(string[] paths)
        {
            if (paths.Length == 1)
            {
                if (File.GetAttributes(paths[0]).HasFlag(FileAttributes.Directory))
                    paths = Directory.GetFiles(paths[0]);
            }

            foreach (string path in paths)
            {
                add_file_to_grid(path);
            }

            if (opt_auto_convert.Checked)
                convertAllEntries();
        }

        #endregion


        #region Methods (private)

        private void set_checks()
        {
            autoClear_chk.Checked = Grimoire.Utilities.OPT.GetBool("hash.auto_clear");

            switch (Grimoire.Utilities.OPT.GetInt("hash.type"))
            {
                case 1:
                    opt_append_ascii.Checked = true;
                    break;

                case 2:
                    opt_remove_ascii.Checked = true;
                    break;

                default:
                    opt_none.Checked = true;
                    break;
            }
        }

        private void convertAllEntries()
        {
            foreach (DataGridViewRow row in fileGrid.Rows)
                convertEntry(row.Index);
        }

        private void convertEntry(int rowIndex)
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
                {
                    lManager.Enter(Logs.Sender.HASHER, Logs.Level.NOTICE, "User opted to cancel conversion of {0}", (string)row.Cells[0].Value);
                    return;
                }

                File.Delete(convertedPath);
                lManager.Enter(Logs.Sender.HASHER, Logs.Level.NOTICE, "Converted Path already exists, deleting file at path: {0}", convertedPath);
            }

            if (File.Exists(originalPath))
            {
                File.Move(originalPath, convertedPath);
                fileGrid.Rows[rowIndex].Cells[3].Value = "Complete";
                lManager.Enter(Logs.Sender.HASHER, Logs.Level.NOTICE, "File at path: {0} converted to path: {1}", originalPath, convertedPath);
            }
        }

        private void add_file_to_grid(string path)
        {
            string originalName = Path.GetFileName(path);

            if (opt_append_ascii.Checked)
            {
                if (!originalName.Contains("(ascii)"))
                    originalName = originalName.Insert(originalName.Length - 4, "(ascii)");
            }

            if (opt_remove_ascii.Checked)
                originalName = originalName.Replace("(ascii)", string.Empty);

            string convertedName = StringCipher.Encode(originalName);

            fileGrid.Rows.Add(originalName, convertedName, Path.GetDirectoryName(path), "Pending");
        }

        #endregion
    }
}
