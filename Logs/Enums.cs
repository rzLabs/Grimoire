namespace Grimoire.Logs.Enums
{
    public enum Level
    {
        ALL,
        NOTICE,
        DEBUG,
        WARNING,
        ERROR        
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
