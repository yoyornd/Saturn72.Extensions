using System;
using Saturn72.Extensions.UnitTesting;
using Xunit;

namespace Saturn72.Extensions.Tests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void AsFormat_empty()
        {
            "    _logic".ShouldEqual("    {0}".AsFormat("_logic"));
        }

        [Fact]
        public void AsFormat_MissingArgThrowsFormatException()
        {
            typeof (FormatException).ShouldBeThrownBy(() => "test{0} {1}".AsFormat(1));
        }

        [Fact]
        public void AsFormat_string_and_object()
        {
            var o = new object();
            "test_logic".ShouldEqual("test{0}".AsFormat("_logic", o));
        }

        [Fact]
        public void AsFormat_strings()
        {
            "test_logic".ShouldEqual("test{0}".AsFormat("_logic"));
        }

        [Fact]
        public void AsFormat_ThrowesException()
        {
            typeof (FormatException).ShouldBeThrownBy(() => "{ Test }".AsFormat("123"));
        }

        [Fact]
        public void EqualsToCaseInSensitive_different_letter()
        {
            Assert.True("teSt".EqualsTo("test"));
        }

        [Fact]
        public void EqualsToCaseInSensitive_FalseCollection()
        {
            Assert.False("test".EqualsTo(new[] {"r", "t", "w", null}));
        }

        [Fact]
        public void EqualsToCaseInSensitive_identical_strings()
        {
            Assert.True("test".EqualsTo("test"));
        }

        [Fact]
        public void EqualsToCaseInSensitive_null()
        {
            string str = null;
            Assert.False("    ".EqualsTo(str));
        }

        [Fact]
        public void EqualsToCaseInSensitive_nullCollection()
        {
            Assert.False("test".EqualsTo(new string[] {null}));
        }

        [Fact]
        public void EqualsToCaseInSensitive_TrueCollection()
        {
            Assert.True("test".EqualsTo(new[] {"test", "test", "test"}));
        }

        [Fact]
        public void EqualsToCaseInSensitive_white_spaces()
        {
            Assert.False("    ".EqualsTo("Test"));
        }

        [Fact]
        public void HasValue_empty_string()
        {
            Assert.False(string.Empty.HasValue());
        }

        [Fact]
        public void HasValue_null()
        {
            string source = null;
            Assert.False(source.HasValue());
        }

        [Fact]
        public void HasValue_white_spaces()
        {
            Assert.False("    ".HasValue());
        }

        [Fact]
        public void HasValue_with_value()
        {
            Assert.True("test_string".HasValue());
        }

        [Fact]
        public void RemoveSubStringInstances_removes_all()
        {
            "AACC".ShouldEqual("AAbbCC".RemoveAllSubStringInstances("bb"));
            "AbCC".ShouldEqual("AAbbCC".RemoveAllSubStringInstances("Ab"));
            "bbCC".ShouldEqual("AAbbCC".RemoveAllSubStringInstances("AA"));
        }

        [Fact]
        public void RemoveSubStringInstances_removesnothing()
        {
            "AbbACC".ShouldEqual("AbbACC".RemoveAllSubStringInstances("AA"));
        }

        [Fact]
        public void RemoveAll_RemovesAllInstances1()
        {
            "AAAaaaBBBbbbCc".RemoveAll("A").ShouldEqual("aaaBBBbbbCc");
        }

        [Fact]
        public void RemoveAll_RemovesAllInstances2()
        {
            "AAAaaaBBBbbbCc".RemoveAll("A", "BB", "bbb", "C").ShouldEqual("aaaBc");
        }

        [Fact]
        public void RemoveAllWhiteSpaces()
        {
            "    A               ".RemoveAllWhiteSpaces().ShouldEqual("A");
        }

        [Fact]
        public void RemoveDuplicateWhiteSpaces()
        {
            "    A        B       ".RemoveDuplicateWhiteSpaces().ShouldEqual(" A B ");
        }
    }
}