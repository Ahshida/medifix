using System;

namespace HTMLReportCreator.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RDLCDatasourceAttribute : Attribute
    {
        public string Name { get; private set; }
        public RDLCDatasourceAttribute(string name)
            : base()
        {
            this.Name = name;
        }
    }
}
