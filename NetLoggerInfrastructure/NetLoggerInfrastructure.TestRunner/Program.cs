using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetLoggerInfrastructure.Extensions;

namespace NetLoggerInfrastructure.TestRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            MultyLogger logger = new MultyLogger(new FileLogger(), new ConsoleLogger());
            logger.SetLevel(LoggerLevel.Debug);
            logger.Info("Тестовое сообщение");
            logger.Debug("Тестовое сообщение");
            logger.Warning("Тестовое сообщение");
            logger.Error("Тестовое сообщение");
            logger.Fatal("Тестовое сообщение");
            Console.ReadKey();
        }
    }
}
