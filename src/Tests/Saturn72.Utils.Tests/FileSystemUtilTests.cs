#region

using System;
using System.IO;
using NUnit.Framework;
using Saturn72.Extensions;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class FileSystemUtilTests
    {
        [Test]
        public void RelativeToAbsolute_NonBackslashedPath()
        {
            var relativePath = "Plugins";
            var result = FileSystemUtil.RelativePathToAbsolutePath(relativePath);

            var expectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            expectedPath.ShouldEqual(result);
        }

        [Test]
        public void FileExists_ReturnTrueOnExistsFile()
        {
            var fileName = Path.GetTempFileName();
            var res = FileSystemUtil.FileExists(fileName);
            File.Delete(fileName);
            res.ShouldBeTrue();
        }


        [Test]
        public void FileExists_ReturnFalseOnNotExistsFile()
        {
            var fileName = @"C:\temp\NotExistsFile.txt";
            DeleteFileIfExists(fileName);

            Assert.False(FileSystemUtil.FileExists(fileName));
        }

        [Test]
        public void FileExists_OnNotExistsPath()
        {
            var fileName = Path.GetTempFileName();
            DeleteFileIfExists(fileName);
            FileSystemUtil.FileExists(fileName).ShouldBeFalse();
            //throws
            typeof(FileNotFoundException).ShouldBeThrownBy(() => FileSystemUtil.FileExists(fileName, true));
        }

        [Test]
        public void FileExists_Throws_OnEmptyPath()
        {
            var fileName = "";
            FileSystemUtil.FileExists(fileName).ShouldBeFalse();
            //throws
            typeof(FileNotFoundException).ShouldBeThrownBy(() => FileSystemUtil.FileExists(fileName, true));
        }

        [Test]
        public void FileExists_OnBadPath()
        {
            var fileName = "dddddd//dsdfsdfsdfsdfsdf";
            FileSystemUtil.FileExists(fileName).ShouldBeFalse();
            //throws
            typeof(FileNotFoundException).ShouldBeThrownBy(() => FileSystemUtil.FileExists(fileName, true));
        }

        [Test]
        public void DeleteFile_Deletes()
        {
            var file = Path.GetTempFileName();

            FileSystemUtil.DeleteFile(file);
            var res = File.Exists(file);

            //we delete here in order to prevent file stay in file system on dailure
            DeleteFileIfExists(file);

            res.ShouldBeFalse();
        }

        #region Utilities

        private static void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        #endregion
    }
}