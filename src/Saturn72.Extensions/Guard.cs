#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#endregion

namespace Saturn72.Extensions
{
    public static class Guard
    {
        private const string MustFollowDefaultMessage =
            "The object does not follows the given rule.\nSee call stack for details";

        public static void NotNull(object[] objects)
        {
            foreach (var obj in objects)
                NotNull(obj, "Object is null: " + nameof(obj));
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
            NotNull(source, message);
            MustFollow(source.HasValue,
                () => { throw new ArgumentException(message, "source"); });
        }

        public static void HasValue(string source, Action action)
        {
            MustFollow(source.HasValue, action);
        }

        public static void NotEmpty<T>(IEnumerable[] source)
        {
            foreach (var s in source)
                NotEmpty(s);
        }

        public static void NotEmpty(IEnumerable source)
        {
            NotEmpty(source, "The source sequence is empty.");
        }

        public static void NotEmpty(IEnumerable source, Action notEmptyAction)
        {
            MustFollow(!source.IsNull() && source.GetEnumerator().MoveNext(), notEmptyAction);
        }

        public static void NotEmpty(IEnumerable source, string message)
        {
            NotEmpty(source, () => { throw new ArgumentException(message); });
        }

        public static void FileExists(string fileName)
        {
            FileExists(fileName, fileName);
        }

        public static void FileExists(string fileName, string message)
        {
            MustFollow(() => File.Exists(fileName), () => { throw new FileNotFoundException(message); });
        }

        public static void FileExists(string fileName, Action notFoundAction)
        {
            MustFollow(() => File.Exists(fileName), notFoundAction);
        }

        public static void DirectoryExists(string path)
        {
            DirectoryExists(path, path);
        }

        public static void DirectoryExists(string path, string message)
        {
            MustFollow(() => Directory.Exists(path), () => { throw new DirectoryNotFoundException(message); });
        }

        public static void DirectoryExists(string path, Action notFoundAction)
        {
            MustFollow(() => Directory.Exists(path), notFoundAction);
        }

        public static void ContainsKey<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        {
            NotNull(dictionary);

            MustFollow(() => dictionary.ContainsKey(key),
                () => { throw new KeyNotFoundException("The dictionary does not contain key ({0})".AsFormat(key)); });
        }
        public static void EqualsTo(IComparable arg1, IComparable arg2)
        {
            MustFollow(arg1.CompareTo(arg2) == 0);
        }

        public static void EqualsTo(IComparable arg1, IComparable arg2, string message)
        {
            MustFollow(arg1.CompareTo(arg2) == 0, message);
        }

        public static void EqualsTo(IComparable arg1, IComparable arg2, Action errorAction)
        {
            MustFollow(arg1.CompareTo(arg2) == 0, errorAction);
        }


        public static void GreaterThan(IComparable arg1, IComparable arg2)
        {
            MustFollow(arg1.CompareTo(arg2) > 0);
        }

        public static void GreaterThan(IComparable arg1, IComparable arg2, string message)
        {
            MustFollow(arg1.CompareTo(arg2) > 0, message);
        }

        public static void GreaterThan(IComparable arg1, IComparable arg2, Action errorAction)
        {
            MustFollow(arg1.CompareTo(arg2) > 0, errorAction);
        }

        public static void GreaterThanOrEqualsTo(IComparable arg1, IComparable arg2)
        {
            MustFollow(arg1.CompareTo(arg2) >= 0);
        }

        public static void GreaterThanOrEqualsTo(IComparable arg1, IComparable arg2, string message)
        {
            MustFollow(arg1.CompareTo(arg2) >= 0, message);
        }

        public static void GreaterThanOrEqualsTo(IComparable arg1, IComparable arg2, Action errorAction)
        {
            MustFollow(arg1.CompareTo(arg2) >= 0, errorAction);
        }


        public static void SmallerThan(IComparable arg1, IComparable arg2)
        {
            MustFollow(arg1.CompareTo(arg2) < 0);
        }

        public static void SmallerThan(IComparable arg1, IComparable arg2, string message)
        {
            MustFollow(arg1.CompareTo(arg2) < 0, message);
        }

        public static void SmallerThan(IComparable arg1, IComparable arg2, Action errorAction)
        {
            MustFollow(arg1.CompareTo(arg2) < 0, errorAction);
        }

        public static void SmallerThanOrEqualsTo(IComparable arg1, IComparable arg2)
        {
            MustFollow(arg1.CompareTo(arg2) <= 0);
        }

        public static void SmallerThanOrEqualsTo(IComparable arg1, IComparable arg2, string message)
        {
            MustFollow(arg1.CompareTo(arg2) <= 0, message);
        }

        public static void SmallerThanOrEqualsTo(IComparable arg1, IComparable arg2, Action errorAction)
        {
            MustFollow(arg1.CompareTo(arg2) <= 0, errorAction);
        }
    }
}