using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;

using MoonSharp.Interpreter.Loaders;
using MoonSharp.Interpreter;

/* ************************************************************************************* *
 * ************************************************************************************* *
 *                                                                                       *
 *      Archimedes v1.0.0                                                                *
 *          - iSmokeDrow 2022.2.18                                                       *
 *                                                                                       *
 *      User defined structure based binary data engine for parsing the properietary     *
 *      Rappelz .rdb file format.                                                        *
 *                                                                                       *
 *      Disclaimer:                                                                      *
 *                                                                                       *
 *      This engine is free to use and replicate as long as credit is given              *
 *       to its author.                                                                  *
 *                                                                                       *
 *      Thanks to:                                                                       *          
 *          - Glandu2 : as always, thanks for the string suggestions                     *
 *                                                                                       *
 * ************************************************************************************* *
 * ************************************************************************************* */
namespace Grimoire.Structures
{
    #region Enums

    /// <summary>
    /// Traditional or User defined
    /// </summary>
    public enum HeaderType
    {
        Traditional,
        Defined
    }

    /// <summary>
    /// Special case that changes how the rdb read loop proceeds
    /// </summary>
    public enum CaseFlag
    {
        None,
        DoubleLoop
    }

    /// <summary>
    /// Secondary type which determines specific engine behaviors applied against primary object types.
    /// </summary>
    public enum ArcType
    {
        TYPE_BYTE = 0,
        TYPE_BYTE_ARRAY = 1,
        TYPE_BIT_VECTOR = 2,
        TYPE_BIT_FROM_VECTOR = 3,
        TYPE_INT16 = 4,
        TYPE_SHORT = 5,
        TYPE_UINT_16 = 6,
        TYPE_USHORT = 7,
        TYPE_INT32 = 8,
        TYPE_INT = 9,
        TYPE_UINT32 = 10,
        TYPE_UINT = 11,
        TYPE_INT64 = 12,
        TYPE_LONG = 13,
        TYPE_SINGLE = 14,
        TYPE_FLOAT = 15,
        TYPE_FLOAT32 = 16,
        TYPE_DOUBLE = 17,
        TYPE_FLOAT64 = 18,
        TYPE_DECIMAL = 19,
        TYPE_DATETIME = 20,
        TYPE_DATESTRING = 21,
        TYPE_SID = 22,
        TYPE_STRING = 23,
        TYPE_STRING_BY_LEN = 24,
        TYPE_STRING_BY_HEADER_REF = 25,
        TYPE_STRING_BY_REF = 26,
        TYPE_STRING_LEN = 27,
        TYPE_ENCODED_INT32 = 28,
        TYPE_SKIP = 29,
        TYPE_COPY_INT32 = 30,

        NONE
    }

    /// <summary>
    /// Flags which can be combine/set to alter the way a cell is processed
    /// </summary>
    [Flags]
    public enum CellFlags
    {   
        Hidden = 1, 
        RowCount = 2, 
        RdbIgnore = 4,
        SqlIgnore = 8,
        LoopCounter = 16,
        None = 1024
    }

    /// <summary>
    /// Determines how a structure will be parsed
    /// </summary>
    [Flags]
    public enum ParseFlags
    {
        Info = 1,
        Structure = 2,
        Both = Info | Structure
    }

    /// <summary>
    /// Type of SQL transaction string is being generated (insert, update)
    /// </summary>
    public enum SqlStringType
    {
        Insert,
        Update
    }

    public enum SearchOperator
    {
        Equal,
        NotEqual,
        Like,
        NotLike,
        Above,
        Below,
        Between
    }

    public enum SearchReturn
    {
        Indicies,
        Values
    }

    #endregion

    #region Interfaces

    /// <summary>
    /// Cell interface that garuntees the presence of Read and Write methods in derived cells
    /// </summary>
    public interface ICellIO
    {
        object Read();

        void Write(object value);
    }

    #endregion

    #region Objects

    /// <summary>
    /// Object representing a given structure lua and information regarding it and it's author.
    /// </summary>
    public class StructureObject : ICloneable
    {
        protected Script scriptObj = null;  

        /// <summary>
        /// Path to the structure file set during this objects construction
        /// </summary>
        public readonly string FilePath;

        /// <summary>
        /// Name (without extension) of the actual structure lua this object represents
        /// </summary>
        public string StructName => Path.GetFileNameWithoutExtension(FilePath);

        /// <summary>
        /// Name of this structure object
        /// </summary>
        public string Name;

        /// <summary>
        /// Overridable default filename for load/save operations (includes extension)
        /// </summary>
        public string RDBName;

        /// <summary>
        /// Overridable database name (will default to Arcadia)
        /// </summary>
        public string DatabaseName;

        /// <summary>
        /// Overridable database table name (will be requested if undefined)
        /// </summary>
        public string TableName;

        /// <summary>
        /// Version of this structure object
        /// </summary>
        public Version Version = new Version(0, 0, 0, 0);

        /// <summary>
        /// Author of this structure object
        /// </summary>
        public string Author;

        /// <summary>
        /// Supported epics of this structure object
        /// </summary>
        public float[] Epic;

        /// <summary>
        /// Encoding which strings will be encoded/decoded
        /// </summary>
        public Encoding Encoding = Encoding.Default;

        /// <summary>
        /// Any special case the engine may need to consider
        /// </summary>
        public CaseFlag SpecialCase;

        /// <summary>
        /// If traditional, standard header read/write will be used.
        /// </summary>
        public HeaderType HeaderType { get; set; } = HeaderType.Traditional;

        /// <summary>
        /// Collection of cells that describe the header section of an rdb
        /// </summary>
        public List<CellBase> HeaderCells = new List<CellBase>();

        /// <summary>
        /// Collection of cells describing the data of an rdb
        /// </summary>
        public List<CellBase> DataCells = new List<CellBase>();

        /// <summary>
        /// Collection of cell names that are not hidden
        /// </summary>
        public string[] VisibleCellNames
        {
            get
            {
                List<string> names = new List<string>();

                foreach (CellBase cell in DataCells)
                    if (!cell.Flags.HasFlag(CellFlags.Hidden))
                        names.Add(cell.Name);

                return names.ToArray();
            }
        }

        /// <summary>
        /// Row object containing header data
        /// </summary>
        public RowObject Header = null;

        /// <summary>
        /// Collection of row objects storing schema and data
        /// </summary>
        public List<RowObject> Rows = new List<RowObject>();

        /// <summary>
        /// The amount of rows loaded into this structure object
        /// </summary>
        public int RowCount => (int)Header[CellFlags.RowCount];

        protected internal string selectStatement { get; set; }

        /// <summary>
        /// SQL Select statement to be used when loading data for this structure object from a SQL database
        /// </summary>
        public string SelectStatement
        {
            get
            {
                if (selectStatement != null)
                    return selectStatement;

                if (TableName == null)
                    throw new NullReferenceException("TableName is null!");

                string statement = "SELECT ";

                foreach (CellBase cell in DataCells)
                {
                    if (cell.Flags.HasFlag(CellFlags.SqlIgnore))
                        continue;

                    statement += $"[{cell.Name}],";
                }

                return selectStatement = $"{statement.Remove(statement.Length - 1, 1)} FROM dbo.{TableName} with (NOLOCK)";
            }
            set => selectStatement = value;
        }

        /// <summary>
        /// Construct this structure object with user input via a structure lua
        /// </summary>
        /// <param name="filename">Path to the structure lua to be parsed</param>
        /// <param name="parse">Defaults to true, if false the script will not be parsed only constructed.</param>
        public StructureObject(string filename, bool parse = true)
        {
            this.FilePath = filename;

            if (filename == null || !File.Exists(filename))
                throw new FileNotFoundException("Cannot find the structure file!", filename);

            if (parse)
                ParseScript();

            // We need to tell our ByteUtility what encoding the user declared (if any)
            ByteUtility.Encoding = Encoding;
        }

        /// <summary>
        /// Create a new Header row object from previously loaded HeaderCells
        /// </summary>
        public void SetHeader() => Header = new RowObject(HeaderCells);

