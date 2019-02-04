using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using rdbCore.Structures;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Grimoire.Utilities
{
    public class Database
    {
        #region Properties

        readonly Tabs.Manager tManager;
        readonly Logs.Manager lManager;
        readonly rdbCore.Core rCore;

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
                string conString = string.Format("Server={0};Database={1};", OPT.GetString("db.ip"), OPT.GetString("db.world.name"));
                if (OPT.GetString("db.trusted.connection") == "1") { conString += "Trusted_Connection=True;"; }
                else { conString += string.Format("uid={0};pwd={1};", OPT.GetString("db.world.user"), OPT.GetString("db.world.password")); }

                return conString;
            }
        }

        static SqlConnection sqlCon { get { return new SqlConnection(connectionString); } }
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

        public List<Row> FetchTable(int rowCount, string tableName)
        {
            Tabs.Styles.rdbTab rTab = tManager.RDBTab;

            rTab.ProgressMax = rowCount;

            List<Row> data = new List<Row>(rowCount);

            List<LuaField> fieldList = rCore.FieldList;

            string selectStatement = generateSelect(tableName);

            using (SqlCommand sqlCmd = new SqlCommand(selectStatement, sqlCon))
            {
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Connection.Open();

                using (SqlDataReader sqlRdr = sqlCmd.ExecuteReader())
                {
                    int curRow = 0;

                    while (sqlRdr.Read())
                    {
                        Row newRow = new Row(fieldList);

                        for (int i = 0; i < fieldList.Count; i++)
                        {
                            LuaField field = fieldList[i];

                            if (field.Show)
                            {
                                object fieldVal = sqlRdr[field.Name];

                                switch (field.Type)
                                {
                                    case "short":
                                        newRow[i] = Convert.ToInt16(sqlRdr[field.Name]);
                                        break;

                                    case "ushort":
                                        newRow[i] = Convert.ToUInt16(sqlRdr[field.Name]);
                                        break;

                                    case "int":
                                        newRow[i] = (fieldVal.GetType() == typeof(DBNull)) ? 0 : Convert.ToInt32(fieldVal);
                                        break;

                                    case "uint":
                                        newRow[i] = Convert.ToInt32(sqlRdr[field.Name]);
                                        break;

                                    case "long":
                                        newRow[i] = Convert.ToInt64(sqlRdr[field.Name]);
                                        break;

                                    case "byte":
                                        byte val = new byte();
                                        newRow[i] = (Byte.TryParse(fieldVal.ToString(), out val)) ? val : 0;
                                        break;

                                    case "bitfromvector":
                                        newRow[i] = Convert.ToInt32(sqlRdr[field.Name]);
                                        break;

                                    case "datetime":
                                        newRow[i] = Convert.ToDateTime(sqlRdr[field.Name]);
                                        break;

                                    case "decimal":
                                        newRow[i] = Convert.ToDecimal(sqlRdr[field.Name]);
                                        break;

                                    case "single":
                                        newRow[i] = Convert.ToSingle(sqlRdr[field.Name]);
                                        break;

                                    case "double":
                                        newRow[i] = Convert.ToDouble(sqlRdr[field.Name]);
                                        break;

                                    case "sid":
                                        newRow[i] = Convert.ToInt32(sqlRdr[field.Name]);
                                        break;

                                    case "string":
                                        newRow[i] = Convert.ToString(sqlRdr[field.Name]);
                                        break;

                                    case "stringbylen":
                                        {
                                            string szVal = Convert.ToString(sqlRdr[field.Name]);
                                            newRow[field.Dependency] = szVal.Length + 1;
                                            newRow[i] = szVal;
                                        }
                                        break;

                                    case "stringbyref":
                                        newRow[i] = Convert.ToString(sqlRdr[field.Name]);
                                        break;
                                }
                            }
                            else
                            {
                                switch (field.Type)
                                {
                                    case "bitvector":
                                        newRow[i] = new BitVector32(0);
                                        break;

                                    case "byte":
                                        newRow[i] = Convert.ToByte(field.Default);
                                        break;

                                    case "int":
                                        newRow[i] = (newRow.KeyIsDuplicate(field.Name)) ? newRow.GetShownValue(field.Name) : field.Default;
                                        break;

                                    case "short":
                                        newRow[i] = Convert.ToInt16(field.Default);
                                        break;

                                    case "string":
                                        newRow[i] = field.Default.ToString();
                                        break;
                                }
                            }
                        }

                        data.Add(newRow);
                        curRow++;
                        if (((curRow * 100) / rowCount) != ((curRow - 1) * 100 / rowCount)) { rTab.ProgressVal = curRow; }
                    }
                }
            }

            rTab.ProgressVal = 0;
            rTab.ProgressMax = 100;
            return data;
        }

        public void ExportToTable(string tableName, List<Row> data)
        {
            if (OPT.GetBool("db.save.backup")) { scriptTable(tableName, true); }

            using (SqlCommand sqlCmd = new SqlCommand("", sqlCon))
            {
                sqlCmd.Connection.Open();
                if (OPT.GetBool("db.save.drop"))
                {
                    scriptTable(tableName, false);
                    sqlCmd.CommandText = string.Format("DROP TABLE {0}", tableName);
                    sqlCmd.ExecuteNonQuery();
                    string script = new StreamReader(string.Format(@"{0}\{1}_{2}_so.sql", scriptDir, tableName, DateTime.Now.ToString("hhMMddyyy"))).ReadToEnd();
                    db.ExecuteNonQuery(script);
                }
                else { sqlCmd.CommandText = string.Format("TRUNCATE TABLE {0}", tableName); sqlCmd.ExecuteNonQuery(); }

                sqlCmd.Connection.Close();
            }

            SqlCommand insertCmd = tManager.RDBCore.InsertStatement;
            insertCmd.Connection = sqlCon;
            insertCmd.CommandText = insertCmd.CommandText.Replace("<tableName>", tableName);

            int rows = data.Count;
            tManager.RDBTab.ProgressMax = rows;
            for (int rowIdx = 0; rowIdx < rows; rowIdx++)
            {
                Row row = data[rowIdx];
                using (SqlCommand sqlCmd = insertCmd)
                {
                    foreach (SqlParameter sqlParam in sqlCmd.Parameters) { sqlParam.Value = row[sqlParam.ParameterName]; }
                    sqlCmd.Connection.Open();
                    sqlCmd.ExecuteNonQuery();
                    sqlCmd.Connection.Close();
                }

                if (((rowIdx * 100) / rows) != ((rowIdx - 1) * 100 / rows)) { tManager.RDBTab.ProgressVal = rowIdx; }
            }

            tManager.RDBTab.ProgressVal = 0;
            tManager.RDBTab.ProgressMax = 100;
        }

        public void Execute(string commandText, QueryType type)
        {
            execute(new SqlCommand() { Connection = sqlCon, CommandText = commandText }, type);
        }

        public void Execute(string commandText, QueryType type, params SqlParameter[] sqlParams)
        {
            SqlCommand newCmd = new SqlCommand() { Connection = sqlCon, CommandText = commandText };
            newCmd.Parameters.AddRange(sqlParams);
            execute(newCmd, type);
        }

        #endregion

        #region Private Methods

        private object execute(SqlCommand sqlCmd, QueryType type)
        {
            using (sqlCmd)
            {
                if (sqlCmd.Connection.State != ConnectionState.Open)
                {
                    lManager.Enter(Logs.Sender.DATA, Logs.Level.SQL_ERROR, "Failed to execute SQL Command because the connection is: {0}\n\nSQL Command: {1}", sqlCmd.Connection.State.ToString(), sqlCmd.CommandText);
                }
                else
                {
                    switch (type)
                    {
                        case QueryType.Execute:
                            return sqlCmd.ExecuteScalar();

                        case QueryType.Execute_Silent:
                            return sqlCmd.ExecuteNonQuery();
                    }
                }
            }

            return null;
        }

        string generateSelect(string tableName)
        {
            string statement = string.Empty;

            if (rCore.UseSelectStatement) { statement = rCore.SelectStatement; }
            else
            {
                statement = "SELECT ";

                List<LuaField> fieldList = rCore.FieldList;

                foreach (LuaField field in fieldList)
                {
                    if (field.Show)
                        statement += string.Format("[{0}],", field.Name);
                }

                statement = string.Format("{0} FROM dbo.{1}", statement.Remove(statement.Length - 1, 1), tableName);
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
                FileName = string.Format(@"{0}\{1}_{2}{3}.sql", scriptDir, tableName, DateTime.Now.ToString("hhMMddyyyy"), (!scriptData) ? "_so" : string.Empty)
            };
            db.Tables[tableName].EnumScript(opts);
        }

        #endregion
    }
}
