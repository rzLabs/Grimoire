using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Grimoire.Configuration.Structures;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System;

namespace Grimoire.Configuration
{
    /// <summary>
    /// Provides configuration storage and real time manipulation
    /// </summary>
    public class ConfigMan
    {
        /// <summary>
        /// List of Configuration descriptions
        /// </summary>
        public List<Option> Options = new List<Option>();

        string confDir = Directory.GetCurrentDirectory();
        string confName = Defaults.ConfigName;
        string _confPath = string.Empty;

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
        public string FolderName => confDir;

        /// <summary>
        /// Fully qualified path to the configuration .json (includes FolderName)
        /// </summary>
        public string FileName => confPath;

        /// <summary>
        /// Count of the elements in the Settings property
        /// </summary>
        public int Count => Options.Count;

        /// <summary>
        /// Default constructor which will use default path variables
        /// </summary>
        public ConfigMan() => parse();

        /// <summary>
        /// Constructor for initializing with a directory and file name.
        /// </summary>
        /// <param name="Directory">Directory holding the configuration .json</param>
        /// <param name="FileName">Name of the configuration .json</param>
        public ConfigMan(string Directory, string FileName)
        {
            confDir = Directory;
            confName = FileName;

            parse();
        }

        /// <summary>
        /// Constructor for initializing with a fully qualified file path to the config .json
        /// </summary>
        /// <param name="FilePath"></param>
        public ConfigMan(string FilePath) => confPath = FilePath;

        public dynamic this[int index] => Options?[index];

        public dynamic this[string key]
        {
            get
            {
                int index = Options.FindIndex(o => o.Name == key);
                if (index >= 0)
                    return Options[index].Value;
                else
                    throw new IndexOutOfRangeException();
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
                if (index >= 0)
                    return Options[index].Value;
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                int index = Options.FindIndex(o => o.Name == key && o.Parent == parent);

                if (index >= 0)
                    Options[index].Value = value;
                else
                    throw new IndexOutOfRangeException();             
            }
        }

        public dynamic GetOption(int index) => Options?[index];

        public dynamic GetOption(string key) => Options.Find(o => o.Name == key);

        public dynamic GetOption(string key, string parent) => Options.Find(o => o.Name == key && o.Parent == parent);

        public string GetDirectory(string key, string parent = null)
        {
            Option opt = (parent != null) ? GetOption(key, parent) : GetOption(key);

            string fileDir = null;
            string filePath = null;
            string valStr = opt.Value;

            if (valStr.StartsWith("/")) //Only the folder name is given
            {
                string workingDir = Directory.GetCurrentDirectory();
                fileDir = workingDir;
                valStr = valStr.Replace("/", "\\");
            }
            else
                fileDir = valStr;

            if (fileDir == valStr)
                filePath = fileDir;
            else
                filePath = $"{fileDir}{valStr as string}";

            return filePath;
        }

        void parse()
        {
            if (File.Exists(confPath))
                confText = File.ReadAllText(confPath);
            else
                throw new FileNotFoundException("File not found");

            JObject grandParent = JObject.Parse(confText);

            if (grandParent.Count > 0) //Gets root of json as JObject
            {
                Options.Clear();

                foreach (JToken parent in grandParent.Children())
                    foreach (JToken child in parent.Children())
                        foreach (JToken grandChild in child.Children())
                        {
                            var val = grandChild.Value<dynamic>();
                            dynamic value;

                            string[] info = val.Path.ToString().Split('.');

                            if (val.Value.Type == JTokenType.Array)
                            {
                                List<string> array = new List<string>();

                                foreach (var item in val.Value.Children())
                                    array.Add(item.Value);

                                value = array;
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
                        }
            }
            else
                throw new JsonException();
        }

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

        Option[] getChildren(string parent) => Options.FindAll(o => o.Parent == parent).ToArray();

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
    }
}
