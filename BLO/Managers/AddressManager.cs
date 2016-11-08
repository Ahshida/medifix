using System.Collections.Generic;
using DBO.Data.Managers;

namespace BLO.Managers
{
    public class AddressManager
    {
        public static List<string> FormatAddress(string address1, string address2, string city, string postcode, string state, string country)
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(address1))
            {
                address1 = address1.Trim();
                if (!address1.EndsWith(","))
                    address1 += ",";
                list.Add(address1);
            }
            if (!string.IsNullOrWhiteSpace(address2))
            {
                address2 = address2.Trim();
                if (!address2.EndsWith(","))
                    address2 += ",";
                list.Add(address2);
            }
            var address3 = "{0} {1}".FormatWith(city, postcode);
            if (!string.IsNullOrWhiteSpace(address3))
                list.Add(address3.Trim() + ",");
            if (!string.IsNullOrWhiteSpace(state))
                list.Add(state);

            return list;
        }
    }
}
