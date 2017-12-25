#region

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using Shouldly;
using Xunit;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class ProcessExtensionsTests
    {
       [Fact]
        public void IsRunning_ThrowsExceptionOnNullProcess()
        {
            Should.Throw<NullReferenceException>(() => ((Process)null).IsRunning());

        }

       [Fact]
        public void IsRunning_ReturnsTrue()
        {
            var proc = Process.GetProcesses()[0];
            proc.IsRunning().ShouldBeTrue();
        }

        [Fact]
        public void IsRunning_ReturnsFalseOnNotRunningProcess()
        {
            new Process().IsRunning().ShouldBeFalse();
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            string path = Uri.UnescapeDataString(new UriBuilder(codeBase).Path);
            var exe = Path.Combine(Path.GetDirectoryName(path), "Resources\\SimpleApp.exe");

            var proc = Process.Start(exe);
            Thread.Sleep(2000);
            proc.CloseMainWindow();
            Thread.Sleep(1000);
            proc.IsRunning().ShouldBeFalse();
        }
    }
}
