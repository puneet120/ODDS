using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using Odds.Core.Entity;
using Odds.Data.Context;

namespace Odds.Services.Interfaces
{
    public class LoggerRepository : ILoggerRepository
    {



        public LoggerRepository(
            )
        {


        }


        /// <summary>
        /// Write Logs
        /// /// </summary>       

        public void LogFileWrite(string message, string innerexception)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = HttpContext.Current.Server.MapPath("~/Logs/");

                logFilePath = logFilePath + "ProgramLog" + "-" + DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists 
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(DateTime.Now + " - " + message);
                streamWriter.WriteLine(innerexception);
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }




        }
    }
}
