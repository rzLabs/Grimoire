using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Grimoire.Tabs.Styles
{
    public partial class Flag : UserControl
    {
        #region Properties

        readonly Logs.Manager lManager;
        protected bool calculating = false;
        public List<string> lists = new List<string>();
        public string listPath = null;

        private int flag
        {
            get
            {
                return (flagIO.Text.Length == 0) ? 0 : Convert.ToInt32(flagIO.Text);
            }
            set { flagIO.Text = value.ToString(); }
        }

        #endregion

        #region Constructors

        public Flag()
        {
            InitializeComponent();
            lManager = Logs.Manager.Instance;
        }

        public Flag(int flag)
        {
            InitializeComponent();
            lManager = Logs.Manager.Instance;
        }

        #endregion

        #region Private Methods

        void generate_file_list()
        {
            string dir = Grimoire.Utilities.OPT.GetString("useflag.directory") ?? string.Format(@"{0}\Flags", Directory.GetCurrentDirectory());

            if (!Directory.Exists(dir))
            {
                string msg = string.Format("useflag.directory does not exist!\n\tDirectory: {0}", dir);

                MessageBox.Show(msg, "Flag Utility Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lManager.Enter(Logs.Sender.FLAG, Logs.Level.ERROR, msg);

                return;
            }

            string[] paths = Directory.GetFiles(dir);

            foreach (string filePath in paths)
                lists.Add(Path.GetFileNameWithoutExtension(filePath));

            lManager.Enter(Logs.Sender.FLAG, Logs.Level.DEBUG, "{0} flag files loaded from {1}", paths.Length, dir);
        }

        void generate_flag_list()
        {
            int flags = 0;
            flagList.Items.Clear();
            flag = 0;

            if (File.Exists(listPath))
            {
                using (StreamReader sR = new StreamReader(listPath))
                {
                    string lineVal = null;
                    while ((lineVal = sR.ReadLine()) != null)
                    {
                        flagList.Items.Add(lineVal);
                        flags++;
                    }
                }

                flagFiles.Text = Path.GetFileNameWithoutExtension(listPath);

                lManager.Enter(Logs.Sender.FLAG, Logs.Level.NOTICE, "{0} Flags loaded to tab: {1} from: {2}", flags, Tabs.Manager.Instance.Text, listPath);
            }
            else
            {
                string msg = string.Format("Could not load flag list at:\n{0}", listPath);
                lManager.Enter(Logs.Sender.FLAG, Logs.Level.ERROR, msg);
                MessageBox.Show(msg, "Load Flags Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Events

        private void UseFlag_Load(object sender, EventArgs e)
        { 
            generate_file_list();

            if (lists.Count == 0)
            {
                string msg = "generate_file_list() failed to generate the flag list!";

                MessageBox.Show(msg, "Flag Utility Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lManager.Enter(Logs.Sender.FLAG, Logs.Level.ERROR, msg);

                return;
            }

            foreach (string list in lists)
                flagFiles.Items.Add(list);

            string flagsDir = Grimoire.Utilities.OPT.GetString("useflag.directory");
            string listName = Grimoire.Utilities.OPT.GetString("useflag.default.list");
            if (listName == null)
            {
                string msg = "No default path for flag file defined!";

                MessageBox.Show(msg, "Flag Path Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                lManager.Enter(Logs.Sender.FLAG, Logs.Level.DEBUG, msg);

                return;
            }
            else
            {
                flagFiles.Text = listName;

                lManager.Enter(Logs.Sender.FLAG, Logs.Level.DEBUG, "Default Flag Path: {0} selected.", listName);
            }
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            calculating = true;

            flag = 0;
            for (int index = 0; index < flagList.Items.Count; index++)
            {
                if (flagList.GetSelected(index))
                {
                    flag |= (int)Math.Pow(2.0, index);
                }
            }

            calculating = false;
        }

        private void reverse_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < flagList.Items.Count; index++)
            {
                flagList.SetSelected(index, ((flag >> index) & 1) == 1);
            }
        }

        private void flagIO_TextChanged(object sender, EventArgs e)
        {
            if (flag == 0) { reverse.Enabled = false; }
            else
            {
                reverse.Enabled = true;

                if (!calculating)
                {
                    if (Grimoire.Utilities.OPT.GetBool("useflag.auto_reverse"))
                    {
                        reverse_Click(null, EventArgs.Empty);
                    }
                }
            }
            if (flagIO.Text.Length == 0) { flag = 0; }
        }

        private void flagFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flagFiles.SelectedIndex == -1)
                return;

            listPath = string.Format(@"{0}\{1}.txt", Grimoire.Utilities.OPT.GetString("useflag.directory"), flagFiles.Text);

            generate_flag_list();
        }

        #endregion
    }
}
