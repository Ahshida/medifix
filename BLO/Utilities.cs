using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using BLO.Objects;
using DBO.Data;
using DBO.Data.Interfaces;
using DBO.Data.Managers;

namespace BLO
{
    public class Utilities : BLOObject
    {
        public static IEnumerable<Type> GetTypes(string nameSpace)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            return types.Where(item => string.Equals(item.Namespace, nameSpace));
        }

        public static Type GetType(string name)
        {
            return Assembly.GetExecutingAssembly().GetType(name);
        }

        public static bool IsDataExists<T>(string id, WhereClause whereClause, params object[] parameters) where T : ITable
        {
            return IsDataExists<T>("ID", id, whereClause, parameters);
        }
        public static bool IsDataExists<T>(string idPropertyName, string id, WhereClause whereClause, params object[] parameters) where T : ITable
        {
            var type = typeof(T);
            DataManager dbo = type.GetDataManager();
            if (string.Equals("_empty", id))
            {
                var descRecords = dbo.Select<T>(whereClause, parameters);
                if (descRecords.Count > 0)
                    return true;
            }
            else
            {
                var descRecords = dbo.Select<T>(whereClause, parameters);
                if (descRecords.Count > 1)
                    return true;
                else
                {
                    var idProperty = type.GetProperty(idPropertyName);
                    var compareId = id.Decrypt<string>().ConvertToTargetTypeValue(idProperty.PropertyType); //new EncryptionManager().Decrypt(id, false).ConvertTo<int>();
                    foreach (var item in descRecords)
                    {
                        if (!object.Equals(compareId, idProperty.GetValue(item, null)))
                            return true;
                    }
                }
            }
            return false;
        }

        public static string getIPAddress(HttpRequestBase request)
        {
            string szRemoteAddr = request.UserHostAddress;
            string szXForwardedFor = request.ServerVariables["X_FORWARDED_FOR"];
            string szIP = "";

            if (szXForwardedFor == null)
            {
                szIP = szRemoteAddr;
            }
            else
            {
                szIP = szXForwardedFor;
                if (szIP.IndexOf(",") > 0)
                {
                    string[] arIPs = szIP.Split(',');

                    foreach (string item in arIPs)
                    {
                        if (!IsPrivateIpAddress(item))
                        {
                            return item;
                        }
                    }
                }
            }
            return szIP;
        }
        private static bool IsPrivateIpAddress(string ipAddress)
        {
            // http://en.wikipedia.org/wiki/Private_network
            // Private IP Addresses are: 
            //  24-bit block: 10.0.0.0 through 10.255.255.255
            //  20-bit block: 172.16.0.0 through 172.31.255.255
            //  16-bit block: 192.168.0.0 through 192.168.255.255
            //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

            var ip = IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            var is24BitBlock = octets[0] == 10;
            if (is24BitBlock) return true; // Return to prevent further processing

            var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
            if (is20BitBlock) return true; // Return to prevent further processing

            var is16BitBlock = octets[0] == 192 && octets[1] == 168;
            if (is16BitBlock) return true; // Return to prevent further processing

            var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
            return isLinkLocalAddress;
        }
    }
}
