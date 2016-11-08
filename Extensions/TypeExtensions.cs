using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DBO.Data.Attributes;
using DBO.Data.Managers;
using DBO.Data.Objects;
using System.ComponentModel.DataAnnotations;

namespace DBO.Data
{
    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type type)
        {
            object item = null;
            if (type.IsValueType)
                item = Activator.CreateInstance(type);
            else
            {
                var constructor = type.GetConstructors().FirstOrDefault();
                if (constructor == null)
                    return item;

                var parameterInfos = constructor.GetParameters();
                if (parameterInfos.Length > 0)
                {
                    List<object> parameters = new List<object>();
                    foreach (var parameterInfo in parameterInfos)
                        parameters.Add(parameterInfo.ParameterType.GetDefaultValue());

                    try
                    {
                        item = Activator.CreateInstance(type, parameters.ToArray());
                    }
                    catch { }
                }
                else
                    item = Activator.CreateInstance(type);
            }

            return item;
        }

        public static bool IsForeignKey(this PropertyInfo propertyInfo)
        {
            var foreignKeys = propertyInfo.DeclaringType.GetCustomAttributes(typeof(ForeignKeyAttribute), true).OfType<ForeignKeyAttribute>();
            var foreignKey = foreignKeys.Where(f => string.Equals(f.ForeignColumn, propertyInfo.Name)).FirstOrDefault();
            return foreignKey != null;
        }
        public static bool IsNumeric(this Type type)
        {
            if (typeof(int?).IsAssignableFrom(type) ||
                typeof(long?).IsAssignableFrom(type) ||
                typeof(short?).IsAssignableFrom(type) ||
                typeof(uint?).IsAssignableFrom(type) ||
                typeof(ulong?).IsAssignableFrom(type) ||
                typeof(ushort?).IsAssignableFrom(type) ||
                typeof(double?).IsAssignableFrom(type) ||
                typeof(float?).IsAssignableFrom(type) ||
                typeof(decimal?).IsAssignableFrom(type))
                return true;
            else
                return false;
        }
        public static bool IsInteger(this Type type)
        {
            if (typeof(int?).IsAssignableFrom(type) ||
                typeof(long?).IsAssignableFrom(type) ||
                typeof(short?).IsAssignableFrom(type) ||
                typeof(uint?).IsAssignableFrom(type) ||
                typeof(ulong?).IsAssignableFrom(type) ||
                typeof(ushort?).IsAssignableFrom(type))
                return true;
            else
                return false;
        }
        public static bool IsCurrency(this Type type)
        {
            return typeof(Currency?).IsAssignableFrom(type);
        }
        public static bool IsDate(this PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.IsDateTime())
            {
                var attr = propertyInfo.GetCustomAttribute<DateOnlyAttribute>(true);
                if (attr != null && attr.ExcludeTime)
                    return true;
                var attr2 = propertyInfo.GetCustomAttribute<DateIncludeTimeAttribute>(true);
                if (attr2 == null || !attr2.IncludeTime)
                    return true;
            }
            return false;
        }
        public static bool IsDateTime(this Type type)
        {
            if (typeof(DateTime?).IsAssignableFrom(type))
                return true;
            else
                return false;
        }
        public static bool IsBooleon(this Type type)
        {
            return typeof(bool?).IsAssignableFrom(type);
        }
        public static bool IsEnum(this Type type)
        {
            if (type.IsEnum)
                return true;
            if (type.IsGenericType)
            {
                var args = type.GetGenericArguments();
                if (args.Length == 1)
                {
                    var enumType = args.First();
                    if (enumType != null && enumType.IsEnum && typeof(Nullable<>).MakeGenericType(enumType) == type)
                        return true;
                }
            }

            return false;
        }
        public static bool IsNullable(this Type type)
        {
            if (type.IsClass)
                return true;

            if (type.IsGenericType)
            {
                var args = type.GetGenericArguments();
                if (args.Length == 1)
                {
                    var childType = args.First();
                    if (childType != null && typeof(Nullable<>).MakeGenericType(childType) == type)
                        return true;
                }
            }

            return false;
        }

        public static bool IsNumeric(this DataType type)
        {
            return type == DataType.Duration;
        }
        public static bool IsDate(this DataType type)
        {
            return type == DataType.Date;
        }
        public static bool IsDateTime(this DataType type)
        {
            return type == DataType.DateTime || type == DataType.Time;
        }
        public static bool IsCurrency(this DataType type)
        {
            return type == DataType.Currency;
        }

        public static uint GetMaxLength(this PropertyInfo propertyInfo)
        {
            var maxLength = propertyInfo.GetCustomAttribute<MaxLengthAttribute>(true);
            if (maxLength != null)
                return maxLength.Length;
            return 0;
        }
    }
}
