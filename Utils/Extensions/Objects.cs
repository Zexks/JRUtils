using System;
using System.Collections.Specialized;

namespace Utils.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object o) =>
                    o is null;

        public static NameValueCollection ToNVC(this object[] data)
        {
            var result = new NameValueCollection();
            for (int i = 0; i < data.Length - 1; i++)
                result.Add(data[i].ToString(), data[++i].ToString());
            return result;
        }
        public static T CastByEx<T>(this object o) => (T)o;
        
        public static T To<T>(this object o) =>
            (o is null || o is DBNull ? (T)Convert.ChangeType(o, typeof(DBNull))
            : (T)Convert.ChangeType(o, typeof(T)));
        public static T ToOrNull<T>(this object o) where T : class
        { try { return (T)o; } catch { return null; } }
        public static T ToOrDefault<T>(this object o)
        { try { return (T)o; } catch { return default(T); } }
        public static bool ToOrNot<T>(this object o, out T newO)
        { try { newO = (T)o; return true; } catch { newO = default(T); return false; } }
    }
}
