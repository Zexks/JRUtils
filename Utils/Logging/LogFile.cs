using System;
using System.IO;

namespace Utils.Logging
{
    public class LogFile : LogBase, IDisposable
    {
        #region Properties
        public bool Append { get; set; }

        public string LogPath { get; set; }
        public string LogName { get; set; }

        #endregion

        #region Constructors
        public LogFile()
        {
            Append = false;
            LogPath = Path.GetTempPath();
            LogName = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.log";
        }
        public LogFile(string path) : this() { LogPath = path; }
        public LogFile(string path, string name) : this() { LogPath = path; LogName = name; }

        #endregion

        #region Writes
        public override void Write(string msg)
        {
            using (var writer = new StreamWriter(LogPath + LogName, Append))
                writer.WriteLine(msg);
        }

        public override void Write(Exception ex)
        {
            Write(FormatException(ex));
        }

        public void Dispose()
        {
            Append = false;
            LogPath = string.Empty;
            LogName = string.Empty;
        }

        #endregion
    }
}
