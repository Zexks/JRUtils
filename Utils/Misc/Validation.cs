using System;
using System.Net.Mail;

namespace Utils
{
    public static class Validation
    {
        public static bool IsDate(string date) =>
            DateTime.TryParse(date, out var dt);

        public static bool IsNumeric(object num)
        {
            if (num == null || num is DateTime) return false;

            if (num is short || num is int || num is long ||
                num is decimal || num is double || num is float ||
                num is bool) return true;

            if (num is string)
                return decimal.TryParse(num as string, out var x);
            else return decimal.TryParse(num.ToString(), out var x);
        }

        public static bool IsEmail(string address) =>
            new MailAddress(address).Address == address;

    }
}
