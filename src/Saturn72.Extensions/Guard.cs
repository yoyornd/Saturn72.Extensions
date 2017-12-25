#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace Saturn72.Extensions
{
    public static class Guard
    {
        private const string MustFollowDefaultMessage =
            "The object does not follows the given rule.\nSee call stack for details";

        public static void Equals<T>(T source, T destination) where T: IComparable
        {
            Equals(source, destination, string.Format("{0} and {1} are not equal", source, destination));
        }
        public static void Equals<T>(T source, T destination, string message) where T : IComparable
        {
            MustFollow(source.Equals(destination), ()=> throw new InvalidOperationException(message));
        }

        public static void Equals<T>(T source, T destination, Action ifNotEqualAction) where T : IComparable
        {
            MustFollow(source.Equals(destination), ifNotEqualAction);
        }

        public static void GreaterThan<T>(T greater, T than) where T : IComparable
        {
            GreaterThan(greater, than, string.Format("{0} is not greater than {1}", greater, than));
        }

        public static void GreaterThan<T>(T greater, T than, string message) where T : IComparable
        {
            GreaterThan(greater, than, () => { throw new ArgumentOutOfRangeException(message); });
        }

        public static void GreaterThan<T>(T greater, T than, Action ifNotGreaterAction) where T : IComparable
        {
            MustFollow(() => greater.CompareTo(than) > 0, ifNotGreaterAction);
        }
        
        public static void GreaterOrEqualTo<T>(T greater, T than) where T : IComparable
        {
            GreaterOrEqualTo(greater, than, string.Format("{0} is not greater or equal than {1}", greater, than));
        }

        public static void GreaterOrEqualTo<T>(T greater, T than, string message) where T : IComparable
        {
            GreaterOrEqualTo(greater, than, () => { throw new ArgumentOutOfRangeException(message); });
        }
        public static void GreaterOrEqualTo<T>(T greater, T than, Action ifNotGreaterOrEqualAction) where T : IComparable
        {
            MustFollow(() => greater.CompareTo(than) >= 0, ifNotGreaterOrEqualAction);
        }

        public static void SmallerThan<T>(T smaller, T than) where T : IComparable
        {
            SmallerThan(smaller, than, string.Format("{0} is not smaller than {1}", smaller, than));
        }

        public static void SmallerThan<T>(T smaller, T than, string message) where T : IComparable
        {
            SmallerThan(smaller, than, () => { throw new ArgumentOutOfRangeException(message); });
        }

        public static void SmallerThan<T>(T smaller, T than, Action ifNotSmallerThanAction) where T : IComparable
        {
            MustFollow(() => smaller.CompareTo(than) < 0, ifNotSmallerThanAction);
        }
        
        public static void SmallerOrEqualTo<T>(T smaller, T than) where T : IComparable
        {
            SmallerOrEqualTo(smaller, than, string.Format("{0} is not smaller or equal than {1}", smaller, than));
        }

        public static void SmallerOrEqualTo<T>(T smaller, T than, string message) where T : IComparable
        {
            SmallerOrEqualTo(smaller, than, () => { throw new ArgumentOutOfRangeException(message); });
        }
        public static void SmallerOrEqualTo<T>(T smaller, T than, Action ifNotSmallerOrEqualAction) where T : IComparable
        {
            MustFollow(() => smaller.CompareTo(than) <= 0, ifNotSmallerOrEqualAction);
        }

        public static void NotNull(object[] objects)
        {
            foreach (var obj in objects)
                NotNull(obj, "Object is null: obj");
        }

        public static void NotNull<T>(T obj) where T : class
        {
            NotNull(obj, "Object is null: obj");
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
            MustFollow(perdicate, MustFollowDefaultMessage);
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


        public static void MustFollow(bool condition)
        {
            MustFollow(condition, MustFollowDefaultMessage);
        }

        public static void MustFollow(bool condition, Action ifNotFollowsAction)
        {
            if (!condition)
                ifNotFollowsAction();
        }

        public static void MustFollow(bool condition, string message)
        {
            MustFollow(condition, () => { throw new InvalidOperationException(message); });
        }

        public static void HasValue(string source)
        {
            HasValue(source, "String value required");
        }

        public static void HasValue(string source, string message)
        {
            HasValue(source, () => { throw new ArgumentException(message, nameof(source)); });
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
            NotEmpty(source, "The source sequence is empty.");
        }

        public static void NotEmpty<T>(IEnumerable<T> source, Action notEmptyAction)
        {
            MustFollow(!source.IsNull() && source.Any(), notEmptyAction);
        }

        public static void NotEmpty<T>(IEnumerable<T> source, string message)
        {
            NotEmpty(source, () => { throw new ArgumentException(message); });
        }

        public static void FileExists(string fileName)
        {
            FileExists(fileName, fileName);
        }

        public static void FileExists(string fileName, string message)
        {
            FileExists(fileName, () => { throw new FileNotFoundException(message); });
        }

        public static void FileExists(string fileName, Action notFoundAction)
        {
            MustFollow(() => File.Exists(fileName), notFoundAction);
        }

        public static void ContainsKey<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key)
        {
            MustFollow(() => source.Select(x => x.Key).ToArray().Contains(key),
                () =>
                {
                    throw new KeyNotFoundException(string.Format("The source does not contain key ({0})", key));
                });
        }
    }
}