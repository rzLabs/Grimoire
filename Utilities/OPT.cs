using System;
using System.Collections.Generic;
using System.IO;

namespace Grimoire.Utilities
{
    public class OPT
    {
        static Logs.Manager lManager = Logs.Manager.Instance;

        protected static Dictionary<string, string> settings = new Dictionary<string, string>();

        static bool exists(string key) { return settings.ContainsKey(key); }

        public static string GetString(string key) { return (exists(key)) ? settings[key].ToString() : null; }

        public static bool GetBool(string key) { return (exists(key)) ? Convert.ToBoolean(Convert.ToInt32(settings[key])) : false; }

        public static int GetInt(string key) { return (exists(key)) ? Convert.ToInt32(settings[key]) : 0; }

        public static double GetDouble(string key) { return (exists(key)) ? Convert.ToDouble(settings[key]) : 0.0; }

        public static bool Update(string key, string value)
        {
            if (settings[key] != null) { settings[key] = value; return true; }

            return false;
        }

        public static void Load()
        {
            if (File.Exists("Grimoire.opt"))
            {
                using (StreamReader sR = new StreamReader("Grimoire.opt"))
                {
                    string currentLineValue = null;
                    while ((currentLineValue = sR.ReadLine()) != null)
                    {
                        if (!currentLineValue.StartsWith("#"))
                        {
                            //Break the line 
                            string[] lineBlocks = currentLineValue.Split(new char[] { ':' }, 2);
                            string settingName = lineBlocks[0];
                            string settingValue = lineBlocks[1];
                            settings.Add(settingName, settingValue);
                        }
                    }
                }

                lManager.Enter(Logs.Sender.OPT, Logs.Level.NOTICE, "OPT Manager Initialized.\n\t- {0} settings loaded from Grimoire.opt", settings.Count);
            }
            else { lManager.Enter(Logs.Sender.OPT, Logs.Level.ERROR, "Failed to load Grimoire.opt, it does not exist"); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public static void Save()
        {
            //TODO: TRY STATEMENT WHORE!
            if (File.Exists("Grimoire.opt")) { File.Delete("Grimoire.opt"); }

            using (StreamWriter sW = new StreamWriter(File.Create("Grimoire.opt")))
            {
                foreach (KeyValuePair<string, string> pair in settings) { sW.Write(string.Format("{0}:{1}\n", pair.Key, pair.Value)); }
            }

            lManager.Enter(Logs.Sender.OPT, Logs.Level.NOTICE, "Grimoire.opt saved.");
        }
    }
}
