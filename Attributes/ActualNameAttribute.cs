using System;

namespace DBO.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ActualNameAttribute : Attribute
    {
        public string Name { get; private set; }
        public ActualNameAttribute(string name)
            : base()
        {
            this.Name = name;
        }
    }
}
