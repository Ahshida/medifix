using System;

namespace DBO.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HiddenAttribute : Attribute
    {
        public bool IsHidden { get; private set; }
        public HiddenAttribute(bool hidden)
        {
            this.IsHidden = hidden;
        }
        public HiddenAttribute() : this(true) { }
    }
}
