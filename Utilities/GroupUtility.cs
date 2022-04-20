using System;
using System.Collections.Generic;
using System.IO;

using Serilog.Events;

namespace Grimoire.Utilities
{
    /// <summary>
    /// Provides a centralized set of utility methods to manage struct select groups
    /// </summary>
    public static class GroupUtility
    {
        /// <summary>
        /// Coillection of group keys and their related expansion status
        /// </summary>
        static Dictionary<string, bool> groups = new Dictionary<string, bool>();

        /// <summary>
        /// Determine if a group with provided key exists
        /// </summary>
        /// <param name="key">Key string for the group</param>
        /// <returns>True if the group exists, false otherwise</returns>
        public static bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            return groups.ContainsKey(key);
        }
       
        /// <summary>
        /// Load the Groups.json at the default or provided path
        /// </summary>
        /// <param name="path">Fully qualified path to the Groups.json</param>
        public static void Load(string path = null)
        {
            string filename = (path == null) ? $"{Directory.GetCurrentDirectory()}\\Groups.json" : path;

            try
            {
                string jsonStr = File.ReadAllText(filename);
                groups = JsonUtility.DeserializeObject<Dictionary<string, bool>>(jsonStr);
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "loading Groups.json", "Groups Load Exception", LogEventLevel.Error);
                return;
            }
        }

        /// <summary>
        /// Save the Groups.json at the default or provided path
        /// </summary>
        /// <param name="path">Fully qualified path to the Groups.json</param>
        public static void Save(string path = null)
        {
            string filename = (path == null) ? $"{Directory.GetCurrentDirectory()}\\Groups.json" : path;

            JsonUtility.SerializeObject(groups, filename);
        }

        /// <summary>
        /// Set (or create) group with provided key and value
        /// </summary>
        /// <param name="key">Key of the group being created</param>
        /// <param name="value">Value of the group being created</param>
        public static void Set(string key, bool value) => groups[key] = value;

        /// <summary>
        /// Get the status of the group of the provided key
        /// </summary>
        /// <param name="key">Key string of the target group</param>
        /// <returns>True if Expanded, false if collapsed</returns>
        public static bool Get(string key)
        {
            if (groups.ContainsKey(key))
                return groups[key];

            return false;
        }

        /// <summary>
        /// Remove the group with given key from the collection
        /// </summary>
        /// <param name="key">Key string of group being removed</param>
        public static void Remove(string key) => groups.Remove(key);
    }
}
