using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grimoire.Utilities
{
    public class StructureManager
    {
        Logs.Manager lManager = Logs.Manager.Instance;

        string[] names = new string[0];

        string scriptPath;
        string script;

        public StructureManager(string directory)
        {
            names = Directory.GetFiles(directory);

            if (names.Length == 0)
            {
                lManager.Enter(Logs.Sender.MANAGER, Logs.Level.ERROR, "There are no files in the provided directory:\n\t{0}", directory);
                return;
            }

        }

        public string Script
        {
            get
            {
                if (script == null)
                {
                    lManager.Enter(Logs.Sender.MANAGER, Logs.Level.DEBUG, "No structure script currently loaded, attempting load.");

                    if (scriptPath == null)
                        lManager.Enter(Logs.Sender.MANAGER, Logs.Level.ERROR, "Cannot load script from a null location!");
                    else
                        script = File.ReadAllText(scriptPath);
                }

                return script;
            }
        }
    }
}
