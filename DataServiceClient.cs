using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using DBO.Data.Managers;
using DBO.Data.Objects;

namespace DBO.Data
{
    public class DataServiceClient
    {
        private ConnectionStringSettings _connectionStringSettings;
        public DataServiceClient(ConnectionStringSettings connectionStringSettings)
        {
            _connectionStringSettings = connectionStringSettings;
        }

        private int? _commandTimeout;
        public int? CommandTimeout
        {
            get
            {
                if (_commandTimeout.HasValue)
                    return _commandTimeout;
                else if (ConfigurationManager.AppSettings["CommandTimeOut"] != null)
                    return ConfigurationManager.AppSettings["CommandTimeOut"].ConvertTo<int>();
                else
                    return null;
            }
            set
            {
                _commandTimeout = value;
            }
        }

        protected IDbConnection GetConnection()
        {
            //var connString = ConfigurationManager.ConnectionStrings["DBOConnectionString"];
            var connString = _connectionStringSettings;
            var connectionString = connString.ConnectionString.ToUpper().Contains("DATA SOURCE") || connString.ConnectionString.ToUpper().Contains("DATABASE")
                ? connString.ConnectionString
                : connString.ConnectionString.Decrypt();
            IDbConnection conn = (IDbConnection)Activator.CreateInstance(Type.GetType(connString.ProviderName), connectionString);
            conn.Open();
            return conn;
        }

        public DataSource Execute(string sql)
        {
            return Execute(sql, true);
        }
        public DataSource ExecuteWithoutException(string sql)
        {
            return Execute(sql, false);
        }
        public DataSource Execute(string sql, bool throwExceptionIfError)
        {
            var result = new DataSource("", sql);

            IDbConnection conn = null;
            IDbCommand cmd = null;
            IDataReader rdr = null;

            try
            {
                conn = GetConnection();
                cmd = conn.CreateCommand();

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                if (this.CommandTimeout.HasValue)
                    cmd.CommandTimeout = this.CommandTimeout.Value;

                rdr = cmd.ExecuteReader();

                var schemaTable = rdr.GetSchemaTable();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    var columnName = rdr.GetName(i);
                    var columnInfo = new ColumnInfo() { Name = columnName };

                    var columnDataType = rdr.GetDataTypeName(i);
                    Type columnType;
                    if (string.Equals(columnDataType, "money"))
                        columnType = typeof(Currency);
                    else
                        columnType = rdr.GetFieldType(i);

                    var allowDBNull = (bool)schemaTable.Rows[i]["AllowDBNull"];
                    columnInfo.IsNullable = allowDBNull;

                    if (allowDBNull &&
                        (typeof(Int16).IsAssignableFrom(columnType) ||
                         typeof(Int32).IsAssignableFrom(columnType) ||
                         typeof(Int64).IsAssignableFrom(columnType) ||
                         typeof(double).IsAssignableFrom(columnType) ||
                         typeof(decimal).IsAssignableFrom(columnType) ||
                         typeof(DateTime).IsAssignableFrom(columnType) ||
                         typeof(byte).IsAssignableFrom(columnType) ||
                         typeof(Currency).IsAssignableFrom(columnType)))
                        columnType = typeof(Nullable<>).MakeGenericType(new Type[] { columnType });

                    columnInfo.DataType = columnType;

                    result.ColumnInfos.Add(columnInfo);
                }
                while (rdr.Read())
                {
                    var newItem = new Row();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        object value = rdr[i];
                        if (value == DBNull.Value)
                            value = null;

                        newItem.Columns.Add(value);
                    }

                    result.Rows.Add(newItem);
                }
            }
            catch (Exception ex)
            {
                if (throwExceptionIfError)
                    throw new Exception(ex.Message + sql);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                    rdr.Dispose();
                    rdr = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }

