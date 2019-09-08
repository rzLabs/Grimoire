using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Daedalus.Enums;
using Daedalus.Structures;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Grimoire.Utilities
{
    public class Database
    {
        #region Properties

        readonly Tabs.Manager tManager;
        readonly Logs.Manager lManager;
        readonly Daedalus.Core rCore;

        static Database instance;
        public static Database Instance
        {
            get
            {
                if (instance == null)
                    instance = new Database();

                return instance;
            }
        }

        static string scriptDir
        {
            get
            {
                string dir = string.Format(@"{0}\Scripts\", Directory.GetCurrentDirectory());
                if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
                return dir;
            }
        }

        private static string connectionString
        {
            get
            {
                string conString = string.Format("Server={0};Database={1};", 
                                                 OPT.GetString("db.ip"),
                                                 OPT.GetString("db.world.name"));

                if (OPT.GetBool("db.trusted.connection"))
                    conString += "Trusted_Connection=True;"; 
                else
                    conString += string.Format("uid={0};pwd={1};", 
                                                OPT.GetString("db.world.user"),
                                                OPT.GetString("db.world.password"));

                return conString;
            }
        }

        static SqlConnection sqlCon { get { return new SqlConnection(connectionString); } }
        public SqlConnection SqlConnection { get { return sqlCon; } }
        static ServerConnection con = new ServerConnection(sqlCon);
        static Server sv = new Server(con);
        static Microsoft.SqlServer.Management.Smo.Database db = sv.Databases[OPT.GetString("db.world.name")];

        #endregion

        #region Constructors

        public Database()
        {
            tManager = Tabs.Manager.Instance;
            lManager = Logs.Manager.Instance;
            rCore = tManager.RDBCore;
        }

        #endregion

        #region Public Methods

        public int FetchRowCount(string tableName)
        {
            try
            {
                DataSet ds = db.ExecuteWithResults(string.Format("SELECT COUNT(*) FROM dbo.{0}", tableName));
                return (int)ds.Tables[0].Rows[0][0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQL Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lManager.Enter(Logs.Sender.DATA, Logs.Level.SQL_ERROR, ex);
            }


            return 0;
        }

        public Row[] FetchTable(int rowCount, string tableName)
        {
            Tabs.Styles.rdbTab rTab = tManager.RDBTab;

            rTab.ProgressMax = rowCount;

            Row[] data = new Row[rowCount];

            Cell[] fieldList = rCore.CellTemplate;

            string selectStatement = generateSelect(tableName);

            using (SqlCommand sqlCmd = new SqlCommand(selectStatement, sqlCon))
            {
                sqlCmd.CommandTimeout = OPT.GetInt("db.connection.timeout");
                sqlCmd.Connection.Open();

                using (SqlDataReader sqlRdr = sqlCmd.ExecuteReader())
                {
                    int curRow = 0;

                    while (sqlRdr.Read())
                    {
                        Row newRow = new Row((Cell[])fieldList.Clone());

                        for (int i = 0; i < fieldList.Length; i++)
                        {
                            Cell field = fieldList[i];                            

                            if (field.Visible)
                            {
                                int fieldOrdinal = sqlRdr.GetOrdinal(field.Name);

                                switch (field.Type)
                                {
                                    case CellType.TYPE_SHORT:
                                        goto case CellType.TYPE_INT_16;

                                    case CellType.TYPE_INT_16:
                                        newRow[i] = sqlRdr[fieldOrdinal] as short? ?? default(short);
                                        break;

                                    case CellType.TYPE_USHORT:
                                        goto case CellType.TYPE_UINT_16;

                                    case CellType.TYPE_UINT_16:
                                        newRow[i] = sqlRdr[fieldOrdinal] as ushort? ?? default(ushort);
                                        break;

                                    case CellType.TYPE_INT:
                                        goto case CellType.TYPE_INT_32;

                                    case CellType.TYPE_INT_32:
                                        newRow[i] = sqlRdr[fieldOrdinal] as int? ?? default(int);
                                        break;

                                    case CellType.TYPE_UINT:
                                        goto case CellType.TYPE_UINT_32;

                                    case CellType.TYPE_UINT_32:
                                        newRow[i] = sqlRdr[fieldOrdinal] as uint? ?? default(uint);
                                        break;

                                    case CellType.TYPE_LONG:
                                        newRow[i] = sqlRdr[fieldOrdinal] as long? ?? default(long);
                                        break;

                                    case CellType.TYPE_BYTE:
                                        {
                                            object fieldVal = sqlRdr[fieldOrdinal];
                                            byte val = new byte();
                                            newRow[i] = (Byte.TryParse(fieldVal.ToString(), out val)) ? val : 0;
                                        }
                                        break;

                                    case CellType.TYPE_BIT_FROM_VECTOR:
                                        newRow[i] = Convert.ToInt32(sqlRdr[fieldOrdinal]);
                                        break;

                                    case CellType.TYPE_DECIMAL:
                                        newRow[i] = Convert.ToDecimal(sqlRdr[fieldOrdinal]);
                                        break;

                                    case CellType.TYPE_FLOAT: case CellType.TYPE_FLOAT_32:
                                        goto case CellType.TYPE_SINGLE;

                                    case CellType.TYPE_SINGLE:
                                        {
                                            decimal v1 = sqlRdr[fieldOrdinal] as decimal? ?? default(decimal);
                                            newRow[i] = decimal.ToSingle(v1);
                                            break;
                                        }

                                    case CellType.TYPE_DOUBLE:
                                        newRow[i] = sqlRdr[fieldOrdinal] as double? ?? default(double);
                                        break;

                                    case CellType.TYPE_STRING:
                                        newRow[i] = Convert.ToString(sqlRdr[fieldOrdinal]);
                                        break;

                                    case CellType.TYPE_STRING_BY_LEN:
                                        {
                                            string szVal = Convert.ToString(sqlRdr[fieldOrdinal]);
                                            newRow[field.Dependency] = szVal.Length + 1;
                                            newRow[i] = szVal;
                                        }
                                        break;

                                    case CellType.TYPE_STRING_BY_REF:
                                        newRow[i] = Convert.ToString(sqlRdr[fieldOrdinal]);
                                        break;
                                }
                            }
                            else
                            { // TODO: Look me over closer
                                switch (field.Type)
                                {
                                    case CellType.TYPE_BIT_VECTOR:
                                        newRow[i] = new BitVector32(0);
                                        break;

                                    case CellType.TYPE_BYTE:
                                        newRow[i] = Convert.ToByte(field.Default);
                                        break;

                                    case CellType.TYPE_INT:
                                        newRow[i] = newRow.KeyIsDuplicate(field.Name) ? newRow.GetShownValue(field.Name) : field.Default;
                                        break;

                                    case CellType.TYPE_SHORT:
                                        newRow[i] = Convert.ToInt16(field.Default);
                                        break;

                                    case CellType.TYPE_STRING:
                                        newRow[i] = field.Default.ToString();
                                        break;
                                }
                            }
                        }

                        data[curRow] = newRow;
                        curRow++;

                        if ((curRow * 100 / rowCount) != ((curRow - 1) * 100 / rowCount))
                            rTab.ProgressVal = curRow; 
                    }
                }
            }

            rTab.ProgressVal = 0;
            rTab.ProgressMax = 100;
            return data;
        }

        public void ExportToTable(string tableName, Row[] data)
        {
            if (OPT.GetBool("db.save.backup"))
                scriptTable(tableName, true);

            using (SqlCommand sqlCmd = new SqlCommand("", sqlCon))
            {
                sqlCmd.Connection.Open();

                if (OPT.GetBool("db.save.drop"))
                {
                    scriptTable(tableName, false);
                    sqlCmd.CommandText = string.Format("DROP TABLE {0}", tableName);
                    sqlCmd.ExecuteNonQuery();
                    string script = new StreamReader(string.Format(@"{0}\{1}_{2}_so.sql",
                                                                               scriptDir,
                                                                               tableName,
                                        DateTime.Now.ToString("hhMMddyyy"))).ReadToEnd();
                    db.ExecuteNonQuery(script);
                }
                else
                {
                    sqlCmd.CommandText = string.Format("TRUNCATE TABLE {0}", tableName);
                    sqlCmd.ExecuteNonQuery();
                }

                sqlCmd.Connection.Close();
            }

            SqlCommand insertCmd = tManager.RDBCore.InsertStatement;
            insertCmd.Connection = sqlCon;
            insertCmd.CommandText = insertCmd.CommandText.Replace("<tableName>", tableName);

            int rows = data.Length;
            tManager.RDBTab.ProgressMax = rows;

            for (int r = 0; r < rows; r++)
            {
                Row row = data[r];
                using (SqlCommand sqlCmd = insertCmd)
                {
                    foreach (SqlParameter sqlParam in sqlCmd.Parameters)
                        sqlParam.Value = row[sqlParam.ParameterName];

                    sqlCmd.Connection.Open();
                    sqlCmd.ExecuteNonQuery();
                    sqlCmd.Connection.Close();
                }

                if ((r * 100 / rows) != ((r - 1) * 100 / rows))
                    tManager.RDBTab.ProgressVal = r;
            }

            tManager.RDBTab.ProgressVal = 0;
            tManager.RDBTab.ProgressMax = 100;
        }

        public string ExecuteScalar(string commandText)
        {
            return execute(new SqlCommand() { Connection = sqlCon, CommandText = commandText }, QueryType.Execute_Scalar).ToString();
        }

        public string ExecuteScalar(string commandText, params SqlParameter[] sqlParams)
        {
            SqlCommand newCmd = new SqlCommand() { Connection = sqlCon, CommandText = commandText };
            newCmd.Parameters.AddRange(sqlParams);
            return execute(newCmd, QueryType.Execute_Scalar).ToString();
        }

        public SqlDataReader ExecuteReader(string commandText)
        {
            using (SqlCommand sqlCmd = new SqlCommand(commandText, sqlCon))
            {
                sqlCmd.Connection.Open();
                return sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        public SqlDataReader ExecuteReader(string commandText, params SqlParameter[] sqlParameters)
        {
            using (SqlCommand sqlCmd = new SqlCommand(commandText, sqlCon))
            {
                sqlCmd.Parameters.AddRange(sqlParameters);
                sqlCmd.Connection.Open();
                return sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        #endregion

        #region Private Methods

        private object execute(SqlCommand sqlCmd, QueryType type)
        {
            using (sqlCmd)
            {
                sqlCmd.Connection.Open();

                switch (type)
                {
                    case QueryType.Execute_Scalar:
                        return sqlCmd.ExecuteScalar();

                    case QueryType.Execute_Silent:
                        return sqlCmd.ExecuteNonQuery();
                }

                sqlCmd.Connection.Close();
            }

            return null;
        }

        string generateSelect(string tableName)
        {
            string statement = string.Empty;

            if (rCore.UseSelectStatement)
                statement = rCore.SelectStatement;
            else
            {
                statement = "SELECT ";

                Cell[] fieldList = rCore.CellTemplate;

                foreach (Cell field in fieldList)
                    if (field.Visible)
                        statement += string.Format("[{0}],", field.Name);

                statement = string.Format("{0} FROM dbo.{1} with (NOLOCK)", statement.Remove(statement.Length - 1, 1), tableName);
            }

            return statement;
        }

        public static void scriptTable(string tableName, bool scriptData)
        {
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

            if (db != null)
            {
                if (db.Tables.Contains(tableName))
                    db.Tables[tableName].EnumScript(opts);
                else
                    throw new KeyNotFoundException(string.Format("Database object does not contain table with key: {0}", tableName));
            }
                
            else
                throw new NullReferenceException("Database object is null!") { Source = "Grimoire.Utilities.Database.db" };           
        }

        #endregion

        public const string SelectItemInfo = "select i.id, s.[value], i.icon_file_name from dbo.ItemResource i left join dbo.StringResource s on s.code = i.name_id order by i.id asc";
        public const string Select_Item_Tooltip = "select top(1) s.[value] from dbo.ItemResource i left join dbo.StringResource s on s.code = i.tooltip_id where i.id = @id";
        public const string Select_Item = "select top(1) t.[value],i.rank,i.[level],i.enhance,i.socket,i.wear_type,i.[class],i.[group] from dbo.ItemResource i left join dbo.StringResource t on t.code = i.tooltip_id where i.id = @id";
    }  
}
