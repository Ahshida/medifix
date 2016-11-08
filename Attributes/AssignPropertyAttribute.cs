using System;
using System.Collections.Generic;
using System.Reflection;

namespace DBO.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public class AssignPropertyAttribute : Attribute
    {
        public string PropertyName { get; private set; }
        public Attribute Attribute { get; private set; }

        public string Parameter1 { get; set; }
        public object Parameter1Value
        {
            get { return null; }
            set { SetAttributeNamedProperty(Parameter1, value); }
        }

        public string Parameter2 { get; set; }
        public object Parameter2Value
        {
            get { return null; }
            set { SetAttributeNamedProperty(Parameter2, value); }
        }

        public string Parameter3 { get; set; }
        public object Parameter3Value
        {
            get { return null; }
            set { SetAttributeNamedProperty(Parameter3, value); }
        }

        public string Parameter4 { get; set; }
        public object Parameter4Value
        {
            get { return null; }
            set { SetAttributeNamedProperty(Parameter4, value); }
        }

        public object ParsingData1 { get { return null; } set { SetAttributeParameters(value); } }
        public object ParsingData2 { get { return null; } set { SetAttributeParameters(value); } }
        public object ParsingData3 { get { return null; } set { SetAttributeParameters(value); } }
        public object ParsingData4 { get { return null; } set { SetAttributeParameters(value); } }
        public object ParsingData5 { get { return null; } set { SetAttributeParameters(value); } }

        private List<object> parameters = new List<object>();
        private void SetAttributeParameters(object parameter)
        {
            var parametersProperty = this.Attribute.GetType().GetProperty("Parameters");
            if (parametersProperty != null)
            {
                parameters.Add(parameter);
                parametersProperty.SetValue(this.Attribute, parameters.ToArray(), null);
            }
        }

        private void SetAttributeNamedProperty(string propertyName, object propertyValue)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo propertyInfo = this.Attribute.GetType().GetProperty(propertyName);
                if (propertyInfo != null)
                    propertyInfo.SetValue(this.Attribute, propertyValue, null);
            }
        }

        public AssignPropertyAttribute(string propertyName, Type attributeType, params object[] attributeParameters)
        {
            if (!typeof(Attribute).IsAssignableFrom(attributeType))
                throw new InvalidCastException(string.Format("AttributeType must be assignable to [{0}].", typeof(Attribute).FullName));

            this.PropertyName = propertyName;
            if (attributeParameters == null || attributeParameters.Length == 0)
                this.Attribute = (Attribute)Activator.CreateInstance(attributeType);
            else
                this.Attribute = (Attribute)Activator.CreateInstance(attributeType, attributeParameters);
        }
    }
}
