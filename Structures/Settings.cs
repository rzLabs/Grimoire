using System;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.ComponentModel;

using Grimoire.Utilities;

namespace Grimoire.Structures
{
    class Settings
    {
        [Description("A list of styles that can be choosen from the 'New' drop-down (Delimited by ',')"), Category("Tab Style"), DisplayName("Styles")]
        public string Styles { get { return OPT.GetString("tab.styles"); } set { OPT.Update("tab.styles", value); } }

        [Description("Default Tab Style to be loaded when starting Grimoire (if any)"), Category("Tab Style"), DisplayName("Default Style"), DefaultValue(Tabs.Style.NONE)]
        public Tabs.Style DefaultStyle
        {
            get
            {
                return (Tabs.Style)Enum.Parse(typeof(Tabs.Style), OPT.GetString("tab.default_style"));
            }
            set
            {
                OPT.Update("tab.default_style", ((Tabs.Style)value).ToString());
            }
        }

        [Description("The IP at which your database can be connected to"), Category("Database")]
        public string IP { get { return OPT.GetString("db.ip"); } set { OPT.Update("db.ip", value.ToString()); } }

        [Description("The Port (if any) your database is behind"), Category("Database"), DefaultValue(1433)]
        public int Port { get { return OPT.GetInt("db.port"); } set { OPT.Update("db.port", value.ToString()); } }

        [Description("Determines if Grimoire will use Windows Authentication to connect to the database, this does not require Username/Password."), Category("Database"), DefaultValue(false)]
        public bool Trusted { get { return OPT.GetBool("db.trusted.connection"); } set { OPT.Update("db.trusted.connection", Convert.ToInt32(value).ToString()); } }

        [Description("The database name of your Arcadia, e.g. ....Arcadia or 62World"), Category("Database"), DefaultValue("Arcadia"), DisplayName("Arcadia Name")]
        public string WorldName { get { return OPT.GetString("db.world.name"); } set { OPT.Update("db.world.name", value.ToString()); } }

        [Description("The username used to connect to the world database"), Category("Database"), DefaultValue("sa"), DisplayName("Arcadia Username")]
        public string WorldUser { get { return OPT.GetString("db.world.username"); } set { OPT.Update("db.world.username", value.ToString()); } }

        [Description("The password used to connect to the world database"), Category("Database"), DisplayName("Arcadia Password")]
        public string WorldPass { get { return OPT.GetString("db.world.password"); } set { OPT.Update("db.world.password", value.ToString()); } }

        [Description("Determines if the target table of the save operation will be dropped and recreate or truncated before inserting the .rdb data"), Category("Database"), DisplayName("Drop Table")]
        public bool DropTable { get { return OPT.GetBool("db.save.drop"); } set { OPT.Update("db.save.drop", value.ToString()); } }

        [Description("Determines if the target table of the save operation will be backed up before inserting the .rdb data"), Category("Database"), DisplayName("Backup Table")]
        public bool BackupTable { get { return OPT.GetBool("db.save.backup"); } set { OPT.Update("db.save.backup", value.ToString()); } }

        // data

        [Description("If defined, this is where any exported/built files will be placed"), Category("Data Utility"), DisplayName("Build Directory"), EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string BuildDirectory { get { return OPT.GetString("build.directory"); } set { OPT.Update("build.directory", value); } }

        [Description("If defined, Grimoire will read any lua configuration variables from this path. Otherwise it will use the default path"), Category("Data Utility"), DisplayName("LUA Config Path")]
        public string LuaConfigPath { get { return OPT.GetString("lua.config.path"); } set { OPT.Update("lua.config.path", value); } }

        [Description("Determines if files like data.000-008 will be backed up before any changes are made to them. (RECOMMENDED)"), Category("Data Utility"), DisplayName("Backups")]
        public bool Backups { get { return OPT.GetBool("backups"); } set { OPT.Update("backups", Convert.ToInt32(value).ToString()); } }

        // files

        [Description("Determines if the structure you select will be loaded the moment you select it or manually with \"Load\" button"), Category("RDB Utility"), DisplayName("Load on Select")]
        public bool AutoLoad { get { return OPT.GetBool("structure.autoload"); } set { OPT.Update("structure.autoload", Convert.ToInt32(value).ToString()); } }

        [Description("Determines if (ascii) is appended to file names being loaded or saved"), Category("RDB Utility"), DefaultValue(false), DisplayName("Use ASCII")]
        public bool UseASCII
        {
            get { return OPT.GetBool("use.ascii"); }
            set
            {
                Tabs.Manager.Instance.RDBTab.UseASCII = value;
                OPT.Update("use.ascii", Convert.ToInt32(value).ToString());
            }
        }

        [Description("Determines if newly created .RDB files will be saved in their hash name version"), Category("RDB Utility"), DefaultValue(false), DisplayName("Save Hashed")]
        public bool SaveHashed { get { return OPT.GetBool("save.hashed"); } set { OPT.Update("save.hashed", Convert.ToInt32(value).ToString()); } }

        // hasher
        [Description("Determines if the file entries will be cleared from the grid after conversion"), Category("Hasher Utility"), DisplayName("Auto Clear")]
        public bool AutoClear { get { return OPT.GetBool("hash.auto_clear"); } set { OPT.Update("hash.auto_clear", Convert.ToInt32(value).ToString()); } }

        [Description("Determines if files are automatically converted after being added to the grid."), Category("Hasher Utility"), DisplayName("Auto Convert")]
        public bool AutoConvert { get { return OPT.GetBool("hash.auto_convert"); } set { OPT.Update("hash.auto_convert", Convert.ToInt32(value).ToString()); } }

        // logger
        //[Description("Determines the level'(s) of messages to be displayed/written by the logging system"), Category("Logger"), DisplayName("Logging Level")]
        //public int LogLevel { get { return OPT.GetInt("log.level"); } set { OPT.UpdateSetting("log.evel", value.ToString()); } }

        // use flag
        [Description("Path to the list of item_use_flag sub flags"), Category("Use Flag Utility"), DisplayName("Flag List Path"), EditorAttribute(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string FlagPath { get { return OPT.GetString("useflag.list_path"); } set { OPT.Update("useflag.list_path", value.ToString()); } }

        [Description("Determines if input flag will be decoded each time you change the current input or paste a new input"), Category("Use Flag Utility"), DisplayName("Auto Reverse Flags")]
        public bool AutoReverse { get { return OPT.GetBool("useflag.auto_reverse"); } set { OPT.Update("useflag.auto_reverse", Convert.ToInt32(value).ToString()); } }
    }
}
