using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Utility.Logging
{
    public enum LogType
    {
        INFO,
        ERROR,
        FAIL,
        CRITICAL
    }

    public static class Logger
    {
        static string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "WXIntergration", "Log.csv");

        public static void Log(string logMessage, LogType? type = LogType.INFO)
        {
            logMessage = logMessage.Replace("\r", " ").Replace("\n", " ").Replace(",", " |").Replace("    ", "").Replace("The statement has been terminated.", "");

            using (StreamWriter sw = File.AppendText(_filePath))
            {
                sw.WriteLine($"{type},{DateTime.Now},{logMessage}");
            }
        }
    }
}