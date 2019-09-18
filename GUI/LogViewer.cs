using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Logs;
using Grimoire.Structures;

namespace Grimoire.GUI
{
    public partial class LogViewer : Form
    {
        Manager lManager = Manager.Instance;

        public LogViewer()
        {
            InitializeComponent();

            parseLogs();
        }

        private void parseLogs()
        {


            int len = lManager.Entries.Count;
            for (int i = 0; i < len; i++)
            {
                Log log = lManager.Entries[i];

                logList.Items.Add(new ListViewItem()
                {
                    Name = string.Format("{0}_{1}", i.ToString("D2"), log.Level)
                });
            }
        }
    }
}
