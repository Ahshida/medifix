using System;
using System.Collections;
using System.Collections.Generic;

namespace DBO.Data.Managers
{
    public static class DBOExtension
    {
        public static T SelectSingle<T>(WhereClause where)
        {
            return SelectSingle(typeof(T), where).ConvertTo<T>();
        }
        public static object SelectSingle(this Type type, WhereClause where)
        {
            var method = type.BaseType.BaseType.GetMethod("SelectSingle", new Type[] { typeof(WhereClause) });
            method = method.MakeGenericMethod(type);
            return method.Invoke(null, new object[] { where });
        }
        public static List<T> Select<T>(WhereClause where)
        {
            return Select(typeof(T), where) as List<T>;
        }
        public static IEnumerable Select(this Type type, WhereClause where)
        {
            var method = type.BaseType.BaseType.GetMethod("Select", new Type[] { typeof(WhereClause) });
            return method.Invoke(null, new object[] { where }) as IEnumerable;
        }

        public static T SelectExact<T>(object id)
        {
            return SelectExact(typeof(T), id).ConvertTo<T>();
        }
        public static object SelectExact(this Type type, object id)
        {
            var method = type.GetMethod("SelectExact");
            return method.Invoke(null, new object[] { id });
        }
        public static DataManager GetDataManager(this Type type)
        {
            var method = type.GetMethod("GetDataManager");
            return method.Invoke(null, new object[] { }) as DataManager;
        }
    }
}
