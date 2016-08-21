#region

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class ProcessExtensionsTests
    {
        [Test]
        public void IsRunning_ThrowsExceptionOnNullProcess()
        {
            Assert.Throws<NullReferenceException>(() => ((Process) null).IsRunning());
        }

        [Test]
        public void IsRunning_ReturnsTrue()
        {
            var proc = Process.GetProcesses()[0];
            Assert.True(proc.IsRunning());
        }

        [Test]
        public void IsRunning_ReturnsFalseOnNotRunningProcess()
        {
            new Process().IsRunning().ShouldBeFalse();

            var curDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var file = Path.Combine(curDir, "resources\\RunsInLoop.exe");
            var proc = Process.Start(file);
            Thread.Sleep(3000);
            proc.CloseMainWindow();
            Thread.Sleep(3000);
            Assert.False(proc.IsRunning());
        }

        [Test]
        public void StartInfoAsString_Throws()
        {
            typeof(NullReferenceException).ShouldBeThrownBy(() => ((Process) null).StartInfoAsString());
        }


        [Test]
        public void AsString_Throws()
        {
            typeof(NullReferenceException).ShouldBeThrownBy(() => ((ProcessStartInfo) null).AsString());
        }


        [Test]
        public void StartInfoAsString_ReturnsStringWithValues()
        {
            var psi = new ProcessStartInfo
            {
                WorkingDirectory = @"C:\Program Files (x86)\Notepad++",
                FileName = "notepad.exe",
                Arguments = @"C:\temp\1.bat"
            };

            var proc = new Process {StartInfo = psi};

            proc.StartInfoAsString()
                .ShouldEqual(
                    "File name: notepad.exe\nArguments: C:\\temp\\1.bat\nWorking directory: C:\\Program Files (x86)\\Notepad++");
        }

        [Test]
        public void StartInfoAsString_ReturnsStringWithEmptyValues()
        {
            var proc = new Process();

            proc.StartInfoAsString().ShouldEqual("File name: \nArguments: \nWorking directory: ");
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

            psi.AsString()
                .ShouldEqual(
                    "File name: notepad++.exe\nArguments: C:\\temp\\1.bat\nWorking directory: C:\\Program Files (x86)\\Notepad++");
        }
    }
}