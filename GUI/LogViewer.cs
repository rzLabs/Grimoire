using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Grimoire.Structures;
using BrightIdeasSoftware;
using Grimoire.Logs.Enums;
using Grimoire.Configuration;

namespace Grimoire.GUI
{
    public partial class LogViewer : Form
    {
        int interval = 0;

        Logs.Manager lManager;

        List<Log> filteredEntries = new List<Log>();

        ConfigManager configMan;
        Level displayLevel
        {
            get
            {
                Level outLvl = Level.ALL;

                if (InvokeRequired)
                    Invoke(new MethodInvoker(delegate { outLvl = (Level)displayType_lst.SelectedIndex; }));
                else
                    outLvl = (Level)displayType_lst.SelectedIndex;

                return outLvl;
            }
        }

        public bool ViewDisposed => logView.IsDisposed;

        public LogViewer(Logs.Manager lManager)
        {
            InitializeComponent();

            this.lManager = lManager;

            configMan = GUI.Main.Instance.ConfigMan;

            interval = (int)configMan["RefreshInterval", "Log"];
            
            configureViewer();

            generate_type_list();
        }

        private void generate_type_list()
        {
            displayType_lst.DataSource = Enum.GetValues(typeof(Level));
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
                Level level = (Level)displayType_lst.SelectedIndex;
                displayFilteredLogs(level);
            }
        }

        void displayFilteredLogs(Level level)
        {
            switch (level)
            {
                case Level.ALL:
                    logView.SetObjects(lManager.Entries.OrderBy(l => l.DateTime));
                    break;

                case Level.DEBUG:
                    logView.SetObjects(lManager.Entries.Where(l => l.Level == Level.DEBUG).OrderBy(l=>l.DateTime));
                    break;

                case Level.NOTICE:
                    logView.SetObjects(lManager.Entries.Where(l => l.Level == Level.NOTICE).OrderBy(l => l.DateTime));
                    break;

                case Level.ERROR:
                    logView.SetObjects(lManager.Entries.Where(l => l.Level == Level.ERROR).OrderBy(l => l.DateTime));
                    break;
            }
        }

        private void LogViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
