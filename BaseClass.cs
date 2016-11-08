using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using DBO.Data.Attributes;
using DBO.Data.Interfaces;

namespace DBO.Data
{
    public class BaseClass : ITable, IModified, IPropertyModified
    {
        [CopyTo(IgnoreCopyTo = true)]
        public bool IsModified
        {
            get;
            set;
        }

        public BaseClass()
        {
            MarkAsDefault();
        }

        #region interface IPropertyModified

        private object _default;
        public void MarkAsDefault()
        {
            this.IsModified = false;
            _default = this.MemberwiseClone();
        }

        public PropertyInfo[] GetModifiedProperties()
        {
            return this.GetModifiedProperties(_default);
        }

        #endregion

        public virtual void BeforeInsert()
        {
            if (this.IsModified)
            {
                if (this is ICreatedDate)
                    (this as ICreatedDate).CreatedDate = DateTime.Now;
                if (this is IModifiedDate)
                    (this as IModifiedDate).ModifiedDate = DateTime.Now;
            }
        }
        public virtual void BeforeUpdate()
        {
            if (this.IsModified)
            {
                if (this is IModifiedDate)
                    (this as IModifiedDate).ModifiedDate = DateTime.Now;
            }
        }
    }

    public class BaseClass<DataManager, T> : BaseClass
        where T : ITable
        where DataManager : DBO.Data.DataManager
    {
        public static DataManager GetDataManager()
        {
            return Activator.CreateInstance<DataManager>();
        }
        public static T SelectSingle(Expression<Func<T, object>> selectedColumns, WhereClause whereClause)
        {
            return GetDataManager().SelectSingle<T>(selectedColumns, whereClause);
        }
        public static T SelectSingle(WhereClause whereClause)
        {
            return GetDataManager().SelectSingle<T>(whereClause);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Generic type to create a List to return.</typeparam>
        /// <returns>
        /// A System.Collections.Generic.List whose elements are the result
        /// of invoking the transform function on each element of source.
        /// </returns>
        public static List<T> Select()
        {
            return GetDataManager().Select<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Generic type to create a List to return.</typeparam>
        /// <param name="whereClause">Where, Top and OrderBy is assign here.</param>
        /// <param name="values">Value which will replace in string.Format {0}, {1}, {2}...</param>
        /// <returns>
        /// A System.Collections.Generic.List whose elements are the result
        /// of invoking the transform function on each element of source.
        /// </returns>
        public static List<T> Select(WhereClause whereClause)
        {
            return GetDataManager().Select<T>(whereClause);
        }

        public static List<T> Select(Expression<Func<T, object>> selectedColumns)
        {
            return GetDataManager().Select<T>(selectedColumns);
        }
        public static List<T> Select(Expression<Func<T, object>> selectedColumns, bool distinctByColumns)
        {
            return GetDataManager().Select<T>(selectedColumns, distinctByColumns);
        }
        public static List<T> Select(Expression<Func<T, object>> selectedColumns, WhereClause whereClause)
        {
            return GetDataManager().Select<T>(selectedColumns, whereClause);
        }
        public static List<T> Select(Expression<Func<T, object>> selectedColumns, bool distinctByColumns, WhereClause whereClause)
        {
            return GetDataManager().Select<T>(selectedColumns, distinctByColumns, whereClause);
        }
        public static long SelectCount()
        {
            return GetDataManager().SelectCount<T>();
        }
        public static long SelectCount(WhereClause whereClause)
        {
            return GetDataManager().SelectCount<T>(whereClause);
        }
    }
}
