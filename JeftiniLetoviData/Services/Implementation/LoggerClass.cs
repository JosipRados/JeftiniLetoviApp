using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeftiniLetoviData.Services.Implementation
{
    public class LoggerClass : ILoggerClass
    {
        public void WriteLog(string message)
        {
            string logPath = "logFile.txt";

            using (StreamWriter stream = new StreamWriter(logPath, true))
            {
                stream.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " " + message);
            }
        }
    }
}
