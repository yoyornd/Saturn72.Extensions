
using Saturn72.Extensions;
using Shouldly;
using System;
using Xunit;

namespace Saturn72.Extensions.Tests
{
    public class UriExtensionsTests
    {
        [Theory]
        [InlineData("relative/url")]
        [InlineData("/relative/url")]
        public void UriExtensions_Combine(string relativeUri)
        {
            var sourceUri = "http://www.saturn72.com";
            var uri = new Uri(sourceUri);
            var cUri = uri.Combine(relativeUri);
            var expUri = (sourceUri + "/" + relativeUri).Replace("com//", "com/");
            cUri.ToString().ShouldBe(expUri);
        }
    }
}
