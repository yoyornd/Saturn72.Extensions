using System;
using Xunit;
using Shouldly;

namespace Saturn72.Extensions.Tests
{
    public class ObjectExtensionsTests
    {

        [Fact]
        public void IsNull_ReturnsTrueOnNullObject()
        {
            ((object)null).IsNull().ShouldBeTrue();
        }

        [Fact]
        public void IsNull_ReturnsFalseOnReferencedObject()
        {
            (new object()).IsNull().ShouldBeFalse();
        }

        [Fact]
        public void NotNull_ReturnsFalseOnNullObject()
        {
            ((object)null).NotNull().ShouldBeFalse();
        }

        [Fact]
        public void NotNull_ReturnsTrueOnReferencedObject()
        {
            (new object()).NotNull().ShouldBeTrue();
        }
        [Fact]
        public void GetPropertyValueByName_ThrowsOnNotExists()
        {
            var tc = new MyTestClass
            {
                StringValue = "CCCEEE",
                TestClass = new MyTestClass
                { StringValue = "internal value" }
            };

            Should.Throw<InvalidOperationException>(() => ObjectExtensionsFunctions.GetPropertyValueByName<string>(tc, "VVV"));
            Should.Throw<InvalidOperationException>(() => ObjectExtensionsFunctions.GetPropertyValueByName<MyTestClass>(tc, "testClass"));
        }

        [Fact]
        public void GetPropertyValueByName()
        {
            var tc = new MyTestClass
            {
                StringValue = "CCCEEE",
                TestClass = new MyTestClass
                { StringValue = "internal value" }
            };

            ObjectExtensionsFunctions.GetPropertyValueByName<string>(tc, "StringValue").ShouldBe(tc.StringValue);
            ObjectExtensionsFunctions.GetPropertyValueByName<MyTestClass>(tc, "TestClass").ShouldBe(tc.TestClass);
        }
    }
    public class MyTestClass
    {
        public string StringValue { get; set; }
        public MyTestClass TestClass { get; set; }
    }
}