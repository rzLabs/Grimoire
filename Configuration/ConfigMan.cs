using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Grimoire.Configuration.Structures;
using Grimoire.Utilities;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System;

using Serilog;

namespace Grimoire.Configuration
{
    /// <summary>
    /// Provides configuration storage and real time manipulation
    /// </summary>
    public class ConfigManager
    {

        #region Properties

        /// <summary>
        /// List of Configuration descriptions
        /// </summary>
        public List<Option> Options = new List<Option>();

        string confDir = System.IO.Directory.GetCurrentDirectory();
        string confName = Defaults.ConfigName;
        string _confPath = string.Empty;

        Stopwatch actionSW = new Stopwatch();

        /// <summary>
        /// Automatically generate config file path
        /// </summary>
        string confPath
        {
            get
            {
                if (string.IsNullOrEmpty(_confPath))
                    _confPath = $"{confDir}\\{confName}";

                return _confPath;
            }
            set
            {
                _confPath = value;
                confDir = Path.GetDirectoryName(_confPath);
                confName = Path.GetFileName(_confPath);
            }
        }

        string confText = string.Empty;

        /// <summary>
        /// Fully qualified path to the directory holding the configuration .json
        /// </summary>
        public string Directory => confDir;

        /// <summary>
        /// Fully qualified path to the configuration .json (includes FolderName)
        /// </summary>
        public string FileName => confPath;

        /// <summary>
        /// Count of the elements in the Settings property
        /// </summary>
        public int Count => Options.Count;

