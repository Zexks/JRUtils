using System;
using System.Collections.Generic;

namespace Utils.Extensions
{
    public static class RandomExtensions
    {
        public static bool Bool(this Random rnd) => Convert.ToBoolean(rnd.Next(2));
        public static void ReSeed(this Random rnd) => rnd = new Random(DateTime.Now.Millisecond);
        public static T OneOf<T>(this Random rnd, params T[] set) => set[rnd.Next(set.Length)];

        public static string GetWord(this Random rnd, int length = 5)
        {
            var result = string.Empty;
            var vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            var consonants = new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };

            for (int x = 0; x < length; x++)
                result += (rnd.Bool() ? vowels[rnd.Next(0, vowels.Length)] : consonants[rnd.Next(0, consonants.Length)]);

            return result;
        }
        public static string GetSentence(this Random rnd, int num = 10)
        {
            var words = new List<string>();
            for (int x = 0; x < num; x++)
                words.Add(rnd.GetWord());
            return string.Join(" ", words) + ".".ToSentenceCase();
        }

        #region Date functions
        public static int Year(this Random rnd) =>
            rnd.Year(DateTime.MaxValue.Year);
        public static int Year(this Random rnd, int max) =>
            rnd.Year(DateTime.MinValue.Year, max);
        public static int Year(this Random rnd, int min, int max) =>
            rnd.Next((min < 0 ? 0 : min), (max > 9999 ? 9999 : max));

        public static int Month(this Random rnd) =>
            rnd.Month(1, 13);
        public static int Month(this Random rnd, int max) =>
            rnd.Month(1, max);
        public static int Month(this Random rnd, int min, int max) =>
            rnd.Next(min, max);

        public static int Day(this Random rnd, int month) =>
            rnd.Day(month, 1, 50);
        public static int Day(this Random rnd, int month, int max) =>
            rnd.Day(month, 1, max);
        public static int Day(this Random rnd, int month, int min, int max)
        {
            var days = DateTime.Parse($"0001/{month}/01").GetDaysThisMonth();
            return rnd.Next(min, max > days ? days : max);
        }

        public static int Hours12(this Random rnd) =>
            rnd.Hours12(13);
        public static int Hours12(this Random rnd, int max) =>
            rnd.Hours12(1, max);
        public static int Hours12(this Random rnd, int min, int max) =>
            rnd.Next((min < 1 ? 1 : min), (max > 13 ? 13 : max));

        public static int Hours24(this Random rnd) =>
            rnd.Hours24(24);
        public static int Hours24(this Random rnd, int max) =>
            rnd.Hours24(0, max);
        public static int Hours24(this Random rnd, int min, int max) =>
            rnd.Next((min < 0 ? 0 : min), (max > 24 ? 24 : max));

        public static int Minutes(this Random rnd) =>
            rnd.Seconds(60);
        public static int Minutes(this Random rnd, int max) =>
            rnd.Seconds(0, max);
        public static int Minutes(this Random rnd, int min, int max) =>
            rnd.Seconds(min, max);

        public static int Seconds(this Random rnd) =>
            rnd.Seconds(60);
        public static int Seconds(this Random rnd, int max) =>
            rnd.Seconds(0, max);
        public static int Seconds(this Random rnd, int min, int max) =>
            rnd.Next((min < 0 ? 0 : min), (max > 60 ? 60 : max));

        public static DateTime Date(this Random rnd) =>
            rnd.Date(DateTime.MinValue, DateTime.MaxValue);
        public static DateTime Date(this Random rnd, DateTime min, DateTime max)
        {
            var month = rnd.Month(min.Month, max.Month);
            var date = $"{rnd.Year(min.Year, max.Year).ToString().PadLeft(4, '0')}/{month}/{rnd.Day(month, min.Day, max.Day)}";
            return DateTime.Parse(date);
        }

        public static TimeSpan Time(this Random rnd) =>
            rnd.Time(DateTime.MinValue.TimeOfDay, DateTime.MaxValue.TimeOfDay);
        public static TimeSpan Time(this Random rnd, TimeSpan max) =>
            rnd.Time(DateTime.MinValue.TimeOfDay, max);
        public static TimeSpan Time(this Random rnd, TimeSpan min, TimeSpan max) =>
            new TimeSpan(rnd.Hours24(min.Hours, max.Hours),
                         rnd.Minutes(min.Minutes, max.Minutes),
                         rnd.Seconds(min.Seconds, max.Seconds));

        public static DateTime Date_Time(this Random rnd) =>
            rnd.Date_Time(DateTime.MinValue, DateTime.MaxValue);
        public static DateTime Date_Time(this Random rnd, DateTime min, DateTime max) =>
            rnd.Date(min, max) + rnd.Time(min.TimeOfDay, max.TimeOfDay);

        public static DayOfWeek DayOWeek(this Random rnd) =>
            rnd.Bool() ? DayOfWeek.Sunday :
            rnd.Bool() ? DayOfWeek.Monday :
            rnd.Bool() ? DayOfWeek.Tuesday :
            rnd.Bool() ? DayOfWeek.Wednesday :
            rnd.Bool() ? DayOfWeek.Thursday :
            rnd.Bool() ? DayOfWeek.Friday : DayOfWeek.Saturday;

        #endregion
    }
}
