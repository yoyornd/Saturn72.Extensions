#region

using System;
using System.IO;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Utils.Tests
{
    public class FileSystemObjectTests
    {
        [Test]
        public void RelativeToAbsolute_NonBackslashedPath()
        {
            var relativePath = "Plugins";
            var result = FileSystemObject.RelativePathToAbsolutePath(relativePath);

            var expectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            expectedPath.ShouldEqual(result);
        }

        [Test]
        public void FileExists_ReturnTrueOnExistsFile()
        {
            var fileName = @"C:\temp\FileSystemObjectTests_file.txt";
            PrepareEnvironment(fileName);
            Assert.True(FileSystemObject.FileExists(fileName));

            File.Delete(fileName);
        }


        [Test]
        public void FileExists_ReturnFalseOnNotExistsFile()
        {
            var fileName = @"C:\temp\NotExistsFile.txt";
            DeleteFileIfExists(fileName);

            Assert.False(FileSystemObject.FileExists(fileName));
        }

        [Test]
        public void FileExists_OnNotExistsPath()
        {
            var fileName = Path.GetTempFileName();
            DeleteFileIfExists(fileName);
            FileSystemObject.FileExists(fileName).ShouldBeFalse();
            //throws
            typeof(FileNotFoundException).ShouldBeThrownBy(() => FileSystemObject.FileExists(fileName, true));
        }

        [Test]
        public void FileExists_Throws_OnEmptyPath()
        {
            var fileName = "";
            FileSystemObject.FileExists(fileName).ShouldBeFalse();
            //throws
            typeof(FileNotFoundException).ShouldBeThrownBy(() => FileSystemObject.FileExists(fileName, true));
        }

        [Test]
        public void FileExists_OnBadPath()
        {
            var fileName = "dddddd//dsdfsdfsdfsdfsdf";
            FileSystemObject.FileExists(fileName).ShouldBeFalse();
            //throws
            typeof(FileNotFoundException).ShouldBeThrownBy(() => FileSystemObject.FileExists(fileName, true));
        }

        [Test]
        public void DeleteFile_Deletes()
        {
            var file = Path.GetTempFileName();

            FileSystemObject.DeleteFile(file);
            var res = File.Exists(file);

            //we delete here in order to prevent file stay in file system on dailure
            DeleteFileIfExists(file);

            res.ShouldBeFalse();
        }

        #region Utilities

        private void PrepareEnvironment(string path)
        {
            DeleteFileIfExists(path);

            var stream = File.CreateText(path);
            stream.Close();
            if (!File.Exists(path))
                throw new FileNotFoundException(
                    "Setup failure: Failed to create file for FileExists_ReturnTrueOnExistsFile test. Path: " + path);
        }

        private static void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        #endregion
    }
}