            return result;
        }

        public DataSource ExecuteSP(string spName, List<DBODataParameter> parameters)
        {
            return ExecuteSP(spName, parameters, true);
        }
        public DataSource ExecuteSP(string spName, List<DBODataParameter> parameters, bool throwExceptionIfError)
        {
            var result = new DataSource("", spName);

            IDbConnection conn = null;
            IDbCommand cmd = null;
            IDataReader rdr = null;
            List<IDbDataParameter> allOutputParams = new List<IDbDataParameter>();

            try
            {
                conn = GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (ConfigurationManager.AppSettings["CommandTimeOut"] != null)
                    cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"]);

                foreach (var param in parameters)
                {
                    IDbDataParameter oo = cmd.CreateParameter();
                    oo.ParameterName = param.ParameterName;
                    oo.DbType = param.DbType;
                    oo.Direction = param.Direction;
                    oo.Size = param.Size;
                    oo.Value = param.Value ?? DBNull.Value;
                    cmd.Parameters.Add(oo);

                    if (oo.Direction == ParameterDirection.Output)
                        allOutputParams.Add(oo);
                }

                rdr = cmd.ExecuteReader();

                var schemaTable = rdr.GetSchemaTable();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    var columnName = rdr.GetName(i);
                    var columnInfo = new ColumnInfo() { Name = columnName };

                    var columnDataType = rdr.GetDataTypeName(i);
                    Type columnType;
                    if (string.Equals(columnDataType, "money"))
                        columnType = typeof(Currency);
                    else
                        columnType = rdr.GetFieldType(i);

                    var allowDBNull = (bool)schemaTable.Rows[i]["AllowDBNull"];
                    columnInfo.IsNullable = allowDBNull;

                    if (allowDBNull &&
                        (typeof(Int16).IsAssignableFrom(columnType) ||
                         typeof(Int32).IsAssignableFrom(columnType) ||
                         typeof(Int64).IsAssignableFrom(columnType) ||
                         typeof(double).IsAssignableFrom(columnType) ||
                         typeof(decimal).IsAssignableFrom(columnType) ||
                         typeof(DateTime).IsAssignableFrom(columnType) ||
                         typeof(byte).IsAssignableFrom(columnType) ||
                         typeof(Currency).IsAssignableFrom(columnType)))
                        columnType = typeof(Nullable<>).MakeGenericType(new Type[] { columnType });

                    columnInfo.DataType = columnType;
                    result.ColumnInfos.Add(columnInfo);
                }
                while (rdr.Read())
                {
                    var newItem = new Row();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        object value = rdr[i];
                        if (value == DBNull.Value)
                            value = null;

                        newItem.Columns.Add(value);
                    }

                    result.Rows.Add(newItem);
                }
            }
            catch (Exception ex)
            {
                if (throwExceptionIfError)
                    throw new Exception(ex.Message + spName);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                    rdr.Dispose();
                    rdr = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }

            foreach (var param in allOutputParams)
                parameters.Where(p => string.Equals(p.ParameterName, param.ParameterName)).First().Value = param.Value;

            return result;
        }

        public Dictionary<Guid, object> SubmitChanges(List<SqlReference> sqlReferences)
        {
            var result = new Dictionary<Guid, object>();

            IDbConnection conn = null;
            IDbCommand cmd = null;
            IDbTransaction trans = null;

            try
            {
                conn = GetConnection();
                cmd = conn.CreateCommand();

                if (sqlReferences.Count > 1)
                {
                    trans = conn.BeginTransaction();
                    cmd.Transaction = trans;
                }

                foreach (var sqlReference in sqlReferences)
                {
                    var sql = sqlReference.Sql;
                    foreach (var keyItem in sqlReference.SqlRelationShips)
                    {
                        if (result.ContainsKey(keyItem.Value))
                            sql = sql.Replace(string.Format("[{0}]", keyItem.Key.ToUpper()), Convert.ToString(result[keyItem.Value]));
                        else
                            throw new IndexOutOfRangeException("Can't find the key with the given GUID from SqlReference. Please make sure the related Insert/Update statement Submitted in same transaction.");
                    }

                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    if (this.CommandTimeout.HasValue)
                        cmd.CommandTimeout = this.CommandTimeout.Value;

                    object returnValue;
                    returnValue = cmd.ExecuteScalar();

                    result.Add(sqlReference.Guid, returnValue);
                }

                if (trans != null)
                    trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new Exception(string.Format("{0}; sql: {1}", ex.Message, cmd.CommandText), ex);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                if (trans != null)
                {
                    trans.Dispose();
                    trans = null;
                }
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }

            return result;
        }
    }
}
