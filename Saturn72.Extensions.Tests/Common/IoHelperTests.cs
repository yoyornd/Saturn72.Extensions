using System.IO;
using Saturn72.Extensions.UnitTesting;
using Xunit;

namespace Saturn72.Extensions.Tests.Common
{
    public class IoHelperTests
    {
        [Fact]
        public void DeleteFile_Deletes()
        {
            var file = Path.GetTempFileName();

            IoUtil.DeleteFile(file);
            File.Exists(file).ShouldBeFalse();
        }
    }
}