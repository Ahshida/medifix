using System;
using DBO.Data.Managers;

namespace DBO.Data.Objects
{
    public struct Time : IComparable, IComparable<Time>, IEquatable<Time>
    {
        private DateTime _dateTime;
        public int Hour { get { return _dateTime.Hour; } set { _dateTime = _dateTime.AddHours(value - _dateTime.Hour); } }
        public int Minute { get { return _dateTime.Minute; } set { _dateTime = _dateTime.AddMinutes(value - _dateTime.Minute); } }
        public int Second { get { return _dateTime.Second; } set { _dateTime = _dateTime.AddSeconds(value - _dateTime.Second); } }
        public double TotalHours { get { return this.Hour + (this.Minute / 60) + (this.Second / 60 / 60); } }
        public double TotalMinutes { get { return (this.Hour * 60) + this.Minute + (this.Second / 60); } }
        public double TotalSeconds { get { return (this.Hour * 60 * 60) + (this.Minute * 60) + this.Second; } }

        public Time(string time)
        {
            _dateTime = DateTime.ParseExact(time, "HH:mm", null);
        }
        public Time(DateTime dateTime)
        {
            _dateTime = dateTime;
        }
        public Time(int hours, int minutes, int seconds)
            : this(DateTime.Now.Date)
        {
            this.Hour = hours;
            this.Minute = minutes;
            this.Second = seconds;
        }

        public DateTime GetDateTime()
        {
            return _dateTime;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            return CompareTo((Time)obj);
        }

        public int CompareTo(Time other)
        {
            var compareHours = this.Hour.CompareTo(other.Hour);
            if (compareHours != 0) return compareHours;
            var compareMinutes = this.Minute.CompareTo(other.Minute);
            if (compareMinutes != 0) return compareMinutes;
            var compareSeconds = this.Second.CompareTo(other.Second);
            if (compareSeconds != 0) return compareSeconds;

            return 0;
        }

        public bool Equals(Time other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is Time)
                return this.Equals((Time)obj);
            else
                return false;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 60) ^ this.Hour;
                result = (result * 60) ^ this.Minute;
                result = (result * 60) ^ this.Second;
                return result;
            }
        }

        public override string ToString()
        {
            return _dateTime.ToString("HH:mm");
        }

        public static bool operator ==(Time a, Time b)
        {
            return Time.Equals(a, b);
        }
        public static bool operator !=(Time a, Time b)
        {
            return !Time.Equals(a, b);
        }

        public static explicit operator DateTime(Time value)
        {
            return value._dateTime;
        }
        public static explicit operator string(Time value)
        {
            return value.ConvertTo<string>();
        }
        public static implicit operator Time(DateTime value)
        {
            return new Time(value);
        }
        public static implicit operator Time(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var parts = value.Split(':');
                DateTime? time = null;
                if (parts.Length > 0)
                    time = new DateTime(0, 0, 0, parts[0].ConvertTo<int>(), 0, 0);
                if (parts.Length > 1)
                    time = time.Value.AddMinutes(parts[1].ConvertTo<double>());
                if (parts.Length > 2)
                    time = time.Value.AddSeconds(parts[2].ConvertTo<double>());
                if (time.HasValue)
                    return new Time(time.Value);
            }
            return new Time(new DateTime(0));
        }

        public static DateTime operator +(DateTime date, Time time)
        {
            return date.AddHours(time.Hour).AddMinutes(time.Minute).AddSeconds(time.Second);
        }
        public static DateTime? operator +(DateTime? date, Time? time)
        {
            if (!date.HasValue)
                return null;
            else if (time.HasValue)
                return date.Value + time.Value;
            else
                return date;
        }
    }
}
