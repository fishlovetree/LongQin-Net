using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Common
{
    public class LogHelper
    {
        private static Logger logger;
        private static string logRuleName = "LongQin";
        static LogHelper() {
            logger = LogManager.GetLogger(logRuleName);
            logger.LoggerReconfigured += logger_LoggerReconfigured;
        }

        static void logger_LoggerReconfigured(object sender, System.EventArgs e) {
            logger = LogManager.GetLogger(logRuleName);
        }

        public static void Error(string message, Exception ex = null) {
            logger.Log(LogLevel.Error, ex, message);
        }

        public static void Info(string message) {
            logger.Log(LogLevel.Info, message);
        }

        public static void Warn(string message) {
            logger.Log(LogLevel.Warn, message);
        }

        public static void Debug(string message) {
            logger.Log(LogLevel.Debug, message);
        }
    }
}
