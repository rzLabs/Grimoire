using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Structures
{
    [Flags]
    public enum EpicFlag
    {
        EPIC_ALL = 1,
        EPIC_4 = 2,
        EPIC_5 = 4,
        EPIC_5_3 = 8,
        EPIC_6 = 16,
        EPIC_6_2 = 32,
        EPIC_6_3 = 64,
        EPIC_7 = 128,
        EPIC_7_1 = 256,
        EPIC_7_2 = 512,
        EPIC_7_3 = 1024,
        EPIC_7_4 = 2048,
        EPIC_8 = 4096,
        EPIC_8_1 = 8192,
        EPIC_8_2 = 16384,
        EPIC_8_3 = 32768,
        EPIC_9 = 65536,
        EPIC_9_1 = 131072,
        EPIC_9_2 = 262144,
        EPIC_9_3 = 524288,
        EPIC_9_4 = 1048576,
        EPIC_9_5 = 2097152,
        EPIC_9_6 = 4194304,
        EPIC_9_7 = 8388608
    }
}
