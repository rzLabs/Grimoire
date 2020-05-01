using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grimoire.Logs.Enums;

namespace Grimoire.Structures
{
    public class Log
    {
        public Sender Sender { get; set; }
        public Level Level { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
