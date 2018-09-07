using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace Utils.Extensions
{
    public static class GenericExtensions
    {
        public static void Use<T>(this T o, Action<T> work) => work(o);
        public static T2 Use<T1, T2>(this T1 o, Func<T1, T2> work) => work(o);

        public static bool IsIn<T>(this T item, params T[] set) =>
            (item == null ? throw new ArgumentNullException("item") : set.Contains(item));

        public static bool IsIBetween<T>(this T actual, T lower, T upper) where T : IComparable<T> =>
            actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) <= 0;
        public static bool IsEBetween<T>(this T actual, T lower, T upper) where T : IComparable<T> =>
            actual.CompareTo(lower) > 0 && actual.CompareTo(upper) < 0;

        public static void AddSet<L, S>(this ICollection<L> list, params S[] vals) where S : L
        {
            foreach (S val in vals)
                list.Add(val);
        }

        public static T To<T>(this IConvertible o) =>
            (T)Convert.ChangeType(o, typeof(T));
        public static T ToOrNull<T>(this IConvertible o) where T : class
        { try { return (T)o; } catch { return null; } }
        public static T ToOrDefault<T>(this IConvertible o)
        { try { return (T)o; } catch { return default(T); } }
        public static bool ToOrNot<T>(this IConvertible o, out T newO)
        { try { newO = (T)o; return true; } catch { newO = default(T); return false; } }

        public static string SToXML<T>(this T o, string root)
        {
            var serializer = new XmlSerializer(typeof(T), root);
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, o);
                using (var reader = new StreamReader(stream.ResetPosition()))
                    return reader.ReadToEnd();
            }
        }
        public static string SToJSON<T>(this T o) =>
            new JavaScriptSerializer().Serialize(o);

        #region Math

        public static T Add<T>(this T x, T y)
        {
            dynamic dx = x, dy = y;
            return dx + dy;
        }
        public static T Subtract<T>(this T x, T y)
        {
            dynamic dx = x, dy = y;
            return dx - dy;
        }
        public static T Mulitply<T>(this T x, T y)
        {
            dynamic dx = x, dy = y;
            return dx * dy;
        }
        public static float Divide<T>(this T x, T y)
        {
            dynamic dx = x;
            return dx / Convert.ToDouble(y);
        }

        #endregion
    }
}
