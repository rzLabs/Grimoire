using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Grimoire.Utilities
{
    public static class Paths
    {
        public static string DefaultDirectory;
        public static string DefaultFileName = string.Empty;
        public static string DefaultExtension = "*";
        static string title;

        public static string Title
        {
            set
            {
                title = value;
            }
        }

        static string description;

        /// <summary>
        /// The description of the displayed dialog.
        /// </summary>
        public static string Description
        {
            set
            {
                description = value;
            }
        }

        public static bool FileMultiSelect = false;
        public static DialogResult FileResult;
        public static DialogResult SaveResult;
        public static DialogResult FolderResult;

        /// <summary>
        /// Takes user input to select a directory and return files from within the selected directory
        /// </summary>
        private static string[] filePaths
        {
            get
            {
                title = (title == null) ? "Please select desired file" : title;

                using (OpenFileDialog ofDlg = new OpenFileDialog() {
                    DefaultExt = DefaultExtension,
                    Title = title,
                    InitialDirectory = DefaultDirectory,
                    Multiselect = FileMultiSelect,
                    FileName = DefaultFileName
                }) {
                    if ((FileResult = ofDlg.ShowDialog(GUI.Main.Instance)) == DialogResult.OK)
                        return File.Exists(ofDlg.FileName) ? ofDlg.FileNames : null;
                }

                return new string[1] { null };
            }
        }

        /// <summary>
        /// Exposes a folder browser dialog to the user and returns the first element from the file paths within the selected directory
        /// </summary>
        public static string FilePath => filePaths?[0];

        /// <summary>
        /// Exposes a folder browser dialog to the user and returns an array of file paths within the selected directory
        /// </summary>
        public static string[] FilePaths => filePaths;

        public static string SavePath
        {
            get
            {
                title = (title == null) ? "Please select save location and file name" : title;

                using (SaveFileDialog svDlg = new SaveFileDialog() {
                    DefaultExt = "*",
                    Title = title,
                    InitialDirectory = DefaultDirectory,
                    FileName = DefaultFileName
                }) {
                    if ((SaveResult = svDlg.ShowDialog(GUI.Main.Instance)) == DialogResult.OK)
                        return svDlg.FileName;
                }

                return null;
            }
        }

        /// <summary>
        /// Exposes a folder browser dialog to the user and returns the users selected directory or null
        /// </summary>
        public static string FolderPath
        {
            get
            {
                using (FolderBrowserDialog fbDlg = new FolderBrowserDialog() {
                    Description = description ?? "Please select desired folder"
                }) {
                    if ((FolderResult = fbDlg.ShowDialog(GUI.Main.Instance)) == DialogResult.OK)
                        return Directory.Exists(fbDlg.SelectedPath) ? fbDlg.SelectedPath : null;
                }

                return null;
            }
        }

        /// <summary>
        /// Verify that the provided dump directories sub folders only container their respective file extensions.
        /// </summary>
        /// <param name="directory">Directory containing previously dumped client</param>
        /// <returns>True if the dump is ready to build a client</returns>
#pragma warning disable CS1998
        public async static Task<bool> VerifyDump(string directory)
        {
            string[] extDirs = Directory.GetDirectories(directory);

            for (int extIdx = 0; extIdx < extDirs.Length; extIdx++)
            {
                string extDir = extDirs[extIdx];

                //check if dir is 2 characters long (e.g. .db/.fx)
                int extOffset = 0;
                extOffset = (extDir[extDir.Length - 3] == '\\') ? 2 : 3;

                string dirExt = extDir.Substring(extDir.Length - extOffset);
                string[] dirFiles = Directory.GetFiles(extDirs[extIdx]);

                for (int dirFileIdx = 0; dirFileIdx < dirFiles.Length; dirFileIdx++)
                {
                    string name = Path.GetFileName(dirFiles[dirFileIdx]);

                    extOffset = (name[name.Length - 3] == '.') ? 2 : 3;
                    string ext = name.Substring(name.Length - extOffset);

                    if (ext != dirExt)
                    {
                        if (MessageBox.Show($"File: {name} does not belong to the directory: /{dirExt}/\n\nWould you like to move it", "Directory Mistmatch Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string destName = $"{directory}//{ext}//{name}";
                            string sourceName = $"{extDir}//{name}";

                            if (File.Exists(destName))
                                if (MessageBox.Show($"A file with the same name already exists in the destination folder!\n\nWould you like to delete it?", "Duplicate File Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                                    File.Delete(destName);
                                else
                                    return false;

                            File.Move(sourceName, destName);
                        }
                        else
                            return false;
                    }
                }
            }

            return true;
        }
#pragma warning restore CS1998

        
    }
}
