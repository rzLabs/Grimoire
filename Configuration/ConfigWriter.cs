using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Configuration
{

    public class ConfigWriter
    {
        public Grim Grim { get; set; }
        public Tab Tab { get; set; }
        public DB DB { get; set; }
        public RDB RDB { get; set; }
        public Data Data { get; set; }
        public Hash Hash { get; set; }
        public Log Log { get; set; }
        public Flag Flag { get; set; }
        public Localization Localization { get; set; }
    }

    public class Grim
    {
        public string BuildDirectory { get; set; }
    }

    public class Tab
    {
        public string[] Styles { get; set; }
        public string DefaultStyle { get; set; }
    }

    public class DB
    {
        public int Engine { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public bool Trusted { get; set; }
        public string WorldName { get; set; }
        public string WorldUser { get; set; }
        public string WorldPass { get; set; }
        public bool Backup { get; set; }
        public bool DropOnExport { get; set; }
        public int Timeout { get; set; }
    }

    public class RDB
    {
        public bool Struct_AutoLoad { get; set; }
        public string Directory { get; set; }
        public string LoadDirectory { get; set; }
        public bool AppendASCII { get; set; }
        public bool SaveHashed { get; set; }
        public string CSV_Directory { get; set; }
        public string SQL_Directory { get; set; }
    }

    public class Data
    {
        public string LoadDirectory { get; set; }
        public int Encoding { get; set; }
        public bool Backup { get; set; }
        public bool ClearOnCreate { get; set; }
    }

    public class Hash
    {
        public bool AutoClear { get; set; }
        public bool AutoConvert { get; set; }
        public int Type { get; set; }
    }

    public class Log
    {
        public string Directory { get; set; }
        public int DisplayLevel { get; set; }
        public int WriteLevel { get; set; }
        public bool SaveOnExit { get; set; }
        public int RefreshInterval { get; set; }
    }

    public class Flag
    {
        public string Directory { get; set; }
        public string Default { get; set; }
        public bool ClearOnChange { get; set; }
    }

    public class Localization
    {
        public string Directory { get; set; }
        public string Locale { get; set; }
    }

}
