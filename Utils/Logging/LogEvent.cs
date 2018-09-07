using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Logging
{
    public class LogEvent : LogBase, IDisposable
    {
        #region Properties
        internal static Dictionary<DefaultSources, string> EventSourceNames = new Dictionary<DefaultSources, string> {
            { DefaultSources.App, "Application" },
            { DefaultSources.Sec, "Security" },
            { DefaultSources.Set, "Setup" },
            { DefaultSources.Sys, "System" },
            { DefaultSources.For, "Forwarded Events" },
        };

        public int ID { get; private set; } = 0;
        public short Catagory { get; private set; } = 0;
        public string Source { get; set; } = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        public string Log { get; set; } = "Application";
        public EventLog ELog { get; set; }

        #endregion

        #region Constructors
        public LogEvent() => InitSource();
        public LogEvent(string source)
        {
            Source = source;
            InitSource();
        }
        public LogEvent(string source, string log)
        {
            Source = source;
            Log = Log;
            InitSource();
        }

        #endregion

        #region Utilities
        public LogEvent SetID(int id) { ID = id; return this; }
        public LogEvent SetCat(short cat) { Catagory = cat; return this; }
        public LogEvent SetID_Cat(int id, short cat) { ID = id; Catagory = cat; return this; }

        protected void InitSource()
        {
            ELog = new EventLog();
            if (!EventLog.SourceExists(Source))
            {
                try
                { EventLog.CreateEventSource(Source, Log); }
                catch (Exception ex)
                { throw new Exception("Failed to create new Event Log. Verify application has rights to setup Event Source.", ex); }
            }
            ELog.Source = Source;
        }

        #endregion

        #region Writes
        public override void Write(string msg) =>
            Write(msg, EventLogEntryType.Information);
        public override void Write(Exception ex) =>
            Write(FormatException(ex), EventLogEntryType.Error);
        public void Write(string text, EventLogEntryType type) =>
            Write(text, type, ID, Catagory);
        public void Write(string text, EventLogEntryType type, int id, short cat = 0) =>
            ELog.WriteEntry(text, type, id, cat);

        public void Dispose()
        {
            ID = 0;
            Catagory = 0;
            Source = null;
            Log = null;
            ELog.Dispose();
        }

        #endregion
    }
}
