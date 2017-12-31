using System;

namespace Saturn72.Extensions
{
    public static class UriExtensions
    {
        public static Uri Combine(this Uri baseUri, string relativePath)
        {
            if (relativePath.StartsWith("/"))
                relativePath = relativePath.Substring(1);
            var uriBuilder = new UriBuilder(baseUri);
            uriBuilder.Path += relativePath;

            return uriBuilder.Uri;
        }
    }
}
