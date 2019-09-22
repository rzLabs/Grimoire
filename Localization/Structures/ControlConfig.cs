using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Grimoire.Localization.Structures
{
    public class ControlConfig
    {
        public string Name;
        public string Comment;
        public FontConfig Font;
        public Point Location;
        public Size Size;
        public TextConfig Text;

        public bool Populated
        {
            get
            {
                return Name.Length > 0 &&
                       Font != null &&
                       Location != null &&
                       Size != null;
            }
        }
    }
}
