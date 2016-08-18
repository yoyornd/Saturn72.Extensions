using System;
using Xunit;

namespace Saturn72.Cron.Tests
{
    public class CronTests
    {
        [Fact]
        public void CronExpression_Initialized()
        {
            var builder = new CronExpressionBuilder();

            Assert.Equal("* * * * *", builder.CronExpression);
        }

        [Fact]
        public void AddMinuteInterval_Below0_ThrowsException()
        {
            var builder = new CronExpressionBuilder();
            Assert.Throws<Exception>(() => builder.MinuteInterval(-29));
        }

        [Fact]
        public void AddMinuteInterval_Below60()
        {
            var builder = new CronExpressionBuilder();
            builder.MinuteInterval(29);
            Assert.Equal("29 * * * *", builder.CronExpression);
        }

        [Fact]
        public void AddMinuteInterval_Above60()
        {
            var builder = new CronExpressionBuilder();
            var minutes = 92;
            builder.MinuteInterval(minutes);

            Assert.Equal("32 * * * *", builder.CronExpression);
        }

        [Fact]
        public void AddHourInterval_Below0_ThrowsException()
        {
            var builder = new CronExpressionBuilder();
            Assert.Throws<Exception>(() => builder.HourInterval(-29));
        }

        [Fact]
        public void AddHourInterval_Below23()
        {
            var builder = new CronExpressionBuilder();
            builder.HourInterval(4);
            Assert.Equal("* 4 * * *", builder.CronExpression);
        }

        [Fact]
        public void AddHourInterval_Above23()
        {
            var builder = new CronExpressionBuilder();
            var hours = 28;
            builder.HourInterval(hours);

            Assert.Equal("* 4 * * *", builder.CronExpression);
        }

        [Fact]
        public void AddDayInterval_Below1_ThrowsException()
        {
            var builder = new CronExpressionBuilder();
            Assert.Throws<Exception>(() => builder.DayInterval(-29));
        }

        [Fact]
        public void AddDayInterval_Below6()
        {
            var builder = new CronExpressionBuilder();
            builder.DayInterval(4);
            Assert.Equal("* * 4 * *", builder.CronExpression);
        }

        [Fact]
        public void AddDayInterval_Above6()
        {
            var builder = new CronExpressionBuilder();
            var days = 35;
            builder.DayInterval(days);

            Assert.Equal("* * 4 * *", builder.CronExpression);
        }

        [Fact]
        public void AddMonthInterval_AddBelow1()
        {
            var builder = new CronExpressionBuilder();
            Assert.Throws<Exception>(() => builder.MonthInterval(-29));
        }

        [Fact]
        public void AddMonthInterval_Below12()
        {
            var builder = new CronExpressionBuilder();
            builder.MonthInterval(4);
            Assert.Equal("* * * 4 *", builder.CronExpression);
        }

        [Fact]
        public void AddMonthInterval_Above12()
        {
            var builder = new CronExpressionBuilder();
            var days = 35;
            builder.MonthInterval(days);

            Assert.Equal("* * * 11 *", builder.CronExpression);
        }

        [Fact]
        public void AddMonthInterval_NullYearMonth()
        {
            var builder = new CronExpressionBuilder();
            Assert.Throws<NullReferenceException>(() => builder.MonthInterval(null));
        }

        [Fact]
        public void AddMonthInterval_YearMonth()
        {
            var builder = new CronExpressionBuilder();
            builder.MonthInterval(YearMonth.April);
            Assert.Equal("* * * 4 *", builder.CronExpression);
        }
        
        [Fact]
        public void YearWeekdayInterval_AddBelow0()
        {
            var builder = new CronExpressionBuilder();
            Assert.Throws<Exception>(() => builder.WeekDayInterval(-29));
        }

        [Fact]
        public void YearWeekdayInterval_Below12()
        {
            var builder = new CronExpressionBuilder();
            builder.WeekDayInterval(4);
            Assert.Equal("* * * * 4", builder.CronExpression);
        }

        [Fact]
        public void YearWeekdayInterval_Above12()
        {
            var builder = new CronExpressionBuilder();
            var days = 34;
            builder.WeekDayInterval(days);

            Assert.Equal("* * * * 6", builder.CronExpression);
        }

        [Fact]
        public void YearWeekdayInterval_NullYearMonth()
        {
            var builder = new CronExpressionBuilder();
            Assert.Throws<NullReferenceException>(() => builder.WeekDayInterval(null));
        }

        [Fact]
        public void YearWeekdayInterval_YearMonth()
        {
            var builder = new CronExpressionBuilder();
            builder.WeekDayInterval(WeekDay.Monday);
            Assert.Equal("* * * * 1", builder.CronExpression);
        }
    }
}