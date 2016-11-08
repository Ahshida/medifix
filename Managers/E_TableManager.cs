using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web.Mvc;
using DBO.Data.Attributes;
using DBO.Data.Interfaces;

namespace DBO.Data.Managers
{
    public static class E_TableManagerExtension
    {
        public static SelectListItem ToSelectListItem<T>(this byte id)
            where T : BaseClass, IName
        {
            return E_TableManager.GetSelectListItem<T>(id);
        }
        public static IEnumerable<SelectListItem> GetSelectList(this PropertyInfo propertyInfo)
        {
            return GetSelectList(propertyInfo, null);
        }
        public static IEnumerable<SelectListItem> GetSelectList(this PropertyInfo propertyInfo, object value)
        {
            var foreignKey = propertyInfo.DeclaringType
                .GetCustomAttributes(typeof(ForeignKeyAttribute), true)
                .Where(f => string.Equals((f as ForeignKeyAttribute).ForeignColumn, propertyInfo.Name))
                .FirstOrDefault();
            if (foreignKey != null)
            {
                if (propertyInfo.PropertyType.IsEnum ||
                    (propertyInfo.PropertyType.IsNullable() && propertyInfo.PropertyType.GetGenericArguments().First().IsEnum))
                    value = value.ConvertTo<byte?>().ConvertTo();
                //{
                //    var method = typeof(E_TableManager).GetMethod("GetList", new Type[] { typeof(int[]) }).MakeGenericMethod((foreignKey as ForeignKeyAttribute).PrimaryTable);
                //    var items = method.Invoke(null, new object[] { null }) as IEnumerable;
                //    return new SelectList(items, "Code", "Name");
                //}
                if (typeof(IName).IsAssignableFrom((foreignKey as ForeignKeyAttribute).PrimaryTable))
                {
                    var method = typeof(E_TableManager).GetMethod("GetSelectList", new Type[] { typeof(string), typeof(int[]) }).MakeGenericMethod((foreignKey as ForeignKeyAttribute).PrimaryTable);
                    return method.Invoke(null, new object[] { value.ConvertTo(), null }) as List<SelectListItem>;
                }
            }
            return null;
        }
    }
    public class E_TableManager
    {
        private static Dictionary<Type, IEnumerable> _cachedSelectList = new Dictionary<Type, IEnumerable>();
        public static void ResetCachedItems()
        {
            _cachedSelectList = new Dictionary<Type, IEnumerable>();
        }
        public static void ResetCachedItem(Type type)
        {
            _cachedSelectList.Remove(type);
        }
        public static void ResetCachedItem<T>()
        {
            _cachedSelectList.Remove(typeof(T));
        }

        public static SelectListItem GetSelectListItem<T>(int id)
            where T : BaseClass, IName
        {
            return GetSelectList<T>("", id).FirstOrDefault();
        }
        public static SelectListItem GetSelectListItem<T>(Enum id)
            where T : BaseClass, IName
        {
            return GetSelectListItem<T>(id.ConvertTo<int>());
        }

        public static List<SelectListItem> GetSelectListByEnum<T>(params Enum[] id)
            where T : BaseClass, IName
        {
            return GetSelectList<T>(id == null ? null : id.Select(item => item.ConvertTo<int>()).ToArray());
        }
        public static List<SelectListItem> GetSelectListByEnum<T>(bool encryptId, params Enum[] id)
            where T : BaseClass, IName
        {
            return GetSelectList<T>(encryptId, id == null ? null : id.Select(item => item.ConvertTo<int>()).ToArray());
        }
        public static List<SelectListItem> GetSelectListByEnum<T>(string selectedValue, params Enum[] id)
            where T : BaseClass, IName
        {
            return GetSelectList<T>(selectedValue, id == null ? null : id.Select(item => item.ConvertTo<int>()).ToArray());
        }
        public static List<SelectListItem> GetSelectListByEnum<T>(bool encryptId, string selectedValue, params Enum[] id)
            where T : BaseClass, IName
        {
            return GetSelectList<T>(encryptId, selectedValue, id == null ? null : id.Select(item => item.ConvertTo<int>()).ToArray());
        }

