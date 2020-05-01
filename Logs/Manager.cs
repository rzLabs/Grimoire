using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Grimoire.Structures;
using Grimoire.Utilities;
using Grimoire.Logs.Enums;

namespace Grimoire.Logs
{
    public class Manager
    {
        int idx = 0;

        public ObservableCollection<Log> Entries = new ObservableCollection<Log>();
        public string LogsDirectory;
        public readonly string LogPath;

        GUI.LogViewer viewer;

        static Manager instance;
        public static Manager Instance
        {
            get
            {
                if (instance == null)
                    instance = new Manager();

                return instance;
            }
        }

        public Manager()
        {
            LogsDirectory = string.Format(@"{0}\Logs\", System.IO.Directory.GetCurrentDirectory());
            LogPath = string.Format(@"{0}\{1}.txt", LogsDirectory, DateTime.UtcNow.ToString("MM-dd-yyyy"));

            if (!Directory.Exists(LogsDirectory))
                Directory.CreateDirectory(LogsDirectory);

            Entries.CollectionChanged += Entries_CollectionChanged;

            Enter(Sender.MANAGER, Level.NOTICE, "Log Manager initialized.");
        }

        private void Entries_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (viewer != null && !viewer.ViewDisposed)
                viewer.Refresh();
        }

        public void Enter(Sender sender, Level level, string message, params object[] args)
        {
            Entries.Add(new Structures.Log()
            {
                Sender = sender,
                Level = level,
                DateTime = DateTime.UtcNow,
                Message = string.Format(message, args),
                StackTrace = null
            });

        }

        public void Enter(Sender sender, Level level, Exception ex)
        {
            Entries.Add(new Structures.Log()
            {
                Sender = sender,
                Level = level,
                DateTime = DateTime.UtcNow,
                Message = ex.Message,
                StackTrace = ex.StackTrace
            });

        }

        public void Enter(Sender sender, Level level, Exception ex, string message, params object[] args)
        {
            Entries.Add(new Structures.Log()
            {
                Sender = sender,
                Level = level,
                DateTime = DateTime.UtcNow,
                Message = string.Format(message, args),
                StackTrace = ex.StackTrace
            });

        }


        #region Public Methods

        public void ShowViewer()
        {
            if (viewer == null)
                viewer = new GUI.LogViewer(instance);

            viewer.Show(GUI.Main.Instance);
        }

        public void Save()
        {
            using (FileStream fs = new FileStream(LogPath, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                foreach (Structures.Log log in Entries)
                {
                    string text = string.Format("[{0} | {1} | {2}]: {3}\n", log.Sender.ToString(), log.Level.ToString(), log.DateTime.ToString("hh:mm:ss"), log.Message);

                    if (log.StackTrace != null)
                        text += string.Format("\n\t- Stack trace:\n\t{0}\n", log.StackTrace);

                    byte[] buffer = Encoding.Default.GetBytes(text);

                    fs.Write(buffer, 0, buffer.Length);
                }
            }
        }

        #endregion

        #region Private Methods



        #endregion
    }
}
