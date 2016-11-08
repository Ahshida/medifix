using System;

namespace DBO.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PrimaryKeyAttribute : Attribute
    {
        public bool IsPrimaryKey { get; private set; }
        public bool IsIdentity { get; private set; }
        public PrimaryKeyAttribute(bool isPrimaryKey, bool isIdentity)
            : base()
        {
            this.IsPrimaryKey = isPrimaryKey;
            this.IsIdentity = isIdentity;
        }
        public PrimaryKeyAttribute(bool isPrimaryKey)
            : this(isPrimaryKey, true)
        {
        }
        public PrimaryKeyAttribute()
            : this(true)
        {
        }
    }
}
