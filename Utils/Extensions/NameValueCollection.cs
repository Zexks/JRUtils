using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extensions
{
    public static class NameValueCollectionExtensions
    {
        public static object[] ToArray(this NameValueCollection nvc)
        {
            var cnt = 0;
            var result = new object[nvc.Count * 2];
            for (var x = 0; x < nvc.Count; x++)
            { result[cnt++] = nvc.Keys[x]; result[cnt++] = nvc[x]; }
            return result;
        }
    }
}
