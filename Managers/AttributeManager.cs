using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DBO.Data.Attributes;

namespace DBO.Data.Managers
{
    public static class AttributeManager
    {
        private static readonly Object lockobj = new Object();
        public static Dictionary<Type, Dictionary<PropertyInfo, IEnumerable<Attribute>>> AttributeCollections { get; private set; }

        public static T GetCustomAttribute<T>(this PropertyInfo propertyInfo, bool isInherits) where T : Attribute
        {
            return GetCustomAttributes(propertyInfo, true).OfType<T>().LastOrDefault();
        }
        public static IEnumerable<Attribute> GetCustomAttributes(PropertyInfo propertyInfo, bool isInherits)
        {
            if (AttributeCollections == null)
                AttributeCollections = new Dictionary<Type, Dictionary<PropertyInfo, IEnumerable<Attribute>>>();

            Dictionary<PropertyInfo, IEnumerable<Attribute>> propertyAttributeCollection = null;
            if (AttributeCollections.ContainsKey(propertyInfo.DeclaringType))
            {
                propertyAttributeCollection = AttributeCollections[propertyInfo.DeclaringType];
                if (propertyAttributeCollection.ContainsKey(propertyInfo))
                    return propertyAttributeCollection[propertyInfo];
            }

            lock (lockobj)
            {
                if (AttributeCollections.ContainsKey(propertyInfo.DeclaringType))
                {
                    propertyAttributeCollection = AttributeCollections[propertyInfo.DeclaringType];
                    if (propertyAttributeCollection.ContainsKey(propertyInfo))
                        return propertyAttributeCollection[propertyInfo];
                }

                if (propertyAttributeCollection == null)
                {
                    propertyAttributeCollection = new Dictionary<PropertyInfo, IEnumerable<Attribute>>();
                    if (!AttributeCollections.ContainsKey(propertyInfo.DeclaringType))
                        AttributeCollections.Add(propertyInfo.DeclaringType, propertyAttributeCollection);
                }

                List<Attribute> customAttributes = new List<Attribute>(propertyInfo
                    .GetCustomAttributes(isInherits)
                    .OfType<Attribute>());
                customAttributes.AddRange(GetCustomAttributes(propertyInfo.DeclaringType, propertyInfo.Name, isInherits));

                propertyAttributeCollection.Add(propertyInfo, customAttributes);

                return customAttributes;
            }
        }

        public static T GetCustomAttribute<T>(this Type type, string propertyName, bool isInherits) where T : Attribute
        {
            return GetCustomAttributes(type, propertyName, true).OfType<T>().LastOrDefault();
        }
        public static IEnumerable<Attribute> GetCustomAttributes(Type type, string propertyName, bool isInherits)
        {
            var customAttributes = new List<Attribute>(type.GetCustomAttributes(isInherits)
                .Where(o => o is AssignPropertyAttribute && string.Equals((o as AssignPropertyAttribute).PropertyName, propertyName))
                .Select(o => (o as AssignPropertyAttribute).Attribute));

            if (isInherits)
            {
                foreach (var typeInterface in type.GetInterfaces())
                {
                    var propertyInfo = typeInterface.GetProperty(propertyName);
                    if (propertyInfo != null)
                        customAttributes.AddRange(GetCustomAttributes(propertyInfo, true));

                    //customAttributes.AddRange(GetCustomAttributes(typeInterface, propertyName, isInherits));
                }
            }

            return customAttributes;
        }

        public static IEnumerable<Attribute> GetCustomAttributes(Type type, bool isInherits)
        {
            var customAttributes = new List<Attribute>(type.GetCustomAttributes(isInherits).OfType<Attribute>());

            if (isInherits)
            {
                foreach (var typeInterface in type.GetInterfaces())
                {
                    customAttributes.AddRange(GetCustomAttributes(typeInterface, isInherits));
                }
            }

            return customAttributes;
        }
    }
}
