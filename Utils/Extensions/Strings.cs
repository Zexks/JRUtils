using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace Utils.Extensions
{
    public static class StringExtensions
    {
        public static string IsNullIfEmpty(this string str) =>
            string.IsNullOrEmpty(str) ? null : str;
        public static bool IsNullOrEmpty(this string str) =>
            string.IsNullOrEmpty(str);

        public static string Frmt(this string str, params object[] args) =>
            string.Format(str, args);

        public static string SubToChar(this string str, char c) =>
            str.SubToChar(0, c);
        public static string SubToChar(this string str, int start, char c) =>
            str.Substring(start, str.IndexOf(c));

        public static IEnumerable<string> Split(this string str, int len)
        {
            if (str.IsNullOrEmpty())
                yield return string.Empty;

            for (var x = 0; x < str.Length; x += len)
                yield return str.Substring(x, Math.Min(len, str.Length - 1));
        }

        public static string DBSafe(this string str) => str.Replace("'", "''");

        public static Guid ToGuid(this string str) => new Guid(str);

        public static IEnumerable<int> IndexesOf(this string str, char c)
        {
            var last = 0;
            while (last < str.Length)
            {
                last = str.IndexOf(c, last);
                if (last < 0) break;
                yield return last++;
            }
        }

        public static T DeJSON<T>(this string str) =>
            new JavaScriptSerializer().Deserialize<T>(str);
        public static T DeXML<T>(this string str) =>
            (T)(new XmlSerializer(typeof(T)).Deserialize(new StringReader(str)));

        public static string ToSentenceCase(this string str)
        {
            str = str.ToLower();
            var set = str.Split('.');
            var result = string.Empty;
            
            foreach (var s in set)
                result += s.Substring(0, 1).ToUpper() + s.Substring(1);

            return result;
        }

        public static string ReplaceAt(this string str, int idx, char newChar) {
            var arr = str.ToCharArray();
            arr[idx] = newChar;
            return new string(arr);
        }

        public static T GetEnum<T>(this string str, bool ignoreCase = true) =>
            (T)Enum.Parse(typeof(T), str, ignoreCase);
    }
}
