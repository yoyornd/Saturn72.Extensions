using System.IO;
using System.Runtime.InteropServices;

namespace Saturn72.Extensions
{
    public static class IoHelper
    {
        public static void FileMustExists(string fileName)
        {
            Guard.MustFollow(() => File.Exists(fileName), () => new FileNotFoundException(fileName));
        }
        public static DirectoryInfo GetDirectoryInfo(string directoryPath)
        {
            Guard.NotEmpty(directoryPath);
            return new DirectoryInfo(directoryPath);
        }

        public static bool DirectoryExists(string path, bool throwException = false)
        {
            var result = Directory.Exists(path);
            if (!result && throwException)
                throw new DirectoryNotFoundException(path);

            return result;
        }

        public static bool DirectoryExists(string path, string username, string password, bool throwException = false)
        {
            var result = false;
            using (var unc = new UncAccessWithCredentials())
            {
                result = unc.NetUseWithCredentials(path, username, password);
            }

            if (!result && throwException)
                throw new DirectoryNotFoundException(path);

            return result;
        }


        public static void CreateDirectoryIfNotExists(string path)
        {
            if (!DirectoryExists(path))
                Directory.CreateDirectory(path);
        }

        [DllImport("shlwapi.dll")]
        private static extern bool PathIsNetworkPath(string pszPath);

        public static bool IsNetworkPath(string path)
        {
            return PathIsNetworkPath(path);
        }

        public static void CopyFolder(string sourceFolder, string destinationPath)
        {
            if (DirectoryExists(destinationPath))
                Directory.Delete(destinationPath, true);

            Directory.CreateDirectory(destinationPath);

            var srcDirInfo = new DirectoryInfo(sourceFolder);
            foreach (var fileInfo in srcDirInfo.GetFiles())
                CopyFile(fileInfo.FullName, destinationPath);

            foreach (var subDir in srcDirInfo.GetDirectories())
            {
                var nextDestinationPath = Path.Combine(destinationPath, subDir.Name);
                CopyFolder(subDir.FullName, nextDestinationPath);
            }
        }

        public static void CopyFile(string sourceFile, string destinationFolder, string newFileName = null)
        {
            CreateDirectoryIfNotExists(destinationFolder);
            var fInfo = new FileInfo(sourceFile);
            var fileName = newFileName.IsNull() ? fInfo.Name : newFileName;
            var destFileName = Path.Combine(destinationFolder, fileName);
            fInfo.CopyTo(destFileName, true);
        }
    }
}