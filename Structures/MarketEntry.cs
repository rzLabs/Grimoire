using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Structures
{
    public class MarketEntry
    {
        public int SortID;
        public string MarketName;
        public int Code;
        public string ItemName;
        public int Price;
        public decimal PriceRatio;
        public int HuntaholicPoint;
        public decimal HuntaholicRatio;
        public int ArenaPoint = default;
        public decimal ArenaRatio = default;

        public bool HasData => Code > 0;

        public MarketEntry()
        {
            SortID = 0;
            MarketName = string.Empty;
            Code = 0;
            Price = 0;
            PriceRatio = 0.000m;
            HuntaholicPoint = 0;
            HuntaholicRatio = 0.000m;
            ArenaPoint = 0;
            ArenaRatio = 0.000m;
        }

        public MarketEntry(int sort_id, string market_name, bool useArena = false)
        {
            SortID = sort_id;
            MarketName = market_name;
            Code = 0;
            Price = 0;
            PriceRatio = 0.000m;
            HuntaholicPoint = 0;
            HuntaholicRatio = 0.000m;

            if (useArena)
            {
                ArenaPoint = 0;
                ArenaRatio = 0.000m;
            }
        }
    }
}
