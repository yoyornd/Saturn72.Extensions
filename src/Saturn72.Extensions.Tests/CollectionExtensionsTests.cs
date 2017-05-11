﻿#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;

#endregion

namespace Saturn72.Extensions.Tests
{
    public class CollectionExtensionsTests
    {
        [Test]
        public
        void AddIfNotExists_NotAddingExistsItem()
        {
            var source = new List<int> {1, 2, 3};
            source.AddIfNotExist(3);

            var all = source.Where(x => x == 3);
            all.Count().ShouldBe(1);
        }

        [Test]
        public
        void AddIfNotExists_AddsNewItem()
        {
            var source = new List<int> {1, 2, 3};
            source.AddIfNotExist(4);
            source.ShouldContain(4);
        }


        [Test]
        public
        void AddIfNotExists_ThrowsOnNullItemToAdd()
        {
            Should.Throw<NullReferenceException>(() => (null as IList<object>)
                .AddIfNotExist(new object()));
        }
    }
}