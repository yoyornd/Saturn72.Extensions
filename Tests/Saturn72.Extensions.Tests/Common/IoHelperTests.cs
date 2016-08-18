using System.IO;
using Saturn72.Extensions.TestSdk;
using NUnit.Framework;

namespace Saturn72.Extensions.Tests.Common
{
    
    public class IoHelperTests:TestsBase
    {
        [Test]
        public void DeleteFile_Deletes()
        {
            var file = Path.GetTempFileName();

            IoUtil.DeleteFile(file);
            File.Exists(file).ShouldBeFalse();
        }
    }
}