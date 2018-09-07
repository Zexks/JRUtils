using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extensions
{
    public static class TimeSpanExtensions
    {
        public static bool LT(this TimeSpan ts, int hours = 0, int minutes = 0, int seconds = 0) =>
            ts.LT(new TimeSpan(hours, minutes, seconds));
        public static bool LT(this TimeSpan ts, TimeSpan ts1) =>
            ts < ts1;

        public static bool LTE(this TimeSpan ts, int hours = 0, int minutes = 0, int seconds = 0) =>
            ts.LTE(new TimeSpan(hours, minutes, seconds));
        public static bool LTE(this TimeSpan ts, TimeSpan ts1) =>
            ts <= ts1;

        public static bool GT(this TimeSpan ts, int hours = 0, int minutes = 0, int seconds = 0) =>
            ts.GT(new TimeSpan(hours, minutes, seconds));
        public static bool GT(this TimeSpan ts, TimeSpan ts1) =>
            ts > ts1;

        public static bool GTE(this TimeSpan ts, int hours = 0, int minutes = 0, int seconds = 0) =>
            ts.GTE(new TimeSpan(hours, minutes, seconds));
        public static bool GTE(this TimeSpan ts, TimeSpan ts1) =>
            ts >= ts1;

        public static string ToWords(this TimeSpan ts, bool withMilliseconds = false)
        {
            bool hasDays = ts.Days > 0,
                 hasHours = ts.Hours > 0,
                 hasMinutes = ts.Minutes > 0,
                 hasSeconds = ts.Seconds > 0,
                 hasMillisec = ts.Milliseconds > 0;

            string lblDays = $"day{(ts.Days == 1 ? "" : "s")}",
                   lblHours = $"hour{(ts.Hours == 1 ? "" : "s")}",
                   lblMinutes = $"minute{(ts.Minutes == 1 ? "" : "s")}",
                   lblSeconds = $"second{(ts.Seconds == 1 ? "" : "s")}";

            return $"{(hasDays ? $"{ts.Days} {lblDays}" : string.Empty)}" +
                   $"{(hasHours ? !hasDays ? $"{ts.Hours} {lblHours}" : hasMinutes || hasSeconds || hasMillisec ? $", {ts.Hours} {lblHours}" : $" and {ts.Hours} {lblHours}" : string.Empty)}" +
                   $"{(hasMinutes ? !hasDays && !hasHours ? $"{ts.Minutes} {lblMinutes}" : hasSeconds || hasMillisec ? $", {ts.Minutes} {lblMinutes}" : $" and {ts.Minutes} {lblMinutes}" : string.Empty)}" +
                   $"{(hasSeconds ? !hasDays && !hasHours && !hasMinutes ? $"{ts.Seconds} {lblSeconds}" : hasMillisec ? $", {ts.Seconds} {lblSeconds}" : $" and {ts.Seconds} {lblSeconds}" : string.Empty)}" +
                   $"{(hasMillisec && withMilliseconds ? !hasDays && !hasHours && !hasMinutes && !hasSeconds ? $"{ts.Milliseconds} ms" : $" and {ts.Milliseconds} ms" : string.Empty)}";
            
            ////Easiest to read, high cyclical complexity.
            //var values = new List<string>();
            //if (hasDays) values.Add($"{ts.Days} {lblDays}");

            //if (hasHours)
            //{
            //    if (!hasDays) values.Add($"{ts.Hours} {lblHours}");
            //    else if (hasMinutes || hasSeconds || hasMillisec) values.Add($", {ts.Hours} {lblHours}");
            //    else values.Add($" and {ts.Hours} {lblHours}");
            //}

            //if (hasMinutes)
            //{
            //    if (!hasDays && !hasHours) values.Add($"{ts.Minutes} {lblMinutes}");
            //    if (hasSeconds || hasMillisec) values.Add($", {ts.Minutes} {lblMinutes}");
            //    else values.Add($" and {ts.Minutes} {lblMinutes}");
            //}

            //if (hasSeconds)
            //{
            //    if (!hasDays && !hasHours && !hasMinutes) values.Add($"{ts.Seconds} {lblSeconds}");
            //    else if (hasMillisec) values.Add($", {ts.Seconds} {lblSeconds}");
            //    else values.Add($" and {ts.Seconds} {lblSeconds}");
            //}

            //if (withMilliseconds && hasMinutes)
            //{
            //    if (!hasDays && !hasHours && !hasMinutes && !hasSeconds) values.Add($"{ts.Milliseconds} ms");
            //    else values.Add($" and {ts.Milliseconds} ms");
            //}

            //return string.Join("", values);
        }

        public static TimeSpan Add(this TimeSpan ts, int days = 0, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0) =>
            ts.Add(new TimeSpan(days, hours, minutes, seconds, milliseconds));
        public static TimeSpan Subtract(this TimeSpan ts, int days = 0, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0) =>
            ts.Subtract(new TimeSpan(days, hours, minutes, seconds, milliseconds));
    }
}
