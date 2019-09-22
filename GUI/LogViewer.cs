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

        public bool ViewDisposed
        {
            get
            {
                return logView.IsDisposed;
            }
        }

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

        public void Refresh()
        {
                logView.SetObjects(lManager.Entries);
        }

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
                DisplayLevel type = (DisplayLevel)displayType_lst.SelectedIndex;

                switch (type)
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
}
