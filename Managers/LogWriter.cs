using System;
using System.IO;
using System.Configuration;

namespace DBO.Data.Managers
{
    public class LogWriter
    {
        public static void WriteVCMApiLog(string message)
        {
            WriteLog("VCMApi", message);
        }
        public static void WriteDBServerLog(string message)
        {
            WriteLog("DBServer", message);
        }

        public static void WriteLog(string folderName, string message)
        {
            var dir = ConfigurationManager.AppSettings["VCMLogPath"].Decrypt();
            if (string.IsNullOrEmpty(dir))
                dir = @"C:\Log";

            if (dir.EndsWith(@"\"))
                dir = dir.Substring(0, dir.Length - 1);

            dir = string.Format(@"{0}\{1}", dir, folderName);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            System.IO.File.AppendAllText(
                string.Format(@"{0}\{1}.log", dir, DateTime.Now.Date.ToString("yyyyMMdd")),
                string.Format("{0}:\t{1}\r\n", DateTime.Now, message));
        }
    }
}
