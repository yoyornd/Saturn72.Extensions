#region

using System;
using System.Diagnostics;

#endregion

namespace Saturn72.Extensions
{
    public static class ProcessExtensions
    {
        public static bool IsRunning(this Process process)
        {
            if (process == null)
                throw new NullReferenceException("process");

            try
            {
                return Process.GetProcessById(process.Id) != null;
            }
            catch (InvalidOperationException)
            {
            }
            catch (ArgumentException)
            {
            }
            return false;
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
    }
}