using System;
using NUnit.Framework;
using Saturn72.Extensions.TestSdk;

namespace Saturn72.Extensions.Tests
{
    public class ActionExtensionsTests:TestsBase
    {
        [Test]
        public void Run_InvokesAction()
        {
            var i = 0;
            ((Action) (() => i++)).Run();

            i.ShouldEqual(1);
        }

        [Test]
        public void RunAll_InvokeAllActions()
        {
            var i = 0;

            new Action[]
            {
                () => i += 1,
                () => i += 2,
                () => i += 3
            }.RunAll();

            i.ShouldEqual(6);
        }
        

        [Test]
        public void RunAll()
        {
            var i = 10;

            new Action[]
            {
                () => i++,
                () => i += i,
                () => i *= 3
            }.RunAll();

            i.ShouldEqual(66);
        }

        [Test]
        public void RunAll_ThrowsException()
        {
            typeof(ArgumentNullException).ShouldBeThrownBy(() =>
                new Action[]
                {
                    () => { throw new ArgumentNullException(); }
                }.RunAll());
        }
    }
}