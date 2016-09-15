using System.Collections.Generic;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

namespace Saturn72.Extensions.Tests
{
    public class ReflectionUtilTests
    {
        [Test]
        public void SetPropertyValueUsingReflection_insertsObject()
        {
            var mc = new MyClass();
            ReflectionUtil.SetPropertyValueUsingReflection(mc, "string", "test");
            mc.String.ShouldEqual("test");

            var v = new MyClass {String = "internal"};
            ReflectionUtil.SetPropertyValueUsingReflection(mc, "object", v);
            mc.Object.ShouldEqual(v);


            var list = new[] {1, 2, 3, 4};
            ReflectionUtil.SetPropertyValueUsingReflection(mc, "intenum", list);
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