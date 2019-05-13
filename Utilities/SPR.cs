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
        DataCore.Core dCore = null;
        Dictionary<string, string> icons = new Dictionary<string, string>();
        
        public void LoadAll()
        {
            dCore = tManager.DataCore;
            readSPR("01_equip(ascii).spr");
            readSPR("02_item(ascii).spr");
        }

        void readSPR(string name)
        {
            IndexEntry entry = dCore.GetEntry(name);
            byte[] sprBuffer = dCore.GetFileBytes(entry);

            using (MemoryStream ms = new MemoryStream(sprBuffer))
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

        public string GetFileName(string key)
        {
            return (icons.ContainsKey(key)) ? icons[key] : null;
        }
    }
}
