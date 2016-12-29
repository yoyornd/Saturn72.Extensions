#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class EnumerableExtensionsTests
    {
        [Test]
        public void NotEmpty_ReturnsTrue()
        {
            new[] {1, 2, 3}.NotEmpty().ShouldBeTrue();
        }

        [Test]
        public void NotEmpty_ReturnsFalse()
        {
            new int[] {}.NotEmpty().ShouldBeFalse();
        }

        [Test]
        public void ForEachItem_ThrowsException()
        {
            var i = 0;
            Assert.Throws<NullReferenceException>(() => ((List<object>) null).ForEachItem(c => i++));
        }




        [Test]
        public void NotEmptyOrNull_returnsFalseCases()
        {
            new List<string>().NotEmptyOrNull().ShouldBeFalse();

            ((IEnumerable<string>) null).NotEmptyOrNull().ShouldBeFalse();

            "".NotEmptyOrNull().ShouldBeFalse();
        }

        [Test]
        public void NotEmptyOrNull_ReturnsTrue()
        {
            "Test".NotEmptyOrNull().ShouldBeTrue();
        }

        [Test]
        public void IsEmpty_ReturnsFalse()
        {
            (new[] {"test"}).IsEmpty().ShouldBeFalse();
        }

        [Test]
        public void IsEmpty_ReturnsTrue()
        {
            ((List<string>) null).IsEmpty().ShouldBeTrue();
            new string[] {}.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void IsEmptyOrNull_returnsTrueCases()
        {
            new List<string>().IsEmptyOrNull().ShouldBeTrue();

            ((IEnumerable<string>)null).IsEmptyOrNull().ShouldBeTrue();
            "".IsEmptyOrNull().ShouldBeTrue();
            "Test".NotEmptyOrNull().ShouldBeTrue();
        }


        [Test]
        public void IsEmptyOrNull_ReturnsFalseCases()
        {
            "Test".IsEmptyOrNull().ShouldBeFalse();
        
            new List<string>().NotEmptyOrNull().ShouldBeFalse();

            ((IEnumerable<string>)null).NotEmptyOrNull().ShouldBeFalse();

            "".NotEmptyOrNull().ShouldBeFalse();
        }

        [Test]
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

        [Test]
        public void maxby_throws_exception_if_empty()
        {
            typeof(ArgumentException).ShouldBeThrownBy(() => new List<TestClass>().MaxOrDefault(tc => tc.Index));
        }

        [Test]
        public void maxby_throws_exception_if_null()
        {
            typeof(ArgumentException).ShouldBeThrownBy(() => ((TestClass[]) null).MaxOrDefault(tc => tc.Index));
        }

        [Test]
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

        [Test]
        public void minby_throws_exception_if_empty()
        {
            typeof(ArgumentException).ShouldBeThrownBy(() => new List<TestClass>().MinBy(tc => tc.Index));
        }

        [Test]
        public void minby_throws_exception_if_null()
        {
            typeof(ArgumentException).ShouldBeThrownBy(() => ((TestClass[]) null).MinBy(tc => tc.Index));
        }

        [Test]
        public void IsEnumerableOfType_ReturnsTrue()
        {
            typeof(List<string>).IsIEnumerableofType().ShouldBeTrue();
        }

        [Test]
        public void IsEnumerableOfType_ReturnsFalse()
        {
            typeof(string).IsIEnumerableofType().ShouldBeFalse();
        }

        [Test]
        public void IsEmpty_Null_ReturnsTrue()
        {
            (null as IEnumerable<object>).IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void IsEmpty_Empty_ReturnsTrue()
        {
            new string[] {}.IsEmpty().ShouldBeTrue();
        }

        [Test]
        public void ItemCount_Throws()
        {
            //on null enum
            typeof(NullReferenceException).ShouldBeThrownBy(() => ((IEnumerable) null).ItemCount());
        }

        [Test]
        public void ItemCount_ReturnsCount()
        {
            //on empty collection
            new int[] {}.ItemCount().ShouldEqual(0);
            new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0}.ItemCount().ShouldEqual(10);
        }

        [Test]
        public void Random_Throws()
        {
            //on null item
            typeof(NullReferenceException).ShouldBeThrownBy(() => ((IEnumerable<object>) null).Random());
            //on empty enumerable
            typeof(ArgumentException).ShouldBeThrownBy(() => new int[] {}.Random());
        }


        [Test]
        public void Random_PicksItems()
        {
            var action = new Action(() =>
            {
                var source = new[] {1, 2, 3, 4, 5, 6};
                var actualValue = source.Random();
                Assert.IsTrue(source.Contains(actualValue));
            });

            for (int i = 0; i < 1000; i++)
            {
                action();
            }
        }

        public class TestClass
        {
            public int Index { get; set; }
        }
    }
}