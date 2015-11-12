using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace Saturn72.Extensions.Common
{
    public static class IoUtil
    {
        public static void DeleteFile(string filePath)
        {
            if (filePath.HasValue() && File.Exists(filePath))
                File.Delete(filePath);
        }

        public static string MapRelativePath(string subFolder)
        {
            return HttpContext.Current.IsNull()
                ? MapSimplePath(subFolder)
                : MapWebPath(subFolder);
        }

        private static string MapWebPath(string subFolder)
        {
            var rPath = subFolder.Replace("\\", "/");

            while (rPath.StartsWith("/") || rPath.StartsWith("~"))
                rPath = rPath.Replace(0, 1, "");
            rPath = "~/" + rPath;

            return HttpContext.Current.Server.MapPath(rPath);
        }

        private static string MapSimplePath(string subFolder)
        {
            var rPath = subFolder.Replace("/", "\\").RemoveAll("~");
            while (rPath.StartsWith("\\"))
                rPath = rPath.Replace(0, 1, "");
            return Path.Combine(Directory.GetCurrentDirectory(), rPath);
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
            IoHelper.FileMustExists(source);
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
                catch (Exception ex)
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
            Guard.MustFollow(() => uri.IsFile, "Cannot get file name from uri since it is not file. uri: " + uri);
            return Path.GetFileName(uri.AbsolutePath);
        }
    }
}