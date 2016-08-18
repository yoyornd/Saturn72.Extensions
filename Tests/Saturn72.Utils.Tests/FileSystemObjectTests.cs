#region

using System;
using System.IO;
using NUnit.Framework;
using Saturn72.Extensions.TestSdk;

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
        public void FileExists_ReturnsFalseOnNotExistsPath()
        {
            var fileName = @"C:\temp\NotExistsFile.txt";
            DeleteFileIfExists(fileName);

            Assert.False(FileSystemObject.FileExists(fileName));
        }

        [Test]
        public void FileExists_ThrowsExceptionOnEmptyPath()
        {
            var fileName = @"C:\temp\NotExistsFile.txt";
            DeleteFileIfExists(fileName);
            Assert.Throws<ArgumentException>(() => FileSystemObject.FileExists(fileName));
        }

        [Test]
        public void FileExists_ThrowsExceptionOnBadPath()
        {
            throw new NotImplementedException();
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