using NUnit.Framework;

namespace Saturn72.Extensions.Tests
{
    public class ObjectExtensionsTests
    {
        [Test]
        public void IsNull_ReturnsFalse()
        {
            Assert.False("text".IsNull());
        }

        [Test]
        public void IsNull_ReturnsTrue()
        {
            Assert.True(((object) null).IsNull());
        }

        [Test]
        public void IsDefault_ReturnsFalse()
        {
            Assert.False("TTT".IsDefault());
        }

        [Test]
        public void IsDefault_ReturnsTrue()
        {
            Assert.True(default(object).IsDefault());
        }
    }
}