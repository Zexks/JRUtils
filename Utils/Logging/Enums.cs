using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Logging
{
    public enum LogType { File, Event, DB }
    public enum AttLevel { Info, Warning, Fail }
    public enum FileType { Text, CSV, JSON, XML }
    public enum DefaultSources { App, Sec, Set, Sys, For }
    public enum ExceptionParts { Data, Help, Result, Inner, Message, Source, Stack, Target }
}
