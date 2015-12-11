using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLoggerInfrastructure.Extensions
{
    public static class LoggerExtensions
    {
        /// <summary>
        /// Вывести сообщение о критической ошибке
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы сообщения</param>
        public static void Fatal (this ILogger logger, string message, params object[] args)
        {
            logger.Write(LoggerLevel.Fatal, message, args);
        }

        public static void Fatal(this ILogger logger, string message, Exception ex)
        {
            logger.Write(LoggerLevel.Fatal, message + " Exception: " + ex.ToString());
        }

        /// <summary>
        /// Вывести сообщение об ошибке
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы сообщения</param>
        public static void Error(this ILogger logger, string message, params object[] args)
        {
            logger.Write(LoggerLevel.Error, message, args);
        }

        public static void Error(this ILogger logger, string message, Exception ex)
        {
            logger.Write(LoggerLevel.Error, message + " Exception: " + ex.ToString());
        }

        /// <summary>
        /// Вывести предупреждение
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы сообщения</param>
        public static void Warning(this ILogger logger, string message, params object[] args)
        {
            logger.Write(LoggerLevel.Warning, message, args);
        }

        /// <summary>
        /// Вывести информационное сообщение
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы сообщения</param>
        public static void Info(this ILogger logger, string message, params object[] args)
        {
            logger.Write(LoggerLevel.Info, message, args);
        }

        /// <summary>
        /// Вывести сообщение отладочного харрактера
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы сообщения</param>
        public static void Debug(this ILogger logger, string message, params object[] args)
        {
            logger.Write(LoggerLevel.Debug, message, args);
        }
    }
}
