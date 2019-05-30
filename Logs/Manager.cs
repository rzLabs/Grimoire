using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grimoire.Logs
{
    public class Manager
    {
        public List<Structures.Log> Entries = new List<Structures.Log>();
        public string LogsDirectory;
        public readonly string LogPath;
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

            Enter(Sender.MANAGER, Level.NOTICE, "Log Manager initialized.");
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

            writeLast();
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

            writeLast();
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

            writeLast();
        }

        void read()
        {
            if (System.IO.File.Exists(LogPath))
            {
                using (StreamReader sr = new StreamReader(LogPath, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] lineVals = line.Split(']');
                    }
                }
            }
        }

        public void writeAll()
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

        public void writeLast()
        {
            using (FileStream fs = new FileStream(LogPath, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                Structures.Log log = Entries[Entries.Count - 1];

                string text = string.Format("[{0} | {1} | {2}]: {3}\n", log.Sender.ToString(), log.Level.ToString(), log.DateTime.ToString("hh:mm:ss"), log.Message);

                if (log.StackTrace != null)
                    text += string.Format("\n\t- Stack trace:\n\t{0}\n", log.StackTrace);

                byte[] buffer = Encoding.Default.GetBytes(text);

                fs.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