        /// <summary>
        /// Create a new header row object based on the provided info and set it as this structure objects header.
        /// </summary>
        /// <param name="date">The date as a DateString</param>
        /// <param name="signature">120 byte array (which can contain strings)</param>
        /// <param name="rowCount">Amount of rows contained in this structure object</param>
        public void SetHeader(DateString date, byte[] signature, int rowCount)
        {
            List<CellBase> cells = new List<CellBase>();

            cells.Add(new DateStringCell("CreationDate"){ Index = 0 });
            cells.Add(new ByteArrayCell("Signature", 120) { Index = 1 });
            cells.Add(new IntCell("RowCount", CellFlags.RowCount) { Index = 2 });

            Header = new RowObject(cells);
            Header[0] = date;
            Header[1] = signature;
            Header[2] = rowCount;

        }

        /// <summary>
        /// Parse the contents of the provided structure lua into information usable by this structure object
        /// </summary>
        /// <param name="flags">Bit vector containing combination of read flags</param>
        public void ParseScript(ParseFlags flags = ParseFlags.Both)
        {
            string scriptStr = File.ReadAllText(FilePath);

            if (string.IsNullOrEmpty(scriptStr))
                throw new NullReferenceException("Failed to read the structure lua contents!");

            scriptObj = new Script();

            string curDir = Directory.GetCurrentDirectory();

            // Enable - `require 'ModuleName'` statements
            ((ScriptLoaderBase)scriptObj.Options.ScriptLoader).ModulePaths = new string[] 
            { 
                $"{curDir}\\Modules\\?",
                $"{curDir}\\Modules\\?.lua"
            };

            // declare our globals with Moonsharp interpreter
            declareGlobals(scriptObj);
            
            scriptObj.DoString(scriptStr);

            Dictionary<string, int> enumDict = new Dictionary<string, int>();

            // If the info flag isnt present, proceed to processing the header and data cells
            if (!flags.HasFlag(ParseFlags.Info))
                goto structure;

            Name = scriptObj.Globals["name"] as string ?? "UNDEFINED";
            Version = new Version(scriptObj.Globals["version"] as string ?? "0.0.0.0");
            Author = scriptObj.Globals["author"] as string ?? "UNDEFINED";
            RDBName = scriptObj.Globals["file_name"] as string;
            DatabaseName = scriptObj.Globals["database"] as string ?? "Arcadia";
            TableName = scriptObj.Globals["table_name"] as string ?? "UNDEFINED";
            SelectStatement = scriptObj.Globals["select_statement"] as string ?? $"SELECT * FROM dbo.{TableName}";
            
            if (scriptObj.Globals["encoding"] != null)
            {
                int codepage = Convert.ToInt32(scriptObj.Globals["encoding"]);

                Encoding = Encoding.GetEncoding(codepage);
            }

            Table epicTbl = scriptObj.Globals["epic"] as Table;

            if (epicTbl == null)
                Epic = new float[1] { 0.0f };
            else
            {
                Epic = new float[epicTbl.Length];

                for (int i = 1; i < epicTbl.Length + 1; i++)
                    Epic[i - 1] = Convert.ToSingle(epicTbl.Get(i).Number);
            }

            SpecialCase = (scriptObj.Globals["special_case"] != null) ? (CaseFlag)Convert.ToInt32(scriptObj.Globals["special_case"]) : CaseFlag.None;

            structure:

            if (!flags.HasFlag(ParseFlags.Structure))
                return;

            Table headerTbl = scriptObj.Globals["header"] as Table;

            if (headerTbl == null)
                SetHeader(new DateString("19900101"), new byte[120], 0);
            else // In the case of .ref file headers
            {
                HeaderType = HeaderType.Defined;
                HeaderCells = generateCells(headerTbl);

                SetHeader();
            }

            Table dataTable = scriptObj.Globals["cells"] as Table;

            if (dataTable == null)
                throw new NullReferenceException("Fields table is null!");

            DataCells = generateCells(dataTable);
        }

        /// <summary>
        /// Init the global variables the provided script object will require to process any provided structure lua
        /// </summary>
        /// <param name="scriptObject"><c>Script</c> object that will be loading a structure lua</param>
        void declareGlobals(Script scriptObject)
        {
            #region Type Globals

            string[] arcTypeNames = Enum.GetNames(typeof(ArcType));
            int[] values = (int[])Enum.GetValues(typeof(ArcType));

            for (int i = 0; i < 31; i++)
                scriptObject.Globals[arcTypeNames[i].Remove(0, 5)] = values[i];

            #endregion

            #region Direction Globals

            scriptObject.Globals["READ"] = "read";
            scriptObject.Globals["WRITE"] = "write";

            #endregion

            #region Special Case Globals

            // These are the ordinal position of the bits
            scriptObject.Globals["HIDDEN"] = "1:1";
            scriptObject.Globals["ROWCOUNT"] = "2:2";
            scriptObject.Globals["RDBIGNORE"] = "3:4";
            scriptObject.Globals["SQLIGNORE"] = "4:8";
            scriptObject.Globals["LOOPCOUNTER"] = "5:16";

            // SpecialCase globals
            scriptObject.Globals["DOUBLELOOP"] = 1;

            #endregion

            #region Flag Type Globals

            scriptObject.Globals["BIT_FLAG"] = 3;
            scriptObject.Globals["ENUM"] = 4;

            #endregion

        }

        /// <summary>
        /// Generate derived BaseCell objects describing rdb data from user defined structure lua
        /// </summary>
        /// <param name="cells">Fields lua table</param>
        /// <returns>Populated list of boxed CellBase objects</returns>
        List<CellBase> generateCells(Table cells)
        {
            List<CellBase> objCollection = new List<CellBase>();

            for (int i = 1; i < cells.Length + 1; i++)
            {
                Table objData = cells.Get(i).Table;

                if (objData == null)
                    throw new NullReferenceException("Failed to get the object properties table!");

                CellBase dataObj = null;

                // Set the Secondary and Primary type for the new data object
                ArcType secondaryType = objData.Get(2).ToObject<ArcType>();
                Type objType = GetObjectType(secondaryType);

                string name = objData.Get(1).String;

                // Create the object in its derived state (we will box it as cellect for transport)
                if (objType == typeof(IntCell))
                {
                    dataObj = new IntCell(name);

                    if (secondaryType == ArcType.TYPE_COPY_INT32)
                        dataObj.Dependency = objData.Get(3).String;
                }
                else if (objType == typeof(EncodedIntCell))
                {
                    dataObj = new EncodedIntCell(name);
                }
                else if (objType == typeof(ShortCell))
                {
                    dataObj = new ShortCell(name);
                }
                else if (objType == typeof(LongCell))
                {
                    dataObj = new LongCell(name);
                }
                else if (objType == typeof(FloatCell))
                {
                    dataObj = new FloatCell(name);
                }
                else if (objType == typeof(DoubleCell))
                {
                    dataObj = new DoubleCell(name);
                }
                else if (objType == typeof(DecimalCell))
                {
                    dataObj = new DecimalCell(name);
                }
                else if (objType == typeof(ByteCell))
                {
                    dataObj = new ByteCell(name);

                    if (secondaryType == ArcType.TYPE_BIT_FROM_VECTOR)
                    {
                        dataObj.Dependency = objData.Get(3).String;
                        dataObj.Offset = (int)objData.Get(4).Number;
                    }
                }
                else if (objType == typeof(ByteArrayCell))
                {
                    dataObj = new ByteArrayCell(name, (int)objData.Get(3).Number);
                }
                else if (objType == typeof(StringCell))
                {
                    if (secondaryType == ArcType.TYPE_STRING)
                        dataObj = new StringCell(name, (int)objData.Get(3).Number);
                    else if (secondaryType == ArcType.TYPE_STRING_BY_LEN | secondaryType == ArcType.TYPE_STRING_BY_HEADER_REF) // Both BY_LEN & BY_HEADER_REF have their dependency in the third slot. Group them
                        dataObj = new StringCell(name, -1, ArcType.TYPE_STRING_BY_LEN, objData.Get(3).String);
                }
                else if (objType == typeof(DateTime))
                {
                    dataObj = new DateTimeCell(name);
                }
                else if (objType == typeof(DateStringCell))
                {
                    dataObj = new DateStringCell(name);
                    dataObj.Length = (int)objData.Get(3).Number;
                }

                dataObj.SecondaryType = secondaryType;

                // Flag will always be the last field
                int flagsOffset = 3;

                if (objType == typeof(StringCell) || objType == typeof(ByteArrayCell) || objType == typeof(int) && secondaryType == ArcType.TYPE_COPY_INT32)
                    flagsOffset = 4;
                else if (objType == typeof(ByteCell) && secondaryType == ArcType.TYPE_BIT_FROM_VECTOR)
                    flagsOffset = 5;

                DynValue flagObj = objData.Get(flagsOffset);

                // So check to make sure the last field isn't nil and is a number
                if (!flagObj.IsNil())
                {
                    if (flagObj.Type == DataType.String) // User may have defined only a single flag by keyword (instead of SetFlags func)
                    {
                        string flag = flagObj.String;

                        int idx = flag.IndexOf(":");

                        if (idx > 0)
                        {
                            flag = flag.Substring(++idx, flag.Length - idx); // Get the actual flag (enum) value by moving the index forward 1 and reading the rest of the string

                            int nFlag;
                            if (int.TryParse(flag, out nFlag))
                                dataObj.Flags = (CellFlags)nFlag;
                        }
                    }
                    else if (flagObj.Type == DataType.Number)
                        dataObj.Flags = (CellFlags)flagObj.Number;
                }

                // The index can only be set once and then becomes immutable.
                dataObj.Index = i - 1;

                objCollection.Add(dataObj);
            }

            return objCollection;
        }

