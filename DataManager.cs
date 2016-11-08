using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DBO.Data.Interfaces;
using DBO.Data.Managers;
using DBO.Data.Objects;
using DBO.Data.Objects.Enums;

namespace DBO.Data
{
    public class DataManager
    {
        protected ConnectionStringSettings _connectionString;
        protected virtual ConnectionStringSettings ConnectionString
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DataProviderType DataProviderType
        {
            get
            {
                var providerName = this.ConnectionString.ProviderName.ToUpper();
                if (providerName.StartsWith("MYSQL"))
                    return DataProviderType.MySql;
                else if (providerName.Contains("ORACLE"))
                    return DataProviderType.Oracle;
                else
                    return DataProviderType.SqlServer;
            }
        }

        public static object ConvertToFitInSql(object value)
        {
            if (value == null)
                return "null";

            var valueType = value.GetType();
            if (typeof(bool).IsAssignableFrom(valueType))
                return Convert.ToByte((bool)value);
            else if (typeof(Int16?).IsAssignableFrom(valueType) ||
                     typeof(Int32?).IsAssignableFrom(valueType) ||
                     typeof(Int64?).IsAssignableFrom(valueType) ||
                     typeof(double?).IsAssignableFrom(valueType) ||
                     typeof(decimal?).IsAssignableFrom(valueType) ||
                     typeof(sbyte?).IsAssignableFrom(valueType) ||
                     typeof(byte?).IsAssignableFrom(valueType))
                return value;
            else if (typeof(Currency?).IsAssignableFrom(valueType))
                return value.ConvertTo<decimal?>();
            else if (typeof(Enum).IsAssignableFrom(valueType))
                return (int)value;
            else if (typeof(DateTime?).IsAssignableFrom(valueType))
                return string.Format("'{0}'", ((DateTime?)value).Value.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            else if (typeof(IEnumerable).IsAssignableFrom(valueType) && !typeof(string).IsAssignableFrom(valueType))
                return string.Join(",", (value as IEnumerable).Cast<object>().Select(item => ConvertToFitInSql(item)));
            else if (value is Type)
                return SqlBuilder.ConvertParameter(value as Type);

            return string.Format("'{0}'", Convert.ToString(value).Replace("'", "''"));
        }
        //}
        //public class DataManager<SqlReference> : DataManager where SqlReference : ISqlReference
        //{
        public T SelectSingle<T>(Expression<Func<T, object>> selectedColumns, WhereClause whereClause, params object[] values) where T : ITable
        {
            if (whereClause.Top != 1)
                whereClause.Top = 1;
            return Select<T>(selectedColumns, false, whereClause, values).SingleOrDefault();
        }
        public T SelectSingle<T>(WhereClause whereClause, params object[] values) where T : ITable
        {
            if (whereClause.Top != 1)
                whereClause.Top = 1;
            return Select<T>(whereClause, values).SingleOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Generic type to create a List to return.</typeparam>
        /// <returns>
        /// A System.Collections.Generic.List whose elements are the result
        /// of invoking the transform function on each element of source.
        /// </returns>
        public List<T> Select<T>() where T : ITable
        {
            return Select<T>(new WhereClause());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Generic type to create a List to return.</typeparam>
        /// <param name="whereClause">Where, Top and OrderBy is assign here.</param>
        /// <param name="values">Value which will replace in string.Format {0}, {1}, {2}...</param>
        /// <returns>
        /// A System.Collections.Generic.List whose elements are the result
        /// of invoking the transform function on each element of source.
        /// </returns>
        public List<T> Select<T>(WhereClause whereClause, params object[] values) where T : ITable
        {
            return Select<T>(null, whereClause, values);
        }

        public List<T> Select<T>(Expression<Func<T, object>> selectedColumns) where T : ITable
        {
            return Select<T>(selectedColumns, false, new WhereClause());
        }
        public List<T> Select<T>(Expression<Func<T, object>> selectedColumns, bool distinctByColumns) where T : ITable
        {
            return Select<T>(selectedColumns, distinctByColumns, new WhereClause());
        }
        public List<T> Select<T>(Expression<Func<T, object>> selectedColumns, WhereClause whereClause, params object[] values) where T : ITable
        {
            return Select<T>(selectedColumns, false, whereClause, values);
        }
        public List<T> Select<T>(Expression<Func<T, object>> selectedColumns, bool distinctByColumns, WhereClause whereClause, params object[] values) where T : ITable
        {
            string tableName = SqlBuilder.ConvertParameter(typeof(T));

            string columns = "*";
            if (selectedColumns != null)
            {
                var columnList = new List<string>();
                if (selectedColumns.Body is NewExpression)
                    foreach (var column in (selectedColumns.Body as NewExpression).Members)
                        columnList.Add(column.Name);
                else if (selectedColumns.Body is UnaryExpression)
                    columnList.Add(((selectedColumns.Body as UnaryExpression).Operand as MemberExpression).Member.Name);

                if (columnList.Count > 0)
                    columns = string.Join(",", columnList);
            }

            var top = whereClause.Top;
            var fromNumber = whereClause.FromNumber;
            var toNumber = whereClause.ToNumber;

            if (top > 0 && this.DataProviderType != Objects.Enums.DataProviderType.SqlServer)
            {
                if (!fromNumber.HasValue)
                    fromNumber = 1;
                if (!toNumber.HasValue)
                    toNumber = top;
                else if (toNumber > top - fromNumber + 1)
                    toNumber = top - fromNumber + 1;

                top = 0;
            }

            string sql = "";
            if (top > 0)
                sql = string.Format("SELECT TOP {0} {2} FROM {1}", whereClause.Top, tableName, columns);
            else
                sql = string.Format("SELECT {1} FROM {0}", tableName, columns);

            if (!string.IsNullOrWhiteSpace(whereClause.Where))
                sql = string.Format("{0} WHERE {1}", sql, whereClause.Where);

            if (distinctByColumns)
                sql = string.Format("{0} GROUP BY {1}", sql, columns);

            if (this.DataProviderType != Objects.Enums.DataProviderType.SqlServer ||
                ((fromNumber <= 0 || fromNumber == null) && (toNumber <= 0 || toNumber == null)))
            {
                if (!string.IsNullOrWhiteSpace(whereClause.OrderBy))
                    sql = "{0} ORDER BY {1}".FormatWith(sql, whereClause.OrderBy);
            }

            if (this.DataProviderType == Objects.Enums.DataProviderType.SqlServer)
            {
                if (fromNumber > 0 || toNumber > 0)
                {
                    if (whereClause.IsSQLServer2000)
                    {
                        string orderBy;
                        if (string.IsNullOrWhiteSpace(whereClause.OrderBy))
                            orderBy = "rn";
                        else
                            orderBy = whereClause.OrderBy;

                        string rowNum = columns + ", rn=IDENTITY(INT,1,1) INTO #TempTable FROM";
                        string rowNumBetween = null;
                        if (fromNumber > 0 && toNumber > 0)
                            rowNumBetween = string.Format("WHERE rn between {0} and {1}", fromNumber, toNumber);
                        else if (fromNumber > 0)
                            rowNumBetween = string.Format("WHERE rn>={0}", fromNumber);
                        else if (toNumber > 0)
                            rowNumBetween = string.Format("WHERE rn<={0}", toNumber);

                        sql = string.Format("{0};SELECT {2} FROM #TempTable a {1};DROP TABLE #TempTable;", sql.Replace(columns + " FROM", rowNum), rowNumBetween, columns);
                    }
                    else
                    {
                        string orderBy;
                        if (string.IsNullOrWhiteSpace(whereClause.OrderBy))
                            orderBy = "(SELECT 0)";
                        else
                            orderBy = whereClause.OrderBy;

                        string rowNum = string.Format("{1}, Row_Number() Over(ORDER BY {0}) as rn FROM", orderBy, columns);
                        string rowNumBetween = null;
                        if (fromNumber > 0 && toNumber > 0)
                            rowNumBetween = string.Format("WHERE rn between {0} and {1}", fromNumber, toNumber);
                        else if (fromNumber > 0)
                            rowNumBetween = string.Format("WHERE rn>={0}", fromNumber);
                        else if (toNumber > 0)
                            rowNumBetween = string.Format("WHERE rn<={0}", toNumber);

                        sql = string.Format("SELECT {2} FROM ({0}) a {1}", sql.Replace(columns + " FROM", rowNum), rowNumBetween, columns);
                    }
                }
            }
            else if (this.DataProviderType == Objects.Enums.DataProviderType.MySql)
            {
                var startNumber = fromNumber - 1;
                if (startNumber >= 0 && toNumber > 0)
                {
                    var rows = toNumber - startNumber;
                    sql = "{0} LIMIT {1}, {2}".FormatWith(sql, startNumber, rows);
                }
                else if (toNumber > 0)
                    sql = "{0} LIMIT {1}".FormatWith(sql, toNumber);
                else if (startNumber >= 0)
                    sql = "{0} LIMIT {1}, {2}".FormatWith(sql, startNumber, int.MaxValue);
            }
            else if (this.DataProviderType == Objects.Enums.DataProviderType.Oracle)
            {
                string rowNumBetween = null;
                if (fromNumber > 0 && toNumber > 0)
                    rowNumBetween = string.Format("WHERE ROWNUM BETWEEN {0} and {1}", fromNumber, toNumber);
                else if (fromNumber > 0)
                    rowNumBetween = string.Format("WHERE ROWNUM>={0}", fromNumber);
                else if (toNumber > 0)
                    rowNumBetween = string.Format("WHERE ROWNUM<={0}", toNumber);

                sql = "select * from ({0}) {1}".FormatWith(sql, rowNumBetween);
            }

            return Execute<T>(new SqlBuilder(sql, values));
        }

        public long SelectCount<T>()
        {
            return SelectCount<T>(new WhereClause());
        }
        public long SelectCount<T>(WhereClause whereClause)
        {
            string where = "";
            if (!string.IsNullOrWhiteSpace(whereClause.Where))
                where = string.Format(" where {0}", whereClause.Where);

            return Execute<SystemItem>(
                new SqlBuilder(new SqlBuilder("select Count(*) as Count from {0} ", typeof(T)).Sql + where)).FirstOrDefault().Count;
        }

        /// <summary>
        /// Call this method for Insert, Update or Delete which is not return any data.
        /// </summary>
        /// <param name="sqlBuilder"></param>
        public void Execute(SqlBuilder sqlBuilder)
        {
            if (sqlBuilder.Sql.Contains("SELECT "))
                throw new ArgumentException("Please use Execute<T>() instead of Execute() for Select Statement to retrieve data from database.");

            Execute<object>(sqlBuilder);
        }

        public int? CommandTimeout { get; set; }

        /// <summary>
        /// Call this method for any Stored Procedure. Not recomment to use this.
        /// </summary>
        /// <returns></returns>
        public List<T> ExecuteSP<T>(List<DBODataParameter> parameters)
        {
            DataServiceClient client = new DataServiceClient(this.ConnectionString);
            try
            {
                var result = new List<T>();

                var dataSource = client.ExecuteSP(typeof(T).Name, parameters);
                if (typeof(DataSource).IsAssignableFrom(typeof(T)))
                {
                    (result as IList).Add(dataSource);
                    return result;
                }

                var properties = typeof(T).GetProperties();
                var propertyDict = new Dictionary<string, PropertyInfo>();
                foreach (var propertyInfo in properties)
                    propertyDict.Add(propertyInfo.Name.ToUpper(), propertyInfo);

                // in case the Column name in tables or stored procedures contains char which invalid for build Property in class
                //for (var i = 0; i < dataSource.ColumnNames.Length; i++)
                //    dataSource.ColumnNames[i] = ConvertToValidPropertyName(dataSource.ColumnNames[i]);

                foreach (var row in dataSource.Rows)
                {
                    var newItem = (T)typeof(T).GetDefaultValue();
                    for (var i = 0; i < dataSource.ColumnInfos.Count; i++)
                    {
                        var columnName = dataSource.ColumnInfos[i].Name;
                        if (propertyDict.ContainsKey(columnName.ToUpper()))
                        {
                            var property = propertyDict[columnName.ToUpper()];
                            property.SetValue(newItem, row.Columns[i].ConvertToTargetTypeValue(property.PropertyType), null);
                        }
                    }
                    if (newItem is IModified)
                        (newItem as IModified).IsModified = false;
                    if (newItem is IPropertyModified)
                        (newItem as IPropertyModified).MarkAsDefault();

                    result.Add(newItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (client.State != CommunicationState.Faulted)
                //    client.Close();
            }
        }

        /// <summary>
        /// Call this method for any custom SQL statement. Not recomment to use this.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlBuilder"></param>
        /// <returns></returns>
        public List<T> Execute<T>(SqlBuilder sqlBuilder)
        {
            DataServiceClient client = new DataServiceClient(this.ConnectionString);
            try
            {
                if (this.CommandTimeout.HasValue)
                    client.CommandTimeout = this.CommandTimeout;

                var result = new List<T>();

                var sql = sqlBuilder.Sql;

                var dataSource = client.Execute(sql);
                if (typeof(DataSource).IsAssignableFrom(typeof(T)))
                {
                    (result as IList).Add(dataSource);
                    return result;
                }

                var properties = typeof(T).GetProperties();
                var propertyDict = new Dictionary<string, PropertyInfo>();
                foreach (var propertyInfo in properties)
                    propertyDict.Add(propertyInfo.Name.ToUpper(), propertyInfo);

                // in case the Column name in tables or stored procedures contains char which invalid for build Property in class
                //for (var i = 0; i < dataSource.ColumnNames.Length; i++)
                //    dataSource.ColumnNames[i] = ConvertToValidPropertyName(dataSource.ColumnNames[i]);

                var columnNames = new List<string>();
                for (var i = 0; i < dataSource.ColumnInfos.Count; i++)
                    columnNames.Add(ValidateColumnName(dataSource.ColumnInfos[i].Name).ToUpper());

                foreach (var row in dataSource.Rows)
                {
                    var newItem = (T)typeof(T).GetDefaultValue();
                    for (var i = 0; i < dataSource.ColumnInfos.Count; i++)
                    {
                        var columnName = columnNames[i];
                        if (propertyDict.ContainsKey(columnName))
                        {
                            var property = propertyDict[columnName];
                            property.SetValue(newItem, row.Columns[i].ConvertToTargetTypeValue(property.PropertyType), null);
                        }
                    }
                    if (newItem is IModified)
                        (newItem as IModified).IsModified = false;
                    if (newItem is IPropertyModified)
                        (newItem as IPropertyModified).MarkAsDefault();

                    result.Add(newItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (client.State != CommunicationState.Faulted)
                //    client.Close();
            }
        }
        /// <summary>
        /// Call this method for any custom SQL statement. Ignore any exception from database. Not recomment to use this.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlBuilder"></param>
        /// <returns></returns>
        public List<T> ExecuteWithoutException<T>(SqlBuilder sqlBuilder)
        {
            DataServiceClient client = new DataServiceClient(this.ConnectionString);
            try
            {
                if (this.CommandTimeout.HasValue)
                    client.CommandTimeout = this.CommandTimeout;

                var result = new List<T>();

                var sql = sqlBuilder.Sql;

                var dataSource = client.ExecuteWithoutException(sql);
                if (typeof(DataSource).IsAssignableFrom(typeof(T)))
                {
                    (result as IList).Add(dataSource);
                    return result;
                }

                var properties = typeof(T).GetProperties();
                var propertyDict = new Dictionary<string, PropertyInfo>();
                foreach (var propertyInfo in properties)
                    propertyDict.Add(propertyInfo.Name.ToUpper(), propertyInfo);

                foreach (var row in dataSource.Rows)
                {
                    var newItem = (T)Activator.CreateInstance(typeof(T));
                    for (var i = 0; i < dataSource.ColumnInfos.Count; i++)
                    {
                        var columnName = dataSource.ColumnInfos[i].Name;
                        if (propertyDict.ContainsKey(columnName.ToUpper()))
                            propertyDict[columnName.ToUpper()].SetValue(newItem, row.Columns[i], null);
                    }
                    if (newItem is IModified)
                        (newItem as IModified).IsModified = false;
                    if (newItem is IPropertyModified)
                        (newItem as IPropertyModified).MarkAsDefault();

                    result.Add(newItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (client.State != CommunicationState.Faulted)
                //    client.Close();
            }
        }
        /// <summary>
        /// Once SubmitChanges is called without exception thrown, the transaction will be reset and all Sql Statement will cleared.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Guid, object> SubmitChanges(List<SqlReference> sqlList)
        {
            if (!(sqlList is List<SqlReference>))
                throw new ArgumentException("sqlList must be List<DBO.DataService.SqlReference>.");

            var _sqlList = sqlList as List<SqlReference>;

            DataServiceClient client = new DataServiceClient(this.ConnectionString);
            try
            {
                if (this.CommandTimeout.HasValue)
                    client.CommandTimeout = this.CommandTimeout;

                var result = client.SubmitChanges(_sqlList);
                foreach (var sqlReference in _sqlList)
                {
                    if (result.ContainsKey(sqlReference.Guid))
                        sqlReference.ReturnValue = result[sqlReference.Guid];
                }

                this.Transaction.ResetTransaction();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (client.State != CommunicationState.Faulted)
                //    client.Close();
            }
        }

        private TransactionManager _transaction;
        public TransactionManager Transaction { get { return _transaction; } }
        /// <summary>
        /// BeginTransaction to use the Insert, Update or Delete statement.
        /// </summary>
        /// <returns></returns>
        public TransactionManager BeginTransaction()
        {
            if (_transaction != null)
                throw new Exception("Please create 1 transaction for 1 DataManager only.");
            _transaction = new TransactionManager(this);
            return _transaction;
        }

        public static bool IsColumnNameValid(string name)
        {
            string chars = "`!@#$%^&*()-+=~:;{}|\\<>,./? ";
            return chars.ToCharArray().Where(item => name.Contains(item)).Count() == 0;
        }
        public static string ValidateColumnName(string name)
        {
            if (!IsColumnNameValid(name))
            {
                foreach (var symbol in "`!@#$%^&*()-+=~:;{}|\\<>,./? ")
                    name = name.Replace(symbol, '_');
            }

            return name;
        }
    }
}
