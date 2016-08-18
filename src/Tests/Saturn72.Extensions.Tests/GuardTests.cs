using System;
using System.Collections.Generic;

using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

namespace Saturn72.Extensions.Tests
{
    public class GuardTests
    {
        [Test]
        public void MustFollows_DoesNotTriggerAction()
        {
            var str = "test";
            Guard.MustFollow(str.Length == 4, () => str = str.ToUpper());
           "test".ShouldEqual(str);
        }

        [Test]
        public void MustFollows_TriggerAction()
        {
            var str = "test";
            Guard.MustFollow(() => str.Length == 0, () => str = str.ToUpper());
            "TEST".ShouldEqual(str);
        }

        [Test]
        public void NotEmpty_TriggersAction()
        {
            var x = 0;
            Guard.NotEmpty(new List<string>(), () => x++);
            1.ShouldEqual(x);
        }

        [Test]
        public void HasValue_DoesNotTriggersAction()
        {
            var x = 0;
            Guard.HasValue("test", () => x++);
            0.ShouldEqual(x);
        }
        [Test]
        public void HasValue_TriggersAction()
        {
            var x = 0;
            Guard.HasValue("", () => x++);
            1.ShouldEqual(x);
        }
        [Test]
        public void HasValue_ThrowsExceptionOnEmptyString()
        {
            typeof (ArgumentNullException).ShouldBeThrownBy(
                () => Guard.HasValue("", () => { throw new ArgumentNullException(); }));
        }

        [Test]
        public void NotNull_ThrowsNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => Guard.NotNull((object) null));
        }

        [Test]
        public void NotNull_ThrowsNullReferenceExceptionWithMessage()
        {
            typeof (NullReferenceException).ShouldBeThrownBy(() => Guard.NotNull((object) null, "message"), "message");
        }

        [Test]
        public void NotNull_TriggerAction()
        {
            var x = 0;
            Guard.NotNull((object) null, () => x++);
            1.ShouldEqual(x);
        }
    }
}