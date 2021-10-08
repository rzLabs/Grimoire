using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Tabs.Structures
{
    public class ItemInfo
    {
        public int ID { get; set; } = 0;

        public int NameID { get; set; }

        public int TooltipID { get; set; }

        public string Name { get; set; }

        public string Tooltip { get; set; }

        public string Icon_FileName { get; set; }
    }
}