        string[] parents
        {
            get
            {
                string[] p = new string[Options.Count];

                for (int i = 0; i < p.Length; i++)
                    p[i] = Options[i].Parent;

                return p;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor which will use default path variables
        /// </summary>
        public ConfigManager() => parse();

        /// <summary>
        /// Constructor for initializing with a directory and file name.
        /// </summary>
        /// <param name="Directory">Directory holding the configuration .json</param>
        /// <param name="FileName">Name of the configuration .json</param>
        public ConfigManager(string Directory, string FileName)
        {
            confDir = Directory;
            confName = FileName;

            parse();
        }

        /// <summary>
        /// Constructor for initializing with a fully qualified file path to the config .json
        /// </summary>
        /// <param name="FilePath"></param>
        public ConfigManager(string FilePath) => confPath = FilePath;

        #endregion

        #region Accessors

        public dynamic this[int index] => Options?[index];

        public dynamic this[string key]
        {
            get
            {
                int index = Options.FindIndex(o => o.Name == key);

                if (index == -1)
                {
                    Log.Error($"No config option w/ key: {key}");
                    return null;
                }

                return Options[index].Value;
            }
            set
            {
                int index = Options.FindIndex(o => o.Name == key);

                if (index >= 0)
                    Options[index].Value = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public dynamic this[string key, string parent]
        {
            get
            {
                int index = Options.FindIndex(o => o.Name == key && o.Parent == parent);

                return Options[index]?.Value;
            }
            set
            {
                int index = Options.FindIndex(o => o.Name == key && o.Parent == parent);

                if (index >= 0)
                    Options[index].Value = value;
            }
        }

        #endregion

        #region Get Methods

        /// <summary>
        /// Get configuration option by its ordinal position on the stack.
        /// </summary>
        /// <param name="index">Index of desired option</param>
        /// <returns>Populated Option or NULL</returns>
        public dynamic Get(int index) => Options?[index];

        /// <summary>
        /// Get configuration option by its name or key
        /// </summary>
        /// <param name="key">Name of the option</param>
        /// <returns>Populated Option or NULL</returns>
        public dynamic Get(string key) => Options.Find(o => o.Name == key);

        /// <summary>
        /// Get configuration option by its name and its category or parent.
        /// </summary>
        /// <param name="key">Name of the option</param>
        /// <param name="parent">Name of the parent</param>
        /// <returns>Populated Option or NULL</returns>
        public dynamic Get(string key, string parent) => Options.Find(o => o.Name == key && o.Parent == parent);

        /// <summary>
        /// Gets the config option matching provided key and parent and converts its value to T
        /// </summary>
        /// <typeparam name="T">Type the value should be returned as</typeparam>
        /// <param name="key">Key of the desired option</param>
        /// <param name="parent">Category or parent of the desired option</param>
        /// <returns>Value converted to T</returns>
        public T Get<T>(string key, string parent)
        {
            int index = -1;

            if ((index = Options.FindIndex(o => o.Name == key && o.Parent == parent)) != -1)
                return Convert.ChangeType(Options[index].Value, typeof(T));

            Log.Error($"Configuration option: {key} in group: {parent} not found!");

            return default(T);
        }

        /// <summary>
        /// Gets the config option matching provided key and parent, if it doesn't exist the default value will be return in its place.
        /// </summary>
        /// <typeparam name="T">Type of the configuration option</typeparam>
        /// <param name="key">Name of the desired option</param>
        /// <param name="parent">Category or Parent of the desired option</param>
        /// <param name="defaultValue">Value to be used if option is not present</param>
        /// <returns>Value converted to T or defaultValue as T</returns>
        public T Get<T>(string key, string parent, object defaultValue)
        {
            int index = -1;

            if ((index = Options.FindIndex(o => o.Name == key && o.Parent == parent)) != -1)
                return Convert.ChangeType(Options[index].Value, typeof(T));
            else
                return (T)defaultValue;
        }

        // WARNING! Must be called on an option with a value of int[] type!
        /// <summary>
        /// Get byte array that has been stored as an int array
        /// </summary>
        /// <param name="key">Option key</param>
        /// <returns>Populated byte array</returns>
        public byte[] GetByteArray(string key)
        {
            int optIdx = Options.FindIndex(o => o.Name == key);

            if (optIdx == -1)
            {
                Log.Error($"No config option w/ key: {key}");
                return null;
            }

            int[] val = Options[optIdx].Value;
            byte[] ret = new byte[val.Length];

            for (int i = 0; i < ret.Length; i++)
                ret[i] = (byte)val[i];

            return ret ?? null;
        }

        /// <summary>
        /// Get the properly formatted directory stored as a normal or relative (.\\Folder) string 
        /// </summary>
        /// <param name="key">Name of the option</param>
        /// <param name="parent">Parent of the option (if applicable)</param>
        /// <returns>Formatted directory path string</returns>
        public string GetDirectory(string key, string parent = null)
        {
            Option opt = (parent != null) ? Get(key, parent) : Get(key);

            string optPath = opt.Value;
            bool isRelative = !Path.IsPathRooted(optPath);

            if (!string.IsNullOrEmpty(optPath))
                if (!isRelative)
                    return optPath;
                else
                    return Path.GetFullPath(optPath);

            return null;
        }

        /// <summary>
        /// Update configuration option containing a int[] or array of bytes.
        /// </summary>
        /// <param name="key">Name of the option</param>
        /// <param name="array">Array of data to be preserved</param>
        public void UpdateByteArray(string key, byte[] array)
        {
            int idx = Options.FindIndex(o => o.Name == key);

            if (idx == -1)
                throw new KeyNotFoundException();

            int[] nArray = new int[array.Length];

            for (int i = 0; i < nArray.Length; i++)
                nArray[i] = (int)array[i];

            Options[idx].Value = nArray;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Writes all currently loaded configuration options into Config.json
        /// </summary>
        /// <returns>Task token</returns>
        public async Task Save()
        {
            await Task.Run(() =>
            {
                JObject rss = new JObject();

                foreach (string parent in parents)
                {
                    JProperty jParent = null;

                    JObject jChildren = new JObject();

                    foreach (Option child in Options.FindAll(c => c.Parent == parent))
                        jChildren.Add(new JProperty(child.Name, child.Value));

                    jParent = new JProperty(parent, jChildren);

                    if (!rss.ContainsKey(parent))
                        rss.Add(jParent);
                }

                if (rss.Count > 0)
                    using (StreamWriter sw = new StreamWriter("Config.json", false, Encoding.Default))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Formatting = Formatting.Indented;
                        serializer.Serialize(sw, rss);
                    }
            });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Parse configuration options stored in the .json format at confPath
        /// </summary>
        void parse()
        {
            int optCnt = 0;

            if (!File.Exists(confPath))
            {
                Log.Error($"Cannot locate config file at:\n\t- {confPath}");
                return;
            }

            confText = File.ReadAllText(confPath);

            actionSW.Start();

            try
            {
                JObject grandParent = JObject.Parse(confText);

                if (grandParent.Count > 0)
                {
                    Options.Clear();

                    List<JToken> parents = new List<JToken>(grandParent.Children());

                    Log.Verbose($"{parents.Count} configuration types available.");

                    for (int p = 0; p < parents.Count; p++)
                    {
                        JToken parent = parents[p];

                        List<JToken> children = new List<JToken>(parent.Children());

                        Log.Verbose($"Loading configurations for: {parent.Path}");

                        for (int c = 0; c < children.Count; c++)
                        {
                            JToken child = children[c];

                            List<JToken> grandChildren = new List<JToken>(child.Children());

                            Log.Verbose($"{grandChildren.Count} loaded!");

                            for (int gc = 0; gc < grandChildren.Count; gc++)
                            {
                                JToken grandChild = grandChildren[gc];

                                var val = grandChild.Value<dynamic>();
                                dynamic value;

                                string[] info = val.Path.ToString().Split('.');

                                if (val.Value.Type == JTokenType.Array)
                                {
                                    if (info[1] == "ModifiedXORKey")
                                    {
                                        List<int> array = new List<int>();

                                        foreach (var item in val.Value.Children())
                                            array.Add((int)item.Value);

                                        value = array.ToArray();
                                    }
                                    else
                                    {
                                        List<string> array = new List<string>();

                                        foreach (var item in val.Value.Children())
                                            array.Add(item.Value);

                                        value = array;
                                    }
                                }
                                else
                                    value = val.Value.Value;

                                Options.Add(new Option()
                                {
                                    Parent = info[0],
                                    Name = info[1],
                                    Type = value.GetType(),
                                    Value = value,
                                    Comments = new List<string>()
                                });

                                optCnt++;
                            }
                        }
                    }

                    actionSW.Stop();

                    Log.Information($"{optCnt} configurations loaded from Config in {StringExt.MilisecondsToString(actionSW.ElapsedMilliseconds)}");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"An exception has occured while parsing the config file!\nMessage:\n\t- {ex.Message}\n\nStack-Trace:\n\t- {ex.StackTrace}");
            }
        }

        #endregion
    }
}
