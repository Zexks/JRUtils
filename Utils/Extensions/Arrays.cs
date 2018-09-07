using System;
using System.Linq;

namespace Utils.Extensions
{
    public static class ArraysExtensions
    {
        /// <summary>
        /// Linq like function to ADD elements to an Array object.
        /// </summary>
        /// <typeparam name="T">The array Type.</typeparam>
        /// <param name="set">The input array.</param>
        /// <param name="item">The item to be added to the array.</param>
        public static void Add<T>(this T[] set, T item)
        {
            var list = set.ToList();
            list.Add(item);
            set = list.ToArray();
        }
        /// <summary>
        /// Linq like function to REMOVE elements to an Array object.
        /// </summary>
        /// <typeparam name="T">The array Type.</typeparam>
        /// <param name="set">The input array.</param>
        /// <param name="item">The item to be removed from the array.</param>
        public static void Remove<T>(this T[] set, T item)
        {
            var list = set.ToList();
            list.Remove(item);
            set = list.ToArray();
        }
    }
}
