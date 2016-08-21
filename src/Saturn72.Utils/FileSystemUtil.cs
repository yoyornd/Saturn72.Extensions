#region

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

#endregion

namespace Saturn72.Extensions
{
    public static class FileSystemUtil
    {
        /// <summary>
        ///     Deletes directory context
        /// </summary>
        /// <param name="path">path to directory</param>
        public static void DeleteDirectoryContent(string path)
        {
            if (!Directory.Exists(path))
                return;
            foreach (var dir in Directory.GetDirectories(path))
                Directory.Delete(dir, true);

            DeleteAllDirectoryFiles(path);
        }

        /// <summary>
        ///     Deletes all directory files
        /// </summary>
        /// <param name="path">path to directory</param>
        /// <param name="ignoredFileExtensions">
        ///     files extensions to be ignored, seperated comma delimited
        ///     <example>FileSystemUtil.DeleteAllDirectoryFiles(@"C:\temp", "xml, json, txt")</example>
        /// </param>
        public static void DeleteAllDirectoryFiles(string path, string ignoredFileExtensions = null)
        {
            if (!Directory.Exists(path))
                return;
            var ignoredExtensions = IsNullEmptyOrWhiteSpaces(ignoredFileExtensions)
                ? ignoredFileExtensions.Split(',').Where(x => IsNullEmptyOrWhiteSpaces(x)).Select(x => x.Trim())
                : new string[] {};
            var files =
                Directory.GetFiles(path).Where(f => !ignoredExtensions.Contains(Path.GetExtension(f).Substring(1)));

            foreach (var file in files)
                File.Delete(file);
        }


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

        private static bool IsNullEmptyOrWhiteSpaces(string source)
        {
            return string.IsNullOrWhiteSpace(source) || string.IsNullOrEmpty(source);
        }
    }
}