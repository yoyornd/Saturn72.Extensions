#region

using System;

#endregion

namespace Saturn72.Utils
{
    public static class UriUtil
    {
        public static Uri Combine(string baseUri, string relativeUri)
        {
            if (string.IsNullOrEmpty(baseUri) || string.IsNullOrWhiteSpace(baseUri))
                throw new ArgumentException("baseUri");

            var headUri = new Uri(baseUri);
            if (string.IsNullOrEmpty(relativeUri) || string.IsNullOrWhiteSpace(relativeUri))
                return headUri;

            Uri result;

            var pass = Uri.TryCreate(headUri, relativeUri, out result);
            if (!pass)
                throw new ArgumentException("relativeUri");

            return result;
        }
    }
}