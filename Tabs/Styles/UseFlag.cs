using System;
using System.IO;
using System.Windows.Forms;

namespace Grimoire.Tabs.Styles
{
    public partial class UseFlag : UserControl
    {
        readonly Logs.Manager lManager;
        protected bool calculating = false;

        public UseFlag()
        {
            InitializeComponent();
            lManager = Logs.Manager.Instance;
        }

        public UseFlag(int flag)
        {
            InitializeComponent();
            lManager = Logs.Manager.Instance;
        }

        private int flag
        {
            get
            {
                return (flagIO.Text.Length == 0) ? 0 : Convert.ToInt32(flagIO.Text);
            }
            set { flagIO.Text = value.ToString(); }
        }

        private void UseFlag_Load(object sender, EventArgs e)
        {
            string flagsPath = Grimoire.Utilities.OPT.GetString("useflag.list_path");
            int flags = 0;
            
            if (File.Exists(flagsPath))
            {
                using (StreamReader sR = new StreamReader(flagsPath))
                {
                    string lineVal = null;
                    while ((lineVal = sR.ReadLine()) != null)
                    {
                        flagList.Items.Add(lineVal);
                        flags++;
                    }
                }

                lManager.Enter(Logs.Sender.USEFLAG, Logs.Level.NOTICE, "{0} Flags loaded to tab: {1}", flags, Tabs.Manager.Instance.Text);
            }
            else
            {
                string msg = string.Format("Could not load flag list at:\n{0}", flagsPath);
                lManager.Enter(Logs.Sender.USEFLAG, Logs.Level.ERROR, msg);
                MessageBox.Show(msg, "Load Flags Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
