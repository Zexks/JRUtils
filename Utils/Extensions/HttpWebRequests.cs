using System;
using System.Net;

namespace Utils.Extensions
{
    public static class HttpWebRequestExtensions
    {
        public static void Use(this HttpWebRequest wc, Action<HttpWebRequest> work)
        {
            work(wc);
            wc.GetResponse().Close();
        }
    }
}
