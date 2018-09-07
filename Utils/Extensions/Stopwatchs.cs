using System;
using System.Diagnostics;

namespace Utils.Extensions
{
    public static class StopwatchExtensions
    {
        public static void Measure(this Stopwatch sw, Action work)
        {
            sw.Reset(); sw.Start();
            work(); sw.Stop();
        }
        public static void Measure(this Stopwatch sw, object arg, Action<object> work)
        {
            sw.Reset(); sw.Start();
            work(arg); sw.Stop();
        }
        
        public static T Measure<T>(this Stopwatch sw, Func<T> work)
        {
            sw.Reset(); sw.Start();
            var tmp = work();
            sw.Stop(); return tmp;
        }
        public static T Measure<T>(this Stopwatch sw, object arg, Func<object, T> work)
        {
            sw.Reset(); sw.Start();
            var tmp = work(arg);
            sw.Stop(); return tmp;
        }

        public static DateTime StopWithTime(this Stopwatch sw)
        { sw.Stop(); return DateTime.Now; }

        public static bool LT(this Stopwatch sw, int hours = 0, int minutes = 0, int seconds = 0) =>
            sw.LT(new TimeSpan(hours, minutes, seconds));
        public static bool LT(this Stopwatch sw, TimeSpan ts) =>
            sw.Elapsed < ts;

        public static bool LTE(this Stopwatch sw, int hours = 0, int minutes = 0, int seconds = 0) =>
            sw.LTE(new TimeSpan(hours, minutes, seconds));
        public static bool LTE(this Stopwatch sw, TimeSpan ts) =>
            sw.Elapsed <= ts;

        public static bool GT(this Stopwatch sw, int hours = 0, int minutes = 0, int seconds = 0) =>
            sw.GT(new TimeSpan(hours, minutes, seconds));
        public static bool GT(this Stopwatch sw, TimeSpan ts) =>
            sw.Elapsed > ts;

        public static bool GTE(this Stopwatch sw, int hours = 0, int minutes = 0, int seconds = 0) =>
            sw.GTE(new TimeSpan(hours, minutes, seconds));
        public static bool GTE(this Stopwatch sw, TimeSpan ts) =>
            sw.Elapsed >= ts;
    }
}
