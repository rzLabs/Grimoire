using System;
using System.Collections.Generic;

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
