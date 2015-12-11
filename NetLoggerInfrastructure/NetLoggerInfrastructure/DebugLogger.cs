using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetLoggerInfrastructure
{
    public class DebugLogger : ILogger
    {
        private LoggerLevel _currentLevel;
        public LoggerLevel CurrentLevel
        {
            get { return _currentLevel; }
        }

        public void Write(LoggerLevel level, string message, params object[] args)
        {
            if (level > _currentLevel)
                return;
            var thrId = Thread.CurrentThread.ManagedThreadId.ToString();
            switch (level)
            {
                case LoggerLevel.Fatal:
                    Debug.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff\t") + thrId + "\t[FTL] " + message, args);
                    break;
                case LoggerLevel.Error:
                    Debug.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff\t") + thrId + "\t[ERR] " + message, args);
                    break;
                case LoggerLevel.Warning:
                    Debug.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff\t") + thrId + "\t[WRN] " + message, args);
                    break;
                case LoggerLevel.Info:
                    Debug.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff\t") + thrId + "\t[INF] " + message, args);
                    break;
                case LoggerLevel.Debug:
                    Debug.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff\t") + thrId + "\t[DBG] " + message, args);
                    break;
            }
        }

        public void SetLevel(LoggerLevel level)
        {
            _currentLevel = level;
        }


        public void Init()
        {
            throw new NotImplementedException();
        }
    }
}
