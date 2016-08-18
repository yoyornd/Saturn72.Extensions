using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

namespace Saturn72.Extensions.Data.Tests
{
    [TestFixture]
    public class ObjectExtensionTests
    {
        [Test]
        public void ToJson_ReturnsEmptyOnNull()
        {
            var result = ((object)null).ToJson();
            "null".ShouldEqual(result);
        }

        [Test]
        public void ToJson_ReturnsString()
        {
            var tClass = new TestClass
            {
                IntValue = 444,
                TestSubClasses = new[]
                {
                    new TestSubClass {IntValue = 0, StringArray = new[] {"a","b","c"} },
                    new TestSubClass {IntValue = 1, StringArray = new[] {"d","e","f"} },
                    new TestSubClass {IntValue = 2, StringArray = new[] {"g","h","i"} }
                }
            };

            var actual = tClass.ToJson();
            var expected = "{\"IntValue\":444,\"TestSubClasses\":[{\"IntValue\":0,\"StringArray\":[\"a\",\"b\",\"c\"]},{\"IntValue\":1,\"StringArray\":[\"d\",\"e\",\"f\"]},{\"IntValue\":2,\"StringArray\":[\"g\",\"h\",\"i\"]}]}";

            expected.ShouldEqual(actual);
        }

    }
}
