using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Grimoire.Logs.Enums;
using Grimoire.DB.Enums;
using Newtonsoft.Json;
using Grimoire.Configuration.Enums;

namespace Grimoire.Configuration.Structures
{
    public class Option
    {
        public string Parent;
        public string Name;
        public Type Type;
        public dynamic Value;
        public List<string> Comments;
    }
}
