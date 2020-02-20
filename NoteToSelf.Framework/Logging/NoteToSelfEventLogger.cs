

namespace NoteToSelf.Framework.Logging
{
    using System.Diagnostics;

    using NoteToSelf.Model;

    public class NoteToSelfEventLogger:INoteToSelfLogger
    { 
        
        //TODO:-RR Add a logger message class in the model that can be used for multiple purposes. Currently this is only being used to track exceptions.
        public void WriteMessage(string sourceMethod,string message)
        {

            if (!EventLog.SourceExists(Constants.EventLoggerLogSource))
                EventLog.CreateEventSource(Constants.EventLoggerLogSource, Constants.EventLoggerLogType);

            EventLog.WriteEntry(Constants.EventLoggerLogSource, sourceMethod + ":" + message, EventLogEntryType.Error);
        }
    }
}
