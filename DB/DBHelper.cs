using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Collections.Specialized;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Grimoire.DB.Enums;
using Grimoire.Configuration;
using Grimoire.Logs.Enums;
using Daedalus.Enums;
using Daedalus.Structures;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Grimoire.DB
{
    /// <summary>
    /// Provides MariaDB interoperability via the MySQLConnector api
    /// </summary>
    public class DBHelper : IDisposable
    {
        Tabs.Manager TabMan = Tabs.Manager.Instance;
        ConfigManager configMan = GUI.Main.Instance.ConfigMan;

        DbConType connType = DbConType.MsSQL;

        #region MSSQL

        SqlConnection msSQL_conn;
        SqlCommand msSQL_cmd;

        #endregion

        #region MySQL

        MySqlConnection mySQL_conn;
        MySqlCommand mySQL_cmd;

        #endregion

        Stopwatch actionSW;

        public event EventHandler<DBError> Error;
        public event EventHandler<DBProgressMax> ProgressMaxSet;
        public event EventHandler<DBProgressValue> ProgressValueSet;
        public event EventHandler<DBMessage> Message;
        public event EventHandler ProgressReset;

        public void OnError(DBError e) => Error?.Invoke(this, e);
        public void OnProgressMaxSet(DBProgressMax p) => ProgressMaxSet?.Invoke(this, p);

        public void OnProgressValueSet(DBProgressValue p) => ProgressValueSet?.Invoke(this, p);

        public void OnMessage(DBMessage m) => Message?.Invoke(this, m);

        public void OnProgressReset() => ProgressReset.Invoke(this, null);

        /// <summary>
        /// Provides enumeration to the ConnectionState property
        /// </summary>
        public enum ConnectionState
        {
            Connected,
            Disconnected
        }

        /// <summary>
        /// Returns the connection state of the current m*SQL_conn object
        /// </summary>
        public ConnectionState State
        {
            get
            {
                switch (connType)
                {
                    case DbConType.MsSQL:
                        return (msSQL_conn != null && msSQL_conn.State == System.Data.ConnectionState.Open) ? ConnectionState.Connected : ConnectionState.Disconnected;

                    case DbConType.MySQL:
                        return (mySQL_conn != null && mySQL_conn.State == System.Data.ConnectionState.Open) ? ConnectionState.Connected : ConnectionState.Disconnected;

                    default:
                        return ConnectionState.Disconnected;
                }
            }
        }

        /// <summary>
        /// Initializes the DBHelper based on information stored in the ConfigMan object using the sql engine enumerated by DbConType
        /// </summary>
        /// <param name="configManager"></param>
        /// <param name="type"></param>
        public DBHelper(ConfigManager configManager, DbConType type = DbConType.MsSQL)
        {
            configMan = configManager;

            connType = type;

            switch (type)
            {
                default:
                    goto case DbConType.MsSQL;

                case DbConType.MySQL:
                    mySQL_conn = new MySqlConnection(ConnectionString);
                    mySQL_cmd = new MySqlCommand() { Connection = mySQL_conn };
                    break;

                case DbConType.MsSQL:
                    msSQL_conn = new SqlConnection(ConnectionString);
                    msSQL_cmd = new SqlCommand() { Connection = msSQL_conn };
                    break;
            }
        }

        string ConnectionString
        {
            get
            {
                string ip = configMan["IP"];
                string name = configMan["WorldName"];
                string user = configMan["WorldUser"];
                string pass = configMan["WorldPass"];
                bool trusted = configMan["Trusted", "DB"];

                switch (connType)
                {
                    case DbConType.MySQL:
                        return $"server={ip};database={name};user={user};password={pass}";

                    case DbConType.MsSQL:
                        {
                            string connStr = $"Server={ip};Database={name};";

                            if (trusted)
                                connStr += "Trusted_Connection=true;";
                            else
                                connStr += $"User ID={user};Password={pass}";

                            return connStr;
                        }
                }

                return null;
            }
            
        }

        public async Task<dynamic> Execute(DbCmdType type = DbCmdType.NonQuery)
        {
            try
            {
                return await execute(type);
            }
            catch (Exception ex)
            {
                OnError(new DBError($"An Exception has occured!\nMessage: {ex.Message}\nStack-Trace: {ex.StackTrace}"));
            }

            return null;
        }

        public async Task<dynamic> Execute(string text, DbCmdType type = DbCmdType.NonQuery)
        {
            try
            {
                NewCommand(text);

                return await execute(type);
            }
            catch (Exception ex)
            {
                OnError(new DBError($"An Exception has occured!\nMessage: {ex.Message}\nStack-Trace: {ex.StackTrace}"));
            }

            return null;
        }

        public async Task<dynamic> Execute(DbCommand cmd, DbCmdType type = DbCmdType.NonQuery)
        {
            try
            {
                SetCommand(cmd);
                return await execute(type);                
            }
            catch (Exception ex)
            {
                OnError(new DBError($"An Exception has occured!\nMessage: {ex.Message}\nStack-Trace: {ex.StackTrace}"));
            }

            return null;
        }

        async Task<dynamic> execute(DbCmdType type = DbCmdType.NonQuery)
        {
            switch (connType)
            {
                case DbConType.MsSQL:
                    switch (type)
                    {
                        case DbCmdType.NonQuery:
                            return await msSQL_cmd.ExecuteNonQueryAsync().ConfigureAwait(true);

                        case DbCmdType.Scalar:
                            return await msSQL_cmd.ExecuteScalarAsync().ConfigureAwait(true);

                        case DbCmdType.Reader:
                            return await msSQL_cmd.ExecuteReaderAsync().ConfigureAwait(true);
                    }
                    break;

                case DbConType.MySQL:
                    switch (type)
                    {
                        case DbCmdType.NonQuery:
                            return await mySQL_cmd.ExecuteNonQueryAsync().ConfigureAwait(true);

                        case DbCmdType.Scalar:
                            return await mySQL_cmd.ExecuteScalarAsync().ConfigureAwait(true);

                        case DbCmdType.Reader:
                            return await mySQL_cmd.ExecuteReaderAsync().ConfigureAwait(true);
                    }
                    break;
            }

            return null;
        }

        public async Task<dynamic> ReadTable(string table)
        {
            actionSW = new Stopwatch();
            actionSW.Start();

            if (await OpenConnection())
            {
                int rowCnt = await Execute($"select count(*) from {table}", DbCmdType.Scalar);

                string selectStr;

                if (TabMan.RDBCore.UseSelectStatement)
                    selectStr = TabMan.RDBCore.SelectStatement;
                else
                    selectStr = generateSelect(table);

                DbDataReader dbRdr = await Execute(selectStr, DbCmdType.Reader);

                Row[] data = new Row[rowCnt];
                Cell[] fieldList = Tabs.Manager.Instance.RDBCore.CellTemplate;

                try
                {
                    OnProgressMaxSet(new DBProgressMax(rowCnt));

                    int prevIdx = 0;
                    int rowIdx = 0;

                    if (dbRdr != null)
                        if (dbRdr.HasRows)
                            foreach (IDataRecord iRow in dbRdr)
                            {
                                Row row = new Row((Cell[])fieldList.Clone());

                                int sqlIdx = 0;

                                // TODO: implement null checking all possible sql fields
                                //if (iRow.IsDBNull(sqlIdx))
                                //    row[sqlIdx] = getDefault(iRow[sqlIdx].GetType());

                                for (int i = 0; i < fieldList.Length; i++)
                                {
                                    Cell field = fieldList[i];

                                    if (field.Visible)
                                    {
                                        switch (field.Type)
                                        {
                                            case CellType.TYPE_SHORT:
                                                goto case CellType.TYPE_INT_16;

                                            case CellType.TYPE_INT_16:
                                                row[i] = Convert.ToInt16(iRow[sqlIdx]);
                                                break;

                                            case CellType.TYPE_USHORT:
                                                goto case CellType.TYPE_UINT_16;

                                            case CellType.TYPE_UINT_16:
                                                row[i] = (ushort)iRow[sqlIdx];
                                                break;

                                            case CellType.TYPE_INT:
                                                goto case CellType.TYPE_INT_32;

                                            case CellType.TYPE_INT_32:
                                                {
                                                    if (iRow.IsDBNull(sqlIdx))
                                                        row[i] = default(int);
                                                    else
                                                        row[i] = Convert.ToInt32(iRow[sqlIdx]);
                                                }
                                                break;

                                            case CellType.TYPE_UINT:
                                                goto case CellType.TYPE_UINT_32;

                                            case CellType.TYPE_UINT_32:
                                                row[i] = (uint)iRow[sqlIdx];
                                                break;

                                            case CellType.TYPE_LONG:
                                                goto case CellType.TYPE_INT_64;

                                            case CellType.TYPE_INT_64:
                                                row[i] = (long)iRow[sqlIdx];
                                                break;

                                            case CellType.TYPE_DATETIME:
                                                row[i] = (DateTime)iRow[sqlIdx];
                                                break;

                                            case CellType.TYPE_BYTE:
                                                {
                                                    var fieldVal = 0;

                                                    if (iRow.IsDBNull(sqlIdx))
                                                        fieldVal = default(byte);
                                                    else
                                                    {
                                                        int value = 0;
                                                        string valStr = iRow[sqlIdx].ToString();

                                                        //Got to account for galas fuckery -_-
                                                        if (string.IsNullOrEmpty(valStr) || valStr == " " || valStr == "False" || valStr == "-")
                                                            valStr = "0";
                                                        else if (valStr == "True")
                                                            valStr = "1";

                                                        if (!int.TryParse(valStr, out value))
                                                            throw new InvalidCastException($"Cannot cast iRow[{sqlIdx}] to int!\nCurrent valStr: {valStr}");

                                                        fieldVal = Convert.ToByte(value);
                                                    }

                                                    row[sqlIdx] = fieldVal;
                                                }

                                                break;

                                            case CellType.TYPE_BIT_FROM_VECTOR:
                                                row[i] = Convert.ToInt32(iRow[sqlIdx]);
                                                break;

                                            case CellType.TYPE_DECIMAL:
                                                row[i] = Convert.ToDecimal(iRow[sqlIdx]);
                                                break;

                                            case CellType.TYPE_FLOAT:
                                            case CellType.TYPE_FLOAT_32:
                                                goto case CellType.TYPE_SINGLE;

                                            case CellType.TYPE_SINGLE:
                                                {
                                                    decimal v1 = Convert.ToDecimal(iRow[sqlIdx]);
                                                    row[i] = decimal.ToSingle(v1);
                                                }
                                                break;

                                            case CellType.TYPE_DOUBLE:
                                                row[i] = Convert.ToDouble(iRow[sqlIdx]);
                                                break;

                                            case CellType.TYPE_SID:
                                                row[i] = ++prevIdx;
                                                break;

                                            case CellType.TYPE_STRING:
                                                row[i] = iRow[sqlIdx] as string;
                                                break;

                                            case CellType.TYPE_STRING_BY_LEN:
                                                {
                                                    string szVal = iRow[sqlIdx] as string;
                                                    row[field.Dependency] = szVal.Length + 1;
                                                    row[i] = szVal;
                                                }
                                                break;

                                            case CellType.TYPE_STRING_BY_REF:
                                                row[i] = iRow[sqlIdx] as string;
                                                break;
                                        }

                                        sqlIdx++;
                                    }
                                    else //TODO: Fields defaults should be set by attempting to call Cell.Value when no value is present!
                                    {
                                        switch (field.Type)
                                        {
                                            case CellType.TYPE_BIT_VECTOR:
                                                row[i] = new BitVector32(0);
                                                break;

                                            case CellType.TYPE_BYTE:
                                                row[i] = Convert.ToByte(field.Default);
                                                break;

                                            case CellType.TYPE_SHORT:
                                            case CellType.TYPE_INT_16:
                                                row[i] = Convert.ToInt16(field.Default);
                                                break;

                                            case CellType.TYPE_INT:
                                            case CellType.TYPE_INT_32:
                                                row[i] = row.KeyIsDuplicate(field.Name) ? row.GetShownValue(field.Name) : field.Default;
                                                break;

                                            case CellType.TYPE_LONG:
                                            case CellType.TYPE_INT_64:
                                                row[i] = Convert.ToInt64(field.Default);
                                                break;

                                            case CellType.TYPE_STRING:
                                                row[i] = field.Default.ToString();
                                                break;
                                        }
                                    }
                                }

                                data[rowIdx++] = row;

                                if ((rowIdx * 100 / rowCnt) != ((rowIdx - 1) * 100 / rowCnt))
                                    OnProgressValueSet(new DBProgressValue(rowIdx));
                            }
                        else
                            OnError(new DBError("dbRdr has no rows!"));
                    else
                        OnError(new DBError("dbRdr is null!"));
                }
                catch (Exception ex)
                {
                    OnError(new DBError($"An Exception has occured!\nMessage: {ex.Message}\nStack-Trace: {ex.StackTrace}"));
                }
                finally
                {
                    CloseConnection();
                    OnProgressReset();

                    actionSW.Stop();

                    OnMessage(new DBMessage($"{table} successfully loaded in {actionSW.ElapsedMilliseconds}ms"));
                }

                return data;
            }
            else
                OnError(new DBError("Failed to connect to the Database!"));

            return null;
        }

        public async Task<dynamic> WriteTable(string table, Row[] data)
        {
            actionSW = new Stopwatch();
            actionSW.Start();

            try
            {
                if (await OpenConnection())
                {
                    clearTable(table);

                    DataTable dataTbl = generateDataTable(table);

                    OnProgressMaxSet(new DBProgressMax(data.Length, "Enumerating export data..."));

                    for (int r = 0; r < data.Length; r++) //loop rows
                    {
                        DataRow newRow = dataTbl.NewRow();
                        Row refRow = TabMan.RDBCore[r];

                        for (int c = 0; c < dataTbl.Columns.Count; c++)
                        {
                            string colName = dataTbl.Columns[c].ColumnName;

                            newRow[c] = refRow[colName];
                        }

                        dataTbl.Rows.Add(newRow);

                        if ((r * 100 / data.Length) != ((r - 1) * 100 / data.Length))
                            OnProgressValueSet(new DBProgressValue(r));
                    }

                    OnProgressReset();

                    executeBulk(dataTbl);

                    return true;
                }
                else
                    OnError(new DBError("Failed to connect to the Database!"));
            }
            catch (Exception ex)
            {
                OnError(new DBError($"An error occured during the export!\nMessage: {ex.Message}\nStack-Trace: {ex.StackTrace}"));
            }
            finally
            {
                CloseConnection();
                OnProgressReset();

                actionSW.Stop();
                Logs.Manager.Instance.Enter(Sender.DATABASE, Level.DEBUG, $"{table} exported loaded in {actionSW.ElapsedMilliseconds}ms");
            }

            return false;
        }

        DataTable generateDataTable(string table)
        {
            DataTable tbl = new DataTable() { TableName = table };

            switch (connType)
            {
                case DbConType.MsSQL:
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter($"select top 0 * from {table}", msSQL_conn))
                        sqlAdapter.Fill(tbl);
                    break;

                case DbConType.MySQL:
                    using (MySqlDataAdapter sqlAdapter = new MySqlDataAdapter($"select top 0 * from {table}", mySQL_conn))
                        sqlAdapter.Fill(tbl);
                    break;
            }

            return tbl;
        }

        void executeBulk(DataTable table)
        {
            int rowCnt = table.Rows.Count;
            long processed = 0;

            OnProgressMaxSet(new DBProgressMax(rowCnt, "Exporting data..."));

            switch (connType)
            {
                case DbConType.MsSQL:                 
                    using (SqlBulkCopy bulkCpy = new SqlBulkCopy(msSQL_conn))
                    {
                        bulkCpy.DestinationTableName = table.TableName;
                        bulkCpy.WriteToServer(table);
                        bulkCpy.SqlRowsCopied += (o, x) => 
                        {
                            processed += x.RowsCopied;

                            if ((processed * 100 / rowCnt) != ((processed - 1) * 100 / rowCnt))
                                OnProgressValueSet(new DBProgressValue((int)processed));
                        };
                    }
                        break;

                case DbConType.MySQL:
                    {
                        MySqlBulkCopy bulkCpy = new MySqlBulkCopy(mySQL_conn);
                        bulkCpy.DestinationTableName = table.TableName;
                        bulkCpy.WriteToServer(table);
                        bulkCpy.RowsCopied += (o, x) =>
                        {
                            processed += x.RowsCopied;

                            if ((processed * 100 / rowCnt) != ((processed - 1) * 100 / rowCnt))
                                OnProgressValueSet(new DBProgressValue((int)processed));
                        };
                    }
                    break;
            }

            OnProgressReset();
        }

        public async Task<bool> OpenConnection()
        {
            if (State != ConnectionState.Connected)
                switch (connType)
                {
                    case DbConType.MsSQL:
                        await msSQL_conn.OpenAsync().ConfigureAwait(true);
                        break;

                    case DbConType.MySQL:
                        await mySQL_conn.OpenAsync().ConfigureAwait(true);
                        break;
                }

            return State != ConnectionState.Disconnected;
        }

        public void CloseConnection(bool dispose = false)
        {
            switch (connType)
            {
                case DbConType.MsSQL:
                    msSQL_conn.Close();
                    if (dispose)
                        msSQL_conn.Dispose();
                    break;

                case DbConType.MySQL:
                    mySQL_conn.Close();
                    if (dispose)
                        mySQL_conn.Dispose();
                    break;
            }
        }

        public void NewCommand(string text)
        {
            switch (connType)
            {
                case DbConType.MsSQL:
                    msSQL_cmd.Connection = msSQL_conn;
                    msSQL_cmd.CommandText = text;

                    if (msSQL_cmd.Parameters.Count > 0)
                        msSQL_cmd.Parameters.Clear();

                    break;

                case DbConType.MySQL:
                    mySQL_cmd.Connection = mySQL_conn;
                    mySQL_cmd.CommandText = text;

                    if (mySQL_cmd.Parameters.Count > 0)
                        mySQL_cmd.Parameters.Clear();

                    break;
            }
        }

        public void SetCommand(DbCommand dbCmd)
        {
            switch (connType)
            {
                case DbConType.MsSQL:
                    msSQL_cmd = dbCmd as SqlCommand;
                    msSQL_cmd.Connection = msSQL_conn;
                    break;

                case DbConType.MySQL:
                    mySQL_cmd = dbCmd as MySqlCommand;
                    mySQL_cmd.Connection = mySQL_conn;
                    break;
            }
        }

        public void ClearParameters()
        {
            switch (connType)
            {
                case DbConType.MsSQL:
                    msSQL_cmd.Parameters.Clear();
                    break;

                case DbConType.MySQL:
                    mySQL_cmd.Parameters.Clear();
                    break;
            }
        }

        public void AddParameter(string name, object value, SqlDbType type)
        {
            if (msSQL_cmd != null)
            {
                SqlParameter sqlParam = msSQL_cmd.CreateParameter();
                sqlParam.ParameterName = name;
                sqlParam.Value = value;
                sqlParam.SqlDbType = type;

                msSQL_cmd.Parameters.Add(sqlParam);
            }
        }

        public void AddParameter(string name, object value, MySqlDbType type)
        {
            if (mySQL_cmd != null)
            {
                MySqlParameter mySQLParam = mySQL_cmd.CreateParameter();
                mySQLParam.ParameterName = name;
                mySQLParam.Value = value;
                mySQLParam.MySqlDbType = type;

                mySQL_cmd.Parameters.Add(mySQLParam);
            }
        }

        string generateSelect(string table)
        {
            string statement = string.Empty;

            if (Tabs.Manager.Instance.RDBCore.UseSelectStatement)
                statement = Tabs.Manager.Instance.RDBCore.SelectStatement;
            else
            {
                statement = "SELECT ";

                Cell[] fieldList = Tabs.Manager.Instance.RDBCore.VisibleCells;

                foreach (Cell field in fieldList)
                        statement += string.Format("[{0}],", field.Name);

                statement = string.Format("{0} FROM dbo.{1} with (NOLOCK)", statement.Remove(statement.Length - 1, 1), table);
            }

            return statement;
        }

        async void clearTable(string table)
        {
            string scriptDir = configMan.GetDirectory("ScriptsDirectory", "DB");

            if (configMan["Engine", "DB"] == 0) // This feature depends on MsSQL SMO and cannot be used on MySQL/MariaDB!
            {
                if (configMan["Backup", "DB"])
                    await Task.Run(() => { ScriptTable(table, true); });

                if (configMan["DropOnExport", "DB"])
                {
                    ScriptTable(table, false);

                    await Execute($"drop table {table}");

                    string scriptPath = $"{scriptDir}\\{table}_{DateTime.Now.ToString("hhMMddyyy")}_so.sql";

                    if (await executeScript(scriptPath) == -1)
                        OnError(new DBError($"Failed to execute script: {scriptPath}"));
                }
            }
            else
                await Execute($"truncate table {table}");
        }

        public void ScriptTable(string tableName, bool scriptData)
        {
            Microsoft.Data.SqlClient.SqlConnection sqlCon = new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
            ServerConnection svConn = new ServerConnection(sqlCon);
            Server sv = new Server(svConn);
            Database db = sv.Databases[configMan["WorldName", "DB"]];

            string scriptDir = configMan.GetDirectory("ScriptsDirectory", "DB");

            ScriptingOptions opts = new ScriptingOptions()
            {
                ScriptData = scriptData,
                ScriptDrops = false,
                ScriptSchema = true,
                IncludeDatabaseContext = true,
                FileName = string.Format(@"{0}\{1}_{2}{3}.sql",
                                                     scriptDir,
                                                     tableName,
                                                     DateTime.Now.ToString("hhMMddyyyy"),
                                                     (!scriptData) ? "_so" : string.Empty)
            };

            OnMessage(new DBMessage(string.Format("Scripting {0} {1}", tableName, (scriptData) ? "Data" : "Schema")));

            if (db != null)
            {
                if (db.Tables.Contains(tableName))
                    db.Tables[tableName].EnumScript(opts);
                else
                    OnError(new DBError($"Database object does not contain table with key: {tableName}"));
            }

            else
                throw new NullReferenceException("Database object is null!");
        }

        async Task<int> executeScript(string filename)
        {
            Microsoft.Data.SqlClient.SqlConnection sqlCon = new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
            ServerConnection svConn = new ServerConnection(sqlCon);
            Server sv = new Server(svConn);

            if (File.Exists(filename))
            {
                string script = new StreamReader(filename).ReadToEnd(); //TODO: Check that the _so actually exists?

                if (string.IsNullOrEmpty(script))
                    OnError(new DBError("Failed to load the schema (_so) script needed to restore the table!"));
                else
                {
                    if (sv != null)
                        try
                        {
                            return sv.ConnectionContext.ExecuteNonQuery(script);
                        }
                        catch (Exception ex)
                        {
                            OnError(new DBError($"An exception occured during script execution!\nMessage: {ex.Message}\n\nCall-Stack: {ex.StackTrace}"));
                        }
                    else
                        OnError(new DBError("Database object is null!"));
                }
            }

            return -1;
        }

        public override string ToString()
        {
            switch (connType)
            {
                case DbConType.MsSQL:
                    {
                        if (msSQL_cmd != null && !string.IsNullOrEmpty(msSQL_cmd.CommandText))
                        {

                        }
                    }
                    break;

                case DbConType.MySQL:
                    throw new NotImplementedException();
            }

            return null;
        }

        object getDefault(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    mySQL_cmd = null;
                    mySQL_conn = null;
                    msSQL_cmd = null;
                    msSQL_conn = null;
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
