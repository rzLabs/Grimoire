using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

using Grimoire.Configuration;
using Grimoire.Structures;

using Serilog;
using Serilog.Events;

namespace Grimoire.Utilities
{
    /// <summary>
    /// Object used to communicate with an SQL Server instance in a generic and asynchronous manner.
    /// </summary>
    public class DatabaseObject : IDisposable
    {    
        /// <summary>
        /// Connection being used by this DatabaseObject
        /// </summary>
        public SqlConnection Connection;

        /// <summary>
        /// Command text to be proceeded upon execution
        /// </summary>
        public string CommandText
        {
            get => command?.CommandText;
            set => command.CommandText = value;
        }

        /// <summary>
        /// Parameters to be processed through CommandText
        /// </summary>
        public SqlParameterCollection Parameters => command?.Parameters;

        /// <summary>
        /// Determine if this DatabaseObject has an open connection
        /// </summary>
        public bool Connected => command != null && command.Connection.State == ConnectionState.Open;

        /// <summary>
        /// Basis for all SQL driven interactions within this DatabaseObject
        /// </summary>
        SqlCommand command;

        /// <summary>
        /// Contruct a DatabaseObject by providing atleast a properly formed connection string. 
        /// Providing a CommandText at this time is optional
        /// </summary>
        /// <param name="connection_string">Properly formed MSSQL connection string</param>
        /// <param name="command_text">Text to be parsed during the SQL transaction of this DatabaseObject</param>
        public DatabaseObject(string connection_string, string command_text = null) 
        {
            Connection = new SqlConnection(connection_string);

            command = new SqlCommand() { Connection = Connection };

            if (command_text != null)
                command.CommandText = command_text;
        }

        /// <summary>
        /// Add a collection of SqlParameter to the SqlCommand
        /// </summary>
        /// <param name="parameters">List of parameters to be added</param>
        public void AddParameters(List<SqlParameter> parameters)
        {
            if (command == null)
            {
                LogUtility.MessageBoxAndLog("Cannot add paramaters to unitialized DatabaseObject!", "Parameter Exception", LogEventLevel.Error);

                return;
            }

            command.Parameters.AddRange(parameters.ToArray());
        }

        /// <summary>
        /// Connect to the target database
        /// </summary>
        /// <returns>True or false based on connection state after connection attempt</returns>
        public async Task<bool> Connect()
        {
            if (Connection == null)
            {
                LogUtility.MessageBoxAndLog("Connection invalid!", "Database Connection Exception", LogEventLevel.Error);

                return false;
            }

            if (command == null || command.Connection == null)
            {
                LogUtility.MessageBoxAndLog("Command invalid!", "Database Connection Exception", LogEventLevel.Error);

                return false;
            }

            if (command.Connection.State == ConnectionState.Open)
                return true;

            await command.Connection.OpenAsync();

            return command.Connection.State == ConnectionState.Open;
        }

        /// <summary>
        /// Close the connection to the database
        /// </summary>
        /// <returns>True or false based on connection state after close attempt</returns>
        public bool Disconnect()
        {
            command?.Connection?.Close();

            return command.Connection.State == ConnectionState.Closed;
        }

        /// <summary>
        /// Reset the sql connection to a completely fresh state
        /// </summary>
        /// <param name="reconnect">If true, automatically reconnect to the database</param>
        /// <returns>True or false depending if the connection was properly reset.</returns>
        public async Task<bool> Reset(bool reconnect)
        {
            command.Connection.Close();
            command.Dispose();

            Connection.Dispose();

            Connection = new SqlConnection(DatabaseUtility.ConnectionString);

            command = new SqlCommand() { Connection = Connection };

            if (reconnect)
                await Connect();

            return command != null && command.Connection != null && (reconnect) ? (command.Connection.State == ConnectionState.Open) ? true : false : false;
        }

        /// <summary>
        /// Execute an asynchronous request to the database for to select a value
        /// </summary>
        /// <typeparam name="T">Type to return selected value as</typeparam>
        /// <returns>Value (if exists) as provided T or default(T)</returns>
        public async Task<T> ExecuteScalar<T>()
        {
            if (!Connected)
            {
                LogUtility.MessageBoxAndLog($"Cannot execute against a closed connection!", "Execute Scalar Exception", LogEventLevel.Error);

                return (T)Convert.ChangeType(-99, typeof(T));
            }

            var result = await command.ExecuteScalarAsync();

            return (result != null) ? (T)Convert.ChangeType(result, typeof(T)) : default(T);
        }

