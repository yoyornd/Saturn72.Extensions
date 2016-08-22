#region

using System;

#endregion

namespace Saturn72.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToTimeStamp(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd_HHmmssffff");
        }

        public static string NullableDateTimeToString(this DateTime? dateTime)
        {
            return dateTime?.ToString("G") ?? string.Empty;
        }

        public static bool SecondTimeSpanHasPass(this DateTime sourceUtcDateTime, int seconds)
        {
            return sourceUtcDateTime.AddSeconds(seconds) < DateTime.UtcNow;
        }
    }
}