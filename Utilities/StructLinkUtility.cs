using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grimoire.Configuration;
using Grimoire.Structures;
using Grimoire.Utilities;

using Newtonsoft.Json;

using Serilog;
using Serilog.Events;

namespace Grimoire.Utilities
{
    /// <summary>
    /// Provides the ability to link a filename to a structure name
    /// </summary>
    public static class StructLinkUtility
    {
        static ConfigManager configMgr = GUI.Main.Instance.ConfigMgr;
        static Dictionary<string, string> links;

        /// <summary>
        /// Reads all links listed in LinksPath (e.g. StructLinks.json)
        /// </summary>
        public static void Parse()
        {
            string linksPath = configMgr.GetDirectory("LinksPath", "Structures");

            if (!File.Exists(linksPath))
            {
                LogUtility.MessageBoxAndLog($"Cannot find links json at path:\n\t- {linksPath}", "Links Parse Exception", LogEventLevel.Error);
                
                return;
            }

            string linksContent = File.ReadAllText(linksPath);

            links = JsonConvert.DeserializeObject<Dictionary<string, string>>(linksContent);

            Log.Information($"{links.Count} links loaded from:\n\t- StructLinks.json");
        }

        /// <summary>
        /// Determine is a filename/structure link exists
        /// </summary>
        /// <param name="filename">Filename to be checked</param>
        /// <returns><c>True or False</c></returns>
        public static bool FilenameLinkExists(string filename) => links.ContainsKey(filename);

        /// <summary>
        /// Determine is a filename/structure link exists
        /// </summary>
        /// <param name="structName">Structure name to be checked</param>
        /// <returns><c>True or False</c></returns>
        public static bool StructLinkExists(string structName) => GetFileName(structName) != null;

        /// <summary>
        /// Get the linked struct file name
        /// </summary>
        /// <param name="filename">Filename being linked against</param>
        /// <returns>Structure filename linked to the provided Filename</returns>
        public static string GetStructName(string filename) => links?[filename];

        /// <summary>
        /// Get the linked file name
        /// </summary>
        /// <param name="filename">Struct filename being linked against</param>
        /// <returns>Filename linked to the provided Structure filename</returns>
        public static string GetFileName(string structName)
        {
            foreach (KeyValuePair<string, string> kvp in links)
                if (kvp.Value == $"{structName}")
                    return kvp.Key;

            return null;
        }
    }
}
