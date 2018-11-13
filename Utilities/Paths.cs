using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Utilities
{
    public static class Paths
    {
        static string title;
        public static string Title
        {
            set
            {
                title = value;
            }
        }

        public static DialogResult FileResult;
        public static DialogResult SaveResult;
        public static DialogResult FolderResult;

        public static string FilePath
        {
            get
            {
                title = (title == null) ? "Please select desired file" : title;
                using (OpenFileDialog ofDlg = new OpenFileDialog() { DefaultExt = "*", Title = title })
                {
                    if ((FileResult = ofDlg.ShowDialog(Grimoire.GUI.Main.Instance)) == DialogResult.OK)
                    {
                        return File.Exists(ofDlg.FileName) ? ofDlg.FileName : null;
                    }
                }

                return null;
            }
        }

        public static string SavePath
        {
            get
            {
                title = (title == null) ? "Please select save location and file name" : title;
                using (SaveFileDialog svDlg = new SaveFileDialog() { DefaultExt = "*", Title = title })
                {
                    if ((SaveResult = svDlg.ShowDialog(Grimoire.GUI.Main.Instance)) == DialogResult.OK)
                    {
                        return svDlg.FileName;
                    }
                }

                return null;
            }
        }

        public static string FolderPath
        {
            get
            {
                using (FolderBrowserDialog fbDlg = new FolderBrowserDialog() { Description = "Please select desired folder" })
                {
                    if ((FolderResult = fbDlg.ShowDialog(Grimoire.GUI.Main.Instance)) == DialogResult.OK)
                    {
                        return Directory.Exists(fbDlg.SelectedPath) ? fbDlg.SelectedPath : null;
                    }
                }

                return null;
            }
        }
    }
}
