using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using DBO.Data.Objects;

namespace DBO.Data.Managers
{
    public static class ConvertManager
    {
        public static string ConvertTo(this object value)
        {
            return value.ConvertTo<string>();
        }
        public static T ConvertTo<T>(this object value)
        {
            var result = value.ConvertToTargetTypeValue(typeof(T));
            if (result == null)
                return default(T);
            else
                return (T)result;
        }

        public static object ConvertToTargetTypeValue(this object value, Type targetType, bool isNotNull)
        {
            var result = value.ConvertToTargetTypeValue(targetType);
            if (result == null && isNotNull)
            {
                if (typeof(string).IsAssignableFrom(targetType))
                    result = string.Empty;
                else if (targetType.IsGenericType)
                {
                    targetType = targetType.GetGenericArguments().First();
                    result = targetType.GetDefaultValue();
                }
            }
            return result;
        }
        public static object ConvertToTargetTypeValue(this object value, Type targetType)
        {
            if (value != null)
            {
                if (targetType.IsAssignableFrom(value.GetType()))
                    return value;
                if (!(value is string) && value.GetType().IsClass && !targetType.IsClass)
                    value = value.ConvertTo<string>().ConvertToTargetTypeValue(targetType);
            }

            if (value == null ||
                (string.Equals(value, "") &&
                 (typeof(decimal?).IsAssignableFrom(targetType) ||
                  typeof(ushort?).IsAssignableFrom(targetType) ||
                  typeof(uint?).IsAssignableFrom(targetType) ||
                  typeof(ulong?).IsAssignableFrom(targetType) ||
                  typeof(short?).IsAssignableFrom(targetType) ||
                  typeof(int?).IsAssignableFrom(targetType) ||
                  typeof(long?).IsAssignableFrom(targetType) ||
                  typeof(byte?).IsAssignableFrom(targetType) ||
                  typeof(sbyte?).IsAssignableFrom(targetType) ||
                  typeof(float?).IsAssignableFrom(targetType) ||
                  typeof(bool?).IsAssignableFrom(targetType) ||
                  typeof(double?).IsAssignableFrom(targetType) ||
                  typeof(DateTime?).IsAssignableFrom(targetType) ||
                  typeof(Time?).IsAssignableFrom(targetType) ||
                  typeof(Currency?).IsAssignableFrom(targetType) ||
                  typeof(Guid?).IsAssignableFrom(targetType))))
                return null;

            if (targetType.IsValueType && value is string)
                value = value.ConvertTo<string>().Trim();

