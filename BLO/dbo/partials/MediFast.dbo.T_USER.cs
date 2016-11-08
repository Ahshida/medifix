using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DBO.Data.Attributes;
using DBO.Data.Managers;

namespace MediFast.dbo
{
    [AssignProperty("CompanyID", typeof(DescriptionAttribute), "Company")]
    [AssignProperty("ContactNo", typeof(DescriptionAttribute), "Contact No.")]
    [AssignProperty("ModifiedDate", typeof(DescriptionAttribute), "Modified Date")]
    [AssignProperty("UserRole", typeof(DescriptionAttribute), "User Role")]
    [AssignProperty("FullName", typeof(DescriptionAttribute), "Full Name")]
    [AssignProperty("Address1", typeof(DescriptionAttribute), "Address")]
    [AssignProperty("Address2", typeof(DescriptionAttribute), "")]
    [AssignProperty("Password", typeof(DataTypeAttribute), DataType.Password)]
    [AssignProperty("Email", typeof(DataTypeAttribute), DataType.EmailAddress)]
    [AssignProperty("Active", typeof(DescriptionAttribute), "Status")]
    public partial class T_USER
    {
        [DatabaseIgnore]
        [DataType(DataType.Password)]
        [Description("Confirm Password")]
        public string ConfirmPassword
        {
            get;
            set;
        }

        [DatabaseIgnore]
        public string company { get { return this.CompanyID.Encrypt(); } }
    }
}
