using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Grimoire.DB.Enums;
using Grimoire.Configuration;
using Grimoire.Logs.Enums;
using Daedalus.Enums;
using Daedalus.Structures;
using System.Windows.Forms;

namespace Grimoire.DB
{
    /// <summary>
    /// Provides MariaDB interoperability via the MySQLConnector api
    /// </summary>
    public class DBHelper : IDisposable
    {
        Tabs.Manager TabMan = Tabs.Manager.Instance;
        ConfigMan configMan = GUI.Main.Instance.ConfigMan;

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
        public DBHelper(ConfigMan configManager, DbConType type)
        {
            configMan = configManager;

            string ip = configMan["IP"];
            string name = configMan["WorldName"];
            string user = configMan["WorldUser"];
            string pass = configMan["WorldPass"];
            bool trusted = configMan["Trusted", "DB"];

            switch (type)
            {
                default:
                    goto case DbConType.MsSQL;

                case DbConType.MySQL:
                    mySQL_conn = new MySqlConnection($"server={ip};database={name};user={user};password={pass}");
                    mySQL_cmd = new MySqlCommand() { Connection = mySQL_conn };
                    break;

                case DbConType.MsSQL:
                    string connStr = $"Server={ip};Database={name};";

                    if (trusted)
                        connStr += "Trusted_Connection=true;";
                    else
                        connStr += $"User ID={user};Password={pass}";

                    msSQL_conn = new SqlConnection(connStr);
                    msSQL_cmd = new SqlCommand() { Connection = msSQL_conn };
                    break;
            }
        }

        public async Task<dynamic> Execute(string text, DbParameter parameters, DbCmdType type = DbCmdType.NonQuery, bool keepAlive = false)
        {
            try
            {
                NewCommand(text);
                return await execute(type);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Exception has occured!\nMessage: {ex.Message}\nStack-Trace: {ex.StackTrace}", "Execute", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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
                MessageBox.Show($"An Exception has occured!\nMessage: {ex.Message}\nStack-Trace: {ex.StackTrace}", "Execute", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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
                MessageBox.Show($"An Exception has occured!\nMessage: {ex.Message}\nStack-Trace: {ex.StackTrace}", "execute", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }

            return null;
        }

        async Task<dynamic> execute(DbCmdType type = DbCmdType.NonQuery)
        {
            if (State == ConnectionState.Connected)
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
                    Tabs.Manager.Instance.RDBTab.ProgressMax = rowCnt;

                    int rowIdx = 0;

                    foreach (IDataRecord iRow in dbRdr)
                    {
                        Row row = new Row((Cell[])fieldList.Clone());

                        int sqlIdx = 0;

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
                                        row[i] = iRow[sqlIdx];
                                        break;

                                    case CellType.TYPE_USHORT:
                                        goto case CellType.TYPE_UINT_16;

                                    case CellType.TYPE_UINT_16:
                                        row[i] = (ushort)iRow[sqlIdx];
                                        break;

                                    case CellType.TYPE_INT:
                                        goto case CellType.TYPE_INT_32;

                                    case CellType.TYPE_INT_32:
                                        row[i] = Convert.ToInt32(iRow[sqlIdx]);
                                        break;

                                    case CellType.TYPE_UINT:
                                        goto case CellType.TYPE_UINT_32;

                                    case CellType.TYPE_UINT_32:
                                        row[i] = (uint)iRow[sqlIdx];
                                        break;

                                    case CellType.TYPE_LONG:
                                        row[i] = (long)iRow[sqlIdx];
                                        break;

                                    case CellType.TYPE_DATETIME:

                                        break;

                                    case CellType.TYPE_BYTE:
                                        {
                                            object fieldVal = Convert.ToByte(dbRdr[sqlIdx]);
                                            byte val = new byte();
                                            row[sqlIdx] = (Byte.TryParse(fieldVal.ToString(), out val)) ? val : 0;
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
                                            break;
                                        }

                                    case CellType.TYPE_DOUBLE:
                                        row[i] = Convert.ToDouble(iRow[sqlIdx]);
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
                            else
                            {
                                switch (field.Type)
                                {
                                    case CellType.TYPE_BIT_VECTOR:
                                        row[i] = new BitVector32(0);
                                        break;

                                    case CellType.TYPE_BYTE:
                                        row[i] = Convert.ToByte(field.Default);
                                        break;

                                    case CellType.TYPE_INT:
                                        row[i] = row.KeyIsDuplicate(field.Name) ? row.GetShownValue(field.Name) : field.Default;
                                        break;

                                    case CellType.TYPE_SHORT:
                                        row[i] = Convert.ToInt16(field.Default);
                                        break;

                                    case CellType.TYPE_STRING:
                                        row[i] = field.Default.ToString();
                                        break;
                                }
                            }
                        }

                        data[rowIdx++] = row;

                        if ((rowIdx * 100 / rowCnt) != ((rowIdx - 1) * 100 / rowCnt))
                            TabMan.RDBTab.ProgressVal = rowIdx;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An Exception has occured!\nMessage: {ex.Message}\nStack-Trace: {ex.StackTrace}", "Read Table", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                finally
                {
                    CloseConnection(true);
                    TabMan.RDBTab.ResetProgress();

                    actionSW.Stop();
                    Logs.Manager.Instance.Enter(Sender.DATABASE, Level.DEBUG, $"{table} successfully loaded in {actionSW.ElapsedMilliseconds}ms");
                }

                return data;
            }
            else
            { } //TODO: handle error bruv

            return null;
        }

        public async Task<dynamic> WriteTable(string table, Row[] data)
        {
            actionSW = new Stopwatch();
            actionSW.Start();

            Tabs.Styles.rdbTab rTab = TabMan.RDBTab;
            int rowCnt = data.Length;

            try
            {
                if (await OpenConnection())
                {
                    if (configMan["DropOnExport", "DB"])
                        await Execute($"drop table {table}");
                    else
                        await Execute($"truncate table {table}");

                    DataTable dataTbl = generateDataTable(table);

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
                    }

                    executeBulk(dataTbl);
                }
                else
                { } //Todo: handle exception bro                   
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured during the export!\nMessage: {ex.Message}\nStack-Trace: {ex.StackTrace}");
            }
            finally
            {
                CloseConnection();
                rTab.ResetProgress();

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

            Tabs.Manager.Instance.RDBTab.ProgressMax = rowCnt;

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
                                TabMan.RDBTab.ProgressVal = (int)processed;
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
                                TabMan.RDBTab.ProgressVal = (int)processed;
                        };
                    }
                    break;
            }

            Tabs.Manager.Instance.RDBTab.ResetProgress();
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

        void NewCommand(string text) => NewCommand(text, null);

        public void NewCommand(string text, params DbParameter[] parameters)
        {
            switch (connType)
            {
                case DbConType.MsSQL:
                    msSQL_cmd.Connection = msSQL_conn;
                    msSQL_cmd.CommandText = text;
                    if (parameters != null)
                        msSQL_cmd.Parameters.AddRange(parameters);
                    break;

                case DbConType.MySQL:
                    mySQL_cmd.Connection = mySQL_conn;
                    mySQL_cmd.CommandText = text;
                    if (parameters != null)
                        mySQL_cmd.Parameters.AddRange(parameters);
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DBHelper()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
