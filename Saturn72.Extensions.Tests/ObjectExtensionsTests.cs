using Xunit;

namespace Saturn72.Extensions.Tests
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void IsNull_ReturnsFalse()
        {
            Assert.False("text".IsNull());
        }

        [Fact]
        public void IsNull_ReturnsTrue()
        {
            Assert.True(((object) null).IsNull());
        }

        [Fact]
        public void IsDefault_ReturnsFalse()
        {
            Assert.False("TTT".IsDefault());
        }

        [Fact]
        public void IsDefault_ReturnsTrue()
        {
            Assert.True(default(object).IsDefault());
        }
    }
}