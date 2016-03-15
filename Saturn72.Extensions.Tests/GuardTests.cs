using System;
using System.Collections.Generic;
using Saturn72.Extensions.TestSdk;
using Xunit;

namespace Saturn72.Extensions.Tests
{
    public class GuardTests
    {
        [Fact]
        public void MustFollows_DoesNotTriggerAction()
        {
            var str = "test";
            Guard.MustFollow(str.Length == 4, () => str = str.ToUpper());
            Assert.Equal("test", str);
        }

        [Fact]
        public void MustFollows_TriggerAction()
        {
            var str = "test";
            Guard.MustFollow(() => str.Length == 0, () => str = str.ToUpper());
            Assert.Equal("TEST", str);
        }

        [Fact]
        public void NotEmpty_TriggersAction()
        {
            var x = 0;
            Guard.NotEmpty(new List<string>(), () => x++);
            1.ShouldEqual(x);
        }

        [Fact]
        public void HasValue_DoesNotTriggersAction()
        {
            var x = 0;
            Guard.HasValue("test", () => x++);
            0.ShouldEqual(x);
        }
        [Fact]
        public void HasValue_TriggersAction()
        {
            var x = 0;
            Guard.HasValue("", () => x++);
            1.ShouldEqual(x);
        }
        [Fact]
        public void HasValue_ThrowsExceptionOnEmptyString()
        {
            typeof (ArgumentNullException).ShouldBeThrownBy(
                () => Guard.HasValue("", () => { throw new ArgumentNullException(); }));
        }

        [Fact]
        public void NotNull_ThrowsNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => Guard.NotNull((object) null));
        }

        [Fact]
        public void NotNull_ThrowsNullReferenceExceptionWithMessage()
        {
            typeof (NullReferenceException).ShouldBeThrownBy(() => Guard.NotNull((object) null, "message"), "message");
        }

        [Fact]
        public void NotNull_TriggerAction()
        {
            var x = 0;
            Guard.NotNull((object) null, () => x++);
            1.ShouldEqual(x);
        }
    }
}