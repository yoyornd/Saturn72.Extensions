using System;
using Saturn72.Extensions.TestSdk;
using NUnit.Framework;

namespace Saturn72.Extensions.Tests
{
    public class DateTimeExtensionsTests
    {
        [Test]
        public void ToTimeStamp_ReturnslegalTimestamp()
        {
            DateTime.MinValue.ToTimeStamp().ShouldEqual("000101010000000000");
        }
    }
}