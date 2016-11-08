using System.Collections.Generic;
using System.Linq;
using System;

namespace DBO.Data.Objects
{
    public class DataSource
    {
        public string Key { get; set; }
        public string Source { get; set; }
        public List<ColumnInfo> ColumnInfos { get; set; }
        //public List<string> ColumnNames { get; set; }
        public List<Row> Rows { get; set; }
        public string ExceptionMessage { get; set; }

        public DataSource() { }
        public DataSource(string key, string sql)
        {
            this.Key = key;
            this.Source = GetSource(sql);
            //this.ColumnNames = new List<string>();
            this.Rows = new List<Row>();
            this.ColumnInfos = new List<ColumnInfo>();
        }

        private string GetSource(string sql)
        {
            var source = sql.Trim();
            if (source.ToUpper().StartsWith("SELECT "))
            {
                if (source.ToUpper().IndexOf(" FROM ") >= 0)
                    source = source.Substring(source.ToUpper().IndexOf(" FROM ") + " FROM ".Length).Trim();
                if (source.ToUpper().IndexOf(" WHERE ") >= 0)
                    source = source.Substring(0, source.ToUpper().IndexOf(" WHERE ")).Trim();
                if (source.ToUpper().IndexOf(" GROUP BY ") >= 0)
                    source = source.Substring(0, source.ToUpper().IndexOf(" GROUP BY ")).Trim();
                if (source.ToUpper().IndexOf(" ORDER BY ") >= 0)
                    source = source.Substring(0, source.ToUpper().IndexOf(" ORDER BY ")).Trim();
            }
            else if (source.ToUpper().StartsWith("EXECUTE "))
            {
                source = source
                    .Substring("EXECUTE ".Length)
                    .Trim()
                    .Split(' ')
                    .FirstOrDefault();
            }

            return source;
        }
    }

    public class Row
    {
        public List<object> Columns { get; set; }
        public Row()
        {
            this.Columns = new List<object>();
        }
    }

    public class ColumnInfo
    {
        public string Name { get; set; }
        public Type DataType { get; set; }
        public bool IsNullable { get; set; }
    }
}
