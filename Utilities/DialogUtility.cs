using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

using Grimoire.GUI;

namespace Grimoire.Utilities
{
    public static class DialogUtility
    {
        public enum DialogType
        {
            Number,
            YesNo,
            Text,
            Folder,
            Path,
            ListInput,
            ListSelect,
            MessageListBox
        };

        /// <summary>
        /// Request input based on provided criteria after user as answered a yes/no question
        /// </summary>
        /// <typeparam name="T">Type to convert user input into</typeparam>
        /// <param name="title">Title/Description</param>
        /// <param name="question">Yes/No question to present the user</param>
        /// <param name="task">Task description for RequestInput</param>
        /// <param name="type">Type of menu to be shown to the user</param>
        /// <returns>Return value of RequestInput</returns>
        public static T RequestQuestionInput<T>(string title, string question, string task, DialogType type = DialogType.Text)
        {
            if (MessageBox.Show(question, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                return RequestInput<T>(title, task, type);

            return default(T);
        }

        public static T RequestQuestionInput<T>(string title, string question, string task, DialogType type, params object[] options)
        {
            if (MessageBox.Show(question, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                return RequestInput<T>(title, task, type, options);

            return default(T);
        }

        /// <summary>
        /// Request input from the user using the provided DialogType
        /// </summary>
        /// <typeparam name="T">Type to convert user input into</typeparam>
        /// <param name="title">Title/Description</param>
        /// <param name="task">Task being performed</param>
        /// <param name="type">Type of menu to be shown to the user</param>
        /// <param name="options">Options relevant to the particular DialogType</param>
        /// <returns>User input Converted to T</returns>
        public static T RequestInput<T>(string title, string task, DialogType type = DialogType.Text, params object[] options)
        {
            object input = null;

            switch (type)
            {
                case DialogType.Number:
                    {
                        using (InputBox inputBx = new InputBox(task, false))
                        {
                            if (inputBx.ShowDialog(Main.Instance) != DialogResult.OK)
                                break;

                            input = Convert.ToInt32(inputBx.Value);
                        }
                    }
                    break;

                case DialogType.YesNo:
                    input = (MessageBox.Show(title, task, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                    break;

                case DialogType.Text:
                    {
                        bool resizable = options.Length > 0 ? (bool)options[0] : false;

                        using (InputBox inputBx = new InputBox(task, resizable))
                        {                        
                            if (inputBx.ShowDialog(Main.Instance) != DialogResult.OK)
                                break;

                            input = inputBx.Value;
                        }
                    }
                    break;

                case DialogType.Folder:
                    {
                        using (FolderBrowserDialog inputGUI = new FolderBrowserDialog() { Description = title })
                        {
                            // 0 - show new folder button
                            // 1 - root folder

                            bool showNewFolder = options.Length > 0 ? (bool)options[0] : false;
                            string rootFldr = options.Length > 1 ? options[1] as string : string.Empty;

                            if (inputGUI.ShowDialog(Main.Instance) != DialogResult.OK)
                                break;

                            input = inputGUI.SelectedPath;
                        }
                    }
                    break;

                case DialogType.Path:
                    {
                        using (OpenFileDialog ofDlg = new OpenFileDialog() { Title = title })
                        {
                            // 0 - initial directory
                            // 1 - default ext

                            string initDir = options.Length > 0 ? options[0] as string : null;
                            string defExt = options.Length > 1 ? options[1] as string : null;

                            ofDlg.InitialDirectory = initDir;
                            ofDlg.DefaultExt = defExt;

                            if (ofDlg.ShowDialog(Main.Instance) != DialogResult.OK)
                                break;

                            input = ofDlg.FileName;
                        }
                    }
                    break;

                case DialogType.ListInput:
                    {
                        using (ListInput listInput = new ListInput(title, (string[])options))
                        {

                        }
                                
                    }
                    break;

                case DialogType.ListSelect:
                    using (ListSelect listSelect = new ListSelect(title, (string[])options))
                        if (listSelect.ShowDialog(Main.Instance) == DialogResult.OK)
                            input = listSelect.SelectedText;
                    break;

                case DialogType.MessageListBox:

                    break;
            }

            return (T)Convert.ChangeType(input, typeof(T));
        }

        public static void ShowMessageListBox(string title, string message, List<string> items)
        {
            using (MessageListBox msgListBox = new MessageListBox(title, message, items))
                msgListBox.ShowDialog(Main.Instance);
        }

        public static void ShowMessageListBox(string title, string msg, string[] items)
        {
            using (MessageListBox msgListBox = new MessageListBox(title, msg, items))
                msgListBox.ShowDialog(Main.Instance);
        }
    }
}
