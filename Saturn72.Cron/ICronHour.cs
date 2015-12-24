namespace Saturn72.Cron
{
    public interface ICronHour
    {
        /// <summary>
        ///     Sets the cron expression with the hours interval.
        /// </summary>
        /// <param name="hours">The minutes interval</param>
        /// <returns>Cron</returns>
        ICronDay HourInterval(int hours);
    }
}