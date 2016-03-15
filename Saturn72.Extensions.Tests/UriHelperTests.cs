using System;
using Saturn72.Extensions.Common;
using Xunit;

namespace Saturn72.Extensions.Tests
{
    public class UriHelperTests
    {
        [Fact]
        public void Combine_ReturnsBaseUriOnEmptyrelativeUrl()
        {
            var baseUri = @"http://www.test.com";
            Assert.Equal(new Uri(baseUri), UriUtil.Combine(baseUri, ""));
        }

        [Fact]
        public void Combine_ReturnsCombinedUri()
        {
            Assert.Equal(new Uri(@"http://www.test.com/rrr"), UriUtil.Combine(@"http://www.test.com", "rrr"));
        }

        [Fact]
        public void Combine_ReturnsCombinedUri_OnNoWWW()
        {
            Assert.Equal(new Uri("http://test.com/rrr"), UriUtil.Combine(@"http://test.com", "rrr"));
        }

        [Fact]
        public void Combine_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => UriUtil.Combine(null, "test"));
            Assert.Throws<ArgumentException>(() => UriUtil.Combine("", "test"));
            Assert.Throws<ArgumentException>(() => UriUtil.Combine("     ", "test"));
            Assert.Throws<UriFormatException>(() => UriUtil.Combine(@"Test", "test"));
            Assert.Throws<UriFormatException>(() => UriUtil.Combine(@"test.com", "test"));
            Assert.Throws<ArgumentException>(() => UriUtil.Combine(@"http://www.test.com", "\\\\\test"));
        }
    }
}