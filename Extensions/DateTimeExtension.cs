using System;

namespace DBO.Data.Managers
{
    public static class DateTimeExtension
    {
        public static string Summarize(this DateTime dateTime, bool showPastTime)
        {
            return ((DateTime?)dateTime).Summarize(showPastTime);
        }
        public static string Summarize(this DateTime? dateTime, bool showPastTime)
        {
            if (dateTime.HasValue)
            {
                var diff = dateTime.Value - DateTime.Now;
                if (diff.TotalMilliseconds > 0)
                {
                    if (diff.TotalDays >= 1)
                        return dateTime.Value.Date.ConvertTo();
                    else if (diff.TotalHours >= 8)
                    {
                        if (dateTime > DateTime.Now.Date)
                            return "Tomorrow";
                        else
                            return dateTime.Value.ToString("HH:mm");
                    }
                    else if (diff.TotalHours >= 1)
                        return "{0} more hours".FormatWith(diff.Hours);
                    else if (diff.TotalMinutes > 5)
                        return "{0} more minutes".FormatWith(diff.Minutes);
                    else if (diff.TotalMinutes >= 1)
                        return "few more minutes";
                    else
                        return "few more seconds";
                }
                else if (!showPastTime)
                {
                    return "Expired";
                }
                else
                {
                    diff = DateTime.Now - dateTime.Value;
                    if (diff.TotalDays >= 1)
                        return dateTime.Value.Date.ConvertTo();
                    else if (diff.TotalHours >= 8)
                    {
                        if (dateTime > DateTime.Now.Date)
                            return dateTime.Value.ToString("HH:mm");
                        else
                            return "Yesterday";
                    }
                    else if (diff.TotalHours > 1)
                        return "{0} hours ago".FormatWith(diff.Hours);
                    else if (diff.Hours == 1)
                        return "an hour ago";
                    else if (diff.TotalMinutes > 5)
                        return "{0} minutes ago".FormatWith(diff.Minutes);
                    else if (diff.TotalMinutes >= 1)
                        return "few minutes ago";
                    else
                        return "few seconds ago";
                }
            }

            return "-";
        }
    }
}
