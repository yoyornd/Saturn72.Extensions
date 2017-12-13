#region

using System;
using System.IO;
using NUnit.Framework;
using Shouldly;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class StreamExtensionsTests
    {
        [Test]
        public void ToByteArray_ReadsStreamToByteArray()
        {
            var buffer = new byte[] {000, 001, 010, 011, 100, 101, 110, 111};
            var stream = new MemoryStream(buffer) as Stream;
            stream.ToByteArray().ShouldBe(buffer);
        }

        [Test]
        public void ToByteArray_Throws()
        {
            Should.Throw<NullReferenceException>(() => ((Stream) null).ToByteArray());
        }
    }
}