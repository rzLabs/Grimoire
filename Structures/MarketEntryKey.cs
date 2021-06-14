using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Structures
{
    public class EntryKey
    {
        public EntryKey(int sort_id, string market_name)
        {
            SortID = sort_id;
            MarketName = market_name;
        }

        public int SortID;
        public string MarketName;
    }
}
