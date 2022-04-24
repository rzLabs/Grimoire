using System;

namespace Grimoire.Tabs
{
    public enum Style
    {
        RDB = 0,
        HASHER = 1,
        DATA = 2,
        //DROPS = 3,
        ITEM = 4,
        MARKET = 5,
        LAUNCHER = 6,
        RDB2 = 98,
        NONE = 99
    }

    [Flags]
    public enum SaveFileType
    {
        RDB = 1,
        SQL = 2,
        CSV = 4,
        ALL = RDB | SQL | CSV
    }

    public enum SaveSqlFileType
    {
        Insert,
        Update
    }
}
