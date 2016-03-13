using System;

namespace Saturn72.Extensions.Common
{
    public static class UriUtil
    {
        public static Uri Combine(string baseUri, string relativeUri)
        {
            Guard.HasValue(baseUri, () => { throw new ArgumentException("baseUri"); });

            var headUri = new Uri(baseUri);

            if (!relativeUri.HasValue())
                return headUri;

            Uri result;

            var pass = Uri.TryCreate(headUri, relativeUri, out result);
            if (!pass)
                throw new ArgumentException("relativeUri");

            return result;
        }
    }
}