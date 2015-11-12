using System;
using System.Collections.Generic;
using System.Linq;

namespace Saturn72.Extensions
{
    public static class Guard
    {
        public static void NotNull(object[] objects)
        {
            foreach (var obj in objects)
                NotNull(obj, "Object is null: "+ nameof(obj));
        }

        public static void NotNull<T>(T tObj) where T : class
        {
            NotNull(tObj, "Object is null: " + nameof(tObj));
        }

        public static void NotNull<T>(T tObj, string message) where T : class
        {
            NotNull(tObj, () => { throw new NullReferenceException(message); });
        }

        public static void NotNull<T>(T tObj, Action ifNotFollowsAction) where T : class
        {
            MustFollow(!tObj.IsNull(), ifNotFollowsAction);
        }

        public static void MustFollow(Func<bool> perdicate)
        {
            MustFollow(perdicate, "The object does not follows the given rule.\nSee call stack for details");
        }

        //TODO: replace generic exception with dedicated one which get call stack data
        public static void MustFollow(Func<bool> perdicate, string message)
        {
            MustFollow(perdicate, () => { throw new Exception(message); });
        }

        public static void MustFollow(Func<bool> perdicate, Action ifNotFollowsAction)
        {
            MustFollow(perdicate(), ifNotFollowsAction);
        }

        public static void MustFollow(bool perdicate, Action ifNotFollowsAction)
        {
            if (!perdicate)
                ifNotFollowsAction();
        }

        public static void MustFollow(bool condition, string message)
        {
            MustFollow(condition, () => { throw new InvalidOperationException(message); });
        }

        public static void HasValue(string source)
        {
            MustFollow(source.HasValue,
                () => { throw new ArgumentException("String has no value while required", "source"); });
        }

        public static void HasValue(string source, Action action)
        {
            MustFollow(source.HasValue, action);
        }

        public static void NotEmpty<T>(IEnumerable<T>[] source)
        {
            foreach (var s in source)
                NotEmpty(s);
        }

        public static void NotEmpty<T>(IEnumerable<T> source)
        {
            NotEmpty(source,"The source sequence is empty.");
        }

        public static void NotEmpty<T>(IEnumerable<T> source, Action notEmptyAction)
        {
            MustFollow(!source.IsNull() && source.Any(), notEmptyAction);
        }

        public static void NotEmpty<T>(IEnumerable<T> source, string message)
        {
            NotEmpty(source,
                    () => { throw new InvalidOperationException(message); });
        }
    }
}