using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetLoggerInfrastructure
{
    public class WinAppEventLogLogger : ILogger
    {
        private EventLog _eventLog;
        private String _assemblyName;

        private LoggerLevel _currentLevel;
        public LoggerLevel CurrentLevel
        {
            get { return _currentLevel; }
        }

        public WinAppEventLogLogger()
        {
            _assemblyName = Assembly.GetEntryAssembly().GetName().Name;
            _eventLog = CreateEventLog(_assemblyName);
        }

        public void Init() { }

        public void Write(LoggerLevel level, string message, params object[] args)
        {
            if (level > _currentLevel)
                return;
            
            var msg = String.Format(message, args);
            Trace.WriteLine(msg);
            try
            {
                switch (level)
                {
                    case LoggerLevel.Fatal:
                        _eventLog.WriteEntry(msg, EventLogEntryType.Error);
                        break;
                    case LoggerLevel.Error:
                        _eventLog.WriteEntry(msg, EventLogEntryType.Error);
                        break;
                    case LoggerLevel.Warning:
                        _eventLog.WriteEntry(msg, EventLogEntryType.Warning);
                        break;
                    case LoggerLevel.Info:
                        _eventLog.WriteEntry(msg, EventLogEntryType.Information);
                        break;
                    case LoggerLevel.Debug:
                        _eventLog.WriteEntry(msg, EventLogEntryType.Information);
                        break;
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex);
            }
            
        }

        public void SetLevel(LoggerLevel level)
        {
            _currentLevel = level;
        }

        private EventLog CreateEventLog(String name)
        {
            if (!EventLog.SourceExists(name))
            {
                EventLog.CreateEventSource(name, name);
            }
            var log = new EventLog(name);
            log.Source = name;
            return log;
        }
    }
}