            if (targetType.IsAssignableFrom(value.GetType()))
                return value;
            else if (typeof(decimal?).IsAssignableFrom(targetType))
            {
                if (value is string && value.ConvertTo<string>().StartsWith("("))
                    return Convert.ToDecimal(value.ConvertTo<string>().Replace("(", "-").Replace(")", ""));
                else if (value is Currency)
                {
                    decimal? returnValue = (Currency?)value;
                    return returnValue;
                }
                else
                    return Convert.ToDecimal(value);
            }
            else if (typeof(ushort?).IsAssignableFrom(targetType))
            {
                if (value is string && value.ConvertTo<string>().StartsWith("("))
                    return Convert.ToUInt16(value.ConvertTo<string>().Replace("(", "-").Replace(")", ""));
                else
                    return Convert.ToUInt16(value);
            }
            else if (typeof(uint?).IsAssignableFrom(targetType))
            {
                if (value is string && value.ConvertTo<string>().StartsWith("("))
                    return Convert.ToUInt32(value.ConvertTo<string>().Replace("(", "-").Replace(")", ""));
                else
                    return Convert.ToUInt32(value);
            }
            else if (typeof(ulong?).IsAssignableFrom(targetType))
            {
                if (value is string && value.ConvertTo<string>().StartsWith("("))
                    return Convert.ToUInt64(value.ConvertTo<string>().Replace("(", "-").Replace(")", ""));
                else
                    return Convert.ToUInt64(value);
            }
            else if (typeof(short?).IsAssignableFrom(targetType))
            {
                if (value is string && value.ConvertTo<string>().StartsWith("("))
                    return Convert.ToInt16(value.ConvertTo<string>().Replace("(", "-").Replace(")", ""));
                else
                    return Convert.ToInt16(value);
            }
            else if (typeof(int?).IsAssignableFrom(targetType))
            {
                if (value is string && value.ConvertTo<string>().StartsWith("("))
                    return Convert.ToInt32(value.ConvertTo<string>().Replace("(", "-").Replace(")", ""));
                else
                    return Convert.ToInt32(value);
            }
            else if (typeof(long?).IsAssignableFrom(targetType))
            {
                if (value is string && value.ConvertTo<string>().StartsWith("("))
                    return Convert.ToInt64(value.ConvertTo<string>().Replace("(", "-").Replace(")", ""));
                else
                    return Convert.ToInt64(value);
            }
            else if (typeof(sbyte?).IsAssignableFrom(targetType))
                return Convert.ToSByte(value);
            else if (typeof(byte?).IsAssignableFrom(targetType))
                return Convert.ToByte(value);
            else if (typeof(float?).IsAssignableFrom(targetType))
                return Convert.ToSingle(value);
            else if (typeof(bool?).IsAssignableFrom(targetType))
            {
                if (value is string)
                {
                    var text = value.ConvertTo<string>();
                    if (text.Contains(","))
                        text = text.Split(',').First();
                    value = text.ToLower();
                    if (!object.Equals(value, bool.TrueString.ToLower()) &&
                        !object.Equals(value, bool.FalseString.ToLower()))
                        return Convert.ToBoolean(Convert.ToByte(value));
                }
                return Convert.ToBoolean(value);
            }
            else if (typeof(double?).IsAssignableFrom(targetType))
            {
                if (value is string && value.ConvertTo<string>().StartsWith("("))
                    return Convert.ToDouble(value.ConvertTo<string>().Replace("(", "-").Replace(")", ""));
                else
                    return Convert.ToDouble(value);
            }
            else if (typeof(DateTime?).IsAssignableFrom(targetType))
            {
                if (value is MySql.Data.Types.MySqlDateTime)
                {
                    if (((MySql.Data.Types.MySqlDateTime)value).IsValidDateTime)
                        return ((MySql.Data.Types.MySqlDateTime)value).GetDateTime();
                    else if (targetType.IsNullable())
                        return null;
                    else
                        return DateTime.MinValue;
                }

                try
                {
                    DateTime? result;
                    var cultureInfo = new CultureInfo("en-US");
                    cultureInfo.DateTimeFormat.ShortDatePattern = ConfigurationManager.AppSettings["SystemDateFormat"];
                    result = Convert.ToDateTime(value.ToString(), cultureInfo);
                    return result;
                }
                catch
                {
                    return null;
                }
            }
            else if (typeof(Time?).IsAssignableFrom(targetType))
            {
                var date = value.ConvertTo<DateTime?>();
                if (date.HasValue)
                    return new Time(date.Value);
                else
                    return null;
            }
            else if (typeof(Currency?).IsAssignableFrom(targetType))
            {
                if (value is string && value.ConvertTo<string>().StartsWith("("))
                    value = value.ConvertTo<string>().Replace("(", "-").Replace(")", "");
                var result = value.ConvertTo<decimal?>();
                if (result.HasValue)
                    return new Currency(result.Value);
                else
                    return null;
            }
            else if (typeof(Enum).IsAssignableFrom(targetType) ||
                     (targetType.IsGenericType && typeof(Enum).IsAssignableFrom(targetType.GetGenericArguments().First())))
            {
                try
                {
                    if (targetType.IsGenericType)
                    {
                        if (value is Enum)
                            return Enum.Parse(targetType.GetGenericArguments().First(), value.ConvertTo<byte>().ConvertTo<string>());
                        else if (string.IsNullOrEmpty(value.ConvertTo<string>()))
                            return null;
                        else
                            return Enum.Parse(targetType.GetGenericArguments().First(), Convert.ToString(value).Trim());
                    }
                    else
                    {
                        if (value is Enum)
                            return Enum.Parse(targetType, value.ConvertTo<byte>().ConvertTo<string>());
                        else if (string.IsNullOrEmpty(value.ConvertTo<string>()))
                            return Enum.Parse(targetType, "0");
                        else
                            return Enum.Parse(targetType, Convert.ToString(value).Trim());
                    }
                }
                catch
                {
                    foreach (var field in targetType.GetFields())
                    {
                        var description = field.GetCustomAttributes(typeof(DescriptionAttribute), true).OfType<DescriptionAttribute>().FirstOrDefault();
                        if (description != null && string.Equals(description.Description, value))
                            return Enum.Parse(targetType, field.Name);
                    }
                    throw new ArgumentOutOfRangeException("value");
                }
            }
            else if (typeof(Type).IsAssignableFrom(targetType))
            {
                try
                {
                    return Type.GetType(value.ConvertTo<string>());
                }
                catch
                {
                    return null;
                }
            }
            else if (typeof(Guid?).IsAssignableFrom(targetType))
            {
                return Guid.Parse(value.ConvertTo<string>());
            }
            else if (typeof(string).IsAssignableFrom(targetType))
            {
                if (value is DateTime?)
                {
                    DateTime date = (value as DateTime?).Value;
                    if (date.Hour > 0 || date.Minute > 0 || date.Second > 0)
                        return date.ToString(ConfigurationManager.AppSettings["SystemDateTimeFormat"]);
                    else
                        return date.ToString(ConfigurationManager.AppSettings["SystemDateFormat"]);
                }
                else if (value is Currency?)
                {
                    var result = value.ConvertTo<Currency>().ToThousand();
                    //if (result.StartsWith("-"))
                    //    result = string.Format("({0})", result.Replace("-", ""));
                    return result;
                }
                else if (value is IEnumerable)
                {
                    var list = new List<string>();
                    foreach (var val in (value as IEnumerable))
                        list.Add(val.ConvertTo());
                    var result = string.Join(",", list);
                    return result;
                }
                else
                {
                    string result = Convert.ToString(value);
                    //if (result.StartsWith("-"))
                    //{
                    //    if (value is short? ||
                    //        value is int? ||
                    //        value is long? ||
                    //        value is decimal? ||
                    //        value is double?)
                    //        result = string.Format("({0})", result.Replace("-", ""));
                    //}
                    return result;
                }
            }
            else
                return Convert.ToString(value);
        }