        /// <summary>
        /// Determine the derived cell type of the given LuaType
        /// </summary>
        /// <param name="type">Cell type loaded from structure lua</param>
        /// <returns>System Type representing a derived cell</returns>
        public Type GetObjectType(ArcType type)
        {
            switch (type)
            {
                case ArcType.TYPE_BYTE:
                    return typeof(ByteCell);

                case ArcType.TYPE_SKIP:
                case ArcType.TYPE_BYTE_ARRAY:
                    return typeof(ByteArrayCell);

                case ArcType.TYPE_BIT_VECTOR:
                    return typeof(IntCell);

                case ArcType.TYPE_BIT_FROM_VECTOR:
                    return typeof(ByteCell);

                case ArcType.TYPE_DECIMAL:
                    return typeof(DecimalCell);

                case ArcType.TYPE_INT16:
                case ArcType.TYPE_SHORT:
                    return typeof(ShortCell);

                case ArcType.TYPE_ENCODED_INT32:
                    return typeof(EncodedIntCell);

                case ArcType.TYPE_INT32:
                case ArcType.TYPE_INT:
                case ArcType.TYPE_SID:
                case ArcType.TYPE_STRING_LEN:
                case ArcType.TYPE_COPY_INT32:
                    return typeof(IntCell);

                case ArcType.TYPE_INT64:
                case ArcType.TYPE_LONG:
                    return typeof(LongCell);

                case ArcType.TYPE_SINGLE:
                case ArcType.TYPE_FLOAT:
                case ArcType.TYPE_FLOAT32:
                    return typeof(FloatCell);

                case ArcType.TYPE_DOUBLE:
                    return typeof(DoubleCell);

                case ArcType.TYPE_DATETIME:
                    return typeof(DateTime);

                case ArcType.TYPE_DATESTRING:
                    return typeof(DateStringCell);

                case ArcType.TYPE_STRING:
                case ArcType.TYPE_STRING_BY_HEADER_REF:
                case ArcType.TYPE_STRING_BY_LEN:
                case ArcType.TYPE_STRING_BY_REF:
                    return typeof(StringCell);
            }

            return null;
        }

        /// <summary>
        /// Determine the real underlying data type of the given LuaType
        /// </summary>
        /// <param name="type">Cell type loaded from structure lua</param>
        /// <returns>System Type representing data</returns>
        public Type GetObjectRealType(ArcType type)
        {
            switch (type)
            {
                case ArcType.TYPE_BYTE:
                case ArcType.TYPE_BIT_FROM_VECTOR:
                    return typeof(byte);

                case ArcType.TYPE_SKIP:
                case ArcType.TYPE_BYTE_ARRAY:
                    return typeof(byte[]);

                case ArcType.TYPE_DECIMAL:
                    return typeof(decimal);

                case ArcType.TYPE_SHORT:
                case ArcType.TYPE_INT16:
                    return typeof(short);

                case ArcType.TYPE_UINT_16:
                case ArcType.TYPE_USHORT:
                    return typeof(ushort);

                case ArcType.TYPE_ENCODED_INT32:
                    return typeof(int);

                case ArcType.TYPE_INT:
                case ArcType.TYPE_INT32:
                case ArcType.TYPE_BIT_VECTOR:
                case ArcType.TYPE_SID:
                case ArcType.TYPE_STRING_LEN:
                    return typeof(int);

                case ArcType.TYPE_UINT32:
                case ArcType.TYPE_UINT:
                    return typeof(uint);

                case ArcType.TYPE_LONG:
                case ArcType.TYPE_INT64:
                    return typeof(long);

                case ArcType.TYPE_SINGLE:
                case ArcType.TYPE_FLOAT:
                case ArcType.TYPE_FLOAT32:
                    return typeof(float);

                case ArcType.TYPE_DOUBLE:
                    return typeof(double);

                case ArcType.TYPE_DATESTRING:
                    return typeof(DateString);

                case ArcType.TYPE_STRING:
                case ArcType.TYPE_STRING_BY_HEADER_REF:
                case ArcType.TYPE_STRING_BY_LEN:
                case ArcType.TYPE_STRING_BY_REF:
                    return typeof(string);
            }

            return typeof(object);
        }

        /// <summary>
        /// Parse a user defined Enums lua that has been saved to the .\Modules folder
        /// </summary>
        /// <param name="key">Name of the table defined in the enum.lua</param>
        /// <returns>Prepared dictionary of enum values</returns>
        public Dictionary<string, int> GetEnum(string key)
        {
            Dictionary<string, int> enumDict = new Dictionary<string, int>();

            if (scriptObj.Globals[key] != null)
            {
                Table enumTbl = scriptObj.Globals[key] as Table;

                for (int i = 1; i < enumTbl.Length + 1; i++)
                {
                    Table valTbl = enumTbl.Get(i).Table;

                    enumDict.Add(valTbl.Get(1).String, (int)valTbl.Get(2).Number);
                }
            }

            return enumDict;
        }

        /// <summary>
        /// Buffer the file at the provided filename and parse the contents based on the structure contained in the previously parsed structure lua.
        /// </summary>
        /// <param name="filename">Path to the rdb file to be read</param>
        public void Read(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"Cannot locate file!\n\t- {filename}");

