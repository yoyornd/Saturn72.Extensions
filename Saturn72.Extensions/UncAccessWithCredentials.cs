using System;
using System.Runtime.InteropServices;

namespace Saturn72.Extensions
{
    /// <summary>
    ///     Enable access to network resource
    ///     <remarks>
    ///         Copied from :
    ///         https://github.com/Santhoshattillid/BethesdaConsentForms/blob/master/BethesdaConsentFormWCFSvc/UNCAccessWithCredentials.cs
    ///     </remarks>
    /// </summary>
    public class UncAccessWithCredentials : IDisposable
    {
        private bool _disposed;
        private string _sDomain;
        private string _sPassword;

        private string _sUncPath;
        private string _sUser;

        /// <summary>
        ///     The last system error code returned from NetUseAdd or NetUseDel.  Success = 0
        /// </summary>
        public int LastError { get; private set; }

        public void Dispose()
        {
            if (!_disposed)
            {
                NetUseDelete();
            }
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        [DllImport("NetApi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern uint NetUseAdd(
            string UncServerName,
            uint Level,
            ref USE_INFO_2 Buf,
            out uint ParmError);

        [DllImport("NetApi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern uint NetUseDel(
            string UncServerName,
            string UseName,
            uint ForceCond);

        public bool NetUseWithCredentials(string uncPath, string user, string password)
        {
            string[] domainNames;
            var usernameIndex = 0;

            if (user.Contains("@"))
            {
                domainNames = user.Split('@');
            }
            else
            {
                if (user.Contains("\\"))
                {
                    domainNames = user.Split('\\');
                    usernameIndex = 1;
                }
                else throw new ArgumentException(@"Please specify user in domain (username@domain or domain\username");
            }

            return NetUseWithCredentials(uncPath, domain: domainNames[1 - usernameIndex], domainUser: domainNames[usernameIndex], password: password);
        }

        /// <summary>
        ///     Connects to a UNC path using the credentials supplied.
        /// </summary>
        /// <param name="uncPath">Fully qualified domain name UNC path</param>
        /// <param name="domain">Domain of User.</param>
        /// <param name="domainUser">A user with sufficient rights to access the path.</param>
        /// <param name="password">Password of User</param>
        /// <returns>True if mapping succeeds.  Use LastError to get the system error code.</returns>
        public bool NetUseWithCredentials(string uncPath, string domain, string domainUser, string password)
        {
            _sUncPath = uncPath;
            _sUser = domainUser;
            _sPassword = password;
            _sDomain = domain;
            return NetUseWithCredentials();
        }

        private bool NetUseWithCredentials()
        {
            try
            {
                var useinfo = new USE_INFO_2
                {
                    ui2_remote = _sUncPath,
                    ui2_username = _sUser,
                    ui2_domainname = _sDomain,
                    ui2_password = _sPassword,
                    ui2_asg_type = 0,
                    ui2_usecount = 1
                };

                uint paramErrorIndex;
                var returncode = NetUseAdd(null, 2, ref useinfo, out paramErrorIndex);
                LastError = (int) returncode;
                return returncode == 0;
            }
            catch
            {
                LastError = Marshal.GetLastWin32Error();
                return false;
            }
        }

        /// <summary>
        ///     Ends the connection to the remote resource
        /// </summary>
        /// <returns>True if it succeeds.  Use LastError to get the system error code</returns>
        public bool NetUseDelete()
        {
            try
            {
                var returncode = NetUseDel(null, _sUncPath, 2);
                LastError = (int) returncode;
                return (returncode == 0);
            }
            catch
            {
                LastError = Marshal.GetLastWin32Error();
                return false;
            }
        }

        ~UncAccessWithCredentials()
        {
            Dispose();
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct USE_INFO_2
        {
            internal string ui2_local;
            internal string ui2_remote;
            internal string ui2_password;
            internal uint ui2_status;
            internal uint ui2_asg_type;
            internal uint ui2_refcount;
            internal uint ui2_usecount;
            internal string ui2_username;
            internal string ui2_domainname;
        }
    }
}