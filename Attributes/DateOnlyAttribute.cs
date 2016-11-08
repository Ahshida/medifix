using System;

namespace DBO.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateOnlyAttribute : Attribute
    {
        public bool ExcludeTime { get; private set; }
        public DateOnlyAttribute(bool excludeTime)
            : base()
        {
            this.ExcludeTime = excludeTime;
        }
        public DateOnlyAttribute()
            : this(true)
        {
        }
    }
}
