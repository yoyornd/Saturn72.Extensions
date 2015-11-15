using System;
using System.Collections.Generic;
using Saturn72.Extensions.UnitTesting;
using Xunit;

namespace Saturn72.Extensions.Tests
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void ForEachItem_ThrowsException()
        {
            var i = 0;
            Assert.Throws<NullReferenceException>(() => ((List<object>) null).ForEachItem(c => i++));
        }

        [Fact]
        public void IsEmpty_ReturnsFalse()
        {
            (new[] {"test"}).IsEmpty().ShouldBeFalse();
        }

        [Fact]
        public void IsEmpty_ReturnsTrue()
        {
            ((List<string>) null).IsEmpty().ShouldBeTrue();
            new string[] {}.IsEmpty().ShouldBeTrue();
        }

        [Fact]
        public void MaxBy_ReturnsValue()
        {
            var arr = new[]
            {
                new TestClass {Index = 1},
                new TestClass {Index = 2},
                new TestClass {Index = 3},
                new TestClass {Index = 4},
                new TestClass {Index = 5}
            };
            var result = arr.MaxOrDefault(tc => tc.Index);

            result.Index.ShouldEqual(5);
        }

        [Fact]
        public void maxby_throws_exception_if_empty()
        {
            typeof(ArgumentException).ShouldBeThrownBy(() => new List<TestClass>().MaxOrDefault(tc => tc.Index));
        }

        [Fact]
        public void maxby_throws_exception_if_null()
        {
           typeof(ArgumentException).ShouldBeThrownBy(() => ((TestClass[]) null).MaxOrDefault(tc => tc.Index));
        }

        [Fact]
        public void minby_returnvalue()
        {
            var arr = new[]
            {
                new TestClass {Index = 2},
                new TestClass {Index = 2},
                new TestClass {Index = 1},
                new TestClass {Index = 4},
                new TestClass {Index = 5}
            };
            var result = arr.MinBy(tc => tc.Index);
            result.Index.ShouldEqual(1);
        }

        [Fact]
        public void minby_throws_exception_if_empty()
        {
            typeof(ArgumentException).ShouldBeThrownBy(() => new List<TestClass>().MinBy(tc => tc.Index));
        }

        [Fact]
        public void minby_throws_exception_if_null()
        {
            typeof(ArgumentException).ShouldBeThrownBy(() => ((TestClass[]) null).MinBy(tc => tc.Index));
        }

        [Fact]
        public void IsEnumerableOfType_ReturnsTrue()
        {
            typeof (List<string>).IsIEnumerableofType().ShouldBeTrue();
        }

        [Fact]
        public void IsEnumerableOfType_ReturnsFalse()
        {
            typeof (string).IsIEnumerableofType().ShouldBeFalse();
        }

        [Fact]
        public void IsEmpty_Null_ReturnsTrue()
        {
            (null as IEnumerable<object>).IsEmpty().ShouldBeTrue();
        }

        [Fact]
        public void IsEmpty_Empty_ReturnsTrue()
        {
            new string[] {}.IsEmpty().ShouldBeTrue();
        }

        public class TestClass
        {
            public int Index { get; set; }
        }
    }
}