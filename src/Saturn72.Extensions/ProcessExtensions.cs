using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Saturn72.Extensions
{
    public static class ProcessExtensions
    {
        public static bool IsRunning(this Process process)
        {
            try
            {
                return Process.GetProcessById(process.Id).NotNull();
            }
            catch (InvalidOperationException)
            {
            }
            catch (ArgumentException)
            {
            }
            return false;
        }

        public static int WaitForExitAndReturnExitCode(this Process process, int timeout = 5000)
        {
            uint exitCode;
            if (!GetExitCodeProcess(process.Handle, out exitCode))
                throw new ExternalException("Process could not exit in give timeout.\nTimeout: {0}".AsFormat(timeout));

            return (int) exitCode;
        }

        public static string StartInfoAsString(this Process proc)

        {
            return AsString(proc.StartInfo);
        }

        public static string AsString(this ProcessStartInfo psi)
        {
            const string format = "File name: {0}\nArguments: {1}\nWorking directory: {2}";
            return string.Format(format, psi.FileName, psi.Arguments, psi.WorkingDirectory);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);
    }
}