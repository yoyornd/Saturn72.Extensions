namespace Saturn72.Cron
{
    public class WeekDay
    {
        #region ctor

        private WeekDay(string name, int value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        public static WeekDay Sunday = new WeekDay("SUN", 0);
        public static WeekDay Monday = new WeekDay("MON", 1);
        public static WeekDay Tuesday    = new WeekDay("TUE", 2);
        public static WeekDay Wednesday = new WeekDay("WEN", 3);
        public static WeekDay Thursday = new WeekDay("THU", 4);
        public static WeekDay Friday = new WeekDay("FRI", 5);
        public static WeekDay Saturday = new WeekDay("SAT", 6);

        #region Properties

        public string Name { get; private set; }
        public int Value { get; private set; }

        #endregion
    }
}