using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using DBO.Data.Managers;

namespace BLO.Objects
{
    public class AppSettings
    {
        public static string SystemDateFormat { get { return ConfigurationManager.AppSettings["SystemDateFormat"]; } }
        public static string SystemDateTimeFormat { get { return ConfigurationManager.AppSettings["SystemDateTimeFormat"]; } }
        public static string ContactUs { get { return ConfigurationManager.AppSettings["ContactUs"]; } }
        public static string ImageUrl { get { return ConfigurationManager.AppSettings["ImageUrl"]; } }
        public static string eClaimsImageUrl { get { return ConfigurationManager.AppSettings["eClaimsImageUrl"]; } }
        public static string CaptchaSiteKey { get { return ConfigurationManager.AppSettings["CaptchaSiteKey"]; } }
        public static string CaptchaSecretKey { get { return ConfigurationManager.AppSettings["CaptchaSecretKey"]; } }
        public static bool IsTestingServer { get { return ConfigurationManager.AppSettings["IsTestingServer"].ConvertTo<bool>(); } }
        public static string AdminEmail { get { return ConfigurationManager.AppSettings["AdminEmail"]; } }
        public static bool EnableOptimizations { get { return ConfigurationManager.AppSettings["EnableOptimizations"].ConvertTo<bool?>() != false; } }
        public static string AppUrl { get { return ConfigurationManager.AppSettings["AppUrl"]; } }
        public static string AppID { get { return ConfigurationManager.AppSettings["AppID"]; } }

        public class EmailSetting
        {
            public static string ServerName { get { return ConfigurationManager.AppSettings["ServerName"]; } }
            public static string Username { get { return ConfigurationManager.AppSettings["Username"]; } }
            public static string Password { get { return ConfigurationManager.AppSettings["Password"]; } }
            public static int Port { get { return (ConfigurationManager.AppSettings["Port"] ?? "").ConvertTo<int>(); } }
            public static MailAddress SupportEmail { get { return new MailAddress(ConfigurationManager.AppSettings["SupportEmail"]); } }
            public static IEnumerable<MailAddress> InformEmail { get { return string.IsNullOrEmpty(ConfigurationManager.AppSettings["InformEmail"]) ? null : ConfigurationManager.AppSettings["InformEmail"].Split(';').Select(item => new MailAddress(item)); } }
            public static bool EnableSsl { get { return (ConfigurationManager.AppSettings["EnableSsl"] ?? "true").ConvertTo<bool>(); } }
        }
    }
}
