namespace Saturn72.Cron
{
    public interface ICronMinute
    {
        /// <summary>
        ///     Sets the cron expression with the minutes interval.
        /// </summary>
        /// <param name="minutes">The minutes interval</param>
        /// <returns>Cron</returns>
        ICronHour MinuteInterval(int minutes);
    }
}