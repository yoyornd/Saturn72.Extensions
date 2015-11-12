using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Saturn72.Extensions
{
    public static class EnumerableExtension
    {
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !(!source.IsNull() && source.Any());
        }

        public static bool IsIEnumerableofType(this Type type)
        {
            var genArgs = type.GetGenericArguments();
            if (genArgs.Length == 1 && typeof (IEnumerable<>).MakeGenericType(genArgs).IsAssignableFrom(type))
                return true;
            return type.BaseType != null && IsIEnumerableofType(type.BaseType);
        }

        public static void ForEachItem<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var src in source)
                action(src);
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, params T[] second)
        {
            return !second.Any() ? first : first.Concat(second.ToList());
        }

        public static bool HasValues(this IEnumerable source)
        {
            return source.GetEnumerator().MoveNext();
        }

        /// <summary>
        ///     Return the max of enumerable by property value
        /// </summary>
        /// <typeparam name="T">Enumerable generic type</typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source">The enumerable</param>
        /// <param name="selector">selector to poerform on enumerable item</param>
        public static T MaxOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return source.IsEmpty() ? default(T) : SortByOrder(source, selector, false).FirstOrDefault();
        }

        /// <summary>
        ///     Gets the max IEnumerable object by internal property
        /// </summary>
        /// <typeparam name="TSource">Enumerable first object</typeparam>
        /// <typeparam name="TKey">Property type to set the maxby on</typeparam>
        /// <param name="source">IEnumerable</param>
        /// <param name="selector">Proerty selector</param>
        /// <returns>TSource</returns>
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            return source.MaxBy(selector, Comparer<TKey>.Default);
        }

        /// <summary>
        ///     Gets the max IEnumerable object by internal property
        /// </summary>
        /// <typeparam name="TSource">enumerable first object</typeparam>
        /// <typeparam name="TKey">Property type to set the maxby on</typeparam>
        /// <param name="source">The IEnumerable to iterate on</param>
        /// <param name="selector">the Selector</param>
        /// <param name="comparer">Comparator</param>
        /// <returns>TSource</returns>
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            Guard.NotNull(new object[] { source, selector, comparer});

            var sourceIterator = source.GetEnumerator();
            if (!sourceIterator.MoveNext())
                throw new InvalidOperationException("Sequence contains no elements");

            var max = sourceIterator.Current;
            var maxKey = selector(max);

            while (sourceIterator.MoveNext())
            {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, maxKey) <= 0) continue;

                max = candidate;
                maxKey = candidateProjected;
            }
            sourceIterator.Dispose();

            return max;
        }

        /// <summary>
        ///     Return the min of enumerable by property value
        /// </summary>
        /// <typeparam name="T">Enumerable generic type</typeparam>
        /// <param name="source">The enumerable</param>
        /// <param name="keySelector">selector to porform on enumerable item</param>
        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
        {
            return SortByOrder(source, keySelector, true).FirstOrDefault();
        }

        private static IEnumerable<T> SortByOrder<T, TKey>(IEnumerable<T> source, Func<T, TKey> keySelector,
          bool inAscendOrder)
        {
            Guard.NotNull(source, nameof(source));
            Guard.NotEmpty(source, nameof(source));

            return inAscendOrder
                ? source.OrderBy(keySelector)
                : source.OrderByDescending(keySelector);
        }
    }
}