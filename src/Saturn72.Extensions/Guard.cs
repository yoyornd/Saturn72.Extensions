using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Saturn72.Extensions
{
    public static class Guard
    {
        private const string MustFollowDefaultMessage = "The object does not follows the given rule.\nSee call stack for details";

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
            NotNull(source);
            MustFollow(source.HasValue,
                () => { throw new ArgumentException(message, "source"); });
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
        public static void ContainsKey<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        {
            NotNull(dictionary);

            MustFollow(() => dictionary.ContainsKey(key),
                () => { throw new KeyNotFoundException("The dictionary does not contain key ({0})".AsFormat(key)); });
        }
    }
}