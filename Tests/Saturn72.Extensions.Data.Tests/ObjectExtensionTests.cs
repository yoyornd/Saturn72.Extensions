using Saturn72.Extensions;
using Xunit;

namespace Saturn72.Extensions.Data.Tests
{
    public class ObjectExtensionTests
    {
        [Fact]
        public void ToJson_ReturnsEmptyOnNull()
        {
            var result = ((object)null).ToJson();
            Assert.Equal("null",result);
        }

        [Fact]
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


            Assert.Equal(expected, actual);
        }

    }
}
