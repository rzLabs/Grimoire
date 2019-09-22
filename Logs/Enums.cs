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
        ALL = 0,
        NOTICE = 1,
        DEBUG = 2,
        ERRORS = 3        
    }

    public enum Sender // TODO: Update me a-doi
    {
        RDB = 0,
        HASHER = 1,
        DATA = 2,
        DROPS = 3,
        FLAG = 4,
        DATABASE = 91,
        OPT = 92,
        MANAGER = 93,
        MAIN = 94,
    }
}
