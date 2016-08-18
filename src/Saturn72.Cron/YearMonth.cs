namespace Saturn72.Cron
{
    public class YearMonth
    {
        public static YearMonth January = new YearMonth("JAN", 1);
        public static YearMonth February = new YearMonth("FEB", 2);
        public static YearMonth March = new YearMonth("MAR", 3);
        public static YearMonth April = new YearMonth("APR", 4);
        public static YearMonth May = new YearMonth("MAY", 5);
        public static YearMonth June = new YearMonth("JUN", 6);
        public static YearMonth July = new YearMonth("JUL", 7);
        public static YearMonth August = new YearMonth("AUG", 8);
        public static YearMonth September = new YearMonth("SEP", 9);
        public static YearMonth October = new YearMonth("OCT", 10);
        public static YearMonth November = new YearMonth("NOV", 11);
        public static YearMonth December = new YearMonth("DEC", 12);

        #region ctor

        private YearMonth(string name, int value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        #region Properties

        public string Name { get; private set; }
        public int Value { get; private set; }

        #endregion
    }
}