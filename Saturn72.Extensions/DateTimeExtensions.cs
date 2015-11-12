using System;

namespace Saturn72.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToTimeStamp(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddHHmmssffff");
        }

        public static string NullableDateTimeToString(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString("G") : "";
        }
    }
}