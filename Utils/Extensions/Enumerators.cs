using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Extensions
{
    public static class EnumeratorExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> set, Action<T> work)
        { foreach (var item in set) work(item); }
        public static IEnumerable<T2> ForEach<T1, T2>(this IEnumerable<T1> set, Func<T1, T2> work)
        {
            var resultSet = new List<T2>();
            foreach (var item in set)
                resultSet.Add(work(item));
            return resultSet;
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> set) =>
            set ?? Enumerable.Empty<T>();
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> set) =>
            set == null || !set.Any();
    }
}
