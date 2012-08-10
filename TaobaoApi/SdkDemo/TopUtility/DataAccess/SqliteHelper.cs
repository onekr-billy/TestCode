using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;
using System.Data;

namespace TopCore.DataAccess
{
    public enum ExecuteType
    {
        Table,
        NonQuery,
        Scalar
    }
    public class SqliteHelper : IDisposable
    {
        private static string _dbFilePath = string.Empty;

        public SqliteHelper()
        {
            if (string.IsNullOrWhiteSpace(_dbFilePath))
                _dbFilePath = MConfig.Get("SqliteDbPath");
        }

        /// <summary>
        /// 数据库文件路径
        /// </summary>
        public string DbFilePath
        {
            get
            {
                return _dbFilePath;
            }
        }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public SQLiteConnection DbConnection
        {
            get
            {
                return new SQLiteConnection();
            }
        }

        /// <summary>
        /// 数据库提供器工厂
        /// </summary>
        public DbProviderFactory FactoryObject
        {
            get
            {
                DbProviderFactory factory = SQLiteFactory.Instance;
                return factory;
            }
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="executeType"></param>
        /// <param name="sqlTxt"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        private dynamic Execute(ExecuteType executeType, string sqlTxt, SQLiteParameter[] sqlParameters)
        {
            dynamic result = null;
            using (var conn = DbConnection)
            {
                conn.ConnectionString = string.Format("Data Source={0}", DbFilePath);
                if (!string.IsNullOrWhiteSpace(sqlTxt))
                {
                    try
                    {
                        conn.Open();
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = sqlTxt;
                        if (sqlParameters.Length > 0)
                            cmd.Parameters.AddRange(sqlParameters);

                        switch (executeType)
                        {
                            case ExecuteType.NonQuery:
                                result = cmd.ExecuteNonQuery();
                                break;
                            case ExecuteType.Table:
                                result = ConvertDataReaderToDictList(cmd.ExecuteReader());
                                break;
                            case ExecuteType.Scalar:
                                result = cmd.ExecuteScalar();
                                break;
                        }
                    }
                    catch
                    { }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// DataReader 转 List
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public List<Dictionary<string, dynamic>> ConvertDataReaderToDictList(DbDataReader dataReader)
        {
            var result = new List<Dictionary<string, dynamic>>();

            if (dataReader != null && dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var dict = new Dictionary<string, dynamic>();

                    for (var i = 0; i < dataReader.FieldCount; i++)
                    {
                        try
                        {
                            var key = dataReader.GetName(i);
                            var val = Convert.ChangeType(dataReader[i], dataReader.GetFieldType(i));
                            if (!string.IsNullOrWhiteSpace(key))
                                dict.Add(key, val);
                        }
                        catch
                        {
                        }
                    }
                    result.Add(dict);
                }
            }

            return result;
        }

        public List<Dictionary<string, dynamic>> ExecuteList(string sqlTxt, SQLiteParameter[] sqlParameters)
        {
            return Execute(ExecuteType.Table, sqlTxt, sqlParameters) as List<Dictionary<string, dynamic>>;
        }

        public int ExecuteNonQuery(string sqlTxt, SQLiteParameter[] sqlParameters)
        {
            return (int)Execute(ExecuteType.NonQuery, sqlTxt, sqlParameters);
        }

        public object ExecuteScalar(string sqlTxt, SQLiteParameter[] sqlParameters)
        {
            return Execute(ExecuteType.Scalar, sqlTxt, sqlParameters) as object;
        }

        public void Dispose()
        {

        }
    }
}
