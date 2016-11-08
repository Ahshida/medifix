using System;

namespace DBO.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateIncludeTimeAttribute : Attribute
    {
        public bool IncludeTime { get; private set; }
        public DateIncludeTimeAttribute(bool includeTime)
            : base()
        {
            this.IncludeTime = includeTime;
        }
        public DateIncludeTimeAttribute()
            : this(true)
        {
        }
    }
}
