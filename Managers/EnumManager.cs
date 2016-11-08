using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using DBO.Data.Objects;

namespace DBO.Data.Managers
{
    public static class EnumManager
    {
        public static string GetDescription(this Enum value, bool showValue)
        {
            if (showValue)
                return string.Format("{0} ({1})", value.GetDescription(), value.ConvertTo<int>());
            else
                return value.GetDescription();
        }
        public static string GetDescription(this Enum value)
        {
            if (value == null)
                return null;

            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(value.ToString());
            if (fieldInfo != null)
            {
                var descriptionAttribute = fieldInfo
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .OfType<DescriptionAttribute>()
                    .FirstOrDefault();

                if (descriptionAttribute != null)
                    return descriptionAttribute.Description;

                return fieldInfo.Name;
            }

            return "";
        }
        public static string GetDescription(this Enum value, Type resourceType)
        {
            if (resourceType != null)
            {
                PropertyInfo property = resourceType.GetProperty(value.ConvertTo<string>());
                if (property != null)
                    return property.GetValue(null, null).ConvertTo<string>();
            }

            return value.GetDescription();
        }

        public static List<CommonDataBoundItem> ConvertToDataBoundItems(this Type itemType)
        {
            var list = new List<CommonDataBoundItem>();
            foreach (var item in itemType.GetFields().Where(field => itemType.IsAssignableFrom(field.FieldType)))
            {
                string description;
                var descriptionAtt = item.GetCustomAttributes(typeof(DescriptionAttribute), true).OfType<DescriptionAttribute>().FirstOrDefault();
                if (descriptionAtt == null)
                    description = item.Name;
                else
                    description = descriptionAtt.Description;

                list.Add(new CommonDataBoundItem(item.GetValue(null).ConvertTo<int>(), description));
            }

            return list;
        }

        public static bool In<T>(this T item, params T[] array)
        {
            foreach (var arrItem in array)
            {
                if (arrItem.Equals(item))
                    return true;
            }
            return false;
        }

        public static SelectList GetSelectList(this Type type)
        {
            return GetSelectList(type, null);
        }
        public static SelectList GetSelectList(this Type type, object value)
        {
            var isNullable = type.IsNullable();
            if (isNullable)
                type = type.GetGenericArguments().First();
            var result = new List<SelectListItem>();
            if (value != null && value.GetType().IsEnum())
                return new SelectList(from Enum e in Enum.GetValues(type)
                                      //where isNullable || e.ConvertTo<byte>() != 0
                                      where e.ConvertTo<byte>() != 0
                                      select new { ID = e, Name = e.GetDescription() }, "ID", "Name", value);
            else
                return new SelectList(from Enum e in Enum.GetValues(type)
                                      //where isNullable || e.ConvertTo<byte>() != 0
                                      where e.ConvertTo<byte>() != 0
                                      select new { ID = e.ConvertTo<byte>(), Name = e.GetDescription() }, "ID", "Name", value);
        }
    }
}
