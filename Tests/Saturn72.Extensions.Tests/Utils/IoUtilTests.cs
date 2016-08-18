
using System;
using System.IO;
using Saturn72.Utils;
using Saturn72.Extensions.TestSdk;

namespace Saturn72.Extensions.Tests.Utils
{
    public class IoUtilTests
    {
        public void RelativeToAbsolute_NonBackslashedPath()
        {
            var relativePath = "Plugins";
            var result = FileSystemObject.RelativePathToAbsolutePath(relativePath);

            var expectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            expectedPath.ShouldEqual(result);
        }
    }
}
