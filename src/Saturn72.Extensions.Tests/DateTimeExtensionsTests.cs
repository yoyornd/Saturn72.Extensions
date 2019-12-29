using System;
using Xunit;
using Shouldly;

namespace Saturn72.Extensions.Tests
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void ToIso8601()
        {
            var dt = DateTime.UtcNow;
            dt.ToIso8601().ShouldBe(dt.ToString("yyyy-MM-ddTHH:mm:ssK"));
        }
    }
}
