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
            DateTime.MinValue.ToTimeStamp().ShouldEqual("000101010000000000");
        }
    }
}