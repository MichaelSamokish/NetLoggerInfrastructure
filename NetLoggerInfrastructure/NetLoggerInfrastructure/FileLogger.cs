using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetLoggerInfrastructure
{
    public class FileLogger : ILogger
    {
        private string _logLocation;
        private string _assemblyName;
        private string _currentDate;
        private string _logFileFullPath;

        private LoggerLevel _currentLevel;
        public LoggerLevel CurrentLevel
        {
            get { return _currentLevel; }
        }

        public FileLogger(string location)
        {
            _logLocation = location;
            _assemblyName = Assembly.GetEntryAssembly().GetName().Name;
            _currentDate = DateTime.Now.ToString("dd.MM.yyyy");
            _logFileFullPath = String.Format("{0}\\{1}.{2}.log", _logLocation, _assemblyName, _currentDate);
            if (!File.Exists(_logFileFullPath))
            {
                File.Create(_logFileFullPath);
            }
        }

        public FileLogger()
        {
            _logLocation = Assembly.GetEntryAssembly().Location.Substring(0,Assembly.GetEntryAssembly().Location.LastIndexOf(Assembly.GetEntryAssembly().GetName().Name));
            _logLocation = _logLocation.Substring(0, _logLocation.LastIndexOf("\\"));
            _assemblyName = Assembly.GetEntryAssembly().GetName().Name;
            _currentDate = DateTime.Now.ToString("ddMMyyyy");
            _logFileFullPath = String.Format("{0}\\{1}.{2}.log", _logLocation, _assemblyName, _currentDate);
            if (!File.Exists(_logFileFullPath))
            {
               File.Create(_logFileFullPath).Close();
            }
        }

        public void Write(LoggerLevel level, string message, params object[] args)
        {
            if (level > _currentLevel)
                return;

            _currentDate = DateTime.Now.ToString("ddMMyyyy");
            _logFileFullPath = String.Format("{0}\\{1}.{2}.log", _logLocation, _assemblyName, _currentDate);
            if(!File.Exists(_logFileFullPath))
            {
                File.Create(_logFileFullPath).Close();
            }

            var thrId = Thread.CurrentThread.ManagedThreadId.ToString();

            string msg = string.Empty;
            switch (level)
            {
                case LoggerLevel.Fatal:
                    
                    msg = DateTime.Now.ToString("hh:mm:ss.fff\t") + thrId + "\t[FTL] " + string.Format(message, args);
                    using (StreamWriter sw = new StreamWriter(_logFileFullPath,true))
                    {
                        sw.WriteLine(msg);
                    }
                    break;
                case LoggerLevel.Error:
                    msg = DateTime.Now.ToString("hh:mm:ss.fff\t") + thrId + "\t[ERR] " + string.Format(message, args);
                    using (StreamWriter sw = new StreamWriter(_logFileFullPath, true))
                    {
                        sw.WriteLine(msg);
                    }
                    break;
                case LoggerLevel.Warning:
                    msg = DateTime.Now.ToString("hh:mm:ss.fff\t") + thrId + "\t[WRN] " + string.Format(message, args);
                    using (StreamWriter sw = new StreamWriter(_logFileFullPath, true))
                    {
                        sw.WriteLine(msg);
                    }
                    break;
                case LoggerLevel.Info:
                    msg = DateTime.Now.ToString("hh:mm:ss.fff\t") + thrId + "\t[INF] " + string.Format(message, args);
                    using (StreamWriter sw = new StreamWriter(_logFileFullPath, true))
                    {
                        sw.WriteLine(msg);
                    }
                    break;
                case LoggerLevel.Debug:
                    msg = DateTime.Now.ToString("hh:mm:ss.fff\t") + thrId + "\t[DBG] " + string.Format(message, args);
                    using (StreamWriter sw = new StreamWriter(_logFileFullPath, true))
                    {
                        sw.WriteLine(msg);
                    }
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
