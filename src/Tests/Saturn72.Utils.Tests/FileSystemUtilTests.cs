#region

using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class FileSystemUtilTests
    {
        [Test]
        public void DeleteDirectoryContent_throws()
        {
            var notExistsPath = Path.Combine(Directory.GetCurrentDirectory(),
                DateTime.Now.ToString("F").Replace(":", "-"));
            typeof(DirectoryNotFoundException).ShouldBeThrownBy(
                () => FileSystemUtil.DeleteDirectoryContent(notExistsPath));
        }
        [Test]
        public void DeleteDirectoryContent_DeleteDirContent()
        {
            var dirPath = Path.GetTempFileName();
            DeleteFileIfExists(dirPath);

            for (int i = 0; i < 5; i++)
            {
                var subDir = Path.Combine(dirPath, i.ToString());
                Directory.CreateDirectory(subDir);

                for (int j = 10 - i; j < 0; j--)
                    File.CreateText(Path.Combine(subDir, string.Format("{0}.txt", j))).Close();
            }
            Thread.Sleep(500);

            try
            {
                FileSystemUtil.DeleteDirectoryContent(dirPath);
                Directory.GetDirectories(dirPath).Length.ShouldEqual(0);
                Directory.GetFiles(dirPath).Length.ShouldEqual(0);
            }
            finally
            {
                Directory.Delete(dirPath);
            }
        }
        [Test]
        public void DirectoryExists_throws()
        {
            //dir not exists = 
            var notExistsPath = Path.Combine(Directory.GetCurrentDirectory(), DateTime.Now.ToString("F").Replace(":", "-"));
            typeof(DirectoryNotFoundException).ShouldBeThrownBy(() => FileSystemUtil.DirectoryExists(notExistsPath, true));
        }

        [Test]
        public void DirectoryExists_Returns_False()
        {
            //dir not exists = 
            var notExistsPath = Path.Combine(Directory.GetCurrentDirectory(), DateTime.Now.ToString("F").Replace(":", "-"));
            FileSystemUtil.DirectoryExists(notExistsPath).ShouldBeFalse();
        }

        [Test]
        public void DirectoryExists_ReturnsTrue()
        {
            //dir not exists = 
            var path = Directory.GetCurrentDirectory();
            FileSystemUtil.DirectoryExists(path).ShouldBeTrue();
        }

        [Test]
        public void MoveFile_Throws()
        {
            //on null source
            typeof(NullReferenceException).ShouldBeThrownBy(() => FileSystemUtil.MoveFile(null, null));
            //on empty source
            typeof(ArgumentException).ShouldBeThrownBy(() => FileSystemUtil.MoveFile(string.Empty, null));
            //on whitespace
            typeof(ArgumentException).ShouldBeThrownBy(() => FileSystemUtil.MoveFile("   ", null));

            //on null destination
            typeof(NullReferenceException).ShouldBeThrownBy(() => FileSystemUtil.MoveFile("ttt", null));
            //on empty destination
            typeof(ArgumentException).ShouldBeThrownBy(() => FileSystemUtil.MoveFile("ttt", string.Empty));
            //on not exist source
            typeof(ArgumentException).ShouldBeThrownBy(() => FileSystemUtil.MoveFile("ttt", "   "));

            //Source file not exists
            var file = Path.GetTempFileName();
            Thread.Sleep(1000);
            File.Delete(file);
            typeof(FileNotFoundException).ShouldBeThrownBy(() => FileSystemUtil.MoveFile(file, "FFF"));
        }

        [Test]
        public void MoveFile_FileMoved()
        {
            //Source file not exists
            var src = Path.GetTempFileName();
            var dest = Path.GetTempFileName();
            Thread.Sleep(300);

            try
            {
                File.Delete(dest);
                FileSystemUtil.MoveFile(src, dest);
                File.Exists(src).ShouldBeFalse();
                File.Exists(dest).ShouldBeTrue();
            }
            finally

            {
                DeleteFileIfExists(src);
                DeleteFileIfExists(dest);
            }
        }

        [Test]
        public void RelativeToAbsolute_NonBackslashedPath()
        {
            var relativePath = "Plugins";
            var result = FileSystemUtil.RelativePathToAbsolutePath(relativePath);

            var expectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            expectedPath.ShouldEqual(result);
        }

        [Test]
        public void RelativeToAbsolute_DotAsPath()
        {
            FileSystemUtil.RelativePathToAbsolutePath(".").ShouldEqual(AppDomain.CurrentDomain.BaseDirectory);
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