using System;
using System.Collections.Generic;
using Xunit;
using Shouldly;
using Saturn72.Extensions;

namespace Saturn72.Extensions.Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(default(string))]
        [InlineData("")]
        [InlineData("   ")]
        public void StringExtensions_HasValue_ReturnsFalse(string source)
        {
            source.HasValue().ShouldBeFalse();
        }

        [Fact]
        public void StringExtensions_HasValue_ReturnsTrue()
        {
            "test_string".HasValue().ShouldBeTrue();
        }

        [Fact]
        public void AsFormat_empty()
        {
            "    _logic".ShouldBe("    {0}".AsFormat("_logic"));
        }

        [Fact]
        public void AsFormat_MissingArgThrowsFormatException()
        {
            Should.Throw<FormatException>(() => "test{0} {1}".AsFormat(1));
        }

        [Fact]
        public void AsFormat_string_and_object()
        {
            var o = new object();
            "test_logic".ShouldBe("test{0}".AsFormat("_logic", o));
        }

        [Fact]
        public void AsFormat_strings()
        {
            "test_logic".ShouldBe("test{0}".AsFormat("_logic"));
        }

        [Fact]
        public void AsFormat_ThrowesException()
        {
            Should.Throw<FormatException>(() => "{ Test }".AsFormat("123"));
        }

        [Fact]
        public void AsFormat_Dictionary()
        {
            var formatDictionary = new Dictionary<string, object>
            {
                {"t1", "TTT"},
                {"t2", 2},
                {"t3", new object()}
            };

            "TTT 2 System.Object".ShouldBe("{t1} {t2} {t3}".AsFormat(formatDictionary));
        }

        private const string Name = "roi";
        private const string ValueAsString = "4";
        private const string Json = "{\"value\":" + ValueAsString + ", \"name\":\"" + Name + "\"}";
        private static readonly int Value = int.Parse(ValueAsString);
        [Fact]
        public void ToObject_FromString()
        {
            var o1 = Json.ToObject<TestClass>();
            o1.Name.ShouldBe(Name);
            o1.Value.ShouldBe(Value);

            var t = Json.ToObject(typeof(TestClass));
            var o2 = t.ShouldBeOfType<TestClass>();
            o2.Name.ShouldBe(Name);
            o2.Value.ShouldBe(Value);
        }
        public class TestClass
        {
            public int Value { get; set; }
            public string Name { get; set; }
        }
    }
}
