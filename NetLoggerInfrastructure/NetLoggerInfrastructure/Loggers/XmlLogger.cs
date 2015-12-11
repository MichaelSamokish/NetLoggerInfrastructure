using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace NetLoggerInfrastructure
{
    public class XmlLogger : ILogger
    {
        private String _logLocation;
        private String _assemblyName;
        private String _assemblyVersion;
        private String _currentDate;
        private String _logFileFullPath;
        
        
        private LoggerLevel _currentLevel;
        public LoggerLevel CurrentLevel
        {
            get { return _currentLevel; }
        }

        public XmlLogger()
        {
            _logLocation = Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.LastIndexOf(Assembly.GetEntryAssembly().GetName().Name));
            _logLocation = _logLocation.Substring(0, _logLocation.LastIndexOf("\\"));
            _assemblyName = Assembly.GetEntryAssembly().GetName().Name;
            _assemblyVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
            _currentDate = DateTime.Now.ToString("ddMMyyyy");
            _logFileFullPath = String.Format("{0}\\{1}.{2}.xml", _logLocation, _assemblyName, _currentDate);
            if(!File.Exists(_logFileFullPath))
            {
                CreateDocument(_logFileFullPath);
            }
        }

        public void Init()
        {
            
        }

        public void Write(LoggerLevel level, string message, params object[] args)
        {
            if (level > _currentLevel)
                return;

            _currentDate = DateTime.Now.ToString("ddMMyyyy");
            _logFileFullPath = String.Format("{0}\\{1}.{2}.xml", _logLocation, _assemblyName, _currentDate);
            if(!File.Exists(_logFileFullPath))
            {
                CreateDocument(_logFileFullPath);
            }

            var thrId = Thread.CurrentThread.ManagedThreadId.ToString();

            var doc = XDocument.Load(_logFileFullPath);
            doc.Element("Logger").Add(new XElement("Record",
                                        new XAttribute("Time", DateTime.Now.ToString("hh:mm:ss.fff")),
                                        new XAttribute("ThreadId", thrId),
                                        new XAttribute("Level", level.ToString()),
                                        new XText(String.Format(message,args))
                                        )
                                     );
            doc.Save(_logFileFullPath);
        }

        public void SetLevel(LoggerLevel level)
        {
            _currentLevel = level;
        }

        private void CreateDocument(String path)
        {
            using(XmlWriter writer = XmlWriter.Create(path))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Logger");
                writer.WriteStartAttribute("AssemblyName");
                writer.WriteString(_assemblyName);
                writer.WriteEndAttribute();
                writer.WriteStartAttribute("AssemblyVersion");
                writer.WriteString(_assemblyVersion);
                writer.WriteEndAttribute();
                writer.WriteStartAttribute("LogDate");
                writer.WriteString(_currentDate);
                writer.WriteEndAttribute();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
