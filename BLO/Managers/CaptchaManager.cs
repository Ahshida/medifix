using System;
using System.Net;
using System.Web;
using BLO.Objects;
using DBO.Data.Managers;
using Newtonsoft.Json.Linq;

namespace BLO.Managers
{
    public class CaptchaManager
    {
        public static bool ValidateUser(string captcha)
        {
            if (string.IsNullOrEmpty(captcha))
                return false;

            using (WebClient client = new WebClient())
            {
                client.Proxy = null;
                var ip = HttpContext.Current.Request["REMOTE_ADDR"];
                var text = client.DownloadString(new Uri("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}".FormatWith(
                    AppSettings.CaptchaSecretKey,
                    HttpUtility.UrlEncode(captcha),
                    ip)));
                dynamic obj = JObject.Parse(text);
                return obj.success;
            }
        }
    }
}
