using System;

namespace DBO.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HasDefaultAttribute : Attribute
    {
        public string DefaultValue { get; private set; }
        public HasDefaultAttribute(string defaultValue)
            : base()
        {
            this.DefaultValue = defaultValue;
        }
    }
}
