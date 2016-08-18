#region

using System.Linq;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class ObjectExtensionTests
    {
        [Test]
        public void IsNull_ReturnsFalse()
        {
            "test".IsNull().ShouldBeFalse();
        }

        [Test]
        public void IsNull_ReturnsTrue()
        {
            ((object) null).IsNull().ShouldBeTrue();
        }


        [Test]
        public void IsDefault_ReturnsFalse()
        {
            "test".IsDefault().ShouldBeFalse();
        }

        [Test]
        public void IsDefault_returnsTrue()
        {
            default(object).IsDefault().ShouldBeTrue();
        }

        [Test]
        public void ToPropertyDictionary_ReturnsTrue()
        {
            var o = new
            {
                int_list = new[] {1, 2, 3},
                obj = new object()
            };
            var res = o.ToPropertyDictionary();

            2.ShouldEqual(res.Keys.Count);
            "int_list".ShouldEqual(res.Keys.ElementAt(0));
            "obj".ShouldEqual(res.Keys.ElementAt(1));
        }
    }
}