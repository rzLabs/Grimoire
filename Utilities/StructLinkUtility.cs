using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grimoire.Configuration;
using Grimoire.Structures;
using Grimoire.Utilities;

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
        static List<StructLink> links;

        /// <summary>
        /// Generate a "default" set of links
        /// </summary>
        public static void GenerateDefault()
        {
            List<StructLink> links = new List<StructLink> { new StructLink() { StructureName = "StringResource", FileName = "db_string.rdb" },
                                                            new StructLink() { StructureName = "ItemResource73", FileName = "db_item.rdb" },
                                                            new StructLink() { StructureName = "MonsterResource73", FileName = "db_monster.rdb" } };

            JsonUtility.SerializeObject(links, $"{Directory.GetCurrentDirectory()}\\Links.json");
        }

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

            links = JsonUtility.DeserializeObject<List<StructLink>>(linksContent);

            Log.Information($"{links.Count} links loaded from:\n\t- StructLinks.json");
        }

        /// <summary>
        /// Determine is a filename/structure link exists
        /// </summary>
        /// <param name="filename">Filename to be checked</param>
        /// <returns><c>True or False</c></returns>
        public static bool FilenameLinkExists(string filename) => links.FindIndex(l=>l.FileName == filename) != -1;

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
        public static string GetStructName(string filename) => links?.Find(l=>l.FileName == filename)?.StructureName;

        /// <summary>
        /// Get the linked file name
        /// </summary>
        /// <param name="filename">Struct filename being linked against</param>
        /// <returns>Filename linked to the provided Structure filename</returns>
        public static string GetFileName(string structName) => links?.Find(l => l.StructureName == structName).FileName;
    }
}
