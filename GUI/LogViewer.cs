using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using Grimoire.Logs;
using Grimoire.Structures;
using Grimoire.Utilities;
using System.Threading;
using System.Collections.ObjectModel;
using BrightIdeasSoftware;

namespace Grimoire.GUI
{
    public partial class LogViewer : Form
    {
        int interval = 0;

        Logs.Manager lManager;

        List<Log> filteredEntries = new List<Log>();

        DisplayLevel displayLevel
        {
            get
            {
                DisplayLevel outLvl = DisplayLevel.ALL;

                if (InvokeRequired)
                    Invoke(new MethodInvoker(delegate { outLvl = (DisplayLevel)displayType_lst.SelectedIndex; }));
                else
                    outLvl = (DisplayLevel)displayType_lst.SelectedIndex;

                return outLvl;
            }
        }

        public bool ViewDisposed => logView.IsDisposed;

        public LogViewer(Logs.Manager lManager)
        {
            InitializeComponent();

            this.lManager = lManager;

            interval = OPT.GetInt("log.display.refresh");
            
            configureViewer();

            generate_type_list();
        }

        private void generate_type_list()
        {
            displayType_lst.DataSource = Enum.GetValues(typeof(DisplayLevel));
            displayType_lst.SelectedIndex = 0;
        }

        new public void Refresh() => displayFilteredLogs(displayLevel);

        private void configureViewer()
        {
            logView.RowHeight = 50;
            logView.Columns.AddRange(new OLVColumn[]
            {
                new OLVColumn("Sender", "Sender") { Width = 100 },
                new OLVColumn("Level", "Level") { Width = 100 },
                new OLVColumn("DateTime", "DateTime") { Width = 100 },
                new OLVColumn("Message", "Message") { FillsFreeSpace = true, WordWrap=true }
            });
        }

        private void displayType_lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (displayType_lst.SelectedIndex != -1)
            {
                DisplayLevel level = (DisplayLevel)displayType_lst.SelectedIndex;
                displayFilteredLogs(level);
            }
        }

        void displayFilteredLogs(DisplayLevel level)
        {
            switch (level)
            {
                case DisplayLevel.ALL:
                    logView.SetObjects(lManager.Entries);
                    break;

                case DisplayLevel.DEBUG:
                    logView.SetObjects(lManager.Entries.Where(l => l.Level == Level.DEBUG));
                    break;

                case DisplayLevel.NOTICE:
                    logView.SetObjects(lManager.Entries.Where(l => l.Level == Level.NOTICE));
                    break;

                case DisplayLevel.ERRORS:
                    logView.SetObjects(lManager.Entries.Where(l => l.Level == Level.ERROR ||
                                                                 l.Level == Level.HASHER_ERROR ||
                                                                 l.Level == Level.SQL_ERROR));
                    break;
            }
        }
    }
}
