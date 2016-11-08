using System.Web;

namespace BLO.Managers
{
    public class IPManager
    {
        public static string GetUserIP(HttpRequestBase request)
        {
            string visitorsIPAddr = string.Empty;
            if (request.UserHostAddress.Length != 0)
                visitorsIPAddr = request.UserHostAddress;
            else if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                visitorsIPAddr = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            return visitorsIPAddr;
        }
    }
}
