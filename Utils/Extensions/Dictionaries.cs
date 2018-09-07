using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

namespace Utils.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Convert a dictionary to a NameValueCollection object of the specifed key/value types.
        /// </summary>
        public static NameValueCollection ToNVC<TKey, TValue>(this Dictionary<TKey, TValue> dict) =>
            dict.Aggregate(new NameValueCollection(), (s, c) => { s.Add(c.Key.ToString(), c.Value.ToString()); return s; });
        /// <summary>
        /// Convert a dictionary to a simple object array
        /// </summary>
        public static object[] ToParams<TKey, TValue>(this Dictionary<TKey, TValue> dict) =>
            string.Join(",", dict.Select(kvp => $"{kvp.Key},{kvp.Value}").ToArray()).Split(',');
        /// <summary>
        /// Converts a dictionary into an HTTP url query string.
        /// </summary>
        public static string ToQueryString<TKey, TValue>(this Dictionary<TKey, TValue> dict) =>
            string.Join("&", dict.Select(kvp => $"{kvp.Key}={WebUtility.UrlEncode(kvp.Value.ToString())}"));
        /// <summary>
        /// Convert a dictionary into a string of key:value pairs separated by a specified deliminator (default of ';')
        /// </summary>
        /// <param name="delim">The character to use as the delimitor between key:value pairs.</param>
        public static string GetString<TKey, TValue>(this Dictionary<TKey, TValue> dict, string delim = ";") =>
            string.Join(delim, (from k in dict.Keys
                              select $"{k.ToString()}:{dict[k].ToString()}").ToArray());
        /// <summary>
        /// Converts a dictionary into a JSON collection object.
        /// </summary>
        /// <returns>JSON object representation of the dictionary.</returns>
        public static string ToJson<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            if (dict.Count < 1) return "[]";

            var tmp = new List<string>();
            foreach (var kvp in dict)
                tmp.Add($"{{\"{kvp.Key}\":\"{kvp.Value}\"}}");

            return "[" + string.Join(",", tmp) + "]";
        }
    }
}
