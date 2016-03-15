using System;
using Saturn72.Extensions.TestSdk;
using Xunit;

namespace Saturn72.Extensions.Tests
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void ToTimeStamp_ReturnslegalTimestamp()
        {
            DateTime.MinValue.ToTimeStamp().ShouldEqual("000101010000000000");
        }
    }
}