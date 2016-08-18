#region

using System;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Utils.Tests
{
    public class UriUtilTests
    {
        [Test]
        public void Combine_ReturnsBaseUriOnEmptyrelativeUrl()
        {
            var baseUri = @"http://www.test.com";
            new Uri(baseUri).ShouldEqual(UriUtil.Combine(baseUri, ""));
        }

        [Test]
        public void Combine_ReturnsCombinedUri()
        {
            new Uri(@"http://www.test.com/rrr").ShouldEqual(UriUtil.Combine(@"http://www.test.com", "rrr"));
        }

        [Test]
        public void Combine_ReturnsCombinedUri_OnNoWWW()
        {
            new Uri("http://test.com/rrr").ShouldEqual(UriUtil.Combine(@"http://test.com", "rrr"));
        }

        [Test]
        public void Combine_ThrowsException()
        {
            typeof(ArgumentException).ShouldBeThrownBy(() => UriUtil.Combine(null, "test"));
            typeof(ArgumentException).ShouldBeThrownBy(() => UriUtil.Combine("", "test"));
            typeof(ArgumentException).ShouldBeThrownBy(() => UriUtil.Combine("     ", "test"));
            typeof(ArgumentException).ShouldBeThrownBy(() => UriUtil.Combine(@"http://www.test.com", "\\\\\test"));
            typeof(UriFormatException).ShouldBeThrownBy(() => UriUtil.Combine(@"Test", "test"));
            typeof(UriFormatException).ShouldBeThrownBy(() => UriUtil.Combine(@"test.com", "test"));
        }
    }
}