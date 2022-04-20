using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archimedes;
using Archimedes.Enums;
using Grimoire.Configuration;
using Grimoire.Utilities;

using MoonSharp.Interpreter;

using Serilog;
using Serilog.Events;

namespace Grimoire.Structures
{
    public class StructureManager : IEnumerable<StructureObject>
    {
        ConfigManager configMgr = GUI.Main.Instance.ConfigMgr;

        string structDir;

        List<StructureObject> structures = new List<StructureObject>();

        static StructureManager instance;

        public List<float> AvailableEpics = new List<float>();

        public static StructureManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new StructureManager();

                return instance;
            }
        }

        /// <summary>
        /// Get a new instance of the structure object bearing the given name.
        /// </summary>
        /// <param name="name">Name of the struct to be generated</param>
        /// <returns>Partially initialized (schema only) unique structure object</returns>
        public async Task<StructureObject> GetStruct(string name)
        {
            StructureObject structObj = structures.Find(s => s.StructName == name).Clone() as StructureObject;

            if (structObj == null)
            {
                LogUtility.MessageBoxAndLog("Failed to get the target structure object!", "GetStruct Exception", LogEventLevel.Error);

                return null;
            }

            await Task.Run(() => structObj.ParseScript(ParseFlags.Structure));

            return structObj;
        }

        public void Load(string directory)
        {
            structDir = directory;

            foreach (string filename in Directory.GetFiles(structDir))
            {
                StructureObject structObj = new StructureObject(filename, false);

                try
                {
                    structObj.ParseScript(ParseFlags.Info);
                }
                catch (Exception ex)
                {
                    if (ex is SyntaxErrorException || ex is ScriptRuntimeException)
                        LogUtility.MessageBoxAndLog($"An exception occured while processing: {Path.GetFileNameWithoutExtension(filename)}\n\n{StringExt.LuaExceptionToString(((InterpreterException)ex).DecoratedMessage)}", "StructureManager Exception", LogEventLevel.Error);

                    return;
                }

                float globalEpic = configMgr.Get<float>("Epic", "Grim");
                float structEpic_1 = structObj.Epic[0];
                float structEpic_2 = (structObj.Epic.Length > 1) ? structObj.Epic[1] : structEpic_1;

                if (structEpic_1 == 0 && structEpic_1 == structEpic_2 || globalEpic >= structEpic_1 && globalEpic <= structEpic_2)
                {
                    structures.Add(structObj);

                    for (int i = 0; i < structObj.Epic.Length; i++)
                    {
                        float epic = structObj.Epic[i];

                        if (!AvailableEpics.Contains(epic))
                            AvailableEpics.Add(epic);
                    }
                }
                            
            }

            Log.Information($"{structures.Count} structures loaded.");
        }

        public List<StructureObject> ByEpic(float[] epic)
        {
            List<StructureObject> _structures = new List<StructureObject>();

            for (int i = 0; i < structures.Count; i++)
            {
                if (epic.Length == 1 && epic[0] == 0)
                    _structures.Add(structures[i]);
                else
                {
                    // loop this structures epic table
                    for (int epicIdx1 = 0; epicIdx1 < structures[i].Epic.Length; epicIdx1++)
                    {
                        float structepic = structures[i].Epic[epicIdx1];

                        // loop the provided epic table
                        for (int epicIdx2 = 0; epicIdx2 < epic.Length; epicIdx2++)
                            if (structepic == epic[epicIdx2])
                                _structures.Add(structures[i].Clone() as StructureObject);
                    }
                }
            }

            return _structures;
        }

        public IEnumerator<StructureObject> GetEnumerator() => structures.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
