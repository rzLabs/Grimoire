using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Grimoire.Tabs.Structures
{
    public struct StructureInfo
    {
        public int Index;
        public string Name
        {
            get { return Path.GetExtension(FilePath);  }
        }
        public string FilePath;
        public int DisplayIndex;
    }
}
