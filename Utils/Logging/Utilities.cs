using System;
using System.Collections;
using System.Collections.Generic;

using Utils.Extensions;

namespace Utils.Logging
{
    public static class Utilities
    {
        //public static string FormatException(Exception ex) => ExceptionFormat(ex);

        public static Func<Exception, string> FormatException
        {
            get => _exceptionFormat;
            set => _exceptionFormat = value;
        }

        private static Func<Exception, string> _exceptionFormat = //Default exception formatting
            (ex) => string.Format("{0}{1}{2}{3}{4}",
                (!ex.Message.IsNull() && !string.IsNullOrEmpty(ex.Message.ToString())) ?
                    string.Format("Message: {0}", ex.Message) : string.Empty,
                (!ex.TargetSite.IsNull() && !string.IsNullOrEmpty(ex.TargetSite.ToString())) ?
                    string.Format("\r\nTargetSite: {0}", ex.TargetSite) : string.Empty,
                (!ex.Data.IsNull() && ParseExceptionData(ex.Data, out var strdata)) ?
                    string.Format("\r\nData: {0}", strdata) : string.Empty,
                (!ex.InnerException.IsNull()) ?
                    string.Format("\r\nInner: {0}", FormatException(ex.InnerException)) : string.Empty,
                (!ex.StackTrace.IsNull() && !string.IsNullOrEmpty(ex.StackTrace.ToString())) ?
                    string.Format("\r\nStack: {0}", ex.StackTrace) : string.Empty
            );

        public static bool ParseExceptionData(IDictionary data, out string strdata)
        {
            strdata = string.Empty;
            if (data.Keys.Count < 1) return false;

            var values = new List<string>();
            foreach (var key in data.Keys)
                values.Add($"{{\"{key.ToString()}\":\"{data[key].ToString()}\"}}");

            strdata = $"[{string.Join(",", values)}]";
            return true;
        }

    }
}
