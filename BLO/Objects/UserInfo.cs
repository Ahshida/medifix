using System;
using BLO.Objects.Enums.MediFast;
using MediFast.dbo;

namespace BLO.Objects
{
    [Serializable]
    public class UserInfo
    {
        public const int Admin = 1;
        public const int AnonymousUser = 2;

        public UserInfo(int userID)
        {
            if (this.ID != userID)
            {
                this.ID = userID;
                if (userID != AnonymousUser)
                {
                    var user = T_USER.SelectExact(userID);
                    if (user != null)
                    {
                        this.Role = user.UserRole;
                        this.FullName = user.FullName;
                        this.CompanyID = user.CompanyID;
                        this.PanelDoctorID = user.PanelDoctorID;
                    }
                }

                if (string.IsNullOrEmpty(this.FullName))
                    this.FullName = "Visitor";
            }
        }

        public UserInfo()
            : this(AnonymousUser)
        {
        }

        public int ID { get; private set; }
        public string FullName { get; private set; }
        public int? CompanyID { get; private set; }
        public int? PanelDoctorID { get; private set; }
        public Enum_USERROLE Role { get; private set; }
    }
}