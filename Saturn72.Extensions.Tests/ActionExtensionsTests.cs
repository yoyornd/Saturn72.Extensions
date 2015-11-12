using System;
using Saturn72.Extensions.UnitTesting;
using Xunit;

namespace Saturn72.Extensions.Tests
{
    public class ActionExtensionsTests
    {
        [Fact]
        public void Run_InvokesAction()
        {
            var i = 0;
            ((Action) (() => i++)).Run();

            i.ShouldEqual(1);
        }

        [Fact]
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
        

        [Fact]
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

        [Fact]
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