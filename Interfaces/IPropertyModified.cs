using System.Reflection;

namespace DBO.Data.Interfaces
{
    public interface IPropertyModified
    {
        void MarkAsDefault();
        PropertyInfo[] GetModifiedProperties();
    }
}