        /// <summary>
        /// Execute an asynchronous query against the database
        /// </summary>
        /// <returns>Rows affected by CommandText</returns>
        public async Task<int> ExecuteNonQuery()
        {
            if (!Connected)
            {
                LogUtility.MessageBoxAndLog($"Cannot execute against a closed connection!", "Execute Scalar Exception", LogEventLevel.Error);

                return -99;
            }

            return await command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Execute an asynchronous database reader against the databast to collect table contents.
        /// </summary>
        /// <returns></returns>
        public async Task<SqlDataReader> ExecuteReader()
        {
            if (!Connected)
            {
                LogUtility.MessageBoxAndLog($"Cannot execute against a closed connection!", "Execute Scalar Exception", LogEventLevel.Error);

                return null;
            }

            return await command.ExecuteReaderAsync();
        }

        /// <summary>
        /// Properly dispose of this DatabaseObject
        /// </summary>
        public void Dispose()
        {
            if (command.Connection.State == ConnectionState.Open)
                command.Connection.Dispose();

            command.Dispose();
        }
    }

    /// <summary>
    /// Provided more expanded interactibility targetted to the Rappelz database and rdb systems.
    /// </summary>
    public static class DatabaseUtility
    {
        /// <summary>
        /// Automatically generated connection string based on settings provided in the Config.json
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                ConfigManager configMgr = GUI.Main.Instance.ConfigMgr;


                string ip = configMgr["IP"];
                string name = configMgr["WorldName"];
                string user = configMgr["WorldUser"];
                string pass = configMgr["WorldPass"];
                bool trusted = configMgr["Trusted", "DB"];

                string connStr = $"Server={ip};Database={name};";

                if (trusted)
                    connStr += "Trusted_Connection=true;";
                else
                    connStr += $"User ID={user};Password={pass}";

                return connStr;

            }
        }

        static DatabaseObject dbObj = null;

        /// <summary>
        /// Get the count of rows in the provided table
        /// </summary>
        /// <param name="tableName">Table to be counted</param>
        /// <returns>Amount of rows existing in the provided table</returns>
        public static async Task<int> GetRowCount(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                return -99;

            dbObj = new DatabaseObject(ConnectionString);

            if (!await dbObj.Connect())
            {
                LogUtility.MessageBoxAndLog($"Failed to open a database connection!", "GetRowCount Exception", LogEventLevel.Error);

                return -99;
            }

            dbObj.CommandText = $"select count(*) from dbo.{tableName}";

            int value = await dbObj.ExecuteScalar<int>();

            dbObj.Disconnect();

            return value;
        }

        /// <summary>
        /// Fetch copy of a database table by the given selectStatement
        /// </summary>
        /// <param name="selectStatement">Statement used to select content from the database</param>
        /// <returns>Populated DataTable object</returns>
        public static DataTable GetDataTable(string selectStatement)
        {
            DataTable data = new DataTable();

            try
            {
                using (DbDataAdapter adapter = new SqlDataAdapter(selectStatement, ConnectionString))
                    adapter.Fill(data);
            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "generating DataTable object!", "GetDataTable Exception", LogEventLevel.Error);

                return null;
            }

