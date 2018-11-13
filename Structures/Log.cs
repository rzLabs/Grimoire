using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Structures
{
    public class Log
    {
        public Logs.Sender Sender { get; set; }
        public Logs.Level Level { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
