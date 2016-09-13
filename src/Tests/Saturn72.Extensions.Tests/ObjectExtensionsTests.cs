using System;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

namespace Saturn72.Extensions.Tests
{
    public class ObjectExtensionsTests
    {
        [Test]
        public void ObjectExtensions_IsNull_ReturnsFalse()
        {
            Assert.False("text".IsNull());
        }

        [Test]
        public void ObjectExtensions_IsNull_ReturnsTrue()
        {
            Assert.True(((object) null).IsNull());
        }

        [Test]
        public void ObjectExtensions_IsDefault_ReturnsFalse()
        {
            Assert.False("TTT".IsDefault());
        }

        [Test]
        public void ObjectExtensions_IsDefault_ReturnsTrue()
        {
            Assert.True(default(object).IsDefault());
        }

        
        [Test]
        public void ObjectExtensions_GetStackTraceFrame_Throws()
        {
            //On depth too deep
            var currentFrameCount =  new StackTrace().FrameCount;
            typeof(ArgumentException).ShouldBeThrownBy(()=>default(object).GetStackTraceFrame(currentFrameCount*2));

            var o = new object();
            typeof(ArgumentException).ShouldBeThrownBy(() => o.GetStackTraceFrame(currentFrameCount * 2));
        }

        [Test]
        public void ObjectExtensions_GetStackTraceFrame()
        {
            var currentFrameCount = new StackTrace().FrameCount;
            var res = default(object).GetStackTraceFrame(currentFrameCount /2);
            res.ShouldNotBeNull();
            res.ShouldBe<StackFrame>();

             res = new object().GetStackTraceFrame(currentFrameCount/2);
            res.ShouldNotBeNull();
            res.ShouldBe<StackFrame>();
        }



        [Test]
        public void ObjectExtensions_GetCallerMethod()
        {
            var res = default(object).GetCallerMethodInfo();
            (res is MethodBase).ShouldBeTrue();
            res.ShouldNotBeNull();

            res = new object().GetCallerMethodInfo();
            (res is MethodBase).ShouldBeTrue();
            res.ShouldNotBeNull();
        }


    }
}