#region

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using Shouldly;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class ProcessExtensionsTests
    {
        [Test]
        public void IsRunning_ThrowsExceptionOnNullProcess()
        {
            Should.Throw<NullReferenceException>(() => ((Process) null).IsRunning());
           
        }

        [Test]
        public void IsRunning_ReturnsTrue()
        {
            var proc = Process.GetProcesses()[0];
            proc.IsRunning().ShouldBeTrue();
        }

        [Test]
        public void IsRunning_ReturnsFalseOnNotRunningProcess()
        {
            new Process().IsRunning().ShouldBeFalse();
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            string path = Uri.UnescapeDataString(new UriBuilder(codeBase).Path);
             var exe = Path.Combine(Path.GetDirectoryName(path),"Resources\\SimpleApp");

            var proc = Process.Start(exe);
            Thread.Sleep(2000);
            proc.CloseMainWindow();
            Thread.Sleep(1000);
            proc.IsRunning().ShouldBeFalse();
        }

        [Test]
        public void StartInfoAsString_Throws()
        {
            Should.Throw<NullReferenceException>(() => ((Process) null).StartInfoAsString());
        }


        [Test]
        public void AsString_Throws()
        {
            Should.Throw<NullReferenceException>(() => ((ProcessStartInfo) null).AsString());
        }


        [Test]
        public void StartInfoAsString_ReturnsStringWithValues()
        {
            var psi = new ProcessStartInfo
            {
                WorkingDirectory = @"C:\Program Files (x86)\Notepad++",
                FileName = "notepad++.exe",
                Arguments = @"C:\temp\1.bat"
            };

            var proc = new Process { StartInfo = psi };

            proc.StartInfoAsString().ShouldBe("File name: notepad++.exe\nArguments: C:\\temp\\1.bat\nWorking directory: C:\\Program Files (x86)\\Notepad++");
        }
        [Test]
        public void StartInfoAsString_ReturnsStringWithEmptyValues()
        {
            var proc = new Process();

            proc.StartInfoAsString().ShouldBe("File name: \nArguments: \nWorking directory: ");
        }

        [Test]
        public void AsString_ReturnsString()
        {
            var psi = new ProcessStartInfo
            {
                WorkingDirectory = @"C:\Program Files (x86)\Notepad++",
                FileName = "notepad++.exe",
                Arguments = @"C:\temp\1.bat"
            };

            psi.AsString().ShouldBe("File name: notepad++.exe\nArguments: C:\\temp\\1.bat\nWorking directory: C:\\Program Files (x86)\\Notepad++");
        }
    }
}