using System;
using System.Reflection;

namespace DBO.Data.Managers
{
    public static class PropertyInfoManager
    {
        public static T GetAttribute<T>(this PropertyInfo property) where T : Attribute
        {
            return property.GetCustomAttribute<T>(true);
        }
        public static T GetAttribute<T>(this MemberInfo property) where T : Attribute
        {
            return (property as PropertyInfo).GetAttribute<T>();
        }
    }
}
