using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DataCore.Structures;

namespace Grimoire.Utilities
{
    public class SPR
    {
        Tabs.Manager tManager = Tabs.Manager.Instance;
        DataCore.Core core = null;
        Dictionary<string, string> icons = new Dictionary<string, string>();

        public int Count => icons.Count;

        public SPR(DataCore.Core core) {
            this.core = core;
        }

        public void LoadFromData(string name) //TODO: you should be sure data core is actually loaded/ready
        {
            IndexEntry entry = core.GetEntry(name);
            byte[] sprBuffer = core.GetFileBytes(entry);

            readSPR(sprBuffer);
        }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
                return;

            byte[] sprBytes = File.ReadAllBytes(filename);

            readSPR(sprBytes);
        }

        void readSPR(byte[] sprBytes)
        {          
            using (MemoryStream ms = new MemoryStream(sprBytes))
            {
                using (TextReader tr = new StreamReader(ms))
                {
                    int count = 0;
                    string line = null;

                    string key = null;
                    string value = null;

                    while ((line = tr.ReadLine()) != null)
                    {
                        if (count > 4)
                        {
                            count = 0;
                            if (!icons.ContainsKey(key))
                                icons.Add(key, value);
                        }

                        switch (count)
                        {
                            case 0:
                                key = line;
                                break;

                            case 2:
                                value = line;
                                break;
                        }

                        count++;
                    }
                }
            }
        }

        public string GetFileName(string key) =>
            (icons.ContainsKey(key)) ? icons[key] : null;
    }
}
