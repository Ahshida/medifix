using System;
using System.Collections.Generic;

namespace DBO.Data.Objects
{
    /// <summary>
    /// Summary description for SqlReference
    /// </summary>
    public class SqlReference : ISqlReference
    {
        public SqlReference()
        {
            this.Guid = Guid.NewGuid();
            this.SqlRelationShips = new Dictionary<string, Guid>();
        }

        public Guid Guid { get; private set; }
        public string Sql { get; set; }
        public Dictionary<string, Guid> SqlRelationShips { get; private set; }
        public object ReturnValue { get; set; }
        public Type TableType { get; set; }
    }

}