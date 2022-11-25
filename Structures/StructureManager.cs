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

        public void Load(string directory = null)
        {
            structures.Clear();

            if (directory is not null)
                structDir = directory;

            foreach (string filename in Directory.GetFiles(structDir))
            {
                StructureObject structObj = new StructureObject(filename, false);

                try
                {
                    structObj.ParseScript(ParseFlags.Info);

                    structures.Add(structObj);
                }
                catch (Exception ex)
                {
                    if (ex is SyntaxErrorException || ex is ScriptRuntimeException)
                        LogUtility.MessageBoxAndLog($"An exception occured while processing: {Path.GetFileNameWithoutExtension(filename)}\n\n{StringExt.LuaExceptionToString(((InterpreterException)ex).DecoratedMessage)}", "StructureManager Exception", LogEventLevel.Error);

                    return;
                }            
            }

            Log.Information($"{structures.Count} structures loaded.");
        }

        public IEnumerator<StructureObject> GetEnumerator() => structures.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
