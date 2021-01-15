using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.DB
{
    public class DBError : EventArgs
    {
        public DBError(string message) => Message = message;

        public string Message;
    }

    public class DBProgressMax : EventArgs
    {
        public DBProgressMax(int maximum) => Maximum = maximum;

        public DBProgressMax(int maximum, string message)
        {
            Maximum = maximum;
            Message = message;
        }

        public int Maximum;
        public string Message;
    }

    public class DBProgressValue : EventArgs
    {
        public DBProgressValue(int value) => Value = value;

        public DBProgressValue(int value, string message)
        {
            Value = value;
            Message = message;
        }

        public int Value;
        public string Message;
    }

    public class DBMessage : EventArgs
    {
        public DBMessage(string message) => Message = message;

        public string Message;
    }
}
