using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Logs
{
    public enum Level
    {
        NOTICE = 0,
        WARNING = 1,
        ERROR = 2,
        SQL_ERROR = 3,
        HASHER_ERROR = 4,
        DEBUG = 99
    }

    public enum DisplayLevel
    {
        NOTICE = 0,
        ERRORS = 1,
        DEBUG = 2,
        ALL = 3
    }

    public enum Sender // TODO: Update me a-doi
    {
        RDB = 0,
        HASHER = 1,
        DATA = 2,
        GM = 3,
        DROPS = 4,
        FLAG = 5,
        DATABASE = 91,
        OPT = 92,
        MANAGER = 93,
        MAIN = 94
    }
}
