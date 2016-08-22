#region

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class StringExtensionsTests
    {
        [Test]
        public void AsFormat_empty()
        {
            "    _logic".ShouldEqual("    {0}".AsFormat("_logic"));
        }

        [Test]
        public void AsFormat_MissingArgThrowsFormatException()
        {
            typeof(FormatException).ShouldBeThrownBy(() => "test{0} {1}".AsFormat(1));
        }

        [Test]
        public void AsFormat_string_and_object()
        {
            var o = new object();
            "test_logic".ShouldEqual("test{0}".AsFormat("_logic", o));
        }

        [Test]
        public void AsFormat_strings()
        {
            "test_logic".ShouldEqual("test{0}".AsFormat("_logic"));
        }

        [Test]
        public void AsFormat_ThrowesException()
        {
            typeof(FormatException).ShouldBeThrownBy(() => "{ Test }".AsFormat("123"));
        }

        [Test]
        public void EqualsToCaseInSensitive_different_letter()
        {
            Assert.True("teSt".EqualsTo("test"));
        }

        [Test]
        public void EqualsToCaseInSensitive_FalseCollection()
        {
            Assert.False("test".EqualsTo(new[] {"r", "t", "w", null}));
        }

        [Test]
        public void EqualsToCaseInSensitive_identical_strings()
        {
            Assert.True("test".EqualsTo("test"));
        }

        [Test]
        public void EqualsToCaseInSensitive_null()
        {
            string str = null;
            Assert.False("    ".EqualsTo(str));
        }

        [Test]
        public void EqualsToCaseInSensitive_nullCollection()
        {
            Assert.False("test".EqualsTo(new string[] {null}));
        }

        [Test]
        public void EqualsToCaseInSensitive_TrueCollection()
        {
            Assert.True("test".EqualsTo(new[] {"test", "test", "test"}));
        }

        [Test]
        public void EqualsToCaseInSensitive_white_spaces()
        {
            Assert.False("    ".EqualsTo("Test"));
        }

        [Test]
        public void HasValue_empty_string()
        {
            Assert.False(string.Empty.HasValue());
        }

        [Test]
        public void HasValue_null()
        {
            string source = null;
            Assert.False(source.HasValue());
        }

        [Test]
        public void HasValue_white_spaces()
        {
            Assert.False("    ".HasValue());
        }

        [Test]
        public void HasValue_with_value()
        {
            Assert.True("test_string".HasValue());
        }

        [Test]
        public void RemoveSubStringInstances_removes_all()
        {
            "AACC".ShouldEqual("AAbbCC".RemoveAll("bb"));
            "AbCC".ShouldEqual("AAbbCC".RemoveAll("Ab"));
            "bbCC".ShouldEqual("AAbbCC".RemoveAll("AA"));
        }

        [Test]
        public void RemoveSubStringInstances_removesnothing()
        {
            "AbbACC".ShouldEqual("AbbACC".RemoveAll("AA"));
        }

        [Test]
        public void RemoveAll_RemovesAllInstances1()
        {
            "AAAaaaBBBbbbCc".RemoveAll("A").ShouldEqual("aaaBBBbbbCc");
        }

        [Test]
        public void RemoveAll_RemovesAllInstances2()
        {
            "AAAaaaBBBbbbCc".RemoveAll("A", "BB", "bbb", "C").ShouldEqual("aaaBc");
        }

        [Test]
        public void RemoveAllWhiteSpaces()
        {
            "    A               ".RemoveAllWhiteSpaces().ShouldEqual("A");
        }

        [Test]
        public void RemoveDuplicateWhiteSpaces()
        {
            "    A        B       ".RemoveDuplicateWhiteSpaces().ShouldEqual(" A B ");
        }

        [Test]
        public void AsFormat_Dictionary()
        {
            var formatDictionary = new Dictionary<string, object>
            {
                {"t1", "TTT"},
                {"t2", 2},
                {"t3", new object()}
            };

            "TTT 2 System.Object".ShouldEqual("{t1} {t2} {t3}".AsFormat(formatDictionary));
        }

        [Test]
        public void RemoveNewLineEscape_Throws()
        {
            typeof(NullReferenceException).ShouldBeThrownBy(() => ((string) null).RemoveNewLineEscape());
        }

        [Test]
        public void RemoveNewLineEscape_ClearsNewLinesEscapes()
        {
            var source = "This\n is\nsource\n\n\n\n\n\n";
            Assert.AreEqual("This is source ", source.RemoveNewLineEscape());
        }

        [Test]
        public void RemoveNewLineEscape_ReturnsSourceOnNoNewLineEscapes()
        {
            var source = "This is source";

            Assert.AreEqual("This is source", source.RemoveNewLineEscape());
        }
    }
}