using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetLoggerInfrastructure
{
    public class MultyLogger : ILogger
    {
        private LoggerLevel _currentLevel;

        /// <summary>
        /// Текущий уровень отладки
        /// </summary>
        public LoggerLevel CurrentLevel
        {
            get { return _currentLevel; }
        }

        private List<ILogger> _loggers;

        public MultyLogger(params ILogger[] loggers)
        {
            _loggers = new List<ILogger>();
            if(loggers != null)
                _loggers.AddRange(loggers);
        }

        /// <summary>
        /// Добавить новый логгер в коллекцию
        /// </summary>
        /// <param name="logger">Логгер</param>
        public void AddLogger(ILogger logger)
        {
            _loggers.Add(logger);
        }

        /// <summary>
        /// Вывести сообщение
        /// </summary>
        /// <param name="level">Уровень отладки</param>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы сообщения</param>
        public void Write(LoggerLevel level, string message, params object[] args)
        {
            if (level > _currentLevel)
                return;

            Parallel.ForEach(_loggers, logger =>
                {
                    logger.Write(level, message, args);
                }
            );
        }

        /// <summary>
        /// Установить уровень отладки логгера
        /// </summary>
        /// <param name="level">Уровень отладки</param>
        public void SetLevel(LoggerLevel level)
        {
            _currentLevel = level;
            foreach(var logger in _loggers)
            {
                logger.SetLevel(_currentLevel);
            }
        }

        public T GetLogger<T>() where T : ILogger
        {
            var findingType = typeof(T);
            var logger = _loggers.Find(l=>l.GetType() == findingType);
            return (T)logger;
        }


        public void Init()
        {
            throw new NotImplementedException();
        }
    }
}
