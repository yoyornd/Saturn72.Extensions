using System;

using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

namespace Saturn72.Extensions.Tests
{
    public class DateTimeExtensionsTests
    {
        [Test]
        public void ToTimeStamp_ReturnslegalTimestamp()
        {
            DateTime.MinValue.ToTimeStamp().ShouldEqual("00010101_0000000000");
        }

        [Test]
        public void SecondTimeSpanPass_TimePassed()
        {
            var sourceDateTime = DateTime.MinValue;
            Assert.True(sourceDateTime.SecondTimeSpanHasPass(100));
        }

        [Test]
        public void SecondTimeSpanPass_TimeWasNotPass()
        {
            var sourceDateTime = DateTime.UtcNow;
            Assert.False(sourceDateTime.SecondTimeSpanHasPass(1000));
        }
    }
}