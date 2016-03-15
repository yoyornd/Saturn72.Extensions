using System.Collections.Generic;
using Saturn72.Extensions.Common;
using Saturn72.Extensions.TestSdk;
using Xunit;

namespace Saturn72.Extensions.Tests.Common
{
    public class ReflectionHelperTests
    {
        [Fact]
        public void SetPropertyValueUsingReflection_insertsObject()
        {
            var mc = new MyClass();
            ReflectionHelper.SetPropertyValueUsingReflection(mc, "string", "test");
            mc.String.ShouldEqual("test");

            var v = new MyClass {String = "internal"};
            ReflectionHelper.SetPropertyValueUsingReflection(mc, "object", v);
            mc.Object.ShouldEqual(v);


            var list = new[] {1, 2, 3, 4};
            ReflectionHelper.SetPropertyValueUsingReflection(mc, "intenum", list);
            mc.IntEnum.ShouldEqual(list);
        }

        internal class MyClass
        {
            public string String { get; set; }
            public object Object { get; set; }
            public IEnumerable<int> IntEnum { get; set; }
        }
    }
}