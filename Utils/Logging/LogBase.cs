using System;

using Utils.Extensions;
using static Utils.Logging.Utilities;

namespace Utils.Logging
{
    public abstract class LogBase
    {
        public LogBase() { }

        public abstract void Write(string msg);
        public abstract void Write(Exception ex);

        public static string FormatException(Exception ex) =>
            Utilities.FormatException(ex);

        public static string JSONException(Exception ex) //=>
        {
            var msg = (!ex.Message.IsNull() && !string.IsNullOrEmpty(ex.Message.ToString())) ?
                      string.Format("\"Message\":\"{0}\",", ex.Message) : string.Empty;
            var tgt = (!ex.TargetSite.IsNull() && !string.IsNullOrEmpty(ex.TargetSite.ToString())) ?
                      string.Format("\"TargetSite\":\"{0}\",", ex.TargetSite) : string.Empty;
            var data = (!ex.Data.IsNull() && ParseExceptionData(ex.Data, out var strdata)) ?
                      string.Format("\"Data\":{0},", strdata) : string.Empty;
            var inner = (!ex.InnerException.IsNull()) ?
                      string.Format("\"Inner\":{{ {0} }},", JSONException(ex.InnerException)) : string.Empty;
            var stack = (!ex.StackTrace.IsNull() && !string.IsNullOrEmpty(ex.StackTrace.ToString())) ?
                      string.Format("\"Stack\":\"{0}\"", ex.StackTrace.Trim()) : string.Empty;

            var final = string.Format("{0}{1}{2}{3}{4}", msg, tgt, data, inner, stack);

            return final;
        }

        //string.Format("{0}{1}{2}{3}{4}",
        //    (!ex.Message.IsNull() && !string.IsNullOrEmpty(ex.Message.ToString())) ?
        //        string.Format("\"Message\":\"{0}\",", ex.Message) : string.Empty,
        //    (!ex.TargetSite.IsNull() && !string.IsNullOrEmpty(ex.TargetSite.ToString())) ?
        //        string.Format("\"TargetSite\":\"{0}\",", ex.TargetSite) : string.Empty,
        //    (!ex.Data.IsNull() && ParseExceptionData(ex.Data, out var strdata)) ?
        //        string.Format("\"Data\":{0},", strdata) : string.Empty,
        //    (!ex.InnerException.IsNull()) ?
        //        string.Format("\"Inner\":\"{0}\",", FormatException(ex.InnerException)) : string.Empty,
        //    (!ex.StackTrace.IsNull() && !string.IsNullOrEmpty(ex.StackTrace.ToString())) ?
        //        string.Format("\"Stack\":\"{0}\"", ex.StackTrace.Trim()) : string.Empty
        //);
    }
}
