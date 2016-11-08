using System;

namespace DBO.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DatabaseIgnoreAttribute : Attribute
    {
        public bool Ignore { get; private set; }

        public DatabaseIgnoreAttribute() : this(true) { }
        public DatabaseIgnoreAttribute(bool ignore)
            : base()
        {
            this.Ignore = ignore;
        }
    }
}
