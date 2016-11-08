using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBO.Data.Interfaces;
using DBO.Data.Managers;

namespace DBO.Data
{
    using DBO.Data.Objects;
    using sys;
    using DBO.Data.Objects.Enums;

    /// <summary>
    /// Summary description for GenerateClasses
    /// </summary>
    public class ClassGenerator<DataManager, SqlReference>
        where DataManager : DBO.Data.DataManager
        where SqlReference : ISqlReference
    {
        Dictionary<string, string> _keyQueries;
        public ClassGenerator()
            : this(new Dictionary<string, string>())
        {
        }
        public ClassGenerator(Dictionary<string, string> keyQueries)
        {
            _keyQueries = new Dictionary<string, string>();
            foreach (var keyQuery in keyQueries)
                _keyQueries.Add(keyQuery.Key, keyQuery.Value);
        }

        private string _dataManagerName;
        public string Generate(string nameSpace, string dataManagerName)
        {
            return Generate(nameSpace, dataManagerName, new string[] { "U", "V", "P" });
        }

        public class TableTypeAndColumns
        {
            public TableTypeAndColumns()
            {
                Columns = new List<Columns>();
            }
            public string TableType { get; set; }
            public List<Columns> Columns { get; set; }
        }
        public string Generate(string nameSpace, string dataManagerName, IEnumerable<string> tableTypes, params string[] tableNames)
        {
            return Generate(nameSpace, dataManagerName, tableTypes, false, tableNames);
        }
        public string GenerateExclude(string nameSpace, string dataManagerName, IEnumerable<string> tableTypes, params string[] tableNames)
        {
            return Generate(nameSpace, dataManagerName, tableTypes, true, tableNames);
        }
        public string Generate(string nameSpace, string dataManagerName, IEnumerable<string> tableTypes, bool isExcluded, params string[] tableNames)
        {
            _dataManagerName = dataManagerName;

            StringBuilder txt = new StringBuilder();
            txt.AppendLine("using System;");
            txt.AppendLine("using System.Collections.Generic;");
            txt.AppendLine("using System.ComponentModel.DataAnnotations;");
            txt.AppendLine("using System.Data;");
            txt.AppendLine("using System.Reflection;");
            txt.AppendLine("using DBO.Data;");
            txt.AppendLine("using DBO.Data.Attributes;");
            txt.AppendLine("using DBO.Data.Interfaces;");
            txt.AppendLine("using DBO.Data.Managers;");
            txt.AppendLine("using DBO.Data.Objects;");
            txt.AppendLine();
            txt.AppendLine(string.Format("namespace {0}", nameSpace ?? "dbo"));
            txt.AppendLine("{");

            var dataClassName = "{0}Class".FormatWith(dataManagerName);
            if (tableTypes.Contains("U"))
            {
                txt.AppendLine("    public class {0}<T> : BaseClass<{1}, T> where T : ITable".FormatWith(dataClassName, typeof(DataManager).Name));
                txt.AppendLine("    {");
                txt.AppendLine("    }");
                txt.AppendLine();
            }

            var allPrimaryKeys = new Dictionary<string, List<string>>();

            var dataManager = (DataManager)Activator.CreateInstance(typeof(DataManager));

            var allColumns = GetAllColumns();
            if (dataManager.DataProviderType == DataProviderType.MySql)
                foreach (var item in allColumns)
                    item.TableType = "U";

            string primaryKeySql = null;
            if (dataManager.DataProviderType == Objects.Enums.DataProviderType.Oracle)
            {
                primaryKeySql = "SELECT cols.TABLE_NAME, cols.COLUMN_NAME " +
                                "FROM all_constraints cons, all_cons_columns cols " +
                                "WHERE cons.constraint_type = 'P' " +
                                "AND cons.constraint_name = cols.constraint_name " +
                                "AND cons.owner = cols.owner " +
                                "AND cons.owner = '{0}' ".FormatWith(dataManagerName) +
                                "ORDER BY cols.table_name";
            }
            else
            {
                primaryKeySql = "select c.TABLE_NAME, c.COLUMN_NAME\n" +
                                "from INFORMATION_SCHEMA.TABLE_CONSTRAINTS pk ,\n" +
                                "INFORMATION_SCHEMA.KEY_COLUMN_USAGE c\n" +
                                "where CONSTRAINT_TYPE = 'PRIMARY KEY'\n" +
                                "and c.TABLE_NAME = pk.TABLE_NAME\n" +
                                "and c.CONSTRAINT_NAME = pk.CONSTRAINT_NAME";

                if (dataManager.DataProviderType == DataProviderType.MySql)
                    primaryKeySql += "\nand c.CONSTRAINT_SCHEMA = pk.CONSTRAINT_SCHEMA and pk.CONSTRAINT_SCHEMA = '{0}'".FormatWith(dataManagerName);
            }
            var primaryKeys = dataManager.Execute<PrimaryKey>(new SqlBuilder(primaryKeySql));
            foreach (var primaryKey in primaryKeys)
            {
                if (!allPrimaryKeys.ContainsKey(primaryKey.TABLE_NAME))
                    allPrimaryKeys.Add(primaryKey.TABLE_NAME, new List<string>());
                allPrimaryKeys[primaryKey.TABLE_NAME].Add(primaryKey.COLUMN_NAME);
            }

            var allTables = new Dictionary<string, TableTypeAndColumns>();
            foreach (var column in allColumns)
            {
                if (!allTables.ContainsKey(column.TableName))
                    allTables.Add(column.TableName, new TableTypeAndColumns() { TableType = column.TableType.Trim() });

                allTables[column.TableName].Columns.Add(column);
            }

            List<Procedures> procedures = new List<Procedures>();
            if (dataManager.DataProviderType == DataProviderType.Oracle)
            {
                procedures = dataManager.Execute<Procedures>(new SqlBuilder("SELECT OBJECT_NAME as Name FROM ALL_OBJECTS WHERE OBJECT_TYPE='PROCEDURE' AND OWNER={0}", dataManagerName));
            }
            else if (dataManager.DataProviderType == DataProviderType.SqlServer)
            {
                procedures = dataManager.Select<Procedures>();//new WhereClause(@"name not like 'sp\_%' ESCAPE '\'"));
            }

            var procedureNoParameterList = new List<Procedures>(procedures.Where(p => !allTables.ContainsKey(p.Name)));
            foreach (var procedure in procedureNoParameterList)
                allTables.Add(procedure.Name, new TableTypeAndColumns() { TableType = "P" });

            var allTablesFiltered = allTables;
            if (tableNames != null && tableNames.Count() > 0)
            {
                var tempAllTablesFiltered = new Dictionary<string, TableTypeAndColumns>();
                if (isExcluded)
                {
                    foreach (var item in allTablesFiltered
                        .Where(t => !tableNames.Select(table => table.ToUpper()).Contains(t.Key.ToUpper())))
                        tempAllTablesFiltered.Add(item.Key, item.Value);
                }
                else
                {
                    foreach (var item in allTablesFiltered
                        .Where(t => tableNames.Select(table => table.ToUpper()).Contains(t.Key.ToUpper())))
                        tempAllTablesFiltered.Add(item.Key, item.Value);
                }
                allTablesFiltered = tempAllTablesFiltered;
            }
            if (tableTypes != null && tableTypes.Count() > 0)
            {
                var tempAllTablesFiltered = new Dictionary<string, TableTypeAndColumns>();
                foreach (var item in allTablesFiltered.Where(t => tableTypes.Contains(t.Value.TableType.Trim())))
                    tempAllTablesFiltered.Add(item.Key, item.Value);
                allTablesFiltered = tempAllTablesFiltered;
            }
            foreach (var table in allTablesFiltered)
            {
                if (!DBODataManager.IsColumnNameValid(table.Key))
                    continue;

                try
                {
                    var columns = table.Value.Columns;

                    var isStoredProc = string.Equals(table.Value.TableType.Trim(), "P");
                    if (isStoredProc)
                    {
                        if (table.Key.ToUpper().Equals("SPROC_MANAGERCHECKLIST"))
                        {
                        }

                        var columnValues = new List<string>();

                        DataSource dataSource = null;
                        try
                        {
                            string sql;
                            if (_keyQueries.ContainsKey(table.Key.ToUpper()))
                                sql = _keyQueries[table.Key.ToUpper()];
                            else
                            {
                                foreach (var column in columns)
                                    columnValues.Add(GetTypeValue(column.TypeName));
                                sql = string.Format("exec {0} {1}", table.Key, string.Join(",", columnValues.ToArray()));
                            }

                            var dataSources = dataManager.ExecuteWithoutException<DataSource>(new SqlBuilder(sql));
                            dataSource = dataSources.FirstOrDefault();
                        }
                        catch
                        {
                            continue;
                        }

                        columns = new List<Columns>();
                        var columnNames = new List<string>();
                        for (var i = 0; i < dataSource.ColumnInfos.Count; i++)
                        {
                            //var columnName = DBO.Data.DataManager.ValidateColumnName(dataSource.ColumnInfos[i].Name);
                            var columnName = dataSource.ColumnInfos[i].Name;
                            if (!columnNames.Contains(columnName))
                            {
                                var type = dataSource.ColumnInfos[i].DataType;
                                columns.Add(new Columns()
                                {
                                    ColumnName = columnName,
                                    IsNullable = dataSource.ColumnInfos[i].IsNullable ? 1 : 0,
                                    TableName = table.Key,
                                    TableType = table.Value.TableType,
                                    TypeName = GetTypeName(type)
                                });
                                columnNames.Add(columnName);
                            }
                        }

                        //txt.Append(string.Format("    public partial class {0} : IModified", table.Key.ToUpper()));
                        txt.Append(string.Format("    public partial class {0} : {1}", table.Key, typeof(IStoredProcedure).Name));
                        //txt.AppendLine(", " + typeof(IStoredProcedure).Name);

                        if (columns.Where(column => (string.Equals(column.ColumnName, "rn") && string.Equals(GetTypeName(dataManager, nameSpace, column), "long?"))).Count() == 1)
                            txt.Append(", " + typeof(IPaging).Name);

                        txt.AppendLine("");
                    }
                    else
                    {
                        if (table.Key.ToUpper().Equals("T_POLICY"))
                        {
                        }

                        var foreignKeys = AllFKConstraints.Where(item => string.Equals(item.FK_Table.ToUpper(), table.Key.ToUpper()));
                        foreach (var foreignKey in foreignKeys)
                        {
                            if (dataManager.DataProviderType == DataProviderType.MySql)
                                txt.AppendLine(string.Format("    [ForeignKey(\"{0}\", typeof({1}), \"{2}\")]", foreignKey.FK_Column, foreignKey.PK_Table.ToLower(), foreignKey.PK_Column));
                            else
                                txt.AppendLine(string.Format("    [ForeignKey(\"{0}\", typeof({1}), \"{2}\")]", foreignKey.FK_Column, foreignKey.PK_Table.ToUpper(), foreignKey.PK_Column));
                        }

                        //txt.Append(string.Format("    public partial class {0} : IModified", table.Key.ToUpper()));
                        if (dataManager.DataProviderType == DataProviderType.MySql)
                            txt.Append(string.Format("    public partial class {0} : {1}<{0}>", table.Key.ToLower(), dataClassName));
                        else
                            txt.Append(string.Format("    public partial class {0} : {1}<{0}>", table.Key.ToUpper(), dataClassName));

                        if (string.Equals(table.Value.TableType.Trim(), "U"))
                            //txt.AppendLine(", ITable, IPropertyModified");
                            txt.Append(", " + typeof(ITable).Name);
                        else if (string.Equals(table.Value.TableType.Trim(), "V"))
                            txt.Append(", " + typeof(IView).Name);
                        else
                            throw new Exception("Please don't include table type which other than 'U', 'V' and 'P'");

                        if (columns.Where(column => (string.Equals(column.ColumnName, "ID") && string.Equals(GetTypeName(dataManager, nameSpace, column), "int")) ||
                                                    (string.Equals(column.ColumnName, "CreatedBy") && string.Equals(GetTypeName(dataManager, nameSpace, column), "int")) ||
                                                    (string.Equals(column.ColumnName, "CreatedDate") && string.Equals(GetTypeName(dataManager, nameSpace, column), "DateTime?")) ||
                                                    (string.Equals(column.ColumnName, "ModifiedBy") && string.Equals(GetTypeName(dataManager, nameSpace, column), "int")) ||
                                                    (string.Equals(column.ColumnName, "ModifiedDate") && string.Equals(GetTypeName(dataManager, nameSpace, column), "DateTime?"))).Count() == 5)
                            txt.Append(", " + typeof(IStandardTable).Name);

                        if (columns.Where(column => (string.Equals(column.ColumnName, "CreatedDate") && string.Equals(GetTypeName(dataManager, nameSpace, column), "DateTime")) ||
                                                    (string.Equals(column.ColumnName, "ModifiedDate") && string.Equals(GetTypeName(dataManager, nameSpace, column), "DateTime"))).Count() == 2)
                            txt.Append(", " + typeof(IDate).Name);
                        if (columns.Where(column => (string.Equals(column.ColumnName, "CreatedDate") && string.Equals(GetTypeName(dataManager, nameSpace, column), "DateTime"))).Count() == 1)
                            txt.Append(", " + typeof(ICreatedDate).Name);
                        if (columns.Where(column => (string.Equals(column.ColumnName, "ModifiedDate") && string.Equals(GetTypeName(dataManager, nameSpace, column), "DateTime"))).Count() == 1)
                            txt.Append(", " + typeof(IModifiedDate).Name);

                        if (columns.Where(column => (string.Equals(column.ColumnName, "CreatedBy") && string.Equals(GetTypeName(dataManager, nameSpace, column), "int")) ||
                                                    (string.Equals(column.ColumnName, "ModifiedBy") && string.Equals(GetTypeName(dataManager, nameSpace, column), "int"))).Count() == 2)
                            txt.Append(", " + typeof(ICreator).Name);

                        if (columns.Where(column => (string.Equals(column.ColumnName, "ID") && string.Equals(GetTypeName(dataManager, nameSpace, column), "byte")) ||
                                                    string.Equals(column.ColumnName, "Code")).Count() == 2)
                            txt.Append(", " + typeof(IEnumCode).Name);
                        if (columns.Where(column => string.Equals(column.ColumnName, "Name") ||
                                                    string.Equals(column.ColumnName, "Name_ar")).Count() == 2)
                            txt.Append(", " + typeof(IMultiLingual).Name);
                        else if (columns.Where(column => string.Equals(column.ColumnName, "Name")).Count() == 1)
                            txt.Append(", " + typeof(IName).Name);
                        if (columns.Where(column => string.Equals(column.ColumnName, "Active") && string.Equals(GetTypeName(dataManager, nameSpace, column), "bool")).Count() == 1)
                            txt.Append(", " + typeof(IActive).Name);

                        if (dataManager.DataProviderType == DataProviderType.MySql)
                            txt.Append(", " + typeof(IMySql).Name);
                        else if (dataManager.DataProviderType == DataProviderType.Oracle)
                            txt.Append(", " + typeof(IOracle).Name);

                        txt.AppendLine("");
                    }

                    txt.AppendLine("    {");
                    if (dataManager.DataProviderType == DataProviderType.MySql)
                        txt.AppendLine(string.Format("        public {0}()", table.Key));
                    else
                        txt.AppendLine(string.Format("        public {0}()", table.Key.ToUpper()));
                    if (!string.Equals(table.Value.TableType.Trim(), "P"))
                        txt.AppendLine("            : base()");
                    txt.AppendLine("        {");
                    txt.AppendLine("            OnCreated();");
                    txt.AppendLine("        }");

                    if (!string.Equals(table.Value.TableType.Trim(), "P"))
                    {
                        var priColumns = columns.Where(c =>
                                        allPrimaryKeys.ContainsKey(table.Key) &&
                                        allPrimaryKeys[table.Key].Contains(FilterKeyword(c.ColumnName)));
                        if (priColumns.Count() > 0)
                        {
                            txt.AppendLine("");
                            var constructorParams = string.Join(", ", priColumns.Select(c => "{0} _{1}".FormatWith(
                                           GetTypeName(dataManager, nameSpace, c),
                                           FilterKeyword(c.ColumnName))));
                            if (dataManager.DataProviderType == DataProviderType.MySql)
                                txt.AppendLine("        public static {0} SelectExact({1})".FormatWith(table.Key, constructorParams));
                            else
                                txt.AppendLine("        public static {0} SelectExact({1})".FormatWith(table.Key.ToUpper(), constructorParams));
                            txt.AppendLine("        {");

                            if (priColumns.Count() == 1)
                            {
                                foreach (var c in priColumns)
                                {
                                    var ctype = GetTypeName(dataManager, nameSpace, c);
                                    if (c.IsNullable == 1)
                                    {
                                        txt.AppendLine("            if (object.Equals(_{0}, null))".FormatWith(c.ColumnName));
                                        txt.AppendLine("                return null;");
                                    }
                                    else if (string.Equals(ctype, "byte") ||
                                             string.Equals(ctype, "long") ||
                                             string.Equals(ctype, "short") ||
                                             string.Equals(ctype, "decimal") ||
                                             string.Equals(ctype, "int"))
                                    {
                                        txt.AppendLine("            if (_{0} <= 0)".FormatWith(c.ColumnName));
                                        txt.AppendLine("                return null;");
                                    }
                                }
                            }

                            txt.AppendLine("            var whereList = new List<string>();");
                            foreach (var c in priColumns)
                            {
                                if (c.IsNullable == 1)
                                {
                                    txt.AppendLine("            if (object.Equals(_{0}, null))".FormatWith(c.ColumnName));
                                    txt.AppendLine("                whereList.Add(new WhereClause(\"{0} is {1}\", _{0}).Where);".FormatWith(c.ColumnName, "{0}"));
                                    txt.AppendLine("            else");
                                    txt.AppendLine("                whereList.Add(new WhereClause(\"{0}={1}\", _{0}).Where);".FormatWith(c.ColumnName, "{0}"));
                                }
                                else
                                {
                                    txt.AppendLine("            whereList.Add(new WhereClause(\"{0}={1}\", _{0}).Where);".FormatWith(c.ColumnName, "{0}"));
                                }
                            }

                            if (dataManager.DataProviderType == DataProviderType.MySql)

                                txt.AppendLine("            return {0}.SelectSingle(new WhereClause(string.Join(\" and \", whereList.ToArray())));".FormatWith(
                                                                table.Key));
                            else
                                txt.AppendLine("            return {0}.SelectSingle(new WhereClause(string.Join(\" and \", whereList.ToArray())));".FormatWith(
                                                                table.Key.ToUpper()));
                            txt.AppendLine("        }");
                        }
                    }

                    //txt.AppendLine("        public bool IsModified");
                    //txt.AppendLine("        {");
                    //txt.AppendLine("            get;");
                    //txt.AppendLine("            set;");
                    //txt.AppendLine("        }");

                    txt.AppendLine();
                    txt.AppendLine("        #region partial method");
                    txt.AppendLine();
                    txt.AppendLine("        partial void OnCreated();");

                    foreach (var column in columns)
                    {
                        var columnName = FilterKeyword(column.ColumnName);
                        var columnType = GetTypeName(dataManager, nameSpace, column);

                        if (!string.IsNullOrEmpty(columnType))
                        {
                            txt.AppendLine(string.Format("        partial void On{0}ing({1} value);", columnName, columnType));
                            txt.AppendLine(string.Format("        partial void On{0}ed();", columnName));
                        }
                    }
                    txt.AppendLine();
                    txt.AppendLine("        #endregion");
                    txt.AppendLine();

                    foreach (var column in columns)
                    {
                        var columnName = FilterKeyword(column.ColumnName);
                        var columnType = GetTypeName(dataManager, nameSpace, column);

                        if (!string.IsNullOrEmpty(columnType))
                        {
                            txt.AppendLine(string.Format("        #region {0} {1}", columnType, columnName));
                            txt.AppendLine();
                            txt.AppendLine(string.Format("        private {0} _{1};", columnType, columnName));

                            if (!string.Equals(columnName, column.ColumnName))
                                txt.AppendLine("        [ActualName(\"{0}\")]".FormatWith(column.ColumnName));

                            if (allPrimaryKeys.ContainsKey(table.Key) && allPrimaryKeys[table.Key].Contains(columnName))
                            {
                                if (column.Is_Identity ||
                                    (string.Equals(column.TypeName.ToUpper(), "UNIQUEIDENTIFIER") && string.Equals(column.DefaultValue, "(newid())")))
                                    txt.AppendLine("        [PrimaryKey]");
                                else
                                    txt.AppendLine("        [PrimaryKey(true, false)]");
                            }

                            if (column.IsNullable < 1)
                                txt.AppendLine("        [Required]");
                            if (string.Equals(columnType, "string") && column.MaxLength > 0)
                                txt.AppendLine(string.Format("        [MaxLength({0})]", column.MaxLength));

                            if (column.DefaultValue != null)
                                txt.AppendLine(string.Format("        [HasDefault(\"{0}\")]", column.DefaultValue));

                            if (string.Equals(column.TypeName.ToUpper(), "DATE"))
                                txt.AppendLine("        [DateOnly]");
                            else if (string.Equals(column.TypeName.ToUpper(), "DATETIME"))
                                txt.AppendLine("        [DateIncludeTime]");

                            txt.AppendLine("        public {0} {1}".FormatWith(columnType, columnName));
                            txt.AppendLine("        {");
                            txt.AppendLine("            get {0} return _{1}; {2}".FormatWith("{", columnName, "}"));
                            txt.AppendLine("            set");
                            txt.AppendLine("            {");
                            txt.AppendLine("                if (!object.Equals(_{0}, value))".FormatWith(columnName));
                            txt.AppendLine("                {");
                            txt.AppendLine("                    On{0}ing(value);".FormatWith(columnName));
                            txt.AppendLine("                    _{0} = value;".FormatWith(columnName));
                            if (!isStoredProc)
                                txt.AppendLine("                    IsModified = true;");
                            txt.AppendLine("                    On{0}ed();".FormatWith(columnName));
                            txt.AppendLine("                }");
                            txt.AppendLine("            }");
                            txt.AppendLine("        }");
                            txt.AppendLine("");
                            txt.AppendLine("        #endregion");
                            txt.AppendLine("");
                        }
                    }

                    //if (string.Equals(table.Value.TableType.Trim(), "U"))
                    //{
                    //    txt.AppendLine("        #region interface IPropertyModified");
                    //    txt.AppendLine("");
                    //    txt.AppendLine("        private object _default;");
                    //    txt.AppendLine("        public void MarkAsDefault()");
                    //    txt.AppendLine("        {");
                    //    txt.AppendLine("            _default = this.MemberwiseClone();");
                    //    txt.AppendLine("        }");
                    //    txt.AppendLine("");
                    //    txt.AppendLine("        public PropertyInfo[] GetModifiedProperties()");
                    //    txt.AppendLine("        {");
                    //    txt.AppendLine("            return this.GetModifiedProperties(_default);");
                    //    txt.AppendLine("        }");
                    //    txt.AppendLine("");
                    //    txt.AppendLine("        #endregion");
                    //}

                    if (string.Equals(table.Value.TableType.Trim(), "P"))
                    {
                        var parameters = new List<string>();
                        foreach (var item in table.Value.Columns)
                        {
                            if (item.IsOutParam)
                                parameters.Add("out {0} {1}".FormatWith(GetTypeName(dataManager, nameSpace, item), item.ColumnName));
                            else
                                parameters.Add("{0} {1}".FormatWith(GetTypeName(dataManager, nameSpace, item), item.ColumnName));
                        }
                        var parameterString = string.Join(", ", parameters);

                        txt.AppendLine(string.Format("        public static List<{0}> ExecuteSP({1})", table.Key.ToUpper(), parameterString));
                        txt.AppendLine("        {");
                        txt.AppendLine("            var parameters = new List<DBODataParameter>();");

                        if (table.Value.Columns.Count > 0)
                        {
                            foreach (var column in table.Value.Columns)
                            {
                                txt.AppendLine("            parameters.Add(new DBODataParameter()");
                                txt.AppendLine("            {");
                                txt.AppendLine("                ParameterName = \"{0}\",".FormatWith(column.ColumnName));
                                txt.AppendLine("                DbType = DbType.{0},".FormatWith(GetDBTypeName(column.TypeName)));
                                txt.AppendLine("                Size = {0},".FormatWith(column.MaxLength ?? 0));
                                if (column.IsOutParam)
                                    txt.AppendLine("                Direction = ParameterDirection.Output");
                                else
                                {
                                    txt.AppendLine("                Direction = ParameterDirection.Input,");
                                    txt.AppendLine("                Value = {0}".FormatWith(column.ColumnName));
                                }
                                txt.AppendLine("            });");
                            }
                        }
                        txt.AppendLine("");
                        txt.AppendLine("            var items = new {0}().ExecuteSP<{1}>(parameters);".FormatWith(dataManager.GetType().FullName, table.Key.ToUpper()));
                        if (table.Value.Columns.Where(item => item.IsOutParam).Count() > 0)
                        {
                            foreach (var column in table.Value.Columns)
                                if (column.IsOutParam)
                                    txt.AppendLine("            {0} = null;".FormatWith(column.ColumnName));
                            txt.AppendLine("            foreach (var param in parameters)");
                            txt.AppendLine("            {");
                            txt.AppendLine("                switch (param.ParameterName)");
                            txt.AppendLine("                {");
                            foreach (var column in table.Value.Columns)
                                if (column.IsOutParam)
                                    txt.AppendLine("                    case \"{0}\": {0} = param.Value.ConvertTo<{1}>(); break;".FormatWith(column.ColumnName, GetTypeName(dataManager, nameSpace, column)));
                            txt.AppendLine("                }");
                            txt.AppendLine("            }");
                        }

                        txt.AppendLine("            return items;");
                        txt.AppendLine("        }");
                    }

                    txt.AppendLine("    }");
                }
                catch
                {
                }
            }

            txt.AppendLine("}");

            return txt.ToString();
        }
        //private bool IsNameValid(string name)
        //{
        //    string chars = "`!@#$%^&*()-+=~:;{}|\\<>,./? ";
        //    return chars.ToCharArray().Where(item => name.Contains(item)).Count() == 0;
        //}
        //private string ValidateName(string name)
        //{
        //    if (!IsNameValid(name))
        //    {
        //        foreach (var symbol in "`!@#$%^&*()-+=~:;{}|\\<>,./? ")
        //            name = name.Replace(symbol, '_');
        //    }

        //    return name;
        //}
        private string FilterKeyword(string name)
        {
            name = DBODataManager.ValidateColumnName(name);

            switch (name)
            {
                case "class":
                    return name.ToUpperInvariant();
                default:
                    return name;
            }
        }

        private List<Columns> GetAllColumns()
        {
            var dataManager = (DataManager)Activator.CreateInstance(typeof(DataManager));
            string sql;
            if (dataManager.DataProviderType == Objects.Enums.DataProviderType.Oracle)
            {
                sql = "select TABLE_NAME as TableName, COLUMN_NAME as ColumnName, DATA_LENGTH as MaxLength, DATA_DEFAULT as DefaultValue, DATA_TYPE as TypeName, CASE Nullable WHEN 'Y' THEN 1 ELSE 0 END as IsNullable, 'U' as TableType, 0 as Is_Identity, Data_Scale as DataScale, Data_Precision as DataPrecision from ALL_TAB_COLUMNS where Owner='{0}'".FormatWith(_dataManagerName);
            }
            else if (dataManager.DataProviderType == DataProviderType.SqlServer)
            {
                sql = "SELECT SO.NAME AS TableName, SC.NAME AS ColumnName, SC.LENGTH as MaxLength, SM.TEXT AS DefaultValue,t.name AS TypeName, SC.IsOutParam, SC.IsNullable, SO.xtype as TableType, c.Is_Identity\n" +
                        "FROM dbo.sysobjects SO INNER JOIN dbo.syscolumns SC ON SO.id = SC.id\n" +
                        "LEFT JOIN dbo.syscomments AS SM ON SC.cdefault = SM.id\n" +
                        "LEFT JOIN sys.types AS t ON SC.xusertype=t.user_type_id\n" +
                        "LEFT JOIN sys.columns AS c ON SC.ID = c.object_id AND SC.colid = c.Column_id\n" +
                    //"WHERE SO.xtype in ('P', 'U', 'V') and SO.NAME not like 'sp\\_%' ESCAPE '\\' and SO.Category<>2\n" +
                        "WHERE SO.xtype in ('P', 'U', 'V') and SO.Category<>2\n" +
                        "ORDER BY SO.[name], SC.colid";
            }
            else if (dataManager.DataProviderType == DataProviderType.MySql)
            {
                sql = "SELECT TABLE_NAME as TableName, COLUMN_NAME as ColumnName, Character_Maximum_Length as MaxLength, COLUMN_DEFAULT as DefaultValue, DATA_TYPE as TypeName, CASE IS_NULLABLE WHEN 'YES' THEN 1 ELSE 0 END as IsNullable, CASE Extra WHEN 'auto_increment' THEN 1 ELSE 0 END as Is_Identity, NUMERIC_PRECISION as DataPrecision, NUMERIC_SCALE as DataScale " +
                        "FROM INFORMATION_SCHEMA.COLUMNS " +
                        "WHERE " + new WhereClause("TABLE_SCHEMA={0}", _dataManagerName).Where;
                //var dataManager = (DataManager)Activator.CreateInstance(typeof(DataManager));
                //return dataManager.Select<Columns>(new WhereClause("TABLE_SCHEMA={0}"), dataManagerName);
            }
            else
                throw new Exception("Data provider not available");

            return dataManager.Execute<Columns>(new SqlBuilder(sql));
        }

        private List<ForeignKeyConstraint> GetAllFKConstraints()
        {
            var dataManager = (DataManager)Activator.CreateInstance(typeof(DataManager));
            string sql;
            if (dataManager.DataProviderType == DataProviderType.Oracle)
            {
                sql = "SELECT a.table_name as FK_Table, a.column_name as FK_Column, a.constraint_name as Constraint_Name, " +
                      "c_pk.table_name PK_Table, r_col.column_name as PK_Column " +
                      "FROM all_cons_columns a " +
                      "JOIN all_constraints c ON a.owner = c.owner AND a.constraint_name = c.constraint_name " +
                      "JOIN all_constraints c_pk ON c.r_owner = c_pk.owner AND c.r_constraint_name = c_pk.constraint_name " +
                      "JOIN all_cons_columns r_col ON c_pk.constraint_name = r_col.constraint_name AND r_col.owner = c_pk.owner " +
                      "WHERE c.constraint_type = 'R' and a.owner = '{0}'".FormatWith(_dataManagerName);
            }
            else if (dataManager.DataProviderType == DataProviderType.SqlServer)
            {
                sql = "SELECT\n" +
                        "FK_Table = FK.TABLE_NAME,\n" +
                        "FK_Column = CU.COLUMN_NAME,\n" +
                        "PK_Table = PK.TABLE_NAME,\n" +
                        "PK_Column = PT.COLUMN_NAME,\n" +
                        "Constraint_Name = C.CONSTRAINT_NAME\n" +
                        "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C\n" +
                        "INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME\n" +
                        "INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME\n" +
                        "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME\n" +
                        "INNER JOIN (\n" +
                        "SELECT i1.TABLE_NAME, i2.COLUMN_NAME\n" +
                        "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1\n" +
                        "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME\n" +
                        "WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY'\n" +
                        ") PT ON PT.TABLE_NAME = PK.TABLE_NAME\n" +
                        "ORDER BY\n" +
                        "1,2,3,4";
            }
            else if (dataManager.DataProviderType == DataProviderType.MySql)
            {
                sql = "SELECT TABLE_NAME as FK_Table, COLUMN_NAME as FK_Column, CONSTRAINT_NAME as Constraint_Name, REFERENCED_TABLE_NAME as PK_Table, REFERENCED_COLUMN_NAME as PK_Column FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_SCHEMA='{0}' AND REFERENCED_TABLE_NAME IS NOT NULL AND REFERENCED_COLUMN_NAME IS NOT NULL".FormatWith(_dataManagerName);
            }
            else
                throw new Exception("Invalid Data Provider");

            return dataManager.Execute<ForeignKeyConstraint>(new SqlBuilder(sql));
        }

        private string GetTypeValue(string typeName)
        {
            if (string.Equals(typeName, "tinyint"))
                return "1";
            else if (string.Equals(typeName, "text"))
                return "''";
            else if (string.Equals(typeName, "smallint"))
                return "1";
            else if (string.Equals(typeName, "int"))
                return "1";
            else if (string.Equals(typeName, "decimal"))
                return "1";
            else if (string.Equals(typeName, "smalldatetime"))
                return "'1/1/2011'";
            else if (string.Equals(typeName, "datetime"))
                return "'1/1/2011'";
            else if (string.Equals(typeName, "float"))
                return "1";
            else if (string.Equals(typeName, "ntext"))
                return "'1'";
            else if (string.Equals(typeName, "bit"))
                return "1";
            else if (string.Equals(typeName, "numeric"))
                return "1";
            else if (string.Equals(typeName, "bigint"))
                return "1";
            else if (string.Equals(typeName, "varchar"))
                return "''";
            else if (string.Equals(typeName, "char"))
                return "''";
            else if (string.Equals(typeName, "nvarchar"))
                return "''";
            else if (string.Equals(typeName, "nchar"))
                return "''";
            else if (string.Equals(typeName, "sysname"))
                return "''";
            else if (string.Equals(typeName, "xml"))
                return "''";
            else if (string.Equals(typeName, "image"))
                return "''";
            else if (string.Equals(typeName, "money"))
                return "1";
            else if (string.Equals(typeName, "smallmoney"))
                return "1";
            else if (string.Equals(typeName, "varbinary"))
                return "1";
            else
                throw new Exception("Not support this column Data Type yet.");
        }
        private string GetTypeValueEmpty(string typeName)
        {
            if (string.Equals(typeName, "tinyint"))
                return "1";
            else if (string.Equals(typeName, "text"))
                return "''";
            else if (string.Equals(typeName, "smallint"))
                return "1";
            else if (string.Equals(typeName, "int"))
                return "1";
            else if (string.Equals(typeName, "decimal"))
                return "1";
            else if (string.Equals(typeName, "smalldatetime"))
                return "'1/1/2011'";
            else if (string.Equals(typeName, "datetime"))
                return "'1/1/2011'";
            else if (string.Equals(typeName, "float"))
                return "1";
            else if (string.Equals(typeName, "ntext"))
                return "''";
            else if (string.Equals(typeName, "bit"))
                return "1";
            else if (string.Equals(typeName, "numeric"))
                return "1";
            else if (string.Equals(typeName, "bigint"))
                return "1";
            else if (string.Equals(typeName, "varchar"))
                return "''";
            else if (string.Equals(typeName, "char"))
                return "''";
            else if (string.Equals(typeName, "nvarchar"))
                return "''";
            else if (string.Equals(typeName, "nchar"))
                return "''";
            else if (string.Equals(typeName, "sysname"))
                return "''";
            else if (string.Equals(typeName, "xml"))
                return "''";
            else if (string.Equals(typeName, "image"))
                return "''";
            else if (string.Equals(typeName, "money"))
                return "1";
            else if (string.Equals(typeName, "smallmoney"))
                return "1";
            else if (string.Equals(typeName, "varbinary"))
                return "1";
            else
                throw new Exception("Not support this column Data Type yet.");
        }

        private string GetTypeName(Type type)
        {
            if (type.IsGenericType)
            {
                if (typeof(Int16?).IsAssignableFrom(type))
                    return "short?";
                else if (typeof(Int32?).IsAssignableFrom(type))
                    return "int?";
                else if (typeof(Int64?).IsAssignableFrom(type))
                    return "long?";
                else if (typeof(double?).IsAssignableFrom(type))
                    return "double?";
                else if (typeof(float?).IsAssignableFrom(type))
                    return "float?";
                else if (typeof(decimal?).IsAssignableFrom(type))
                    return "decimal?";
                else if (typeof(DateTime?).IsAssignableFrom(type))
                    return "DateTime?";
                else if (typeof(byte?).IsAssignableFrom(type))
                    return "byte?";
                else if (typeof(Currency?).IsAssignableFrom(type))
                    return "Currency?";
            }

            return type.Name;
        }

        private List<ForeignKeyConstraint> _allFKConstraints;
        protected List<ForeignKeyConstraint> AllFKConstraints
        {
            get
            {
                if (_allFKConstraints == null)
                    _allFKConstraints = GetAllFKConstraints();
                return _allFKConstraints;
            }
        }
        private string GetTypeName(DataManager dataManager, string nameSpace, Columns column)//string tableName, string columnName, string typeName, int isNullable, int provision)
        {
            if (this.AllFKConstraints.Contains(null,
                new CustomEqualityComparer<ForeignKeyConstraint>((item1, item2) =>
                    string.Equals(item1.FK_Table, column.TableName) &&
                    string.Equals(item1.FK_Column, column.ColumnName))))
            {
                var item = _allFKConstraints
                    .Where(fk => string.Equals(fk.FK_Table, column.TableName) && string.Equals(fk.FK_Column, column.ColumnName))
                    .FirstOrDefault();
                if (item != null)
                {
                    var assemblyName = dataManager.DataProviderType == DataProviderType.MySql
                        ? item.PK_Table.ToLower()
                        : item.PK_Table.ToUpper();
                    assemblyName = string.Format("{0}.{1}, BLO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                        nameSpace, assemblyName);

                    var type = Type.GetType(assemblyName);
                    if (typeof(IEnumCode).IsAssignableFrom(type))
                    {
                        var folder = type.FullName.Split('.').First();
                        var assemblyNameEnum = string.Format("BLO.Objects.Enums.{0}.{1}, BLO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                            folder, item.PK_Table.ToUpper().Replace("E_", "Enum_").Replace("T_", "Enum_"));
                        var enumType = Type.GetType(assemblyNameEnum);
                        if (enumType != null)
                        {
                            if (column.IsNullable == 1)
                                return enumType.FullName + "?";
                            else
                                return enumType.FullName;
                        }
                    }
                }
            }

            var typeName = column.TypeName.ToLower();

            string convertedTypeName = "";
            if (string.Equals(typeName, "tinyint"))
                convertedTypeName = "byte";
            else if (string.Equals(typeName, "text"))
                convertedTypeName = "string";
            else if (string.Equals(typeName, "smallint"))
                convertedTypeName = "short";
            else if (string.Equals(typeName, "int"))
                convertedTypeName = "int";
            else if (string.Equals(typeName, "smalldatetime"))
                convertedTypeName = "DateTime";
            else if (string.Equals(typeName, "datetime"))
                convertedTypeName = "DateTime";
            else if (string.Equals(typeName, "date"))
                convertedTypeName = "DateTime";
            else if (string.Equals(typeName, "float"))
                convertedTypeName = "float";
            else if (string.Equals(typeName, "ntext"))
                convertedTypeName = "string";
            else if (string.Equals(typeName, "bit"))
                convertedTypeName = "bool";
            else if (string.Equals(typeName, "decimal"))
            {
                if (column.DataScale == 2)
                    convertedTypeName = "Currency";
                else
                    convertedTypeName = "decimal";
            }
            else if (string.Equals(typeName, "numeric"))
                convertedTypeName = "decimal";
            else if (string.Equals(typeName, "number"))
            {
                if (column.DataScale == 2)
                    convertedTypeName = "Currency";
                else if (!column.DataScale.HasValue || column.DataScale > 0)
                    convertedTypeName = "decimal";
                else
                {
                    if (column.DataPrecision <= 32)
                        convertedTypeName = "int";
                    else
                        convertedTypeName = "long";
                }
            }
            else if (string.Equals(typeName, "bigint"))
                convertedTypeName = "long";
            else if (string.Equals(typeName, "varchar"))
                convertedTypeName = "string";
            else if (string.Equals(typeName, "varchar2"))
                convertedTypeName = "string";
            else if (string.Equals(typeName, "char"))
                convertedTypeName = "string";
            else if (string.Equals(typeName, "nvarchar"))
                convertedTypeName = "string";
            else if (string.Equals(typeName, "nvarchar2"))
                convertedTypeName = "string";
            else if (string.Equals(typeName, "nchar"))
                convertedTypeName = "string";
            else if (string.Equals(typeName, "sysname"))
                convertedTypeName = "string";
            else if (string.Equals(typeName, "xml"))
                convertedTypeName = "string";
            else if (string.Equals(typeName, "image"))
                convertedTypeName = "";
            else if (string.Equals(typeName, "money"))
                convertedTypeName = "Currency";
            else if (string.Equals(typeName, "smallmoney"))
                convertedTypeName = "Currency";
            else if (string.Equals(typeName, "varbinary"))
                convertedTypeName = "byte[]";
            else if (string.Equals(typeName, "uniqueidentifier"))
                convertedTypeName = "Guid";
            else if (string.Equals(typeName, "clob"))
                convertedTypeName = "string";
            else if (string.Equals(typeName, "blob"))
                convertedTypeName = "byte[]";
            else if (string.Equals(typeName, "enum"))
                convertedTypeName = "string";
            else if (string.Equals(typeName, "longtext"))
                convertedTypeName = "string";
            else
                convertedTypeName = typeName;
            //throw new Exception("Not support this column Data Type yet.");

            if (column.IsNullable == 1)
            {
                if (string.Equals(convertedTypeName, "short") ||
                    string.Equals(convertedTypeName, "int") ||
                    string.Equals(convertedTypeName, "long") ||
                    string.Equals(convertedTypeName, "double") ||
                    string.Equals(convertedTypeName, "float") ||
                    string.Equals(convertedTypeName, "decimal") ||
                    string.Equals(convertedTypeName, "DateTime") ||
                    string.Equals(convertedTypeName, "byte") ||
                    string.Equals(convertedTypeName, "bool") ||
                    string.Equals(convertedTypeName, "Guid") ||
                    string.Equals(convertedTypeName, "Currency"))
                    convertedTypeName += "?";
            }

            return convertedTypeName;
        }

        private System.Data.DbType GetDBTypeName(string typeName)
        {
            if (string.Equals(typeName, "tinyint"))
                return System.Data.DbType.Byte;
            else if (string.Equals(typeName, "text"))
                return System.Data.DbType.String;
            else if (string.Equals(typeName, "smallint"))
                return System.Data.DbType.Int16;
            else if (string.Equals(typeName, "int"))
                return System.Data.DbType.Int32;
            else if (string.Equals(typeName, "smalldatetime"))
                return System.Data.DbType.DateTime;
            else if (string.Equals(typeName, "datetime"))
                return System.Data.DbType.DateTime;
            else if (string.Equals(typeName, "date"))
                return System.Data.DbType.Date;
            else if (string.Equals(typeName, "float"))
                return System.Data.DbType.Double;
            else if (string.Equals(typeName, "ntext"))
                return System.Data.DbType.AnsiString;
            else if (string.Equals(typeName, "bit"))
                return System.Data.DbType.Boolean;
            else if (string.Equals(typeName, "decimal"))
                return System.Data.DbType.Decimal;
            else if (string.Equals(typeName, "numeric"))
                return System.Data.DbType.Decimal;
            else if (string.Equals(typeName, "bigint"))
                return System.Data.DbType.Int64;
            else if (string.Equals(typeName, "varchar"))
                return System.Data.DbType.String;
            else if (string.Equals(typeName, "char"))
                return System.Data.DbType.String;
            else if (string.Equals(typeName, "nvarchar"))
                return System.Data.DbType.AnsiString;
            else if (string.Equals(typeName, "nchar"))
                return System.Data.DbType.AnsiString;
            else if (string.Equals(typeName, "sysname"))
                return System.Data.DbType.String;
            else if (string.Equals(typeName, "xml"))
                return System.Data.DbType.String;
            else if (string.Equals(typeName, "image"))
                return System.Data.DbType.Binary;
            else if (string.Equals(typeName, "money"))
                return System.Data.DbType.Currency;
            else if (string.Equals(typeName, "smallmoney"))
                return System.Data.DbType.Currency;
            else if (string.Equals(typeName, "varbinary"))
                return System.Data.DbType.Binary;
            else if (string.Equals(typeName, "uniqueidentifier"))
                return System.Data.DbType.Guid;
            else
                return System.Data.DbType.String;
        }
    }
}

namespace sys
{
    /// <summary>
    /// Summary description for Tables
    /// </summary>
    public class Tables : ITable
    {
        public Tables()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string NAME { get; set; }
        public object OBJECT_ID { get; set; }
        public object PRINCIPAL_ID { get; set; }
        public object SCHEMA_ID { get; set; }
        public object PARENT_OBJECT_ID { get; set; }
        public object TYPE { get; set; }
        public object TYPE_DESC { get; set; }
        public object CREATE_DATE { get; set; }
        public object MODIFY_DATE { get; set; }
        public object IS_MS_SHIPPED { get; set; }
        public object IS_PUBLISHED { get; set; }
        public object IS_SCHEMA_PUBLISHED { get; set; }
        public object LOB_DATA_SPACE_ID { get; set; }
        public object FILESTREAM_DATA_SPACE_ID { get; set; }
        public object MAX_COLUMN_ID_USED { get; set; }
        public object LOCK_ON_BULK_LOAD { get; set; }
        public object USES_ANSI_NULLS { get; set; }
        public object IS_REPLICATED { get; set; }
        public object HAS_REPLICATION_FILTER { get; set; }
        public object IS_MERGE_PUBLISHED { get; set; }
        public object IS_SYNC_TRAN_SUBSCRIBED { get; set; }
        public object HAS_UNCHECKED_ASSEMBLY_DATA { get; set; }
        public object TEXT_IN_ROW_LIMIT { get; set; }
        public object LARGE_VALUE_TYPES_OUT_OF_ROW { get; set; }
    }

    public class Columns : ITable
    {
        public string TableName { get; set; }
        private string _columnName;
        public string ColumnName
        {
            get { return _columnName; }
            set
            {
                //if (value.Contains("@"))
                //    _columnName = value.Replace("@", "");
                //else
                _columnName = value;
            }
        }
        public string TypeName { get; set; }
        public bool IsOutParam { get; set; }
        public object DefaultValue { get; set; }
        public int IsNullable { get; set; }
        public string TableType { get; set; }
        public bool Is_Identity { get; set; }
        public int? MaxLength { get; set; }
        public int? DataPrecision { get; set; }
        public int? DataScale { get; set; }
    }

    public class PrimaryKey : IView
    {
        public string TABLE_NAME { get; set; }
        public string COLUMN_NAME { get; set; }
    }

    public class Procedures : ITable
    {
        public string Name { get; set; }
    }

    public class ForeignKeyConstraint : ITable
    {
        public string FK_Table { get; set; }
        public string FK_Column { get; set; }
        public string PK_Table { get; set; }
        public string PK_Column { get; set; }
        public string Constraint_Name { get; set; }
    }
}