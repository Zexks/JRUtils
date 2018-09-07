using System;
using System.Globalization;

using static Utils.Misc.Gets;

namespace Utils.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Get week of the year (1 - 53) in integer format.
        /// </summary>
        /// <param name="dt">Input date.</param>
        /// <returns>Integer representation of the week of the year.</returns>
        public static int GetWeekOfYear(this DateTime dt) =>
            dt.GetWeekOfYear(CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        /// <summary>
        /// Get week of the year (1 - 53) in integer format, with specific Calendar rules and for a specific Day of the week.
        /// </summary>
        /// <param name="dt">Input date.</param>
        /// <param name="rule">Defines rules for first week of the year.</param>
        /// <param name="day">Defines the first day of a week.</param>
        /// <returns>Integer representation of the week of the year as accounted by the rules specified.</returns>
        public static int GetWeekOfYear(this DateTime dt, CalendarWeekRule rule, DayOfWeek day) =>
            DateTimeFormatInfo.CurrentInfo.Calendar.GetWeekOfYear(dt, rule, day);
        /// <summary>
        /// Get the number of days in the month.
        /// </summary>
        /// <param name="dt">Input date.</param>
        /// <returns>Integer count of days in the months.</returns>
        public static int GetDaysThisMonth(this DateTime dt) =>
            DateTimeFormatInfo.CurrentInfo.Calendar.GetDaysInMonth(dt.Year, dt.Month);

        /// <summary>
        /// Get the datetime of the first specified day of the week in a month.
        /// </summary>
        /// <param name="dt">Input date for the month to check.</param>
        /// <param name="dow">Day of the Week to look for.</param>
        /// <returns>DateTime representation of the first occurance of the specified day of the week in the input month.</returns>
        public static DateTime GetFirstOfMonth(this DateTime dt, DayOfWeek dow) =>
            dt.GetNthOfMonth(dow, 1);
        /// <summary>
        /// Get the datetime of the second specified day of the week in a month.
        /// </summary>
        /// <param name="dt">Input date for the month to check.</param>
        /// <param name="dow">Day of the Week to look for.</param>
        /// <returns>DateTime representation of the second occurance of the specified day of the week in the input month.</returns>
        public static DateTime GetSecondOfMonth(this DateTime dt, DayOfWeek dow) =>
            dt.GetNthOfMonth(dow, 2);
        /// <summary>
        /// Get the datetime of the third specified day of the week in a month.
        /// </summary>
        /// <param name="dt">Input date for the month to check.</param>
        /// <param name="dow">Day of the Week to look for.</param>
        /// <returns>DateTime representation of the third occurance of the specified day of the week in the input month.</returns>
        public static DateTime GetThirdOfMonth(this DateTime dt, DayOfWeek dow) =>
            dt.GetNthOfMonth(dow, 3);
        /// <summary>
        /// Get the datetime of the fourth specified day of the week in a month.
        /// </summary>
        /// <param name="dt">Input date for the month to check.</param>
        /// <param name="dow">Day of the Week to look for.</param>
        /// <returns>DateTime representation of the fourth occurance of the specified day of the week in the input month.</returns>
        public static DateTime GetFourthOfMonth(this DateTime dt, DayOfWeek dow) =>
            dt.GetNthOfMonth(dow, 4);
        /// <summary>
        /// Get the datetime of the last specified day of the week in a month.
        /// </summary>
        /// <param name="dt">Input date for the month to check.</param>
        /// <param name="dow">Day of the Week to look for.</param>
        /// <returns>DateTime representation of the last occurance of the specified day of the week in the input month.</returns>
        public static DateTime GetLastOfMonth(this DateTime dt, DayOfWeek dow) =>
            dt.GetNthOfMonth(dow, 5);
        /// <summary>
        /// Get the datetime of the Nth specified day of the week in a month.
        /// </summary>
        /// <param name="dt">Input date for the month to check.</param>
        /// <param name="dow">Day of the Week to look for.</param>
        /// <returns>DateTime representation of the Nth occurance of the specified day of the week in the input month.</returns>
        public static DateTime GetNthOfMonth(this DateTime dt, DayOfWeek dow, int num)
        {
            var cnt = 0;
            var valid = new DateTime();
            var last = new DateTime(dt.Year, dt.Month, 1) + dt.TimeOfDay;
            while (cnt < num && (last.Month == dt.Month))
            {
                if (last.DayOfWeek == dow)
                {
                    valid = last;
                    cnt++;
                }
                if (cnt < num) last = last.AddDays(1);
            }
            return valid;
        }

        public static DateTime AddWeek(this DateTime dt, int weeks) =>
            dt.AddDays(7);

        //NOTE: There are no Add companions to the following methods as those are included in .Net by default.
        //These are just helpers for negative values in that Add function.
        
        /// <summary>
        /// Return a new dateTime object minus a number of years.
        /// </summary>
        /// <param name="amt">The number of years to remove.</param>
        /// <returns>Current datetime minus number of years.</returns>
        public static DateTime SubtractYears(this DateTime dt, int amt) =>
            dt.AddYears(amt * -1);
        /// <summary>
        /// Return a new dateTime object minus a number of months.
        /// </summary>
        /// <param name="amt">The number of years to remove.</param>
        /// <returns>Current datetime minus number of months.</returns>
        public static DateTime SubtractMonths(this DateTime dt, int amt) =>
            dt.AddMonths(amt * -1);
        /// <summary>
        /// Return a new dateTime object minus a number of days.
        /// </summary>
        /// <param name="amt">The number of years to remove.</param>
        /// <returns>Current datetime minus number of days.</returns>
        public static DateTime SubtractDays(this DateTime dt, int amt) =>
            dt.AddDays(amt * -1);
        /// <summary>
        /// Return a new dateTime object minus a number of hours.
        /// </summary>
        /// <param name="amt">The number of years to remove.</param>
        /// <returns>Current datetime minus number of hours.</returns>
        public static DateTime SubtractHours(this DateTime dt, int amt) =>
            dt.AddHours(amt * -1);
        /// <summary>
        /// Return a new dateTime object minus a number of minutes.
        /// </summary>
        /// <param name="amt">The number of years to remove.</param>
        /// <returns>Current datetime minus number of minutes.</returns>
        public static DateTime SubtractMinutes(this DateTime dt, int amt) =>
            dt.AddMinutes(amt * -1);
        /// <summary>
        /// Return a new dateTime object minus a number of seconds.
        /// </summary>
        /// <param name="amt">The number of years to remove.</param>
        /// <returns>Current datetime minus number of seconds.</returns>
        public static DateTime SubtractSeconds(this DateTime dt, int amt) =>
            dt.AddSeconds(amt * -1);

    }
}
