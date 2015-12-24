using Saturn72.Extensions;

namespace Saturn72.Cron
{
    public interface ICronWeekDay
    {
        /// <summary>
        ///     Sets the cron expression with the week day interval.
        /// </summary>
        /// <param name="weekDay">The day of the week</param>
        /// <returns>Cron</returns>
        void WeekDayInterval(int weekDay);
    }

    public static class CronWeekDayExtensions
    {
        public static void WeekDayInterval(this ICronWeekDay cronWeekDay, WeekDay day)
        {
            Guard.NotNull(day);
            cronWeekDay.WeekDayInterval(day.Value);
        }
    }
}