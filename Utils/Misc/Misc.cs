using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

using Utils.Extensions;

namespace Utils
{
    public static class Misc
    {
        /// <summary>
        /// Wait a set number of seconds.
        /// </summary>
        /// <param name="time">Number of seconds.</param>
        public static void Wait(int time) => Wait(time.To<float>());
        /// <summary>
        /// Wait a set number of seconds. (With millisecond precision)
        /// </summary>
        /// <param name="time">Number of seconds.</param>
        public static void Wait(float time) => Thread.Sleep((time * 1000).To<int>());

        public static void Poller(TimeSpan span, Action work) =>
            Poller(span, span, work);
        public static void Poller(TimeSpan init, TimeSpan reset, Action work) =>
            new Timer(state => work(), null, init, reset);
        public static void Poller(TimeSpan init, TimeSpan reset, Action<object> work, object arg) => 
            new Timer(state => work(state), arg, init, reset);
        
        //public static string RandomWord(int charSize = 5)
        //{
        //    var maxRepeats = 2;
        //    var word = string.Empty;
        //    var rnd = new Random(DateTime.Now.Millisecond);

        //    //Weights based on analysis here: http://en.algoritmy.net/article/40379/Letter-frequency-English
        //    var vwls = new Dictionary<char, float> {
        //        { 'a', 8.167f },
        //        { 'e', 12.702f },
        //        { 'i', 6.966f },
        //        { 'o', 7.507f },
        //        { 'u', 2.758f },
        //        { 'y', 1.974f }
        //    };
        //    var cons = new Dictionary<char, float> {
        //        { 'b', 1.492f },
        //        { 'c', 2.782f },
        //        { 'd', 4.253f },
        //        { 'f', 2.228f },
        //        { 'g', 2.015f },
        //        { 'h', 6.094f },
        //        { 'j', 0.153f },
        //        { 'k', 0.772f },
        //        { 'l', 4.025f },
        //        { 'm', 2.406f },
        //        { 'n', 6.749f },
        //        { 'p', 1.929f },
        //        { 'q', 0.095f },
        //        { 'r', 5.987f },
        //        { 's', 6.327f },
        //        { 't', 9.056f },
        //        { 'v', 0.978f },
        //        { 'w', 2.360f },
        //        { 'x', 0.150f },
        //        { 'z', 0.074f }
        //    };

        //    var ttlWeight = (cons.Values.Sum() + vwls.Values.Sum());
        //    var rndNum = rnd.NextDouble();

        //    bool getVowel()
        //    {
        //        var foundCons = 0;
        //        var foundVwls = 0;
        //        for (var x = word.Length - 1; x >= 0; x--)
        //        {
        //            foundCons += cons.Contains(word[x]).To<int>();
        //            foundVwls += vwls.Contains(word[x]).To<int>();

        //            if (foundCons > 0 && foundCons == maxRepeats) return true;
        //            else if (foundVwls > 0 && foundCons == maxRepeats) return false;
        //        }
        //        return false;
        //    }

        //    for (var c = charSize; c > 0; c--)
        //    {
        //        if ((c == charSize && rnd.Bool()) || getVowel())
        //            word += vwls[rnd.Next(0, vwls.Length)];
        //        else word += cons[rnd.Next(0, cons.Length)];
        //        Wait(.001f);
        //    }

        //    return word;
        //}
        
        /// <summary>
        /// Constructor enhancements. Since you can't extend constructors, this is a static class to add defaults and simplify commonly used object constructors.
        /// </summary>
        public static class Gets
        {
            public static TimeSpan TimeSpan(int days = 0, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0) =>
                new TimeSpan(days, hours, minutes, seconds, milliseconds);

        }

        public static class Arrays
        {
            public static void Add<T>(ref T[] set, T item)
            {
                var list = set.ToList();
                list.Add(item);
                set = list.ToArray();
            }
            public static void Remove<T>(ref T[] set, T item)
            {
                var list = set.ToList();
                list.Remove(item);
                set = list.ToArray();
            }
        }

        /// <summary>
        /// Simplified functions for dealing with ASP.Net web.config files.
        /// </summary>
        public static class WebConfig
        {
            /// <summary>
            /// Used to pull values from the appSettings node.
            /// </summary>
            /// <param name="name">The key value of a node.</param>
            /// <returns>The value parameter of specified Add node or an empty string if not found.</returns>
            public static string GetSetting(string name)
            {
                try { return ConfigurationManager.AppSettings.Get(name); }
                catch(Exception) { return string.Empty; }
            }
            /// <summary>
            /// Used to pull values from the connectionStrings node.
            /// </summary>
            /// <param name="name">The name value of a node.</param>
            /// <returns>The connectionString parameter of the specified Add node or an empty string if not found.</returns>
            public static string GetConnection(string name)
            {
                try { return ConfigurationManager.ConnectionStrings[name].ConnectionString; }
                catch(Exception) { return string.Empty; }
            }
        }

        /// <summary>
        /// Simple static Web method calls.
        /// </summary>
        public static class WebCalls
        {
            /// <summary>
            /// Simple GET web response.
            /// </summary>
            /// <param name="url">Url to GET response from.</param>
            /// <returns>The reponse of the GET opperation as a string.</returns>
            public static string Get(Uri url) {
                using (var wc = new WebClient())
                    return Encoding.ASCII.GetString(wc.DownloadData(url));
            }
            /// <summary>
            /// Simple POST web request.
            /// </summary>
            /// <param name="url">Url to POST to.</param>
            /// <param name="kvps">Parameter list for post as key:value pairs.</param>
            /// <returns>The response of the POST opperation as a string.</returns>
            public static string Post(string url, params object[] kvps) {
                var response = string.Empty;
                using (var wc = new WebClient())
                    response = Encoding.ASCII.GetString(wc.UploadValues(url, kvps.ToNVC()));
                return response;
            }

        }

        /// <summary>
        /// Custom conversion engine.
        /// </summary>
        public class TypeSwitch
        {
            /// <summary>
            /// Type conversion map where the Type is the Key and the Value is the function needed to convert an object from the Key type to another type.
            /// </summary>
            Dictionary<Type, Func<object, object>> matches = new Dictionary<Type, Func<object, object>>();

            /// <summary>
            /// Add a type conversion case to the matches map.
            /// </summary>
            /// <typeparam name="T1">The key type.</typeparam>
            /// <typeparam name="T2">The type to convert to.</typeparam>
            /// <param name="f">The function needed to convert the Key type into the specified type.</param>
            /// <returns>Returns this TypeSwitch object.</returns>
            public TypeSwitch Case<T1, T2>(Func<T2, T1> f)
            {
                matches.Add(typeof(T1), (x) => f((T2)x));
                return this;
            }
            /// <summary>
            /// Execute a convertion using the type map.
            /// </summary>
            /// <typeparam name="T">The key type we're converting.</typeparam>
            /// <param name="x">The object to be converted.</param>
            /// <returns>The converted input parameter as the specified type.</returns>
            public T Switch<T>(object x) => (T)matches[typeof(T)](x);
        }

    }
}
