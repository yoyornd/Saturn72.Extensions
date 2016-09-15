using NUnit.Framework;

namespace Saturn72.Extensions.Tests
{
    public class ObjectExtensionsTests
    {
        [Test]
        public void ObjectExtensions_IsNull_ReturnsFalse()
        {
            Assert.False("text".IsNull());
        }

        [Test]
        public void ObjectExtensions_IsNull_ReturnsTrue()
        {
            Assert.True(((object) null).IsNull());
        }

        [Test]
        public void ObjectExtensions_IsDefault_ReturnsFalse()
        {
            Assert.False("TTT".IsDefault());
        }

        [Test]
        public void ObjectExtensions_IsDefault_ReturnsTrue()
        {
            Assert.True(default(object).IsDefault());
        }
    }
}