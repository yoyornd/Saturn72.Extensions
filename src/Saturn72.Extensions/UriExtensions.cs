using System;

namespace Saturn72.Extensions
{
    public static class UriExtensions
    {
        public static Uri Combine(this Uri baseUri, string relativePath)
        {
            while(relativePath.StartsWith("/"))
                relativePath = relativePath.Substring(1);

            var uriBuilder = new UriBuilder(baseUri);
            uriBuilder.Path += "/";
            while (uriBuilder.Path.EndsWith("//"))
                uriBuilder.Path = uriBuilder.Path.Substring(0, uriBuilder.Path.Length - 1);

            uriBuilder.Path += relativePath;

            return uriBuilder.Uri;
        }

        public static Uri CombineAsQuery(this Uri baseUri, string queryParams)
        {
            if (queryParams.StartsWith("/"))
                queryParams = queryParams.Substring(1);
            var uriBuilder = new UriBuilder(baseUri);
            uriBuilder.Path += queryParams;

            return uriBuilder.Uri;
        }
    }
}
