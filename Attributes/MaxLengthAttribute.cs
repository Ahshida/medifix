using System;

namespace DBO.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MaxLengthAttribute : Attribute
    {
        public uint Length { get; private set; }
        public MaxLengthAttribute(uint length)
            : base()
        {
            this.Length = length;
        }
    }
}