        public static string GetJQGridSelect<T>(params int[] id)
            where T : BaseClass, IName
        {
            return string.Join(";", GetSelectList<T>(id).Select(item => string.Format("{0}:{1}", item.Value, item.Text)));
        }
        public static string GetJQGridSelect<T>(Func<T, SelectListItem> func, params int[] id)
            where T : BaseClass, ITable
        {
            return string.Join(";", GetList<T>(id).Select(item => func(item)).Select(item => string.Format("{0}:{1}", item.Value, item.Text)));
        }

        public static List<SelectListItem> GetSelectList<T>(params int[] id)
            where T : BaseClass, IName
        {
            return GetSelectList<T>("", id);
        }
        public static List<SelectListItem> GetSelectList<T>(bool encryptId, params int[] id)
            where T : BaseClass, IName
        {
            return GetSelectList<T>(encryptId, "", id);
        }
        public static List<SelectListItem> GetSelectList<T>(string selectedValue, params int[] id)
            where T : BaseClass, IName
        {
            return GetSelectList<T>(false, selectedValue, id);
        }
        public static List<SelectListItem> GetSelectList<T>(bool encryptId, string selectedValue, params int[] id)
            where T : BaseClass, IName
        {
            var items = GetList<T>(id);

            List<SelectListItem> result = null;
            var idProperty = typeof(T).GetProperty("ID");
            if (!string.Equals("en", Thread.CurrentThread.CurrentCulture.Name))
            {
                var nameProperty = typeof(T).GetProperty("Name_" + Thread.CurrentThread.CurrentCulture.Name);
                if (nameProperty != null)
                {
                    result = items.SelectInNativeMode(item => new SelectListItem()
                    {
                        Value = idProperty.GetValue(item, null).ToString(),
                        Text = nameProperty.GetValue(item, null).ConvertTo<string>()
                    }).ToList();
                }
            }

            if (result == null)
            {
                result = items.SelectInNativeMode(item => new SelectListItem()
                {
                    Value = encryptId ? idProperty.GetValue(item, null).Encrypt() : idProperty.GetValue(item, null).ToString(),
                    Text = item.Name
                }).ToList();
            }

            if (!string.IsNullOrEmpty(selectedValue))
                foreach (var item in result)
                    if (string.Equals(item.Value, selectedValue))
                    {
                        item.Selected = true;
                        return result;
                    }

            return result;
        }

        public static T GetListItem<T>(Enum id)
            where T : BaseClass, IName
        {
            return GetListItem<T>(id.ConvertTo<int>());
        }
        public static T GetListItem<T>(int id)
            where T : BaseClass, IName
        {
            var items = GetList<T>(id);

            var idProperty = typeof(T).GetProperty("ID");
            if (id > 0)
                return items.Where(item => id == idProperty.GetValue(item, null).ConvertTo<int>()).FirstOrDefault();
            else
                return default(T);
        }

        public static IEnumerable<T> GetListByEnum<T>(params Enum[] id)
            where T : BaseClass
        {
            return GetList<T>(id == null ? null : id.Select(item => item.ConvertTo<int>()).ToArray());
        }
        public static IEnumerable<T> GetList<T>(params int[] id)
            where T : BaseClass
        {
            IEnumerable<T> items;
            if (!_cachedSelectList.ContainsKey(typeof(T)))
                _cachedSelectList[typeof(T)] = GetDynamicList<T>();
            items = _cachedSelectList[typeof(T)] as IEnumerable<T>;

            var idProperty = typeof(T).GetProperty("ID");
            if (id != null && id.Length > 0)
                items = items.Where(item => id.Contains(idProperty.GetValue(item, null).ConvertTo<int>()));

            return items;
        }

        public static IEnumerable<T> GetDynamicList<T>()
            where T : BaseClass, ITable
        {
            IEnumerable<T> items;
            WhereClause where;
            if (typeof(IName).IsAssignableFrom(typeof(T)))
                where = new WhereClause() { OrderBy = "Name" };
            else if (typeof(IEnumCode).IsAssignableFrom(typeof(T)))
                where = new WhereClause() { OrderBy = "Code" };
            else
                where = new WhereClause();

            items = DBOExtension.Select<T>(where);

            return items;
        }
    }
}