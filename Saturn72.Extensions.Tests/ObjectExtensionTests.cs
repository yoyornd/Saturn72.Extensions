using System.Linq;
using Saturn72.Extensions.UnitTesting;
using Xunit;

namespace Saturn72.Extensions.Tests
{
    public class ObjectExtensionTests
    {
        [Fact]
        public void IsNull_ReturnsFalse()
        {
            "test".IsNull().ShouldBeFalse();
        }

        [Fact]
        public void IsNull_ReturnsTrue()
        {
            ((object) null).IsNull().ShouldBeTrue();
        }


        [Fact]
        public void IsDefault_ReturnsFalse()
        {
            "test".IsDefault().ShouldBeFalse();
        }

        [Fact]
        public void IsDefault_returnsTrue()
        {
            default(object).IsDefault().ShouldBeTrue();
        }

        [Fact]
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