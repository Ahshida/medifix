using System.ComponentModel;
using DBO.Data.Attributes;
using ReportListing.Attributes;

namespace MediFast.dbo
{
    [AssignProperty("InsurerID", typeof(DescriptionAttribute), "Insurer")]
    [AssignProperty("InsurerID", typeof(HiddenAttribute))]
    [AssignProperty("BankID", typeof(DescriptionAttribute), "Bank")]
    [AssignProperty("Title", typeof(DescriptionAttribute), "Position")]
    [AssignProperty("ContactPerson", typeof(DescriptionAttribute), "Contact Person")]
    [AssignProperty("Active", typeof(DescriptionAttribute), "Status")]
    [AssignProperty("Active", typeof(ReferenceDataAttribute), "True:Active;False:Inactive")]
    [AssignProperty("CreatedBy", typeof(HiddenAttribute))]
    [AssignProperty("CreatedDate", typeof(HiddenAttribute))]
    [AssignProperty("ModifiedBy", typeof(HiddenAttribute))]
    [AssignProperty("ModifiedDate", typeof(HiddenAttribute))]
    public partial class T_BANKBRANCH
    {
    }
}
