using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Saturn72.Extensions
{
    public static class StringExtensions
    {
        public static bool IsUrl(this string source)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(source, UriKind.Absolute, out uriResult)
                && new[] { Uri.UriSchemeHttp, Uri.UriSchemeHttps, Uri.UriSchemeFile }.Contains(uriResult.Scheme);

            return result;
        }

        public static string RemoveNewLineEscape(this string str)
        {
            Guard.NotNull(str);

            return str.Replace(Environment.NewLine, " ").Replace("\n", " ").RemoveDuplicateWhiteSpaces();
        }
        public static bool EqualsTo(this string source, string compareTo, bool ignoreCases = true)
        {
            if (source == null && compareTo == null)
                return true;
            if (source == null || compareTo == null)
                return false;

            return ignoreCases
                ? string.Equals(source, compareTo, StringComparison.OrdinalIgnoreCase)
                : string.Equals(source, compareTo);
        }
        public static bool EqualsTo(this string first, string[] stringArray)
        {
            return stringArray.All(s=> first.EqualsTo(s));
        }

        public static string RemoveDuplicateWhiteSpaces(this string source)
        {
            return Regex.Replace(source, @"\s{2,}", " ");
        }

        public static string AsFormat(this string source, params object[] args)
        {
            return string.Format(source, args);
        }

        public static string AsFormat(this string source, IEnumerable<KeyValuePair<string, object>> args)
        {
            return source.AsFormat(args.ToArray());
        }

        public static string AsFormat(this string source, IDictionary<string, object> args)
        {
            return source.AsFormat(args.ToArray());
        }

        public static string AsFormat(this string source, params KeyValuePair<string, object>[] args)
        {
            return args.Aggregate(source,
                (current, keyValuePair) => current.Replace("{" + keyValuePair.Key + "}", keyValuePair.Value.ToString()));
        }

        public static bool HasValue(this string str)
        {
            return !string.IsNullOrEmpty(str) && str.RemoveAllWhiteSpaces().Length > 0;
        }

        public static string RemoveAllWhiteSpaces(this string str)
        {
            return str.RemoveAll(" ");
        }

        public static string RemoveAll(this string str, string toRemove)
        {
            return str.Replace(toRemove, string.Empty);
        }

        public static int[] GetAllIndexes(this string str, char chr)
        {
            var result = new List<int>();

            for (var i = 0; i < str.Length; i++)
                if (chr == str[i])
                    result.Add(i);
            return result.ToArray();
        }

        public static void ThrowExceptionOnEmpty<TInternalException>(this string str, string message,
            TInternalException tException = null)
            where TInternalException : Exception, new()
        {
            if (!str.HasValue())
                throw new ArgumentException(message, tException ?? new TInternalException());
        }

        public static void ThrowExceptionOnEmpty<TException>(this string str, TException tException = null)
            where TException : Exception, new()
        {
            if (!str.HasValue())
                throw tException ?? new TException();
        }

        /// <summary>
        ///     Replace the end of the string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="lastChar">The last char to replace from.</param>
        /// <param name="replacementString">Replacement string.</param>
        /// <returns></returns>
        public static string ReplaceFromLastToEnd(this string str, char lastChar, string replacementString)
        {
            return str.Replace(str.LastIndexOf(lastChar), str.Length, replacementString);
        }

        /// <summary>
        ///     Replace the end of the string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="toReplace">The last string to replace from.</param>
        /// <param name="replacementString">Replacement string.</param>
        /// <returns></returns>
        public static string ReplaceFromLastToEnd(this string str, string toReplace, string replacementString)
        {
            return str.Replace(str.LastIndexOf(toReplace), str.Length, replacementString);
        }

        /// <summary>
        ///     RemoveAll the end of the string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fromIndex">The last character index.</param>
        /// <returns>System_String</returns>
        public static string Replace(this string str, int fromIndex, int toIndex, string replacementString)
        {
            //verifying out-of-bound
            if (fromIndex > toIndex || fromIndex < 0 || fromIndex >= str.Length)
                return str;

            if (toIndex > str.Length)
                toIndex = str.Length;

            return str.Substring(0, fromIndex) + replacementString + str.Substring(toIndex, str.Length - toIndex);
        }

        public static string RemoveAll(this string str, params string[] toRemove)
        {
            toRemove.ForEachItem(t => str = str.Replace(t, string.Empty));
            return str;
        }

        /// <summary>
        ///     Converts string to Int32.
        /// </summary>
        /// <param name="str">string to be parsed.</param>
        /// <returns>int. If passed the string value, else 0</returns>
        public static int ToInt32(this string str)
        {
            int result;

            int.TryParse(str, out result);
            return result;
        }

        /// <summary>
        ///     Converts string to boolean.
        /// </summary>
        /// <param name="str">string to be parsed.</param>
        /// <returns>bool. true only if the string value is "true", else false.</returns>
        public static bool ToBoolean(this string str)
        {
            bool result;

            bool.TryParse(str, out result);
            return result;
        }

        public static bool Contains(this string str, string subString, bool ignoreCase, RegExOption regExOption)
        {
            if (ignoreCase)
            {
                str = str.ToLower();
                subString = subString.ToLower();
            }
            return Contains(str, subString, regExOption);
        }
        /// <summary>
        ///     checks if a sub string is contained in string.
        /// </summary>
        /// <param name="str">the string</param>
        /// <param name="subString">the sub string</param>
        /// <param name="regExOption">sub string place</param>
        /// <returns>bool. if the substring is empty or null - false is returned</returns>
        public static bool Contains(this string str, string subString, RegExOption regExOption)
        {
            //Case caould not be contained, null or empty
            if (!subString.HasValue() || subString.Length > str.Length)
                return false;

            switch (regExOption)
            {
                case RegExOption.StartsWith:
                    return str.StartsWith(subString);

                case RegExOption.MiddleContained:
                    return str.Contains(subString) && str.IndexOf(subString) > 1;

                case RegExOption.NotContained:
                    return !str.Contains(subString);

                case RegExOption.EndsWith:
                    return str.EndsWith(subString);
                case RegExOption.Equals:
                    return str == subString;
                default:
                    return false;
            }
        }
    }
}