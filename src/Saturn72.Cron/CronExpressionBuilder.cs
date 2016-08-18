using System.Linq;
using Saturn72.Extensions;

namespace Saturn72.Cron
{
    public class CronExpressionBuilder : ICronMinute, ICronHour, ICronDay, ICronMonth, ICronWeekDay
    {
        private const string CronExpressionFormat = "{0} {1} {2} {3} {4}";

        public CronExpressionBuilder()
        {
            CronExpression = CronExpressionFormat.AsFormat("*", "*", "*", "*", "*");
        }

        public string CronExpression { get; private set; }

        public ICronMonth DayInterval(int days)
        {
            Guard.MustFollow(() => days > 0, nameof(days));

            ReplaceCronValue(2, days%31);
            return this;
        }

        public ICronDay HourInterval(int hours)
        {
            Guard.MustFollow(() => hours >= 0, nameof(hours));

            ReplaceCronValue(1, hours%24);
            return this;
        }

        public ICronHour MinuteInterval(int minutes)
        {
            Guard.MustFollow(() => minutes >= 0, nameof(minutes));

            ReplaceCronValue(0, minutes%60);
            return this;
        }

        /// <summary>
        ///     Sets the cron expression with the months interval.
        /// </summary>
        /// <param name="months">The days interval</param>
        /// <returns>Cron</returns>
        public ICronWeekDay MonthInterval(int months)
        {
            Guard.MustFollow(() => months > 0, nameof(months));

            ReplaceCronValue(3, months%12);
            return this;
        }

        /// <summary>
        ///     Sets the cron expression with the week day interval.
        /// </summary>
        /// <param name="weekDay">The day of the week</param>
        /// <returns>Cron</returns>
        public void WeekDayInterval(int weekDay)
        {
            Guard.MustFollow(() => weekDay >= 0, nameof(weekDay));
            ReplaceCronValue(4, weekDay%7);
        }

        private void ReplaceCronValue(int index, int value)
        {
            var list = CronExpression.Split(' ').ToList();
            list[index] = value.ToString();

            CronExpression = CronExpressionFormat.AsFormat(list[0], list[1], list[2], list[3], list[4]);
        }

        public static bool IsValidCronExpression(string cronExpression)
        {
            return true;
            //TODO implement this here
        }

}
}