using System;

namespace DBO.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ForeignKeyAttribute : Attribute
    {
        public string ForeignColumn { get; private set; }
        public Type PrimaryTable { get; private set; }
        public string PrimaryColumn { get; private set; }
        public ForeignKeyAttribute(string foreignColumn, Type primaryTable, string primaryColumn)
            : base()
        {
            this.ForeignColumn = foreignColumn;
            this.PrimaryTable = primaryTable;
            this.PrimaryColumn = primaryColumn;
        }
    }
}
