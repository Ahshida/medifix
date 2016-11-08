using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DBO.Data.Attributes;

namespace DBO.Data.Managers
{
    public static class CloneManager
    {
        public static T Clone<T>(this object source)
        {
            var newItem = Activator.CreateInstance(typeof(T));
            return source.CopyTo(newItem).ConvertTo<T>();
        }

        //public static object CopyTo<T>(this T source, object destination)
        public static TTarget CopyTo<TSource, TTarget>(this TSource source, TTarget destination, params string[] excludeProperties)
        {
            Type destinationType;
            if (destination is IList)
                destinationType = destination.GetType().GetGenericArguments().FirstOrDefault();
            else
                destinationType = destination.GetType();

            Type sourceType;
            if (source is IEnumerable)
                sourceType = source.GetType().GetGenericArguments().FirstOrDefault();
            else
                sourceType = source.GetType();

            var sourceProperties = sourceType.GetProperties();
            var destinationProperties = destinationType.GetProperties();

            Dictionary<PropertyInfo, PropertyInfo> properties = new Dictionary<PropertyInfo, PropertyInfo>();
            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destinationProperty in destinationProperties)
                {
                    if (excludeProperties != null && excludeProperties.Contains(sourceProperty.Name))
                        continue;

                    if (string.Equals(sourceProperty.Name.ToUpper(), destinationProperty.Name.ToUpper()))
                    {
                        var copyTo = destinationProperty.GetCustomAttributes(typeof(CopyToAttribute), true).OfType<CopyToAttribute>().FirstOrDefault();
                        if (copyTo == null || !copyTo.IgnoreCopyTo)
                            properties.Add(sourceProperty, destinationProperty);
                        break;
                    }
                }
            }

            //if (source == null)
            //{
            //    return destination;
            //}
            //else
            {
                if (destination is IList)
                {
                    foreach (var item in (source as IEnumerable))
                        (destination as IList).Add(item.CopyTo(Activator.CreateInstance(destinationType), properties));
                    return destination;
                }
                else
                    return source.CopyTo(destination, properties);
            }
        }

        private static TTarget CopyTo<TSource, TTarget>(this TSource source, TTarget destination, Dictionary<PropertyInfo, PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                if (!property.Value.CanWrite)
                    continue;

                try
                {
                    if (typeof(Enum).IsAssignableFrom(property.Value.PropertyType) &&
                        typeof(String).IsAssignableFrom(property.Key.PropertyType))
                    {
                        var value = property.Key.GetValue(source, null);
                        if (string.Equals(value, ""))
                            value = null;
                        property.Value.SetValue(destination, Convert.ToInt32(value), null);
                    }
                    else if (property.Key.PropertyType.GetConstructor(new Type[] { }) == null)
                        property.Value.SetValue(destination, property.Key.GetValue(source, null), null);
                    else
                    {
                        var sourceValue = property.Key.GetValue(source, null);
                        if (sourceValue == null)
                            property.Value.SetValue(destination, null, null);
                        else
                            property.Value.SetValue(
                                destination,
                                sourceValue.CopyTo(Activator.CreateInstance(property.Value.PropertyType)),
                                null);
                    }
                }
                catch (Exception ex)
                {
                    LogWriter.WriteDBServerLog(string.Format("The property '{0}' is not able to convert.", property.Key.Name));
                    throw ex;
                }
            }

            return destination;
        }
    }
}