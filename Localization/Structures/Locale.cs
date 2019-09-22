using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Localization.Structures
{
    public struct Locale
    {
        public string Name;
        public string DisplayName;
        public int Encoding;
        public List<ControlConfig> Controls;

        public bool Populated
        {
            get
            {
                return Name.Length > 0 &&
                       DisplayName.Length > 0 &&
                       Encoding > 0 &&
                       Controls.Count > 0;
            }
        }
    }
}
