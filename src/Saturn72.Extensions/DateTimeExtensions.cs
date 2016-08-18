using System;

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
            return dateTime.HasValue ? dateTime.Value.ToString("G") : "";
        }
    }
}