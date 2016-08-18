using Saturn72.Extensions;

namespace Saturn72.Cron
{
    public interface ICronMonth
    {
        /// <summary>
        ///     Sets the cron expression with the months interval.
        /// </summary>
        /// <param name="months">The days interval</param>
        /// <returns>Cron</returns>
        ICronWeekDay MonthInterval(int months);
    }

    public static class CronMonthExtensions
    {
        public static ICronWeekDay MonthInterval(this ICronMonth cron, YearMonth month)
        {
            Guard.NotNull(month);
            return cron.MonthInterval(month.Value);
        }

    }
}