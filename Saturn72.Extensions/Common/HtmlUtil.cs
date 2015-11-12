
using System.Text.RegularExpressions;

namespace Saturn72.Extensions.Utils
{
    public class HtmlUtil
    {
        public static string RemoveHtmlEscapes(string source)
        {
            return Regex.Replace(source, @"<[^>]+>|&nbsp;", "").Trim().RemoveDuplicateWhiteSpaces();
        }
    }
}
