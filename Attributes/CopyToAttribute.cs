using System;

namespace DBO.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CopyToAttribute : Attribute
    {
        public bool IgnoreCopyTo { get; set; }

        public CopyToAttribute()
            : base()
        {
            this.IgnoreCopyTo = false;
        }
    }
}
