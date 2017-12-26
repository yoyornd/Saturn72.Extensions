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
            var psi = new ProcessStartInfo
            {
                WorkingDirectory = Path.GetFullPath("Resources"),
                FileName = "dotnet",
                Arguments = "SampleApp.dll"
            };
            var proc = Process.Start(psi);
            proc.IsRunning().ShouldBeTrue();
            proc.Close();
            proc.IsRunning().ShouldBeFalse();
        }
    }
}