            return data;
        }

        /// <summary>
        /// Read table data from an SQL Server based on the provided structure object and report thread safe progress to the caller
        /// </summary>
        /// <param name="structure">StructureObject to be processed</param>
        /// <param name="progressObj">Progress object used to report progress</param>
        /// <returns>Populated collection of rows</returns>
        public static async Task<RowObject[]> ReadTableData(StructureObject structure, IProgress<int> progressObj)
        {
            dbObj = new DatabaseObject(ConnectionString);

            if (!await dbObj.Connect())
            {
                LogUtility.MessageBoxAndLog($"Failed to open a connection to the database!", "ReadTableRows Exception", LogEventLevel.Error);

                return null;
            }

            dbObj.CommandText = $"SELECT count(*) FROM dbo.{structure.TableName}";

            int rowCount = await dbObj.ExecuteScalar<int>();
            int rowIdx = 0;

            dbObj.CommandText = structure.SelectStatement;

            RowObject[] rows = new RowObject[rowCount];

            using (SqlDataReader sqlRdr = await dbObj.ExecuteReader())
            {
                foreach (IDataRecord iRow in sqlRdr)
                {
                    RowObject row = new RowObject(structure.DataCells);

                    for (int i = 0; i < row.Length; i++)
                    {
                        CellBase cell = row.GetCell(i);

                        // we ofc do not want to process ignored cells
                        if (cell.Flags.HasFlag(CellFlags.SqlIgnore))
                            continue;

                        // strings need to be modified before shipping
                        if (cell.PrimaryType == typeof(string))
                        {
                            // Set our value
                            row[cell.Index] = ByteConverterExt.ToBytes(iRow[cell.Name] as string);

                            // We need to set the lengths of string values
                            if (cell.SecondaryType == ArcType.TYPE_STRING_BY_LEN)
                            {
                                // Get dependent cell
                                CellBase depCell = row.GetCell(cell.Dependency);

                                // Set this values length as the dependent cells value 
                                row[depCell.Index] = ((byte[])row[cell.Index]).Length + 1;
                            }

                            continue; // Do not let the method proceed or this altered value will get overwritten
                        }

                        // Using the cells index avoids alignment issues
                        row[cell.Index] = iRow[cell.Name];
                    }

                    rows[rowIdx++] = row;
                }
            }

            dbObj.Disconnect();
            dbObj.Dispose();

            return rows;
        }

        /// <summary>
        /// Write table data to an SQL Server based on the provided structure and the data it contains
        /// </summary>
        /// <param name="structure">Loaded StructureObject</param>
        /// <param name="progressObj">Thread safe object to report progress over</param>
        /// <returns>True if the operation is successful, otherwise false</returns>
        public static async Task<bool> WriteTableData(StructureObject structure, IProgress<int> progressObj)
        {
            Stopwatch actionSW = new Stopwatch();
            actionSW.Start();

            try
            {
                dbObj = new DatabaseObject(ConnectionString);

                DataTable dataTbl = await GenerateDataTable(structure.TableName);

                for (int i = 0; i < structure.Rows.Count; i++) //loop rows
                {
                    DataRow newRow = dataTbl.NewRow();
                    RowObject refRow = structure.Rows[i];

                    foreach (KeyValuePair<CellBase, object> pair in refRow)
                    {
                        if (pair.Key.Flags.HasFlag(CellFlags.SqlIgnore))
                            continue;

                        if (pair.Key.PrimaryType == typeof(string))
                        {
                            newRow[pair.Key.Name] = ByteConverterExt.ToString((byte[])pair.Value);
                            continue;
                        }

                        newRow[pair.Key.Name] = pair.Value;
                    }

                    dataTbl.Rows.Add(newRow);
                }

                if (!await dbObj.Connect())
                    return false;

                dbObj.CommandText = $"TRUNCATE TABLE {structure.TableName}";
                await dbObj.ExecuteNonQuery();

                await Task.Run(() => ExecuteBulk(dataTbl, progressObj) );

                dbObj.Disconnect();

                actionSW.Stop();

                Log.Information($"{structure.TableName} exported in {StringExt.MilisecondsToString(actionSW.ElapsedMilliseconds)}");

                return true;

            }
            catch (Exception ex)
            {
                LogUtility.MessageBoxAndLog(ex, "writing sql table", "SQL Export Exception", LogEventLevel.Error);

                return false;
            }
        }

        /// <summary>
        /// Generate an empty datatable to be populated during RDB->SQL exports
        /// </summary>
        /// <param name="tableName">Name of the table to be generated</param>
        /// <returns>Datatable prepared to accept data</returns>
        public async static Task<DataTable> GenerateDataTable(string tableName)
        {
            if (dbObj == null)
            {
                LogUtility.MessageBoxAndLog("dbObj is null!", "ExecuteBulk Exception", LogEventLevel.Error);

                return null;
            }

            if (!await dbObj.Connect())
            {
                LogUtility.MessageBoxAndLog($"Failed to open a database connection!", "ExecuteBulk Exception", LogEventLevel.Error);

                return null;
            }

            DataTable tbl = new DataTable() { TableName = tableName };

            using (SqlDataAdapter sqlAdapter = new SqlDataAdapter($"select top 0 * from {tableName}", dbObj.Connection))
                sqlAdapter.Fill(tbl);

            dbObj.Disconnect();

            return tbl;
        }

        /// <summary>
        /// Execute bulk SQL writes while reporting progress
        /// </summary>
        /// <param name="table">Prepared datatable containing data</param>
        /// <param name="progress">Progress event</param>
        public static async void ExecuteBulk(DataTable table, IProgress<int> progress)
        {
            if (dbObj == null)
            {
                LogUtility.MessageBoxAndLog("dbObj is null!", "ExecuteBulk Exception", LogEventLevel.Error);

                return;
            }

            int rowCnt = table.Rows.Count;

            using (SqlBulkCopy bulkCpy = new SqlBulkCopy(dbObj.Connection))
            {
                if (!await dbObj.Connect())
                {
                    LogUtility.MessageBoxAndLog($"Failed to open a database connection!", "ExecuteBulk Exception", LogEventLevel.Error);

                    return;
                }

                bulkCpy.DestinationTableName = table.TableName;
                bulkCpy.WriteToServer(table);
            }

            dbObj.Disconnect();
        }

        /// <summary>
        /// Properly dispose of this utility
        /// </summary>
        public static void Dispose()
        {
            dbObj.Disconnect();

            dbObj.Dispose();
        }

        /// <summary>
        /// Get the default value for the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static object getDefault(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            return null;
        }
    }
}
