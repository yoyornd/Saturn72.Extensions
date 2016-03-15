
using System;
using System.Diagnostics;
using System.IO;
using Saturn72.Extensions.Common;
using Xunit;

namespace Saturn72.Extensions.Tests.Utils
{
    public class IoUtilTests
    {
        [Fact]
        public void RelativeToAbsolute_NonBackslashedPath()
        {
            var relativePath = "Plugins";
            var result = IoUtil.RelativePathToAbsolutePath(relativePath);

            var expectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            Assert.Equal(expectedPath, result);
        }
    }
}
