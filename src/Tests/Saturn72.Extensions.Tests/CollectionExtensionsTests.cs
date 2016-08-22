#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class CollectionExtensionsTests
    {
        [Test]
        public void AddIfNotExists_NotAddingExistsItem()
        {
            var source = new List<int> {1, 2, 3};
            source.AddIfNotExist(3);

            var all = source.Where(x => x == 3);
            all.Count().ShouldEqual(1);
        }

        [Test]
        public void AddIfNotExists_AddsNewItem()
        {
            var source = new List<int> {1, 2, 3};
            source.AddIfNotExist(4);

            source.Count.ShouldEqual(4);
            source.ShouldContainInstance(4);

            //Add null
            var col = new List<object> {new object(), new object()};
            col.AddIfNotExist(null);
            col.Last().ShouldBeNull();
            col.ShouldCount(3);
        }


        [Test]
        public void AddIfNotExists_Throws_OnNullCollection()
        {
            typeof(NullReferenceException).ShouldBeThrownBy(() => (null as IList<object>).AddIfNotExist(new object()));
        }
    }
}