            using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(filename)))
            {
                Header.Read(stream);

                SID.New();

                for (int i = 0; i < RowCount; i++)
                {
                    int readCount = 1;

                    if (SpecialCase == CaseFlag.DoubleLoop)
                    {
                        byte[] buffer = new byte[4];

                        stream.Read(buffer, 0, buffer.Length);

                        readCount = BitConverter.ToInt32(buffer, 0);
                    }

                    for (int l = 0; l < readCount; l++)
                    {
                        RowObject dataRow = new RowObject(DataCells);

                        dataRow.Read(stream, Header);

                        Rows.Add(dataRow);
                    }
                }
            }
        }

        /// <summary>
        /// Write the previously loaded contents of this structure object to disk at the provided path
        /// </summary>
        /// <param name="filename"></param>
        public void Write(string filename)
        {
            using (MemoryStream Stream = new MemoryStream())
            {
                Header["Signature"] = ByteUtility.ToBytes("\0\0\0\0\0\0\0\0Written by Archemedes v0.1.0", 120);
                Header.Write(Stream);

                int rowCount = Rows.Count;

                if (SpecialCase == CaseFlag.DoubleLoop)
                {
                    int pVal = 0;

                    for (int rowIdx = 0; rowIdx < rowCount; rowIdx++)
                    {
                        RowObject row = Rows[rowIdx];
                        int cVal = row.GetValueByFlag<int>(CellFlags.LoopCounter);

                        if (pVal != cVal)
                        {
                            IntCell counter = row.GetCellByFlag(CellFlags.LoopCounter) as IntCell;
                            List<RowObject> treeRows = Rows.FindAll(r => (int)r[counter.Index] == cVal);

                            byte[] buffer = BitConverter.GetBytes(treeRows.Count);

                            Stream.Write(buffer, 0, buffer.Length);

                            for (int tR = 0; tR < treeRows.Count; tR++)
                                treeRows[tR].Write(Stream);

                            pVal = cVal;
                        }
                    }
                }
                else
                    for (int i = 0; i < rowCount; i++)
                        Rows[i].Write(Stream);

                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                    Stream.WriteTo(fs);
            }
        }

        /// <summary>
        /// Generate a csv document from the schema and data contents of this structure object
        /// </summary>
        /// <returns>Formatted string representing csv file data</returns>
        public string GenerateCSV()
        {
            string csv = string.Empty;

            string[] names = VisibleCellNames;

            csv += $"{string.Join(", ", names)}\n";

            for (int i = 0; i < Rows.Count; i++)
                csv += $"{Rows[i].ToCSVString()}\n";

            return csv;
        }

        /// <summary>
        /// Search the loaded collection of rows for cells containing a value'(s) that meet the provided operator criteria
        /// </summary>
        /// <param name="column">Cell being targeted</param>
        /// <param name="value">Value to be compared against</param>
        /// <param name="op">Operator of the comparison</param>
        /// <returns>List of row/cell index pairs if returnType is Indicies, List of value objects otherwise</returns>
        public object Search(string column, Array value, SearchOperator op) => Search(column, value, op);

        /// <summary>
        /// Search the loaded collection of rows for cells containing a value'(s) that meet the provided operator criteria
        /// </summary>
        /// <param name="column">Cell being targeted</param>
        /// <param name="value">Value to be compared against</param>
        /// <param name="ret">Type of data to be returned.</param>
        /// <returns>List of row/cell index pairs if returnType is Indicies, List of value objects otherwise</returns>
        public object Search(string column, Array value, SearchReturn ret) => Search(column, value, SearchOperator.Equal, ret);

        /// <summary>
        /// Search the loaded collection of rows for cells containing a value'(s) that meet the provided operator criteria
        /// </summary>
        /// <param name="column">Cell being targeted</param>
        /// <param name="value">Value to be compared against</param>
        /// <param name="op">Operator of the comparison</param>
        /// <param name="returnType">Type of data to be returned.</param>
        /// <returns>List of row/cell index pairs if return type is Indicies, List of value objects otherwise</returns>
        public object Search(string column, Array value, SearchOperator op = SearchOperator.Equal, SearchReturn returnType = SearchReturn.Values) 
        {
            if (column == null)
                throw new ArgumentNullException("Column name cannot be null!");

            CellBase cell = DataCells.Find(c => c.Name == column);

            IList values = new List<object>();

            if (returnType == SearchReturn.Indicies)
                values = new List<KeyValuePair<int, int>>();

            if (cell == null)
                throw new NullReferenceException($"Could not find cell by name: {column}");
                                            
            for (int i = 0; i < RowCount; i++)
            {
                RowObject row = Rows[i];

                for (int j = 0; j < value.Length; j++) // Since the input 'value' is an array of values between 1 and n elements, lets loop the generic array
                {
                    dynamic curVal = (cell.PrimaryType == typeof(string)) ? Encoding.GetString((byte[])row[column]) : Convert.ChangeType(row[column], cell.PrimaryType);
                    dynamic cmpVal = Convert.ChangeType(value.GetValue(j), cell.PrimaryType); // Now we can convert the generic array element into a proper value type

                    bool add = false;

                    switch (op)
                    {
                        case SearchOperator.Above:
                            add = curVal > cmpVal;
                            break;

                        case SearchOperator.Below:
                            add = curVal < cmpVal;
                            break;

                        case SearchOperator.Equal:
                            add = curVal == cmpVal;
                            break;

                        case SearchOperator.NotEqual:
                            add = curVal != cmpVal;
                            break;

                        case SearchOperator.Like:
                            {
                                string curStr = curVal.ToString();
                                string cmpStr = cmpVal.ToString();

                                if (curStr.Contains(cmpStr))
                                    add = true;
                            }
                            break;

                        case SearchOperator.NotLike:
                            {
                                string curStr = curVal.ToString();
                                string cmpStr = cmpVal.ToString();

                                if (!curStr.Contains(cmpStr))
                                    add = true;
                            }
                            break;

                        case SearchOperator.Between:
                            {
                                if (value.Length != 2)
                                    throw new InvalidDataException("the input value must consist of two comma delimited numbers!");

                                int min = Convert.ToInt32(value.GetValue(0));
                                int max = Convert.ToInt32(value.GetValue(1));

                                if (curVal >= min && curVal <= max)
                                    add = true;
                            }
                            break;
                    }

                    if (add)
                        if (returnType == SearchReturn.Indicies)
                            values.Add(new KeyValuePair<int, int>(i, cell.Index));
                        else
                            values.Add(curVal);
                }
            }

            if (returnType == SearchReturn.Values)
                return values.Cast<object>().ToArray();

            return values.Cast<KeyValuePair<int, int>>().ToArray();
        }

        /// <summary>
        /// Create a clone of this structure object (contains only the schema, not actual data!)
        /// </summary>
        /// <returns>Clone of this structure</returns>
        public object Clone()
        {
            StructureObject cloneStruct = new StructureObject(FilePath, false);

            cloneStruct.Name = Name;
            cloneStruct.Author = Author;
            cloneStruct.RDBName = RDBName;
            cloneStruct.DatabaseName = DatabaseName;
            cloneStruct.TableName = TableName;
            cloneStruct.SelectStatement = SelectStatement;
            cloneStruct.Version = Version;
            cloneStruct.Epic = Epic;
            cloneStruct.Encoding = Encoding;
            cloneStruct.SpecialCase = SpecialCase;
            cloneStruct.HeaderType = HeaderType;

            for (int i = 0; i < HeaderCells.Count; i++)
            {
                ICloneable clonableCell = HeaderCells[i] as ICloneable;

                cloneStruct.HeaderCells.Add((CellBase)clonableCell.Clone());
            }

            for (int i = 0; i < DataCells.Count; i++)
            {
                ICloneable clonableCell = DataCells[i] as ICloneable;

                cloneStruct.DataCells.Add((CellBase)clonableCell.Clone());
            }

            return cloneStruct;
        }
    }

    /// <summary>
    /// Enumerable object representing a collection of cells which exposes its own IO calls.
    /// </summary>
    public class RowObject : IEnumerable<KeyValuePair<CellBase, object>>
    {
        List<CellBase> cells = new List<CellBase>();
        object[] values = null;

        /// <summary>
        /// Construct a new row object instance based on the provided cells
        /// </summary>
        /// <param name="cells">Descriptions of the data to be held in this row object</param>
        public RowObject(List<CellBase> cells)
        {
            this.cells = cells;

            values = new object[cells.Count];
        }

        /// <summary>
        /// Get the boxed value object for the provided cell name
        /// </summary>
        /// <param name="name">Name of the cell to be described</param>
        /// <returns>Base cell class capable of describing the cell</returns>
        public object this[string name]
        {
            get
            {
                int index = cells.Find(c => c.Name == name).Index;

                return values?[index];
            }
            set
            {
                int index = cells.Find(c => c.Name == name).Index;

                if (index >= 0 && index < values.Length)
                    values[index] = value;
            }
        }

        /// <summary>
        /// Get the boxed value object at the given index
        /// </summary>
        /// <param name="index">0 based index</param>
        /// <returns>Boxed (object) value</returns>
        public object this[int index]
        {
            get => values?[index];
            set
            {
                if (index >= 0 && index < values.Length)
                    values[index] = value;
            }
        }

        /// <summary>
        /// Get the first boxed value object whose cell bears the provided flag
        /// </summary>
        /// <param name="flags">Flag'(s) to be matched against</param>
        /// <returns>Boxed (object) value</returns>
        public object this[CellFlags flags] => values?[GetCellByFlag(flags).Index];

        /// <summary>
        /// Length (of cells) contained in this row object
        /// </summary>
        public int Length => cells.Count;

        /// <summary>
        /// Enables enumeration over this row object by returning key value pairs of CellBase, object
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<CellBase, object>> GetEnumerator()
        {
            foreach (CellBase cell in cells)
                yield return new KeyValuePair<CellBase, object>(cell, values[cell.Index]);
        }

        /// <summary>
        /// Get a given cells description object
        /// </summary>
        /// <param name="index">Zero based index of the cell</param>
        /// <returns>Populated CellBase or null</returns>
        public CellBase GetCell(int index) => cells?[index];

        /// <summary>
        /// Get a given cells description object
        /// </summary>
        /// <param name="name">Name of the cell</param>
        /// <returns>Populated CellBase or null</returns>
        public CellBase GetCell(string name) => cells?.Find(c => c.Name == name);

        /// <summary>
        /// Get Cell description by the provided flags vector
        /// </summary>
        /// <param name="flags">Flags Vector <b>int</b> representing flag list</param>
        /// <returns>Cell description</returns>
        public CellBase GetCellByFlag(CellFlags flags) => cells.Find(c => c.Flags == flags);

        /// <summary>
        /// Get the bit cells (and their values) bearing the provided dependency
        /// </summary>
        /// <param name="name">Cell the bit fields depend on</param>
        /// <returns>Collection of cells and their values</returns>
        public KeyValuePair<CellBase, object>[] GetBits(string name)
        {
            List<CellBase> retCells = cells.FindAll(c => c.Dependency == name);
            KeyValuePair<CellBase, object>[] results = new KeyValuePair<CellBase, object>[retCells.Count];

            for (int i = 0; i < retCells.Count; i++)
                results[i] = new KeyValuePair<CellBase, object>(retCells[i], values[retCells[i].Index]);

            return results;
        }

        /// <summary>
        /// Get the value of a cell bearing the provided flags vector
        /// </summary>
        /// <typeparam name="T">Type to return the value as</typeparam>
        /// <param name="flags">Flags vector to be checked against</param>
        /// <returns>Value converted to T</returns>
        public T GetValueByFlag<T>(CellFlags flags) => (T)Convert.ChangeType(values?[cells.Find(c => c.Flags == flags).Index], typeof(T));

        /// <summary>
        /// Iterate over and read all cells in this row object
        /// </summary>
        /// <param name="stream">Stream of the rdb file being read</param>
        public void Read(MemoryStream stream, RowObject header = null)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                CellBase cell = cells[i];

                cell.Stream = stream;

                if (cell.SecondaryType == ArcType.TYPE_BIT_FROM_VECTOR) // this type is not physically read from the stream
                {
                    string depObjName = cell.Dependency;

                    int vector = (int)this[depObjName];
                    int offset = cell.Offset;

                    values[i] = Convert.ToInt32((vector & (1 << offset)) != 0);

                    continue;
                }
                else if (cell.SecondaryType == ArcType.TYPE_SKIP) // Is not actually read, just move the stream.
                {
                    values[i] = new byte[cell.Length];

                    stream.Seek(cell.Length, SeekOrigin.Current);

                    continue;
                }
                else if (cell.SecondaryType == ArcType.TYPE_COPY_INT32) // Value is fetched from previously read cell.
                {
                    CellBase depCell = cells?.Find(c => c.Name == cell.Dependency);

                    values[i] = values[depCell.Index];

                    stream.Seek(4, SeekOrigin.Current);

                    continue;
                }
                else if (cell.SecondaryType == ArcType.TYPE_SID) // Is not actually read, just increment the sid value and continue;
                {
                    values[i] = SID.Increment;

                    continue;
                }
                else if (cell.SecondaryType == ArcType.TYPE_STRING_BY_LEN) // Must pass in the length stored by the dependency cell before reading
                {
                    string depObjName = cell.Dependency;

                    cell.Length = (int)this[depObjName];
                }
                else if (cell.SecondaryType == ArcType.TYPE_STRING_BY_HEADER_REF) // Must get the length of the cell by its dependency header cell
                {
                    string depObjName = cell.Dependency;

                    cell.Length = (int)header[depObjName];
                }

                ICellIO readableObj = cells[i] as ICellIO;

                values[i] = readableObj.Read();
            }
        }

        /// <summary>
        /// Iterate over and write all cell data in this row object to disk
        /// </summary>
        /// <param name="stream">Stream of the rdb file being written to</param>
        public void Write(MemoryStream stream)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                CellBase cell = cells[i];

                // Bits get collected into a BIT_FROM_VECTOR, do not write!
                if (cell.SecondaryType == ArcType.TYPE_BIT_FROM_VECTOR)
                    continue;

                // Generate a new bit vector (int) from the toggled offsets
                if (cell.SecondaryType == ArcType.TYPE_BIT_VECTOR)
                {
                    // Get the dependent bit fields
                    KeyValuePair<CellBase, object>[] bits = GetBits(cell.Name);

                    // Create a default vector
                    int vector = default(int);

                    // Toggle the bit indexes appropriately
                    foreach (KeyValuePair<CellBase, object> bit in bits)
                        vector |= (Convert.ToInt32(bit.Value) << bit.Key.Offset);

                    // Update this cells value accordingly and proceed to write
                    values[cell.Index] = vector;
                }
                else if (cell.SecondaryType == ArcType.TYPE_SKIP) // Create the blank data to be written
                    values[cell.Index] = new byte[cell.Length];
                else if (cell.SecondaryType == ArcType.TYPE_COPY_INT32)
                {
                    CellBase depCell = cells?.Find(c => c.Name == cell.Dependency);

                    values[cell.Index] = values[depCell.Index];
                }

                cell.Stream = stream;

                ICellIO writableObj = cell as ICellIO;

                writableObj.Write(values[i]);
            }
        }

        /// <summary>
        /// Convert this rows contents into a csv string
        /// </summary>
        /// <returns>Formatted csv string</returns>
        public string ToCSVString()
        {
            List<string> valueStrings = new List<string>();

            for (int i = 0; i < Length; i++)
            {           
                CellBase cell = GetCell(i);
                string cVal;

                if (cell.Flags.HasFlag(CellFlags.SqlIgnore))
                    continue;

                if (cell.PrimaryType == typeof(string))
                    cVal = ByteUtility.ToString((byte[])values[cell.Index]);
                else
                    cVal = values[cell.Index].ToString();

                valueStrings.Add(cVal);
            }

            return string.Join(", ", valueStrings);
        }

        /// <summary>
        /// Convert the schema and data of this row object into a prepared insert or update sql statement string.
        /// </summary>
        /// <param name="tableName">Table name (optional, <b>use only with type Update</b>)</param>
        /// <param name="type">Type of SQL string to be created</param>
        /// <param name="whereColumn">Name of column to use for where statement (optional, <b>use only with type Update</b>)</param>
        /// <returns>Prepared SQL statement</returns>
        public string ToSqlString(string tableName, SqlStringType type = SqlStringType.Insert, string whereColumn = null)
        {
            if (type == SqlStringType.Update && string.IsNullOrEmpty(whereColumn))
                throw new Exception("SqlStringType.Update requires a valid where column!");

            string cmdStr = (type == SqlStringType.Insert) ? $"INSERT INTO {tableName} " : $"UPDATE {tableName} SET ";
            string cellStr = string.Empty;
            string valStr = string.Empty;
            string tmpStr = string.Empty;

            for (int i = 0; i <= Length; i++)
            {
                CellBase cell;

                if (i == Length)
                {
                    if (type == SqlStringType.Insert)
                        return $"{cmdStr}({cellStr.Remove(cellStr.Length - 1)}) values ({valStr.Remove(valStr.Length - 1)})";
                    else
                    {
                        cell = GetCell(whereColumn);
                        return $"{cmdStr}{tmpStr.Remove(tmpStr.Length - 2, 2)} where [{whereColumn}] = {values[cell.Index]}";
                    }
                }

                cell = GetCell(i);
                object valObj = values[cell.Index];

                if (cell.Flags.HasFlag(CellFlags.SqlIgnore))
                    continue;

                if (type == SqlStringType.Insert)
                    cellStr += $"[{cell.Name}]";
                else
                {
                    cellStr = $"[{cell.Name}] = ";
                    valStr = string.Empty;
                }

                // Strings must be wrapped in '' and instances of the ' literal must be changed to sql friendly ''
                if (cell.SecondaryType == ArcType.TYPE_STRING || cell.SecondaryType == ArcType.TYPE_STRING_BY_LEN || cell.SecondaryType == ArcType.TYPE_STRING_BY_REF)
                {
                    string objStr = ByteUtility.ToString((byte[])valObj).ToString().TrimEnd('\0');
                    string convStr = objStr.Replace("'", "''");
                    valStr += $"'{convStr}'";
                }
                else if (cell.SecondaryType == ArcType.TYPE_DATETIME)
                {
                    //yyyy-MM-dd HH:mm:ss.fff
                    //1999-12-31 15:00:00.000
                    DateTime objDT = (DateTime)valObj;
                    valStr += $"'{objDT.ToString("yyyy-MM-dd HH:mm:ss.fff")}'";
                }
                else
                    valStr += valObj.ToString();

                if (type == SqlStringType.Insert)
                {
                    cellStr += ",";
                    valStr += ",";
                }
                else
                    tmpStr += $"{cellStr} {valStr}, ";
            }

            return null;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>
    /// Object representing a date created from an 8 byte string
    /// </summary>
    public class DateString
    {
        string format = "yyyyMMdd";

        public DateTime DateTime;

        public DateString(string value)
        {
            int year = 1990;
            int month = 1;
            int day = 1;

            if (!int.TryParse(value.Substring(0, 4), out year))
                throw new Exception("Failed to parse the year from the provided date string");

            if (!int.TryParse(value.Substring(4, 2), out month))
                throw new Exception("Failed to parse the month from the provided date string");

            if (!int.TryParse(value.Substring(6, 2), out day))
                throw new Exception("Failed to parse the day from the provided date string");

            DateTime = new DateTime(year, month, day);
        }

        public string ToString(string format)
        {
            if (format == null)
                return null;

            this.format = format;

            return ToString();
        }

        public override string ToString() => DateTime.ToString(format);
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Integer bits scrambling utility
    /// </summary>
    public struct ScrambleMap
    {
        /// <summary>
        /// Construct the map table
        /// </summary>
        static ScrambleMap()
        {
            int len = 32;

            for (int i = 0; i < len; ++i)
                map[i] = i;

            int idx = 3; //gap

            for (int i = 0; i < len; ++i)
            {
                int t = map[i];

                while (idx >= len)
                    idx -= len;

                map[i] = map[idx];
                map[idx] = t;

                idx += (3 + i);
            }
        }

        /// <summary>
        /// Scramble the bits of the given value to obscure the true integer
        /// </summary>
        /// <param name="value">Unencoded Value</param>
        /// <returns>Encoded Value</returns>
        public static int Scramble(int value)
        {
            int r = 0;

            for (int i = 0; i < 32; i++)
                if ((value & (1 << i)) != 0)
                    r |= (1 << map[i]);

            return r;
        }

        /// <summary>
        /// Restore the bits of the given value to restore the original unencoded value.
        /// </summary>
        /// <param name="value">Encoded value</param>
        /// <returns>Unencoded value</returns>
        public static int Restore(int value)
        {
            int r = 0;

            for (int i = 0; i < 32; ++i)
                if ((value & (1 << map[i])) != 0)
                    r |= (1 << i);

            return r;
        }

        static int[] map = new int[32];
    }

    /// <summary>
    /// Seeded identity value that can be incremented and decremented
    /// </summary>
    public static class SID
    {
        /// <summary>
        /// The current value of this seeded id
        /// </summary>
        public static int Current { get; set; } = 0;

        /// <summary>
        /// Increment (by 1) and return the value of this seeded id
        /// </summary>
        public static int Increment => ++Current;

        /// <summary>
        /// Decrement (by 1) and return the value of this seeded id
        /// </summary>
        public static int Decrement => Math.Max(--Current, 0);

        /// <summary>
        /// Reset the value of this seed id to 0 unless another value is provided
        /// </summary>
        /// <param name="value"></param>
        public static void New(int value = 0) => Current = value;
    }

    /// <summary>
    /// Conversion of string<>byte[] using the provided Encoding
    /// </summary>
    public static class ByteUtility
    {
        /// <summary>
        /// Encoding used to convert string<>byte[]
        /// </summary>
        public static Encoding Encoding = Encoding.Default;

        /// <summary>
        /// Convert the provided string into its encoded bytes (w/ additional blank bytes if length is provided)
        /// </summary>
        /// <param name="text">String to be encoded/converted</param>
        /// <param name="length">Length of the final array (optional)</param>
        /// <returns>Enocded byte[] representing a string</returns>
        public static byte[] ToBytes(string text, int length = -1) 
        {
            byte[] msgBuffer = Encoding.GetBytes(text);
            byte[] outBuffer = msgBuffer;

            if (outBuffer == null)
                return null;

            if (length != -1)
            {
                outBuffer = new byte[length];
                Buffer.BlockCopy(msgBuffer, 0, outBuffer, 0, msgBuffer.Length);
            }

            return outBuffer;
        }

        /// <summary>
        /// Convert the provided byte[] into an encoded string
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string ToString(byte[] buffer) => Encoding?.GetString(buffer);
    }

    #endregion

    #region Cells

    /// <summary>
    /// Base cell object describing a section of data in a given rdb
    /// </summary>
    public class CellBase
    {
        /// <summary>
        /// Stream object used to read/write data to/from file buffer.
        /// </summary>
        public MemoryStream Stream = null;

        private int index = -1;

        /// <summary>
        /// Index this Cell occupies in its parent row object (<b>immutable once set)</b>
        /// </summary>
        public int Index
        {
            get => index;
            set
            {
                if (index == -1)
                    index = value;
            }
        }

        /// <summary>
        /// Name of this cell
        /// </summary>
        public string Name;

        /// <summary>
        /// Length (in bytes) of this cell
        /// </summary>
        public int Length { get; set; } = -1;

        /// <summary>
        /// Offset of this cells value <b>(only used for ArcType.BIT_FROM_VECTOR!)</b>
        /// </summary>
        public int Offset;

        /// <summary>
        /// Default value this Cell would need if no other value has been provided.
        /// </summary>
        public object Default = new object();

        /// <summary>
        /// Primary type of the actual data this cell is linked to.
        /// </summary>
        public Type PrimaryType { get; set; } = typeof(object);

        /// <summary>
        /// Secondary type of this cell used in engine operations
        /// </summary>
        public ArcType SecondaryType { get; set; } = ArcType.NONE;

        /// <summary>
        /// Cell flags that may alter the behavior of the engine
        /// </summary>
        public CellFlags Flags { get; set; } = CellFlags.None;

        /// <summary>
        /// Cell whose value this cell depends on to be processed properly
        /// </summary>
        public string Dependency;

        /// <summary>
        /// Construct a new instance of the base cell descriptor class
        /// </summary>
        /// <param name="name">Name of the cell being created</param>
        /// <param name="type">Primary data type of the created cell</param>
        /// <param name="secondaryType">Secondary engine type of the created cell</param>
        public CellBase(string name, Type type, ArcType secondaryType)
        {
            Name = name;
            PrimaryType = type;
            SecondaryType = secondaryType;
            Length = defaultLength(secondaryType);
            Default = type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// Get the default length of a given LuaType
        /// </summary>
        /// <param name="type">LuaType loaded from structure lua</param>
        /// <returns>Length <b>(in bytes)</b></returns>
        int defaultLength(ArcType type)
        {
            switch (type)
            {
                case ArcType.TYPE_BYTE:
                case ArcType.TYPE_BIT_FROM_VECTOR:
                    return 1;

                case ArcType.TYPE_SHORT:
                case ArcType.TYPE_USHORT:
                case ArcType.TYPE_INT16:
                case ArcType.TYPE_UINT_16:
                    return 2;

                case ArcType.TYPE_INT:
                case ArcType.TYPE_UINT:
                case ArcType.TYPE_INT32:
                case ArcType.TYPE_UINT32:
                case ArcType.TYPE_BIT_VECTOR:
                case ArcType.TYPE_SID:
                case ArcType.TYPE_STRING_LEN:
                case ArcType.TYPE_SINGLE:
                case ArcType.TYPE_FLOAT:
                case ArcType.TYPE_FLOAT32:
                case ArcType.TYPE_DECIMAL:
                    return 4;

                case ArcType.TYPE_LONG:
                case ArcType.TYPE_INT64:
                case ArcType.TYPE_DOUBLE:
                case ArcType.TYPE_DATESTRING:
                    return 8;

                case ArcType.TYPE_BYTE_ARRAY:
                case ArcType.TYPE_STRING:
                case ArcType.TYPE_STRING_BY_HEADER_REF:
                case ArcType.TYPE_STRING_BY_LEN:
                case ArcType.TYPE_STRING_BY_REF:
                    return -1;

                default:
                    return -1;
            }
        }
    }

    /// <summary>
    /// Cell representing a byte value
    /// </summary>
    public class ByteCell : CellBase, ICellIO, ICloneable
    {
        public ByteCell(string name) : base(name, typeof(byte), ArcType.TYPE_BYTE)
        {
        }

        public ByteCell(string name, CellFlags flags) : base(name, typeof(byte), ArcType.TYPE_BYTE)
        {
            base.Flags = flags;
        }

        public object Read() => (byte)Stream.ReadByte();

        public void Write(object value)
        {
            byte val = Convert.ToByte(value);

            byte[] buffer = new byte[1] { val };

            Stream.Write(buffer, 0, buffer.Length);
        }

        public object Clone()
        {
            ByteCell cell = new ByteCell(Name);

            cell.Index = Index;
            cell.Flags = Flags;
            cell.Length = Length;
            cell.Offset = Offset;
            cell.PrimaryType = PrimaryType;
            cell.SecondaryType = SecondaryType;

            return cell;
        }
    }

    /// <summary>
    /// Cell representing a collection of bytes
    /// </summary>
    public class ByteArrayCell : CellBase, ICellIO, ICloneable
    {
        public ByteArrayCell(string name, int length) : base(name, typeof(byte[]), ArcType.TYPE_BYTE_ARRAY)
        {
            if (length == -1)
                throw new ArgumentOutOfRangeException("Cannot construct ByteArrayCell, length is invalid!");

            Length = length;
        }

        public ByteArrayCell(string name, int length, CellFlags flags) : base(name, typeof(byte[]), ArcType.TYPE_BYTE_ARRAY)
        {
            Flags = flags;

            if (length == -1)
                throw new ArgumentOutOfRangeException("Cannot construct ByteArrayCell, length is invalid!");

            Length = length;
        }

        public object Read()
        {
            if (Length == -1)
                throw new ArgumentOutOfRangeException("Cannot read ByteArrayCell, length is invalid!");

            byte[] value = new byte[Length];

            Stream.Read(value, 0, value.Length);

            return value;
        }

        public void Write(object value)
        {
            byte[] buffer = (byte[])value;

            Stream.Write(buffer, 0, buffer.Length);
        }

        public object Clone()
        {
            ByteArrayCell cell = new ByteArrayCell(Name, Length);

            cell.Index = Index;
            cell.Flags = Flags;
            cell.Offset = Offset;
            cell.PrimaryType = PrimaryType;
            cell.SecondaryType = SecondaryType;

            return cell;
        }
    }

    /// <summary>
    /// Cell representing a decimal value
    /// </summary>
    public class DecimalCell : CellBase, ICellIO, ICloneable
    {
        public DecimalCell(string name) : base(name, typeof(decimal), ArcType.TYPE_DECIMAL) { }

        public DecimalCell(string name, CellFlags flags) : base (name, typeof(decimal), ArcType.TYPE_DECIMAL) 
        {
            Flags = flags;
        }

        public object Read()
        {
            byte[] buffer = new byte[4];

            Stream.Read(buffer, 0, buffer.Length);

            int nVal = BitConverter.ToInt32(buffer, 0);
            decimal dVal = nVal / 100m;

            return dVal;
        }

        public void Write(object value)
        {
            decimal val = (decimal)value;
            int nVal = Convert.ToInt32(val * 100);

            byte[] buffer = BitConverter.GetBytes(nVal);

            Stream.Write(buffer, 0, buffer.Length);
        }

        public object Clone()
        {
            DecimalCell cell = new DecimalCell(Name);

            cell.Index = Index;
            cell.Flags = Flags;
            cell.Offset = Offset;
            cell.PrimaryType = PrimaryType;
            cell.SecondaryType = SecondaryType;

            return cell;
        }
    }

    /// <summary>
    /// Cell representing a short or int16 value
    /// </summary>
    public class ShortCell : CellBase, ICellIO, ICloneable
    {
        public ShortCell(string name) : base(name, typeof(short), ArcType.TYPE_SHORT) { }

        public ShortCell(string name, CellFlags flags) : base(name, typeof(short), ArcType.TYPE_SHORT)
        {
            Flags = flags;
        }

        public object Read()
        {
            byte[] buffer = new byte[2];

            Stream.Read(buffer, 0, buffer.Length);

            return BitConverter.ToInt16(buffer, 0);
        }

        public void Write(object value)
        {
            short val = Convert.ToInt16(value);

            byte[] buffer = BitConverter.GetBytes(val);

            Stream.Write(buffer, 0, buffer.Length);
        }

        public object Clone()
        {
            ShortCell cell = new ShortCell(Name);

            cell.Index = Index;
            cell.Flags = Flags;
            cell.Offset = Offset;
            cell.PrimaryType = PrimaryType;
            cell.SecondaryType = SecondaryType;

            return cell;
        }
    }

    /// <summary>
    /// Cell representing a <b>4</b> byte <b>int</b> / <b>Int32</b> value
    /// </summary>
    public class IntCell : CellBase, ICellIO, ICloneable
    {
        public IntCell(string name) : base(name, typeof(int), ArcType.TYPE_INT32)
        {
        }

        public IntCell(string name, CellFlags flags) : base(name, typeof(int), ArcType.TYPE_INT32)
        {
            base.Flags = flags;
        }

        public object Read()
        {
            byte[] buffer = new byte[4];

            Stream.Read(buffer, 0, buffer.Length);

            return BitConverter.ToInt32(buffer, 0);
        }

        public void Write(object value)
        {
            int val = Convert.ToInt32(value);

            byte[] buffer = BitConverter.GetBytes(val);

            Stream.Write(buffer, 0, buffer.Length);
        }

        public object Clone()
        {
            IntCell cell = new IntCell(Name);

            cell.Index = Index;
            cell.Flags = Flags;
            cell.Offset = Offset;
            cell.PrimaryType = PrimaryType;
            cell.SecondaryType = SecondaryType;

            return cell;
        }
    }

    /// <summary>
    /// Cell representing a 4 byte encoded <b>int</b> value
    /// </summary>
    public class EncodedIntCell : CellBase, ICellIO, ICloneable
    {
        public EncodedIntCell(string name) : base(name, typeof(int), ArcType.TYPE_ENCODED_INT32) { }

        public object Read() 
        {
            byte[] buffer = new byte[4];

            Stream.Read(buffer, 0, buffer.Length);

            int encodedVal = BitConverter.ToInt32(buffer, 0);

            return ScrambleMap.Restore(encodedVal);
        }

        public void Write(object value) 
        {
            int val = Convert.ToInt32(value);
            int encVal = ScrambleMap.Scramble(val);

            byte[] buffer = BitConverter.GetBytes(encVal);

            Stream.Write(buffer, 0, buffer.Length);
        }

        public object Clone()
        {
            EncodedIntCell cell = new EncodedIntCell(Name);

            cell.Index = Index;
            cell.Flags = Flags;
            cell.Offset = Offset;
            cell.PrimaryType = PrimaryType;
            cell.SecondaryType = SecondaryType;

            return cell;
        }
    }

    /// <summary>
    /// Cell representing an 8 byte <b>long</b> or <b>Int64</b> value
    /// </summary>
    public class LongCell : CellBase, ICellIO, ICloneable
    {
        public LongCell(string name) : base(name, typeof(long), ArcType.TYPE_LONG) { }

        public LongCell(string name, CellFlags flags) : base(name, typeof(long), ArcType.TYPE_LONG)
        {
            Flags = flags;
        }

        public object Read()
        {
            byte[] buffer = new byte[8];

            Stream.Read(buffer, 0, buffer.Length);

            return BitConverter.ToInt64(buffer, 0);
        }

        public void Write(object value) 
        {
            long val = (long)value;

            byte[] buffer = BitConverter.GetBytes(val);

            Stream.Write(buffer, 0, buffer.Length);
        }

        public object Clone()
        {
            LongCell cell = new LongCell(Name);

            cell.Index = Index;
            cell.Flags = Flags;
            cell.Offset = Offset;
            cell.PrimaryType = PrimaryType;
            cell.SecondaryType = SecondaryType;

            return cell;
        }
    }

    /// <summary>
    /// Cell representing an 8 byte <b>double</b> value
    /// </summary>
    public class DoubleCell : CellBase, ICellIO, ICloneable
    {
        public DoubleCell(string name) : base(name, typeof(double), ArcType.TYPE_DOUBLE) { }

        public DoubleCell(string name, CellFlags flags) : base(name, typeof(double), ArcType.TYPE_DOUBLE)
        {
            Flags = flags;
        }

        public object Read()
        {
            byte[] buffer = new byte[8];

            Stream.Read(buffer, 0, buffer.Length);

            return BitConverter.ToDouble(buffer, 0);
        }

        public void Write(object value)
        {
            double val = Convert.ToDouble(value);

            byte[] buffer = BitConverter.GetBytes(val);

            Stream.Write(buffer, 0, buffer.Length);
        }

        public object Clone()
        {
            DoubleCell cell = new DoubleCell(Name);

            cell.Index = Index;
            cell.Flags = Flags;
            cell.Offset = Offset;
            cell.PrimaryType = PrimaryType;
            cell.SecondaryType = SecondaryType;

            return cell;
        }
    }

    /// <summary>
    /// Cell representing a 4 byte <b>single</b> / <b>float</b> value
    /// </summary>
    public class FloatCell : CellBase, ICellIO, ICloneable
    {
        public FloatCell(string name) : base(name, typeof(float), ArcType.TYPE_FLOAT) { }

        public object Read()
        {
            byte[] buffer = new byte[4];

            Stream.Read(buffer, 0, buffer.Length);

            return BitConverter.ToSingle(buffer, 0);
        }

        public void Write(object value) 
        {
            float val = Convert.ToSingle(value);

            byte[] buffer = BitConverter.GetBytes(val);

            Stream.Write(buffer, 0, buffer.Length);
        }

        public object Clone()
        {
            FloatCell cell = new FloatCell(Name);

            cell.Index = Index;
            cell.Flags = Flags;
            cell.Offset = Offset;
            cell.PrimaryType = PrimaryType;
            cell.SecondaryType = SecondaryType;

            return cell;
        }
    }

    /// <summary>
    /// Cell representing an 8 byte <b>DateString</b> value
    /// </summary>
    public class DateStringCell : CellBase, ICellIO, ICloneable
    {
        public DateStringCell(string name) : base(name, typeof(DateString), ArcType.TYPE_DATESTRING)
        {
            base.Length = 8;
        }

        public DateStringCell(string name, CellFlags flags) : base(name, typeof(DateString), ArcType.TYPE_DATESTRING)
        {
            base.Length = 8;
            base.Flags = flags;
        }

        public object Read()
        {
            if (Length != 8)
                throw new ArgumentOutOfRangeException("DateStringObject Length must be 8!");

            byte[] buffer = new byte[Length];

            Stream.Read(buffer, 0, buffer.Length);

            string dateStr = ByteUtility.ToString(buffer);

            return new DateString(dateStr);
        }

        public void Write(object value)
        {
            DateString val = value as DateString;

            string valStr = val.ToString("yyyyMMdd");

            byte[] buffer = ByteUtility.ToBytes(valStr);

            Stream.Write(buffer, 0, buffer.Length);
        }

        public object Clone()
        {
            DateStringCell cell = new DateStringCell(Name);

            cell.Index = Index;
            cell.Flags = Flags;
            cell.Offset = Offset;
            cell.PrimaryType = PrimaryType;
            cell.SecondaryType = SecondaryType;

            return cell;
        }
    }

    /// <summary>
    /// Cell representing a 4 byte <b>Datetime</b> value by calculating seconds from epoch
    /// </summary>
    public class DateTimeCell : CellBase, ICellIO, ICloneable
    {
        public DateTimeCell(string name) : base(name, typeof(DateTime), ArcType.NONE) { }

        public object Read()
        {
            byte[] buffer = new byte[4];

            Stream.Read(buffer, 0, buffer.Length);

            var secFromEpoch = BitConverter.ToInt32(buffer, 0);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return epoch.AddSeconds(secFromEpoch);
        }

        public void Write(object value)
        {
            DateTime dt = (DateTime)value;
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var seconds = Convert.ToInt32((dt - epoch).TotalSeconds);

            byte[] buffer = BitConverter.GetBytes(seconds);

            Stream.Write(buffer, 0, buffer.Length);
        }

        public object Clone()
        {
            DateTimeCell cell = new DateTimeCell(Name);

            cell.Index = Index;
            cell.Flags = Flags;
            cell.Offset = Offset;
            cell.PrimaryType = PrimaryType;
            cell.SecondaryType = SecondaryType;

            return cell;
        }
    }

    /// <summary>
    /// Cell representing a variable length string value
    /// </summary>
    public class StringCell : CellBase, ICellIO, ICloneable
    {
        public StringCell(string name, int length = -1, ArcType secondaryType = ArcType.TYPE_STRING, string dependency = null) : base(name, typeof(string), secondaryType)
        {
            Dependency = dependency;
            Length = length;
        }

        public object Read()
        {
            long begOffset = Stream.Position;
            long strLen = 0;

            // Read only til we see a null character, then record the offset and reset the position.
            // This will allow us to only read useful data
            for (int i = 0; i < Length; i++)
            {
                if (Stream.ReadByte() == 0)
                {
                    strLen = (Stream.Position - begOffset) - 1;
                    break;
                }
            }

            // Calculate the amount of space we need to advance the stream
            long remainder = Length - strLen;

            // Reset the stream to read the string
            Stream.Seek(begOffset, SeekOrigin.Begin);

            byte[] buffer = new byte[strLen];

            Stream.Read(buffer, 0, buffer.Length);

            Stream.Seek(remainder, SeekOrigin.Current);

            // We will not encode the string data at this time for performance reasons. 
            // Let the calling application determine culture and encode
            return buffer;
        }

        public void Write(object value)
        {
            // Resize the input byte array so we can set the last character as a null
            byte[] buffer = (byte[])value;
            byte[] outBuffer = new byte[buffer.Length + 1];

            Buffer.BlockCopy(buffer, 0, outBuffer, 0, buffer.Length);

            // Set the last character to a null
            outBuffer[outBuffer.Length - 1] = (byte)'\0';

            Stream.Write(outBuffer, 0, outBuffer.Length);

            // If this cell is of the secondary type string, we must pad out the rest of the cell length with 0x00
            if (SecondaryType == ArcType.TYPE_STRING)
            {
                int remainder = Length - outBuffer.Length;

                if (remainder > 0)
                {
                    buffer = new byte[remainder];

                    Stream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public object Clone()
        {
            StringCell cell = new StringCell(Name);

            cell.Index = Index;
            cell.Flags = Flags;
            cell.Offset = Offset;
            cell.PrimaryType = PrimaryType;
            cell.SecondaryType = SecondaryType;

            return cell;
        }
    }

    #endregion
}