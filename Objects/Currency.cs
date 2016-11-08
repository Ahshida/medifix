using System;
using System.Globalization;
using DBO.Data.Managers;

namespace DBO.Data.Objects
{
    public struct Currency : IComparable, IComparable<Currency>, IEquatable<Currency>
    {
        public decimal OriginalValue;
        public Currency(decimal value)
        {
            OriginalValue = Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            return CompareTo((Currency)obj);
        }

        public int CompareTo(Currency other)
        {
            return this.OriginalValue.CompareTo(other.OriginalValue);
        }

        public bool Equals(Currency other)
        {
            return object.Equals(this.OriginalValue, other.OriginalValue);
        }
        public bool Equals(object obj)
        {
            return this.Equals((Currency)obj);
        }
        public override int GetHashCode()
        {
            return this.OriginalValue.GetHashCode();
        }

        public override string ToString()
        {
            return Math.Round(this.OriginalValue, 2).ToString("N", CultureInfo.CurrentCulture);
        }

        public static bool operator ==(Currency a, decimal b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Currency a, decimal b)
        {
            return !a.Equals(b);
        }
        public static Currency operator -(Currency d)
        {
            return -d.OriginalValue;
        }
        public static Currency operator -(Currency d1, decimal d2)
        {
            return d1.OriginalValue - d2;
        }
        public static Currency operator --(Currency d)
        {
            return --d.OriginalValue;
        }
        public static Currency operator %(Currency d1, decimal d2)
        {
            return d1.OriginalValue % d2;
        }
        public static Currency operator *(Currency d1, decimal d2)
        {
            return d1.OriginalValue * d2;
        }
        public static Currency operator /(Currency d1, decimal d2)
        {
            return d1.OriginalValue / d2;
        }
        public static Currency operator +(Currency d)
        {
            return d;
        }
        public static Currency operator +(Currency d1, decimal d2)
        {
            return d1.OriginalValue + d2;
        }
        public static Currency operator ++(Currency d)
        {
            return d.OriginalValue++;
        }
        public static bool operator <(Currency d1, decimal d2)
        {
            return d1.OriginalValue < d2;
        }
        public static bool operator <=(Currency d1, decimal d2)
        {
            return d1.OriginalValue <= d2;
        }
        public static bool operator >(Currency d1, decimal d2)
        {
            return d1.OriginalValue > d2;
        }
        public static bool operator >=(Currency d1, decimal d2)
        {
            return d1.OriginalValue >= d2;
        }

        public static explicit operator uint(Currency value)
        {
            return (uint)value.OriginalValue;
        }
        public static explicit operator sbyte(Currency value)
        {
            return (sbyte)value.OriginalValue;
        }
        public static explicit operator ulong(Currency value)
        {
            return (ulong)value.OriginalValue;
        }
        public static explicit operator float(Currency value)
        {
            return (float)value.OriginalValue;
        }
        public static explicit operator double(Currency value)
        {
            return (double)value.OriginalValue;
        }
        public static explicit operator short(Currency value)
        {
            return (short)value.OriginalValue;
        }
        public static explicit operator int(Currency value)
        {
            return (int)value.OriginalValue;
        }
        public static explicit operator ushort(Currency value)
        {
            return (ushort)value.OriginalValue;
        }
        public static explicit operator long(Currency value)
        {
            return (long)value.OriginalValue;
        }
        public static explicit operator char(Currency value)
        {
            return (char)value.OriginalValue;
        }
        public static explicit operator byte(Currency value)
        {
            return (byte)value.OriginalValue;
        }
        //public static explicit operator decimal(Currency value)
        //{
        //    return value.OriginalValue;
        //}
        public static explicit operator string(Currency value)
        {
            return value.ConvertTo<string>();
        }
        public static implicit operator decimal(Currency value)
        {
            return value.OriginalValue;
        }
        public static implicit operator Currency(decimal value)
        {
            return new Currency(value);
        }
        public static implicit operator Currency(double value)
        {
            return (decimal)value;
        }
        public static implicit operator Currency(float value)
        {
            return (decimal)value;
        }
        public static implicit operator Currency(byte value)
        {
            return (decimal)value;
        }
        public static implicit operator Currency(char value)
        {
            return (decimal)value;
        }
        public static implicit operator Currency(int value)
        {
            return (decimal)value;
        }
        public static implicit operator Currency(long value)
        {
            return (decimal)value;
        }
        public static implicit operator Currency(sbyte value)
        {
            return (decimal)value;
        }
        public static implicit operator Currency(short value)
        {
            return (decimal)value;
        }
        public static implicit operator Currency(uint value)
        {
            return (decimal)value;
        }
        public static implicit operator Currency(ulong value)
        {
            return (decimal)value;
        }
        public static implicit operator Currency(ushort value)
        {
            return (decimal)value;
        }
        public static implicit operator Currency(string value)
        {
            decimal result;
            if (decimal.TryParse(value, out result))
                return result;
            else
                return 0;
        }
    }
}
