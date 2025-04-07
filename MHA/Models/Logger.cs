using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA
{
    public class Logger
    {
        private static readonly string logFile = "historian_log.txt"; // Logs Errors And Outputs Status To Historian Text File 

        public static void Log(string message)
        {
            string entry = $"[{DateTime.Now}] {message}";

            File.AppendAllText(logFile, entry + Environment.NewLine);
        }
    }
}
