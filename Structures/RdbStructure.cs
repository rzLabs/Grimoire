using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daedalus.Structures;
using Daedalus.Utilities;

namespace Grimoire.Structures
{
    public class RdbStructure
    {
        string script = null;
        LUA lua;
        Cell[] headerCells = null;
        Cell[] rowCells = null;

        public RdbStructure(string script)
        {
            if (script == null)
                throw new ArgumentNullException("script object is null!");
            else if (script.Length == 0)
                throw new Exception("script object is empty!");

            this.script = script;

            lua = new LUA(script);

            if (lua.UseHeader)
                headerCells = lua.GetFieldList("header");

            rowCells = lua.GetFieldList("fields");

        }
    }
}
