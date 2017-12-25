using System;
using System.Diagnostics;

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
    }
}