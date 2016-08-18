namespace Saturn72.Cron
{
    public interface ICronDay
    {
        /// <summary>
        ///     Sets the cron expression with the days interval.
        /// </summary>
        /// <param name="days">The days interval</param>
        /// <returns>Cron</returns>
        ICronMonth DayInterval(int days);
    }
}