using System;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

namespace Saturn72.Extensions.Tests
{
    public class RuntimeUtilTests
    {
        [Test]
        public void ObjectExtensions_GetStackTraceFrame_Throws()
        {
            //On depth too deep
            typeof(ArgumentException).ShouldBeThrownBy(
                () => RuntimeUtil.GetStackTraceFrame(new StackTrace().GetFrames().Length*2));
        }

        [Test]
        public void ObjectExtensions_GetStackTraceFrame()
        {
            var currentFrameCount = new StackTrace().FrameCount;
            var res = RuntimeUtil.GetStackTraceFrame(currentFrameCount/2);
            res.ShouldNotBeNull();
            res.ShouldBe<StackFrame>();
        }


        [Test]
        public void ObjectExtensions_GetCallerMethod()
        {
            var res = RuntimeUtil.GetCallerMethodInfo();
            (res is MethodBase).ShouldBeTrue();
            res.ShouldNotBeNull();
        }
    }
}