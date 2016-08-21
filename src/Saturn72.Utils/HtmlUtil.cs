#region

using System.Text.RegularExpressions;

#endregion

namespace Saturn72.Extensions
{
    public class HtmlUtil
    {
        public static string RemoveHtmlEscapes(string source)
        {
            var tmp = Regex.Replace(source, @"<[^>]+>|&nbsp;", "").Trim();
            return Regex.Replace(tmp, @"\s{2,}", " ");
        }
    }
}