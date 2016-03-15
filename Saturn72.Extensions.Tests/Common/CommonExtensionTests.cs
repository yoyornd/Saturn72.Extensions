using System.Collections.Generic;
using System.ComponentModel;
using Saturn72.Extensions.Common;
using Saturn72.Extensions.TestSdk;
using Xunit;

namespace Saturn72.Extensions.Tests.Common
{
    public class CommonExtensionTests
    {
        [Fact]
        public void GetCustomTypeConverter_ConvertsDecimal()
        {
            var converter = CommonHelper.GetCustomTypeConverter(typeof (decimal));
            converter.ShouldBe<DecimalConverter>();
            converter.ConvertFrom("4.20").ShouldEqual((decimal)4.2);
        }

        [Fact]
        public void GetCustomTypeConverter_CopnvertsInt32()
        {
            var converter = CommonHelper.GetCustomTypeConverter(typeof (int));
            converter.ShouldBe<Int32Converter>();
            converter.ConvertFrom("1").ShouldEqual(1);
        }

        [Fact]
        public void GetCustomTypeConverter_ReturnsListConverter()
        {
            CommonHelper.GetCustomTypeConverter(typeof (List<string>)).ShouldBe<GenericListTypeConverter<string>>();
        }
    }
}