using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLoggerInfrastructure
{

    /// <summary>
    /// Уровни логгирования
    /// </summary>
    public enum LoggerLevel
    {
        Fatal = 0,
        Error = 1,
        Warning = 2,
        Info = 3,
        Debug = 4
    }

    /// <summary>
    /// Базовый интерфейс логгера
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Текущий уровень отладки логгера
        /// </summary>
        LoggerLevel CurrentLevel { get; }

        /// <summary>
        /// Инициализация логгера
        /// </summary>
        void Init();
        
        /// <summary>
        /// Записать сообщение в лог
        /// </summary>
        /// <param name="level">Уровень отладки</param>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы сообщения</param>
        void Write(LoggerLevel level, String message, params object[] args);

        /// <summary>
        /// Установить уровень отладки логгера
        /// </summary>
        /// <param name="level"></param>
        void SetLevel(LoggerLevel level);
    }
}
