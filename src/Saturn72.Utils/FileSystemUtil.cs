#region

using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;

#endregion

namespace Saturn72.Extensions
{
    public static class FileSystemUtil
    {
        public static void DeleteFile(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath) && !string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                File.Delete(filePath);
        }

        public static string RelativePathToAbsolutePath(string subFolder)
        {
            return HttpContext.Current == null
                ? FileSystemRelativePathToAbsolutePath(subFolder)
                : WebRelativePathToAbsolutePath(subFolder);
        }

        private static string WebRelativePathToAbsolutePath(string subFolder)
        {
            var rPath = subFolder.Replace("\\", "/");

            while (rPath.StartsWith("/") || rPath.StartsWith("~"))
                rPath = ReplaceIndexed(rPath, 0, 1, "");
            rPath = "~/" + rPath;

            return HttpContext.Current.Server.MapPath(rPath);
        }

        private static string FileSystemRelativePathToAbsolutePath(string subFolder)
        {
            var rPath = subFolder.Replace("/", "\\").Replace("~", string.Empty);
            while (rPath.StartsWith("\\"))
                rPath = ReplaceIndexed(rPath, 0, 1, "");

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rPath);
        }

        public static void DeleteDirectory(string directoryToDelete)
        {
            if (Directory.Exists(directoryToDelete))
                Directory.Delete(directoryToDelete, true);
        }

        public static void CreateDirectoryIfNotExists(string directoryToCreate)
        {
            if (Directory.Exists(directoryToCreate))
                return;

            Directory.CreateDirectory(directoryToCreate);
        }

        public static bool FileExists(string filename, bool throwException = false)
        {
            var result = File.Exists(filename);
            if (!result && throwException)
                throw new FileNotFoundException(filename);

            return result;
        }

        public static void MoveFileAsync(string source, string destination)
        {
            if (!File.Exists(source))
                throw new FileNotFoundException(source);

            var temp = Path.GetTempFileName();

            new Task(() =>
            {
                try
                {
                    if (FileExists(destination))
                    {
                        File.Delete(temp);
                        File.Move(destination, temp);
                    }
                    File.Move(source, destination);
                }
                catch (Exception)
                {
                    File.Move(temp, destination);
                }
            }).Start();
        }

        public static string GetFileNameFromUri(string uri)
        {
            return GetFileNameFromUri(new Uri(uri));
        }

        private static string GetFileNameFromUri(Uri uri)
        {
            if (!uri.IsFile)
                throw new UriFormatException("Cannot get file name from uri since it is not file. uri: " + uri);

            return Path.GetFileName(uri.AbsolutePath);
        }

        public static string ReplaceIndexed(string source, int fromIndex, int toIndex, string replacementString)
        {
            //verifying out-of-bound
            if (fromIndex > toIndex || fromIndex < 0 || fromIndex >= source.Length)
                return source;

            if (toIndex > source.Length)
                toIndex = source.Length;

            return source.Substring(0, fromIndex) + replacementString +
                   source.Substring(toIndex, source.Length - toIndex);
        }
    }
}