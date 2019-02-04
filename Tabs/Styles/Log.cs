using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.Tabs.Styles
{
    public partial class Log : UserControl
    {
        Logs.Manager lManager = Logs.Manager.Instance;

        public Log()
        {
            InitializeComponent();
            Read_Logs();
        }

        public void Read_Logs()
        {
            foreach (Structures.Log log in lManager.Entries)
            {
                logGrid.Rows.Add(log.Sender.ToString(),
                    log.Level.ToString(),
                    log.DateTime.ToString(),
                    log.Message,
                    log.StackTrace);
            }
        }
    }
}
