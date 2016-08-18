using System;
using System.Security;
using System.Text.RegularExpressions;
using System.Web;

namespace Saturn72.Extensions
{
    public class NetCommonHelper
    {
        public bool IsTrustLevel(AspNetHostingPermissionLevel permissionLevel)
        {
            return GetTrustLevel() == permissionLevel;
        }

        /// <summary>
        ///     Finds the trust level of the running application
        ///     (http://blogs.msdn.com/dmitryr/archive/2007/01/23/finding-out-the-current-trust-level-in-asp-net.aspx)
        /// </summary>
        /// <returns>The current trust level.</returns>
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            //set minimum
            var trustLevel = AspNetHostingPermissionLevel.None;
            foreach (AspNetHostingPermissionLevel level in Enum.GetValues(typeof (AspNetHostingPermissionLevel)))
            {
                try
                {
                    new AspNetHostingPermission(level).Demand();
                    trustLevel = level;
                    break; //we've set the highest permission we can
                }
                catch (SecurityException)
                {
                }
            }
            return trustLevel;
        }

        /// <summary>
        ///     Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email,
                "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$",
                RegexOptions.IgnoreCase);
            return result;
        }
    }
}