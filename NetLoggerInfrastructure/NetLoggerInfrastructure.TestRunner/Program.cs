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
            MultyLogger logger = new MultyLogger(new FileLogger(), new ConsoleLogger(),new WinAppEventLogLogger());
            logger.SetLevel(LoggerLevel.Debug);
            logger.Info("Test info message");
            logger.Debug("Test Debug message");
            logger.Warning("Test Warning message");
            logger.Error("Test Error message");
            logger.Fatal("TestFatalMessage");
            Console.ReadKey();
        }
    }
}
