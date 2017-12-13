using System;
using System.IO;
using Shouldly;
using Xunit;

namespace Saturn72.Extensions.Tests
{
    public class StreamExtensionsTests
    {
        [Fact]
        public void StreamExtensions_ToByteArray_ReadsStreamToByteArray()
        {
            var buffer = new byte[] {000, 001, 010, 011, 100, 101, 110, 111};
            var stream = new MemoryStream(buffer) as Stream;
            stream.ToByteArray().ShouldBe(buffer);
        }

        [Fact]
        public void StreamExtensions_ToByteArray_Throws()
        {
            Should.Throw<NullReferenceException>(() => ((Stream) null).ToByteArray());
        }
    }
}