#region

using System;
using System.Net.Http;

#endregion

namespace Saturn72.Extensions
{
    public static class HttpContentExtensions
    {
        public static T GetHttpContentDispositionProperty<T>(this HttpContent httpContent, Func<HttpContent, T> func)
        {
            ThrowOnNull(httpContent);
            return func(httpContent);
        }

        private static void ThrowOnNull(HttpContent httpContent)
        {
            var action = new Action<string, object>((objName, obj) =>
            {
                if (obj == null)
                    throw new NullReferenceException(objName);
            });

            action(nameof(httpContent), httpContent);
            action(nameof(httpContent.Headers), httpContent.Headers);
            action(nameof(httpContent.Headers.ContentDisposition), httpContent.Headers.ContentDisposition);
        }


        public static string GetContentDispositionName(this HttpContent httpContent)
        {
            return GetHttpContentDispositionProperty(httpContent,
                ht => ht.Headers.ContentDisposition.Name.Replace("\"", ""));
        }


        public static string GetContentDispositionFileName(this HttpContent httpContent)
        {
            return GetHttpContentDispositionProperty(httpContent,
                ht => ht.Headers.ContentDisposition.FileName.Replace("\"", ""));
        }
    }
}