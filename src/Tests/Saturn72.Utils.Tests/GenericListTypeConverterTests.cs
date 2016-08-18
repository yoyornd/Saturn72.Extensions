#region

using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Utils.Tests
{
    public class GenericListTypeConverterTests
    {
        [Test]
        public void GetCustomTypeConverter_ConvertsDecimal()
        {
            var converter = CommonHelper.GetCustomTypeConverter(typeof(decimal));
            converter.ShouldBe<DecimalConverter>();
            converter.ConvertFrom("4.20").ShouldEqual((decimal) 4.2);
        }

        [Test]
        public void GetCustomTypeConverter_CopnvertsInt32()
        {
            var converter = CommonHelper.GetCustomTypeConverter(typeof(int));
            converter.ShouldBe<Int32Converter>();
            converter.ConvertFrom("1").ShouldEqual(1);
        }

        [Test]
        public void GetCustomTypeConverter_ReturnsListConverter()
        {
            CommonHelper.GetCustomTypeConverter(typeof(List<string>)).ShouldBe<GenericListTypeConverter<string>>();
        }
    }
}