        public static string ToThousand(this Currency? value)
        {
            return (value ?? 0).ToThousand();
        }
        public static string ToThousand(this Currency value)
        {
            return value.OriginalValue.ToThousand();
        }
        public static string ToThousand(this decimal? value)
        {
            return (value ?? 0).ToThousand();
        }
        public static string ToThousand(this decimal value)
        {
            return String.Format("{0:#,0.00}", value);
        }

        public static string ToSummaryString(this string value)
        {
            return value.ToSummaryString(35);
        }
        public static string ToSummaryString(this string value, int length)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            if (value.Length > length)
                return string.Format("{0}...", value.Substring(0, length, true));
            return value;
        }

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

        public static string DefaultFormat(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm:ss");
        }
        public static string DefaultFormat(this DateTime? date)
        {
            if (date.HasValue)
                return DefaultFormat(date.Value);
            else
                return string.Empty;
        }

        public static string ConvertToAudatexFormat<T>(this T value)
        {
            return value.ConvertToAudatexFormat(false);
        }

        public static string ConvertToAudatexFormat<T>(this T value, bool isYear)
        {

            if (typeof(T).IsAssignableFrom(typeof(bool?)))
            {
                if (value.ConvertTo<bool>())
                    return "1";
                else
                    return "0";
            }
            else if (typeof(T).IsAssignableFrom(typeof(double?)) ||
               typeof(T).IsAssignableFrom(typeof(decimal?))
                )
            {
                string strValue = String.Format("{0:0.00}", value.ConvertTo<decimal>());
                return strValue.Replace(".", "");
            }
            else if (typeof(T).IsAssignableFrom(typeof(DateTime?)))
            {
                if (isYear)
                {
                    if (value.ConvertTo<DateTime?>() == null)
                    {

                        return " 0/ 0/ 0";

                    }
                    else
                    {
                        return " 0/ 0/" + value.ConvertTo<DateTime>().Year.ConvertTo<string>();
                    }
                }
                else
                {
                    if (value.ConvertTo<DateTime?>() == null)
                    {
                        return " 0/ 0/ 0";
                    }
                    else
                    {
                        return value.ConvertTo<string>();
                    }
                }
            }
            else if (typeof(T).IsAssignableFrom(typeof(Time)))
            {
                if (value == null)
                {
                    return "0: 0: 0.   0";
                }
                else
                {
                    return value.ConvertTo<string>() + ":00.0000";
                }
            }
            else if (typeof(T).IsAssignableFrom(typeof(int?)) ||
                   typeof(T).IsAssignableFrom(typeof(short?)) ||
                   typeof(T).IsAssignableFrom(typeof(long?)) ||
                   typeof(T).IsAssignableFrom(typeof(byte?))
            )
            {
                if (value == null)
                {
                    return "0";
                }
                else
                {
                    return value.ConvertTo<string>();
                }
            }
            else
            {
                if (value == null)
                {
                    return "";
                }
                else
                {
                    return value.ConvertTo<string>();
                }
            }
        }

        public static readonly string[] _sizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public static string ToPrettySize(this int value, int decimalPlaces = 0)
        {
            return value.ConvertTo<long>().ToPrettySize(decimalPlaces);
        }
        public static string ToPrettySize(this long value, int decimalPlaces = 0)
        {
            if (value < 0)
            {
                throw new ArgumentException("Bytes should not be negative", "value");
            }
            var mag = (int)Math.Max(0, Math.Log(value, 1024));
            var adjustedSize = Math.Round(value / Math.Pow(1024, mag), decimalPlaces);
            return String.Format("{0} {1}", adjustedSize, _sizeSuffixes[mag]);
        }
    }
}
